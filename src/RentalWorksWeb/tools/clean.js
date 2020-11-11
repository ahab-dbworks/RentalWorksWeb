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

const cleanFolderAsync = async (folder) => {
    //console.log(dirPath);
    let nodes = fse.readdirSync(folder);
    let directoryIsEmpty = true;
    let hasOtherFiles = false;
    let hasJsFile = false;
    let hasJsMapFile = false;
    const files = [];
    for (let i = 0; i < nodes.length; i++) {
        const node = nodes[i];
        const filePath = path.resolve(folder, node);
        const fileInfo = fse.statSync(filePath);
        if (fileInfo.isFile()) {
            files.push(node);
            directoryIsEmpty = false;
            if ( !(node.endsWith('.js') || node.endsWith('.js.map')) ) {
                hasOtherFiles = true;
            }
            if (node.endsWith('.js')) {
                hasJsFile = true;
                await fse.remove(filePath);
                console.log(`- Deleting: ${filePath}`);
            }
            else if (node.endsWith('.js.map')) {
                hasJsMapFile = true;
                await fse.remove(filePath);
                console.log(`- Deleting: ${filePath}`);
            }
        }
        else if (fileInfo.isDirectory()) {
            directoryIsEmpty = false;
        }
    }
    const shouldDelete = (fse.readdirSync(folder).length === 0);
    return shouldDelete;
}

const cleanFoldersAsync = async (dirPath) => {
    let allfolders = [];
    await getAllFoldersRecursiveAsync(dirPath, allfolders);
    let removedFolder = false;
    for (let i = 0; i < allfolders.length; i++) {
        const folder = allfolders[i];
        if (await cleanFolderAsync(folder)) {
            console.log(`- Deleting: ${folder}`);
            await fse.remove(folder);
            removedFolder = true;
        }
    }
    return removedFolder;
}

process.on('unhandledRejection', (reason) => {
    failIf(true, reason);
});

// closure for asynchronous code
(async () => {
    try {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- Clean RentalWorks -');
        const pathSource = path.resolve(__dirname, '..', 'Source')
        console.log(pathSource);
        let removedFolder = false;
        do {
            removedFolder = await cleanFoldersAsync(pathSource);
        } while (removedFolder)

        // clean again in case there are any left over empty folders after the last clean
        await cleanFoldersAsync(pathSource);
        console.log('- Finished Clean RentalWorks -');
    } catch (ex) {
        failIf(true, ex);
    }
}) ();