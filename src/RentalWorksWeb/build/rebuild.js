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

process.on('unhandledRejection', (reason) => {
    failIf(true, reason);
});

// closure for asynchronous code
(async () => {
    try {
        console.log('//------------------------------------------------------------------------------------');
        console.log('- Rebuild RentalWorks -');
        const pathRentalWorks = path.resolve(__dirname, '..');
        const pathWebApi = path.resolve(__dirname, '..', '..', 'RentalWorksWebApi');
        //console.log(pathRentalWorks);
        //console.log(pathWebApi);

        process.chdir(pathRentalWorks);
        childProcess.execSync('npm run clean', { stdio: 'inherit' });

        process.chdir(pathWebApi);
        childProcess.execSync('npm run rentalworks-1-restore-packages', { stdio: 'inherit' });
        childProcess.execSync('npm run rentalworks-2-rebuild-typescript', { stdio: 'inherit' });
        childProcess.execSync('npm run rentalworks-3-build-app', { stdio: 'inherit' });

        console.log('- Finished Rebuild RentalWorks -');
    } catch (ex) {
        failIf(true, ex);
    }
}) ();