const fse = require('fs-extra'); // https://www.npmjs.com/package/fs-extra
const path = require('path'); // https://nodejs.org/api/path.html
const childProcess = require('child_process'); // https://nodejs.org/api/child_process.html

function failIf(expression, reason) {
    if (expression) {
        console.error(reason);
        console.error('Clean failed!');
        process.exit();
    }
}

const getAllFoldersRecursiveAsync = async (dirPath, arrayOfFolders) => {
    const nodes = fse.readdirSync(dirPath);
    arrayOfFolders = arrayOfFolders || [];
    nodes.forEach(async(node) => {
        const nodePath = path.resolve(dirPath, node);
        if (fse.statSync(nodePath).isDirectory()) {
            arrayOfFolders.push(nodePath);
            arrayOfFolders = await getAllFoldersRecursiveAsync(nodePath, arrayOfFolders);
        }
    });
    return arrayOfFolders;
}

const shouldDeleteFolderAsync = async (dirPath) => {
    //console.log(dirPath);
    const nodes = fse.readdirSync(dirPath);
    let directoryIsEmpty = true;
    let hasTsFile = false;
    let hasJsFile = false;
    let hasJsMapFile = false;
    const files = [];
    nodes.forEach(async (node) => {
        const filePath = path.resolve(dirPath, node);
        const fileInfo = fse.statSync(filePath);
        if (fileInfo.isFile()) {
            files.push(node);
            directoryIsEmpty = false;
            hasTsFile = node.endsWith('.ts');
            hasJsFile = node.endsWith('.js');
            hasJsMapFile = node.endsWith('.js.map');
        }
        else if (fileInfo.isDirectory()) {
            directoryIsEmpty = false;
        }
    });
    let shouldDelete = directoryIsEmpty || !hasTsFile && (hasJsFile || hasJsMapFile);
    //if (shouldDelete) {
    //    const confirmDeleteResponse = await prompts({
    //        type: 'confirm',
    //        name: 'value',
    //        initial: false,
    //        message: `Delete: "${dirPath}"?\n${files.join('\n')}`
    //    });
    //    shouldDelete = confirmDeleteResponse.value;
    //}
    return shouldDelete;
}

const cleanFolderAsync = async (dirPath) => {
    let allfolders = [];
    await getAllFoldersRecursiveAsync(dirPath, allfolders);
    for (let i = 0; i < allfolders.length; i++) {
        const folder = allfolders[i];
        if (await shouldDeleteFolderAsync(folder)) {
            console.log(`- Deleting: ${folder}`);
            await fse.remove(folder)
        }
    }
}

process.on('unhandledRejection', (reason) => {
    failIf(true, reason);
});

// closure for asynchronous code
(async () => {
    try {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- Clean RentalWorks -');
        const pathSource = path.resolve(process.cwd(), 'Source')
        await cleanFolderAsync(pathSource);

        // clean again in case there are any left over empty folders after the last clean
        await cleanFolderAsync(pathSource);
        console.log('- Finished Clean RentalWorks -');
    } catch (ex) {
        failIf(true, ex);
    }
}) ();