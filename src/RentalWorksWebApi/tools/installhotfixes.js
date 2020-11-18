//const prompts = require('prompts'); // https://www.npmjs.com/package/prompts#-types
//const fse = require('fs-extra'); // https://www.npmjs.com/package/fs-extra
const fs = require('fs');
const path = require('path'); // https://nodejs.org/api/path.html
const childProcess = require('child_process'); // https://nodejs.org/api/child_process.html

function failIf(expression, reason) {
    if (expression) {
        console.error(reason);
        console.error('Script failed!');
        process.exit();
    }
}

process.on('unhandledRejection', (reason) => {
    failIf(true, reason);
});

// closure for asynchronous code
(async () => {
    try {
        const appSettingsPath = path.resolve(__dirname, '..', 'appsettings.json');
        const appSettingsJsonWithComments = fs.readFileSync(appSettingsPath, 'utf8');
        const appSettingsJson = appSettingsJsonWithComments.replace(/\\"|"(?:\\"|[^"])*"|(\/\/.*|\/\*[\s\S]*?\*\/)/g, (m, g) => g ? "" : m).trim();
        const appSettings = JSON.parse(appSettingsJson);
        const connectionStringParts = appSettings.ApplicationConfig.DatabaseSettings.ConnectionString.split(';');
        let databaseSettings = {};
        for (let i = 0; i < connectionStringParts.length; i++) {
            const connectionStringPart = connectionStringParts[i].trim();
            if (connectionStringParts.length > 0) {
                const fieldParts = connectionStringParts[i].split('=');
                databaseSettings[fieldParts[0].toLowerCase()] = fieldParts[1];
            }
        }
        if (typeof databaseSettings.server === 'string' && typeof databaseSettings.database === 'string') {
            // install hotfixes based on WebApi appsettings.json login info
            //const cmdInstallHotfixes = `osql -S "${databaseSettings.server}" -d "${databaseSettings.database}" -U "${databaseSettings['user id']}" -P "${databaseSettings.password}" -Q "exec fw_installhotfixes 'O'"`;
            const cmdInstallHotfixes = `osql -S "${databaseSettings.server}" -d "${databaseSettings.database}" -U "dbworks" -P "db2424" -Q "exec fw_installhotfixes 'O'"`;
            console.log(cmdInstallHotfixes);
            childProcess.execSync(cmdInstallHotfixes, { stdio: 'inherit' });
        } else {
            throw new Error('Install Hotfixes scripts requires a Server and Database property be defined in the ConnectionString');
        }
    } catch (ex) {
        failIf(true, ex);
    }
}) ();