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
        console.log('- Build RentalWorks -');
        const pathRentalWorks = path.resolve(__dirname, '..');
        const pathWebApi = path.resolve(__dirname, '..', '..', 'RentalWorksWebApi');
        //console.log(pathRentalWorks);
        //console.log(pathWebApi);

        process.chdir(pathWebApi);
        childProcess.execSync('npm run rentalworks-3-build-app', { stdio: 'inherit' });

        console.log('- Finished Build RentalWorks -');
    } catch (ex) {
        failIf(true, ex);
    }
})();