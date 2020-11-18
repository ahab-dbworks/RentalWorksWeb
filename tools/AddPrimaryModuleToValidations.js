//const childProcess = require('child_process'); // https://nodejs.org/api/child_process.html
//const prompts = require('prompts'); // https://www.npmjs.com/package/prompts#-types
const fse = require('fs-extra'); // https://www.npmjs.com/package/fs-extra
const path = require('path'); // https://nodejs.org/api/path.html

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

const updateSourceCode = async (dirPath) => {
    const errors = [];
    console.log(dirPath);
    let validationDirectories = fse
        .readdirSync(dirPath)
        .filter(
            item =>
                fse.statSync(path.join(dirPath, item)).isDirectory()
        )
        .sort();
    for (let i = 0; i < validationDirectories.length; i++) {
        const pathValidation = path.join(dirPath, validationDirectories[i]);
        let validationTemplates = fse
            .readdirSync(pathValidation)
            .filter(
                item =>
                    fse.statSync(path.join(pathValidation, item)).isFile() && item.endsWith('.htm')
            )
            .sort();
        for (let i = 0; i < validationTemplates.length; i++) {
            const pathValidationTemplate = path.join(pathValidation, validationTemplates[i]);
            //console.log(pathValidationTemplate);
            let validationTemplateFile = fse.readFileSync(pathValidationTemplate, 'utf8');
            const matchesName = /data-name="(\w+)"/.exec(validationTemplateFile);
            const validationName = matchesName[1].replace('Validation', '');
            
            // remove any data-primarymoduletags
            //validationTemplateFile = validationTemplateFile.replace(/data-primarymodule="(\w+)"\s/g, '')
            
            // add a data-primarymodule tag to a validation if it doesn't have one
            if (!/data-primarymodule="(\w+)"\s/.test(validationTemplateFile)) {
                validationTemplateFile = validationTemplateFile.replace(/data-name="(\w+)"/, `${matchesName[0]} data-primarymodule="${validationName}"`)
                fse.writeFileSync(pathValidationTemplate, validationTemplateFile);
                //console.log(validationTemplateFile);
            }
        }
    }
}

// closure for asynchronous code
(async () => {
    try {
        const pathRentalWorksValidations = path.resolve(process.cwd(), 'src', 'RentalWorksWeb', 'Source', 'Validations');
        await updateSourceCode(pathRentalWorksValidations);

        const pathTrakitWorksValidations = path.resolve(process.cwd(), 'src', 'RentalWorksWebApi', 'TrakitWorks', 'Source', 'Validations');
        await updateSourceCode(pathTrakitWorksValidations);
    } catch (ex) {
        failIf(true, ex);
    }
}) ();