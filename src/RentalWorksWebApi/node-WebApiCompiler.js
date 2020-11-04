const spawn = require('child-process-promise').spawn;
const childProcess = require('child_process'); // https://nodejs.org/api/child_process.html
const fse = require('fs-extra');
const path = require('path');
const WebpackReportsCompiler = require('./node-WebpackReportsCompiler');
const UNSUPPORTED_CONFIGURATION = 'Unsupported configuration.';

class WebApiCompiler {
    static get TARGET_ALL() { return 'all'; };
    static get TARGET_API() { return 'api' };
    static get TARGET_WEB() { return 'web' };
    static get TARGET_TRAKITWORKS() { return 'trakitworks' };
    static get TARGET_REPORTS() { return 'reports' };
    static get TARGET_QUIKSCAN() { return 'quikscan' };
    static get BUILD_CONFIGURATION_DEVELOPMENT() { return 'dev'; };
    static get BUILD_CONFIGURATION_PRODUCTION() { return 'prod'; };
    static get BUILD_ACTION_BUILD() { return 'build'; };
    static get BUILD_ACTION_RUN() { return 'run'; };
    static get BUILD_ACTION_WATCH() { return 'watch'; };
    //------------------------------------------------------------------------------------
    constructor(buildAction, target, buildMode, reports) {
        this.buildAction = buildAction;
        this.target = target;
        this.buildConfiguration = buildMode;
        this.dotnetConfiguration = this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT ? "Debug" : "Release";
        this.reports = reports;
        this.appSolutionDir = path.resolve(__dirname, '../../');
        this.buildTypeScript = false;
    }
    //------------------------------------------------------------------------------------
    async removeDir_prod_downloads() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/temp/downloads');
        await fse.remove('wwwroot/temp/downloads');
    }
    //------------------------------------------------------------------------------------
    async removeDir_prod_rentalworks() {
        console.log('//------------------------------------------------------------------------------------');
        const webDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/rentalworks');
        console.log(`Deleting: ${webDir}`);
        await fse.remove(webDir);
    }
    //------------------------------------------------------------------------------------
    async removeDir_prod_trakitworks() {
        console.log('//------------------------------------------------------------------------------------');
        const webDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/trakitworks');
        console.log(`Deleting: ${webDir}`);
        await fse.remove(webDir);
    }
    //------------------------------------------------------------------------------------
    async removeDir_prod_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: wwwroot/Reports');
        await fse.remove('wwwroot/Reports');
    }
    //------------------------------------------------------------------------------------
    async removeDir_prod_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        const quikscanDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/quikscan');
        console.log(`Deleting: ${quikscanDir}`);
        await fse.remove(quikscanDir);
    }
    //------------------------------------------------------------------------------------
    async removeDir_publishfolder() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Deleting: ../../build/RentalWorksWebApi');
        await fse.remove('../../build/RentalWorksWebApi');
    }
    //------------------------------------------------------------------------------------
    async npm_i() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`cd ${this.appSolutionDir}`);
        await process.chdir(this.appSolutionDir);
        console.log('npm i');
        childProcess.execSync('npm i', { stdio: 'inherit' });

        if (this.target === WebApiCompiler.TARGET_ALL || this.target === WebApiCompiler.TARGET_WEB) {
            const pathWeb = path.resolve(this.appSolutionDir, 'src/RentalWorksWeb');
            console.log('//------------------------------------------------------------------------------------');
            console.log(`cd ${pathWeb}`);
            await process.chdir(pathWeb);
            console.log('npm i');
            childProcess.execSync('npm i', { stdio: 'inherit' });
        }

        if (this.target === WebApiCompiler.TARGET_ALL || this.target === WebApiCompiler.TARGET_QUIKSCAN) {
            const pathQuikScan = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan');
            console.log('//------------------------------------------------------------------------------------');
            console.log(`cd ${pathQuikScan}`);
            await process.chdir(pathQuikScan);
            console.log('npm i');
            childProcess.execSync('npm i', { stdio: 'inherit' });
        }

        if (this.target === WebApiCompiler.TARGET_ALL || this.target === WebApiCompiler.TARGET_TRAKITWORKS) {
            const pathTraktitWorks = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/TrakitWorks');
            console.log('//------------------------------------------------------------------------------------');
            console.log(`cd ${pathTraktitWorks}`);
            await process.chdir(pathTraktitWorks);
            console.log('npm i');
            childProcess.execSync('npm i', { stdio: 'inherit' });
        }

        const pathWebApi = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi');
        console.log('//------------------------------------------------------------------------------------');
        console.log(`cd ${pathWebApi}`);
        await process.chdir(pathWebApi);
        console.log('npm i');
        childProcess.execSync('npm i', { stdio: 'inherit' });

        console.log(`cd ${this.appSolutionDir}`);
        await process.chdir(this.appSolutionDir);
    }
    //------------------------------------------------------------------------------------
    async dotnet_restore() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('dotnet restore');
        childProcess.execSync('npm i', { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async clean_api() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Cleaning compiled files...')
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/bin/Debug');
            await fse.remove('../../lib/Fw/src/FwStandard/bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/bin/Release');
            await fse.remove('../../lib/Fw/src/FwStandard/bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/obj/Debug');
            await fse.remove('../../lib/Fw/src/FwStandard/obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwStandard/obj/Release');
            await fse.remove('../../lib/Fw/src/FwStandard/obj/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/bin/Debug');
            await fse.remove('../../lib/Fw/src/FwCore/bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/bin/Release');
            await fse.remove('../../lib/Fw/src/FwCore/bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/obj/Debug');
            await fse.remove('../../lib/Fw/src/FwCore/obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: ../../lib/Fw/src/FwCore/obj/Release');
            await fse.remove('../../lib/Fw/src/FwCore/obj/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: bin/Debug');
            await fse.remove('bin/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: bin/Release');
            await fse.remove('bin/Release');
        }
        //________________________________________________________
        if (this.buildConfiguration === 'dev') {
            console.log('Deleting: obj/Release');
            await fse.remove('obj/Debug');
        } else if (this.buildConfiguration === 'prod') {
            console.log('Deleting: obj/Release');
            await fse.remove('obj/Release');
        }
    }
    //------------------------------------------------------------------------------------
    async build_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Building Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        await this.removeDir_prod_reports();
        process.chdir(path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi'));
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration, this.reports);
        const stats = await compiler.build();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async watch_webpack_reports() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watching Webpack Reports');
        console.log('//------------------------------------------------------------------------------------');
        await this.removeDir_prod_reports();
        let compiler = new WebpackReportsCompiler(this.buildAction, this.target, this.buildConfiguration, this.reports);
        const stats = await compiler.watch();
        console.log(stats.toString({ colors: true }));
    }
    //------------------------------------------------------------------------------------
    async build_rentalworks() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- RentalWorks -');

        const jsAppBuilderConfigFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWeb/JSAppBuilder.config');
        const versionFilePath = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt');
        const version = (await fse.readFile(versionFilePath, 'utf8')).trim();
        const srcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWeb');
        const destDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/rentalworks');
        let publish = false;
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            publish = false;
            await fse.copy(`${srcDir}/libraries/fw/theme/fwaudio`, `${srcDir}/theme/fwaudio`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwcursors`, `${srcDir}/theme/fwcursors`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwfonts`, `${srcDir}/theme/fwfonts`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwimages`, `${srcDir}/theme/fwimages`);
        }
        else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            publish = true;
            await this.removeDir_prod_rentalworks();
            const webOutputThemeDir = path.resolve(destDir, 'theme');
            const webOutputLibrariesDir = path.resolve(destDir, 'libraries');
            await fse.ensureDir(destDir);
            await fse.ensureDir(webOutputThemeDir);
            await fse.ensureDir(webOutputLibrariesDir);

            await fse.copy(`${srcDir}/theme/audio`, `${destDir}/theme/audio`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwaudio`, `${destDir}/theme/fwaudio`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwcursors`, `${destDir}/theme/fwcursors`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwfonts`, `${destDir}/theme/fwfonts`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwimages`, `${destDir}/theme/fwimages`);
            await fse.copy(`${srcDir}/theme/images`, `${destDir}/theme/images`);
            await fse.copy(`${srcDir}/libraries/ckeditor`, `${destDir}/libraries/ckeditor`);
            await fse.copy(`${srcDir}/web.config`, `${destDir}/web.config`);
            await fse.copy(versionFilePath, `${destDir}/version.txt`);
        }
        if (this.buildTypeScript) {
            console.log(`- Compiling TypeScript...`);
            // clean TypScript
            process.chdir(path.resolve(this.appSolutionDir, 'src/RentalWorksWeb'));
            childProcess.execSync(`npx tsc --build --clean`, { stdio: 'inherit' })
            childProcess.execSync(`npx tsc --build"`, { stdio: 'inherit' })
            process.chdir(this.appSolutionDir);
        }
        console.log(`- Building App...`);
        await spawn('dotnet', [path.resolve(this.appSolutionDir, 'lib/Fw/build/JSAppBuilder/JSAppBuilder.dll'), '-ConfigFilePath', jsAppBuilderConfigFile, '-SolutionDir', this.appSolutionDir, '-Version', version, '-UpdateSchema', 'false', '-Publish', publish, '-AttachDebugger', 'false'], { stdio: 'inherit' });
        //console.log(`Finished running JSAppBuilder for RentalWorksWeb`);
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            //console.log('//------------------------------------------------------------------------------------');
            //console.log('Fixing urls on index page...')
            const pathIndexFile = `${srcDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            const appSettingsJsonFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/appsettings.json');
            const appSettingsJsonWithComments = await fse.readFile(appSettingsJsonFile, 'utf8');
            const appSettingsJson = appSettingsJsonWithComments.replace(/\\"|"(?:\\"|[^"])*"|(\/\/.*|\/\*[\s\S]*?\*\/)/g, (m, g) => g ? "" : m).trim();
            const appSettings = JSON.parse(appSettingsJson);
            let devPath = '/rentalworksdev';
            if (appSettings && appSettings.ApplicationConfig && appSettings.ApplicationConfig.Apps && appSettings.ApplicationConfig.Apps.rentalworks && typeof appSettings.ApplicationConfig.Apps.rentalworks.DevPath === 'string') {
                devPath = appSettings.ApplicationConfig.Apps.rentalworks.DevPath
            }
            fileText = fileText.replace(/\[appbaseurl\]/g, `${devPath}/`);
            await fse.writeFile(pathIndexFile, fileText);
        }
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            console.log('Fixing urls on index page...')
            const pathIndexFile = `${destDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            fileText = fileText.replace(/\[appbaseurl\]/g, './');
            fileText = fileText.replace(/\[appvirtualdirectory\]/g, '');
            await fse.writeFile(pathIndexFile, fileText);
            console.log(`Minifiying RentalWorks JavaScript with google-closure-compiler...`);
            await fse.move(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/script1-${version}.js`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/script1-${version}.merged.js`));
            await spawn('npx', ['google-closure-compiler', '--js=' + path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/script1-${version}.merged.js`), '--js_output_file=' + path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/script1-${version}.js`)], { stdio: 'inherit' });
            await fse.unlink(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/script1-${version}.merged.js`));
            //console.log(`Finished minifying RentalWorks JavaScript`);
            console.log(`Minifiying RentalWorks CSS with clean-css-cli...`);
            await fse.move(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/theme/style-${version}.css`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/theme/style-${version}.merged.css`));
            await spawn('npx', ['clean-css-cli', '-o', path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/theme/style-${version}.css`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/theme/style-${version}.merged.css`)], { stdio: 'inherit' });
            await fse.unlink(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/rentalworks/theme/style-${version}.merged.css`));
            //console.log(`Finished minifying RentalWorks CSS`);
        }
        console.log('- Finished RentalWorks -');
    }
    //------------------------------------------------------------------------------------
    async watch_web() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watch has not been implemented for Web');
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async build_trakitworks() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- TrakitWorks -');
        const jsAppBuilderConfigFile = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'TrakItWorks', 'JSAppBuilder.config');
        const versionFilePath = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'version.txt');
        const version = (await fse.readFile(versionFilePath, 'utf8')).trim();
        const srcDir = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'TrakItWorks');
        const destDir = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'apps', 'trakitworks');
        let publish = false;
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            publish = false;
            const themeSrcDir = path.resolve(srcDir, 'libraries', 'fw', 'theme');
            const themeDestDir = path.resolve(srcDir, 'theme');
            await fse.copy(path.resolve(themeSrcDir, 'fwaudio'), path.resolve(themeDestDir, 'fwaudio'));
            await fse.copy(path.resolve(themeSrcDir, 'fwcursors'), path.resolve(themeDestDir, 'fwcursors'));
            await fse.copy(path.resolve(themeSrcDir, 'fwfonts'), path.resolve(themeDestDir, 'fwfonts'));
            await fse.copy(path.resolve(themeSrcDir, 'fwimages'), path.resolve(themeDestDir, 'fwimages'));
        }
        else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            publish = true;
            await this.removeDir_prod_trakitworks();
            const webOutputThemeDir = path.resolve(destDir, 'theme');
            const webOutputLibrariesDir = path.resolve(destDir, 'libraries');
            await fse.ensureDir(destDir);
            await fse.ensureDir(webOutputThemeDir);
            await fse.ensureDir(webOutputLibrariesDir);

            await fse.copy(`${srcDir}/theme/audio`, `${destDir}/theme/audio`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwaudio`, `${destDir}/theme/fwaudio`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwcursors`, `${destDir}/theme/fwcursors`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwfonts`, `${destDir}/theme/fwfonts`);
            await fse.copy(`${srcDir}/libraries/fw/theme/fwimages`, `${destDir}/theme/fwimages`);
            await fse.copy(`${srcDir}/theme/images`, `${destDir}/theme/images`);
            await fse.copy(`${srcDir}/libraries/ckeditor`, `${destDir}/libraries/ckeditor`);
            await fse.copy(`${srcDir}/web.config`, `${destDir}/web.config`);
            await fse.copy(versionFilePath, `${destDir}/version.txt`);
        }
        if (this.buildTypeScript) {
            console.log(`- Compiling TypeScript...`);
            // clean TypScript
            process.chdir(path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/TrakItWorks'));
            childProcess.execSync(`npx tsc --build --clean`, { stdio: 'inherit' })
            childProcess.execSync(`npx tsc --build"`, { stdio: 'inherit' })
            process.chdir(this.appSolutionDir);
        }
        console.log(`- Building App...`);
        const pathJsAppBuilderDll = path.resolve(this.appSolutionDir, 'lib/Fw/build/JSAppBuilder/JSAppBuilder.dll');
        childProcess.execSync(`dotnet "${pathJsAppBuilderDll}" -ConfigFilePath "${jsAppBuilderConfigFile}" -SolutionDir "${this.appSolutionDir}" -Version ${version} -UpdateSchema false -Publish ${publish} -AttachDebugger false`, { stdio: 'inherit' });
        //console.log(`Finished running JSAppBuilder for TrakItWorks`);
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            //console.log('//------------------------------------------------------------------------------------');
            //console.log('Fixing urls on index page...')
            const pathIndexFile = `${srcDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            const appSettingsJsonFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/appsettings.json');
            const appSettingsJsonWithComments = await fse.readFile(appSettingsJsonFile, 'utf-8');
            const appSettingsJson = appSettingsJsonWithComments.replace(/\\"|"(?:\\"|[^"])*"|(\/\/.*|\/\*[\s\S]*?\*\/)/g, (m, g) => g ? "" : m).trim();
            const appSettings = JSON.parse(appSettingsJson);
            let devPath = '/trakitworksdev';
            if (appSettings && appSettings.ApplicationConfig && appSettings.ApplicationConfig.Apps && appSettings.ApplicationConfig.Apps.trakitworks && typeof appSettings.ApplicationConfig.Apps.trakitworks.DevPath === 'string') {
                devPath = appSettings.ApplicationConfig.Apps.trakitworks.DevPath
            }
            fileText = fileText.replace(/\[appbaseurl\]/g, `${devPath}/`);
            await fse.writeFile(pathIndexFile, fileText);
        }
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            //console.log('//------------------------------------------------------------------------------------');
            //console.log('Fixing urls on index page...')
            const pathIndexFile = `${destDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            fileText = fileText.replace(/\[appbaseurl\]/g, './');
            fileText = fileText.replace(/\[appvirtualdirectory\]/g, '');
            await fse.writeFile(pathIndexFile, fileText);
            console.log(`- Minifiying TrakItWorks JavaScript with google-closure-compiler...`);
            const pathScript1 = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'apps', 'trakitworks', `script1-${version}.js`);
            const pathScript1Merged = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'apps', 'trakitworks', `script1-${version}.merged.js`);
            await fse.move(pathScript1, pathScript1Merged);
            childProcess.execSync(`npx google-closure-compiler --js="${pathScript1}" --js_output_file="${pathScript1Merged}"`, { stdio: 'inherit' });
            await fse.unlink(pathScript1Merged);
            //console.log(`Finished minifying TrakItWorks JavaScript`);
            console.log(`- Minifiying TrakItWorks CSS with clean-css-cli...`);
            const pathCss = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'apps', 'trakitworks', 'theme', `style-${version}.css`);
            const pathCssMerged = path.resolve(this.appSolutionDir, 'src', 'RentalWorksWebApi', 'apps', 'trakitworks', 'theme', `style-${version}.merged.css`);
            await fse.move(pathCss, pathCssMerged);
            childProcess.execSync(`npx clean-css-cli -o "${pathCss}" "${pathCssMerged}"`, { stdio: 'inherit' });
            await fse.unlink(pathCssMerged);
            //console.log(`Finished minifying TrakItWorks CSS`);
        }
        console.log('- Finished TrakitWorks -');
    }
    //------------------------------------------------------------------------------------
    async watch_trakitworks() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watch has not been implemented for TrakItWorks');
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async build_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- QuikScan -');

        const jsAppBuilderConfigFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan/JSAppBuilder.config');
        const versionFilePath = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt');
        const version = (await fse.readFile(versionFilePath, 'utf8')).trim();
        const srcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan');
        const destDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps/quikscan');
        let publish = false;
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            publish = false;
            await fse.copy(path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/version.txt'), path.resolve(srcDir, 'version.txt'));
            await fse.copy(path.resolve(srcDir, 'theme'), path.resolve(destDir, 'theme'));
            await fse.copy(path.resolve(srcDir, 'views'), path.resolve(destDir, 'views'));
        }
        else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            publish = true;
            await this.removeDir_prod_quikscan();
            const quikScanOutputThemeDir = path.resolve(destDir, 'theme');
            await fse.ensureDir(destDir);
            await fse.ensureDir(quikScanOutputThemeDir);
        }
        if (this.buildTypeScript) {
            console.log(`- Compiling TypeScript...`);
            process.chdir(path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/QuikScan'));
            childProcess.execSync(`tsc --build --clean`, { stdio: 'inherit' });
            childProcess.execSync(`tsc --build`, { stdio: 'inherit' });
            //console.log(`Finished QuikScan TypeScript`);
            process.chdir(this.appSolutionDir);
        }
        console.log(`- Building App...`);
        await spawn('dotnet', [path.resolve(this.appSolutionDir, 'lib/Fw/build/JSAppBuilder/JSAppBuilder.dll'), '-ConfigFilePath', jsAppBuilderConfigFile, '-SolutionDir', this.appSolutionDir, '-Version', version, '-UpdateSchema', 'false', '-Publish', publish, '-AttachDebugger', 'false'], { stdio: 'inherit' });
        //console.log(`Finished running JSAppBuilder for QuikScan`);
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
            //console.log('//------------------------------------------------------------------------------------');
            //console.log('Fixing urls on index page...')
            const pathIndexFile = `${srcDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            const appSettingsJsonFile = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/appsettings.json');
            const appSettingsJsonWithComments = await fse.readFile(appSettingsJsonFile, 'utf8');
            const appSettingsJson = appSettingsJsonWithComments.replace(/\\"|"(?:\\"|[^"])*"|(\/\/.*|\/\*[\s\S]*?\*\/)/g, (m, g) => g ? "" : m).trim();
            const appSettings = JSON.parse(appSettingsJson);
            let devPath = '/quikscandev';
            if (appSettings && appSettings.ApplicationConfig && appSettings.ApplicationConfig.Apps && appSettings.ApplicationConfig.Apps.quikscan && typeof appSettings.ApplicationConfig.Apps.quikscan.DevPath === 'string') {
                devPath = appSettings.ApplicationConfig.Apps.quikscan.DevPath
            }
            fileText = fileText.replace(/\[appbaseurl\]/g, `${devPath}/`);
            await fse.writeFile(pathIndexFile, fileText);
        }
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            const pathIndexFile = `${destDir}/index.htm`;
            let fileText = await fse.readFile(pathIndexFile, 'utf8');
            fileText = fileText.replace(/\[appbaseurl\]/g, './');
            fileText = fileText.replace(/\[appvirtualdirectory\]/g, '');
            await fse.writeFile(pathIndexFile, fileText);
            console.log(`Minifiying QuikScan with google-closure-compiler...`);
            await fse.move(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.js`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.merged.js`));
            await spawn('npx', ['google-closure-compiler', '--js=' + path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.merged.js`), '--js_output_file=' + path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.js`)], { stdio: 'inherit' });
            //await spawn('npx', ['uglifyjs', path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.js`), '-o', path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.min.js`), '--compress', '--mangle'], { stdio: 'inherit' });
            await fse.unlink(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/script-${version}.merged.js`));
            console.log(`Minifiying QuikScan CSS with clean-css-cli...`);
            await fse.move(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/theme/style-${version}.css`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/theme/style-${version}.merged.css`));
            await spawn('npx', ['clean-css-cli', '-o', path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/theme/style-${version}.css`), path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/theme/style-${version}.merged.css`)], { stdio: 'inherit' });
            await fse.unlink(path.resolve(this.appSolutionDir, `src/RentalWorksWebApi/apps/quikscan/theme/style-${version}.merged.css`));
        }
        if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
            await fse.copy(`${srcDir}/theme/fwaudio`, `${destDir}/theme/fwaudio`);
            await fse.copy(`${srcDir}/theme/fwcursors`, `${destDir}/theme/fwcursors`);
            await fse.copy(`${srcDir}/theme/fwfonts`, `${destDir}/theme/fwfonts`);
            await fse.copy(`${srcDir}/theme/fwimages`, `${destDir}/theme/fwimages`);
            await fse.copy(`${srcDir}/theme/images`, `${destDir}/theme/images`);

            await fse.copy(`${srcDir}/index.js`, `${destDir}/index.js`);
            await fse.copy(`${srcDir}/web.config`, `${destDir}/web.config`);
            await fse.copy(versionFilePath, `${destDir}/version.txt`);
        }
        // build project with webpack
        //let compiler = new WebpackQuikScanCompiler(this.buildAction, this.target, this.buildConfiguration);
        //const stats = await compiler.build();
        //console.log(stats.toString({ colors: true }));
        console.log('- Finished QuikScan -');
    }
    //------------------------------------------------------------------------------------
    async watch_quikscan() {
        console.log('//------------------------------------------------------------------------------------');
        console.log('Watch has not been implemented for QuikScan');
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async build_webapi() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet build --configuration ${this.dotnetConfiguration}`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['build', '--configuration', this.dotnetConfiguration], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async publish_webapi() {
        const appsSrcDir = path.resolve(this.appSolutionDir, 'src/RentalWorksWebApi/apps');
        const appsDestDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/apps');
        const rentalworksDestSrcDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/RentalWorks');
        const quikscanDestSrcDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/QuikScan');
        const trakitworksDestSrcDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/TrakitWorks');
        const webSrcDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/apps/rentalworks');
        const webDestDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWeb');
        const downloadsDestDir = path.resolve(this.appSolutionDir, 'build/RentalWorksWebApi/wwwroot/temp/downloads');
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet publish -o ../../build/RentalWorksWebApi WebApi.csproj --configuration ${this.dotnetConfiguration} --self-contained -r win-x64`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['publish', '-o', '../../build/RentalWorksWebApi', 'WebApi.csproj', '--configuration', this.dotnetConfiguration, '--self-contained', '-r', 'win-x64'], { stdio: 'inherit' });
        console.log('Deleting: appsettings.json');
        await fse.unlink('../../build/RentalWorksWebApi/appsettings.json', function (error) { if (error) { throw error; } console.log('Deleted appsettings.json'); });
        console.log('//------------------------------------------------------------------------------------');
        await fse.remove(appsDestDir);

        // prevent any source folders from getting deployed by accident.
        await fse.remove(rentalworksDestSrcDir);
        await fse.remove(quikscanDestSrcDir);
        await fse.remove(trakitworksDestSrcDir);

        await fse.ensureDir(appsDestDir);
        console.log(`Deploying apps folder from: "${appsSrcDir}" to "${appsDestDir}"`);
        await fse.copy(appsSrcDir, appsDestDir);
        console.log(`Deploying legacy RentalWorksWeb folder from: "${webSrcDir}" to "${webDestDir}"`);
        await fse.copy(webSrcDir, webDestDir);
        console.log('//------------------------------------------------------------------------------------');
        console.log('Adding: wwwroot/temp/downloads');
        await process.chdir('../../build/RentalWorksWebApi/wwwroot');
        await fse.ensureDir(downloadsDestDir);
        console.log('//------------------------------------------------------------------------------------');
    }
    //------------------------------------------------------------------------------------
    async run_webapi() {
        console.log('//------------------------------------------------------------------------------------');
        console.log(`dotnet run --configuration ${this.dotnetConfiguration} --launch-profile WebApi`);
        console.log('//------------------------------------------------------------------------------------');
        await spawn('dotnet', ['run', '--configuration', this.dotnetConfiguration, '--launch-profile', 'WebApi'], { stdio: 'inherit' });
    }
    //------------------------------------------------------------------------------------
    async build() {
        try {
            if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                this.buildTypeScript = true;
            }
            if (this.target === WebApiCompiler.TARGET_ALL) {
                if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
                    if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                        this.buildTypeScript = true;
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.build_rentalworks();
                        await this.build_trakitworks();
                        await this.build_quikscan();
                        await this.build_webpack_reports();
                        await this.build_webapi();
                    } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_RUN) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.build_rentalworks();
                        await this.build_trakitworks();
                        await this.build_quikscan();
                        await this.build_webpack_reports();
                        await this.run_webapi();
                    } else {
                        throw UNSUPPORTED_CONFIGURATION;
                    }
                } else if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                    await this.clean_api();
                    await this.removeDir_prod_downloads();
                    await this.removeDir_prod_rentalworks();
                    await this.removeDir_prod_trakitworks();
                    await this.removeDir_prod_quikscan();
                    await this.removeDir_prod_reports();
                    await this.removeDir_publishfolder();
                    await this.npm_i();
                    await this.build_rentalworks();
                    await this.build_trakitworks();
                    await this.build_quikscan();
                    await this.build_webpack_reports();
                    await this.publish_webapi();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_API) {
                if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_DEVELOPMENT) {
                    if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.build_webapi();
                    } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_RUN) {
                        await this.npm_i();
                        await this.dotnet_restore();
                        await this.clean_api();
                        await this.run_webapi();
                    } else {
                        throw UNSUPPORTED_CONFIGURATION;
                    }
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_REPORTS) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    await this.npm_i();
                    await this.build_webpack_reports();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    await this.npm_i();
                    await this.watch_webpack_reports();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } else if (this.target === WebApiCompiler.TARGET_WEB) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.build_rentalworks();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.watch_web();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            } 
            else if (this.target === WebApiCompiler.TARGET_TRAKITWORKS) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.build_trakitworks();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.watch_trakitworks();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            }
            else if (this.target === WebApiCompiler.TARGET_QUIKSCAN) {
                if (this.buildAction === WebApiCompiler.BUILD_ACTION_BUILD) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.build_quikscan();
                } else if (this.buildAction === WebApiCompiler.BUILD_ACTION_WATCH) {
                    if (this.buildConfiguration === WebApiCompiler.BUILD_CONFIGURATION_PRODUCTION) {
                        await this.npm_i();
                    }
                    await this.watch_quikscan();
                } else {
                    throw UNSUPPORTED_CONFIGURATION;
                }
            }
        } catch (ex) {
            if (Array.isArray(ex)) {
                for (let i = 0; i < ex.length; i++) {
                    console.error(ex[i]);
                }
            } else {
                console.error(ex);
            }
        }
    }
    //------------------------------------------------------------------------------------
}

module.exports = WebApiCompiler;