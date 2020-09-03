#!/usr/bin/env node

const spawn  = require('child_process');
const process = require('process');

function enableGitHooks() {
    const enableHooksPathCommand = 'git config core.hooksPath .githooks';
    console.log('Changing git hooks path to commited directory: .githooks');
    console.log(enableHooksPathCommand);
    console.log(spawn.execSync(enableHooksPathCommand).toString());
}

function disableGitHooks() {
    const disableHooksPathCommand = 'git config --unset core.hooksPath';
    console.log('Reverting to default uncommited githooks path: .git/hooks');
    console.log(disableHooksPathCommand);
    console.log(spawn.execSync(disableHooksPathCommand).toString());
}

// the first arg is 'node'
// the second arg is path to this script
// real args start in 3rd position
for (let argno = 2; argno < process.argv.length; argno++) {
    const arg = process.argv[argno];
    if (arg === '-enablegithooks') {
        enableGitHooks();
    }
    else if (arg === '-disablegithooks') {
        disableGitHooks();
    }
}
