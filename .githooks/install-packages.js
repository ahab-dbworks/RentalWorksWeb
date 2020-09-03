#!/usr/bin/env node

const childProcess  = require('child_process');
const process = require('process');
const path = require('path');

// npm install in only the directories where a package-lock.json changed
function runNpmInstall(packageLockPath) {
    const dirname = path.dirname(packageLockPath);
    console.log('------------------------------------------------------------');
    console.log(`${dirname}: npm i`);
    process.chdir(dirname);
    console.log(childProcess.execSync('npm i').toString());
}

// get a list of files that were changed in the last build
const changedFilesBuffer = childProcess.execSync('git diff-tree -r --name-only --no-commit-id ORIG_HEAD HEAD');

// convert buffer into an array of changed filenames
const changedFiles = changedFilesBuffer.toString().split('\n');

// run npm install in the directory of any package-lock.json files that were modified
for (let fileno = 0; fileno < changedFiles.length; fileno++) {
    const filename = changedFiles[fileno];
    if (filename.endsWith('package-lock.json')) {
        runNpmInstall(filename);
    }    
}
