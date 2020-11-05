//--------------------------------------------------------------------------
//Purpose:       
//This node.js script will produce a new build of RentalWorksWeb or TrackitWorksWeb in the "build" directory.
// Please use forward slash / for file paths
//--------------------------------------------------------------------------
//Author:        Justin Hoffman, Mike Vermilion
//Last modified: 10/27/2020
//--------------------------------------------------------------------------

//const childProcessPromise = require('child-process-promise'); // https://www.npmjs.com/package/child-process-promise
const childProcess = require('child_process'); // https://nodejs.org/api/child_process.html
const prompts = require('prompts'); // https://www.npmjs.com/package/prompts#-types
const fse = require('fs-extra'); // https://www.npmjs.com/package/fs-extra
const path = require('path'); // https://nodejs.org/api/path.html
const archiver = require('archiver');

function failIf(expression, reason) {
    if (expression) {
        console.error(reason);
        console.error('Build failed!');
        process.exit();
    }
}

process.on('unhandledRejection', (reason) => {
    failIf(true, reason);
});

const PRODUCTNAME_TRAKITWORKS = 'TrakitWorks';
const PRODUCTNAME_RENTALWORKS = 'RentalWorks';

function formatBytes(bytes, decimals = 2) {
    if (bytes === 0) return '0 Bytes';

    const k = 1024;
    const dm = decimals < 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    const i = Math.floor(Math.log(bytes) / Math.log(k));

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

// closure for asynchronous code
(async () => {
    try {
        let productname, tagprefix;

        // fail if DwRentalWorksWebPath environment variable is not set
        failIf(typeof process.env.DwRentalWorksWebPath === 'undefined' || process.env.DwRentalWorksWebPath === '', 'Environment Variable DwRentalWorksWebPath is NOT defined.');
        const repoPath = path.resolve(process.env.DwRentalWorksWebPath);
        const buildPath = path.resolve(repoPath, `build`);
        await fse.ensureDir(buildPath);

        // Prompt the user which app to build
        const responseApp = await prompts({
            type: 'select',
            name: 'value',
            initial: 0,
            choices: [
                { title: PRODUCTNAME_RENTALWORKS, value: PRODUCTNAME_RENTALWORKS },
                { title: PRODUCTNAME_TRAKITWORKS, value: PRODUCTNAME_TRAKITWORKS }
            ],
            message: 'Select system to build',
            validate: value => value === 1 || value === 2
        });
        productname = responseApp.value;
        if (productname === PRODUCTNAME_RENTALWORKS) {
            tagprefix = 'web';
        } else if (productname === PRODUCTNAME_TRAKITWORKS) {
            tagprefix = 'tw';
        }

        // load the previous version number
        let productVersionFilePath = '';
        if (productname == PRODUCTNAME_RENTALWORKS) {
            productVersionFilePath = path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', 'version-previous-rentalworks.txt');
        }
        else if (productname == PRODUCTNAME_TRAKITWORKS) {
            productVersionFilePath = path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi' ,'version-previous-trakitworks.txt');
        }
        failIf(!await fse.exists(productVersionFilePath), `Missing file: ${productVersionFilePath}`);
        let previousversionno = await fse.readFile(productVersionFilePath, 'utf8');
        if (previousversionno.length === 0 || previousversionno.split('.').length !== 4) {
            previousversionno = '0.0.0.0';
        }
        const previousversionnoparts = previousversionno.split('.');
        const shortversionno = `${previousversionnoparts[0]}.${previousversionnoparts[1]}.${previousversionnoparts[2]}`;
        const buildno = previousversionnoparts[3];

        const apiVersionFilePath = path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', 'version.txt');
        let apiVersion = await fse.readFile(apiVersionFilePath, 'utf8');
        if (apiVersion.length === 0 || apiVersion.split('.').length !== 4) {
            apiVersion = '0.0.0.0';
        }
        const apiversionnoparts = apiVersion.split('.');
        const shortapiversionno = `${apiversionnoparts[0]}.${apiversionnoparts[1]}.${apiversionnoparts[2]}`;
        const apibuildno = apiversionnoparts[3];

        // Prompt for new version number
        const responseBuildNumber = await prompts({
            type: 'text',
            name: 'value',
            initial: shortapiversionno + '.' + (parseInt(apibuildno) + 1).toString(),
            message: `Enter version number (Previous ${productname} Release: ${previousversionno})`,
            validate: value => value.length > 0 || value.split().length === 4
        });
        const fullversionno = responseBuildNumber.value;
        failIf(fullversionno.length === 0, `Invalid version number: '${fullversionno}'`);
        let fullversionoparts = fullversionno.split('.');
        failIf(isNaN(fullversionoparts[0]) || isNaN(parseInt(fullversionoparts[1])) || isNaN(parseInt(fullversionoparts[2])) || isNaN(parseInt(fullversionoparts[3])), `Invalid version number: '${fullversionno}'`);

        // Prompt for hotfix number
        let hotfixno = '000';
        const responseHotfixNumber = await prompts({
            type: 'text',
            name: 'value',
            initial: '000',
            message: `Enter the current Hotfix Number (ie. 025): `
        });
        if (responseHotfixNumber.value.length > 0) {
            // pad with leading zeros to 3 digits
            hotfixno = responseHotfixNumber.value.padStart(3, '0');
        }

        // Prompt to Commit and FTP
        const responseCommitAndFtp = await prompts({
            type: 'confirm',
            name: 'value',
            initial: false,
            message: `Commit version change and upload to FTP?`
        });
        const commitAndFtp = responseCommitAndFtp.value;
        if (commitAndFtp) {
            if (fullversionno === previousversionno) {
                console.log('New Version matches previous version, please enter previous version no');

                //console.log(shortversionno + '.' + (parseInt(buildno) - 1).toString());
                // Prompt for previous version number if user is trying to build the previous version again
                const responseBuildNumber = await prompts({
                    type: 'text',
                    name: 'value',
                    initial: shortversionno + '.' + (parseInt(buildno) - 1).toString(),
                    message: `Previous ${productname} Version`,
                    validate: value => value.length > 0 || value.split().length === 4
                });

                // fail if DwRentalWorksWebPath environment variable is not set
                failIf(typeof process.env.DwFtpUploadUser === 'undefined' || process.env.DwFtpUploadUser === '', 'Environment Variable DwFtpUploadUser is NOT defined.');

                // fail if DwRentalWorksWebPath environment variable is not set
                failIf(typeof process.env.DwFtpUploadPassword === 'undefined' || process.env.DwFtpUploadPassword === '', 'Environment Variable DwFtpUploadPassword is NOT defined.');
            }
        }

        console.time('Build Time');

        // determine ZIP filename
        console.log('Determine Zip filename');
        const buildnoforzip = fullversionno.replace(/\./g, '_');
        const zipfilename = `${productname}Web_${buildnoforzip}.zip`;
        const zipPath = path.resolve(buildPath, zipfilename);
        console.log(`Zip File: ${zipfilename}`);

        // Update the version.txt files
        console.log('Update the version.txt files');
        fse.writeFileSync(path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', `version-previous-${productname}.txt`), fullversionno);
        if (productname === PRODUCTNAME_RENTALWORKS) {
            await fse.writeFile(path.resolve(process.env.DwRentalWorksWebPath, 'src', `${productname}Web/version.txt`), fullversionno);
        } else {
            await fse.writeFile(path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', `${productname}/version.txt`), fullversionno);
        }
        await fse.writeFile(path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', 'QuikScan', 'version.txt'), fullversionno);
        await fse.writeFile(path.resolve(process.env.DwRentalWorksWebPath, 'src', 'RentalWorksWebApi', 'version.txt'), fullversionno);

        const pdffilename = `v${fullversionno}.pdf`;

        if (commitAndFtp) {
            // We need to commit the version files and Tag the repo here because other commits may come in while we Build
            // command-line Git push in the modified version and assembly files
            await process.chdir(repoPath);
            childProcess.execSync(`git config --global gc.auto 0`, { stdio: 'inherit' });
            if (productname === PRODUCTNAME_RENTALWORKS) {
            childProcess.execSync(`git add "src/${productname}Web/version.txt"`, { stdio: 'inherit' });
            }
            else if (productname === PRODUCTNAME_TRAKITWORKS) {
                childProcess.execSync(`git add "src/RentalWorksWebApi/${productname}/version.txt"`, { stdio: 'inherit' });
            }
            childProcess.execSync(`git add "src/RentalWorksWebApi/QuikScan/version.txt"`, { stdio: 'inherit' });
            childProcess.execSync(`git add "src/RentalWorksWebApi/version.txt"`, { stdio: 'inherit' });
            childProcess.execSync(`git add "src/RentalWorksWebApi/version-previous-${productname}.txt"`, { stdio: 'inherit' });
            childProcess.execSync(`git commit -m "${tagprefix}: ${fullversionno}"`, { stdio: 'inherit' });
            childProcess.execSync(`git push`, { stdio: 'inherit' });
            childProcess.execSync(`git tag ${tagprefix}/v${fullversionno}`, { stdio: 'inherit' });
            childProcess.execSync(`git push origin ${tagprefix}/v${fullversionno}`, { stdio: 'inherit' });

            // copy the document header image to the build directory 
            await fse.copyFile(path.resolve(repoPath, `releasedocumentlogo.png`), path.resolve(repoPath, `build/releasedocumentlogo.png`));

            // command-line gren make Build Release Document for all issues between the previous version's tag and this current tag
            await process.chdir(path.resolve(repoPath, 'build'));
            childProcess.execSync(`npx gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v${fullversionno}.md -t ${tagprefix}/v${fullversionno}..${tagprefix}/v${previousversionno} -c ../config.grenrc`, { stdio: 'inherit' });

            // produce a PDF of the MD file
            await process.chdir(repoPath);
            childProcess.execSync(`npx md-to-pdf build/v${fullversionno}.md`, { stdio: 'inherit' });
            childProcess.execSync(`start build/${pdffilename}`, { stdio: 'inherit' });

            // Need to use curl to publish the PDF file to ZenDesk as a new "article"
            // curl https://dbworks.zendesk.com/api/v2/help_center/sections/{id}/articles.json \
            //   -d '{"article": {"title": "RentalWorksWeb v2019.1.1.XX", "body": "RentalWorksWeb v2019.1.1.XX has been released", "locale": "en-us" }, "notify_subscribers": false}' \
            //   -v -u {email_address}:{password} -X POST -H "Content-Type: application/json"
            // Note: "section" will be something like RentalWorksWeb > Release Documents
            // Note: need to research how to attach documents
        }

        await fse.ensureDir(path.resolve(process.env.DwRentalWorksWebPath, `build`))

        // clean build artifacts
        console.log('Cleaning build artifacts');
        await fse.remove(path.resolve(process.env.DwRentalWorksWebPath, `build/RentalWorksWeb`));
        await fse.remove(path.resolve(process.env.DwRentalWorksWebPath, `build/RentalWorkWebApi`));
        await fse.remove(path.resolve(process.env.DwRentalWorksWebPath, `build/TrakitWorksWeb`));
        await fse.remove(path.resolve(process.env.DwRentalWorksWebPath, `build/TrakitWorksWebApi`));
        await fse.remove(path.resolve(process.env.DwRentalWorksWebPath, `build/${zipfilename}`));

        // create the Hotfix reference file
        console.log('create the Hotfix reference file');
        await fse.ensureDir(path.resolve(process.env.DwRentalWorksWebPath, `build/${productname}Web`))
        const hotfixfilename = path.resolve(process.env.DwRentalWorksWebPath, `build/v${fullversionno}_Hotfix_${hotfixno}`);
        await fse.writeFile(hotfixfilename, '');

        // build the API
        console.log('Run WebApi publish script');
        const webApiSrcPath = path.resolve(process.env.DwRentalWorksWebPath, `src/RentalWorksWebApi`);
        await process.chdir(webApiSrcPath);
        childProcess.execSync('npm i', { stdio: 'inherit' });
        if (productname === PRODUCTNAME_RENTALWORKS) {
            childProcess.execSync('npm run rentalworks-publish', { stdio: 'inherit' });
        }
        else if (productname === PRODUCTNAME_TRAKITWORKS) {
            childProcess.execSync('npm run trakitworks-publish', { stdio: 'inherit' });
        }
        await process.chdir(repoPath);

        let webPublishPath = path.resolve(process.env.DwRentalWorksWebPath, `build/${productname}Web`);
        let webApiPublishPath = path.resolve(process.env.DwRentalWorksWebPath, `build/${productname}WebApi`);
        let jestSrcTsPath = path.resolve(process.env.DwRentalWorksWebPath, `src/JestTest/src/ts`);
        if (productname === "TrakitWorks") {
            await fse.move(path.resolve(buildPath, 'RentalWorksWebApi'), path.resolve(buildPath, 'TrakitWorksWebApi'));
        }

        // need to get creative with a promise here so we wait for the zip to be written to disk
        await (async () => {
            return new Promise(async (resolve, reject) => {
                var output = fse.createWriteStream(zipPath);
                var archive = archiver('zip');
                output.on('close', function () {
                    console.log(`Finished zipping: ${zipfilename} ${formatBytes(archive.pointer())}`);
                    resolve();
                });
                archive.on('error', function (err) {
                    throw err;
                    reject(err);
                });
                archive.pipe(output)
                archive.directory(webPublishPath, `${productname}Web`);
                archive.directory(webApiPublishPath, `${productname}WebApi`);
                archive.directory(jestSrcTsPath, 'ts');
                await archive.finalize();
            });
        })();

        // delete the work files
        await fse.remove(path.resolve(buildPath, `${productname}Web`));
        await fse.remove(path.resolve(buildPath, `${productname}WebApi`));

        // copy the ZIP delivable to "history" sub-directory
        await fse.ensureDir(path.resolve(buildPath, 'history'));
        await fse.copy(zipPath, path.resolve(buildPath, `history/${zipfilename}`));

        if (commitAndFtp) {
            // Create FTP command file to upload the zip
            const ftpcommandfilename = 'ftp.txt';
            await process.chdir(buildPath);
            let file = [];
            file.push(`open ftp.dbworks.com`);
            file.push(`${process.env.DwFtpUploadUser}`);
            file.push(`${process.env.DwFtpUploadPassword}`);
            file.push(`cd Update`);
            file.push(`cd ${productname}Web`);
            file.push(`cd ${shortversionno}`);
            file.push(`cd QA`);
            file.push(`put ${hotfixfilename}`);
            file.push(`put ${pdffilename}`);
            file.push(`put ${zipfilename}`);
            file.push(`quit`);
            file = file.join('\n');
            await fse.writeFile(ftpcommandfilename, file);
            // Run the FTP command using the command file created above, delete the command file
            childProcess.execSync(`ftp -s:${ftpcommandfilename} -v`, { stdio: 'inherit' });
            await fse.remove(ftpcommandfilename);
        }

        //console.log(`Building: ${productname} v${fullversionno}`);
        //console.log(`Hotfix ${hotfixno}`);
        //console.log(`Commit and FTP: ${commitAndFtp ? 'YES' : 'NO'}`);

        console.timeEnd('Build Time');
    }
    catch (ex) {
        failIf(true, ex);
    }
})();

