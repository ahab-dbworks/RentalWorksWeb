require('dotenv').config()
import { ModuleBase } from '../shared/ModuleBase';
import { Contact } from './modules/Contact';

const { createLogger, format, transports } = require('winston');
const { combine, timestamp, label, printf } = format;
const myFormat: any = printf(({ level, message, label, timestamp }) => {
    return `${timestamp} ${level}: ${message}`;
});
const logger = createLogger({
    format: combine(
        timestamp(),
        myFormat
    ),
    defaultMeta: { service: 'user-service' },
    transports: [
        new transports.Console(),
        new transports.File({ filename: 'error.log', level: 'error' }),
        new transports.File({ filename: 'combined.log', level: 'info' }),
    ]
});
try {
    if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
    if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';

    //globals
    let continueTest: boolean = true;

    //login
    test('Login', async () => {
        continueTest = await ModuleBase.authenticate()
            .then((data) => { })
            .catch(err => logger.error('authenticate: ', err));
    }, 45000);
    //ModuleBase.emailResults()
    //contact
    if (continueTest) {
        const module: Contact = new Contact();
        describe('Create new Contact, fill out form, save record', () => {
            if (continueTest) {
                test('Open module and create', async () => {
                    await module.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await module.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Contact form data', async () => {
                    await module.populateNewContact()
                        .then()
                        .catch(err => logger.error('populateNewContact: ', err))
                }, 10000);
                test('Save new Contact', async () => {
                    await module.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await module.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            }
        });

        describe('Create new Contact, fill out form without a required field and attempt to save record', () => {
            if (continueTest) {
                test('Open module and create', async () => {
                    await module.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await module.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Contact form data without Email (set as a required field)', async () => {
                    await module.populateNewContactWithoutEmail()
                        .then()
                        .catch(err => logger.error('populateNewContactWithoutEmail: ', err))
                }, 10000);
                test('Save new Contact (expect error)', async () => {
                      continueTest = await module.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                        expect(continueTest).toBe(false);
                }, 10000);
                test('Detect error class on form field', async () => {
                    await page.waitForSelector('.fwformfield.error')
                        .then(() => {
                            logger.info(`Error class detected.`);
                        })
                        .catch(err => logger.error('detectErrorOnField: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await module.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                    continueTest = true;
                }, 10000);
                test('Detect "Close Tab" notification', async () => {
                    await page.waitForSelector('.advisory .fwconfirmation-button')
                        .then(() => {
                            logger.info(`"Close Tab" notification detected.`);
                        })
                        .catch(err => logger.error('detectCloseTabNotification: ', err));
                }, 10000);
                test('Close record without saving', async () => {
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[1].click() // clicks "Don't Save" option
                        .then(() => {
                            logger.info(`Closed tab without saving`);
                        })
                        .catch(err => logger.error('closeWithoutSaving: ', err));
                }, 10000);
            }
        });

        describe('Create new Contact with duplicate email', () => {
            if (continueTest) {
                let email;
                test('Open module and create', async () => {
                    await module.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await module.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Contact form data (return email value)', async () => {
                    await module.populateNewContactDuplicateEmail()
                        .then((data) => {
                            email = data;
                        })
                        .catch(err => logger.error('populateNewContact: ', err))
                }, 10000);
                test('Save new Contact', async () => {
                    continueTest = await module.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await module.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
                test('Open module and create', async () => {
                    await module.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await module.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Contact form data with duplicate email', async () => {
                    await module.populateNewContact(email)
                        .then()
                        .catch(err => logger.error('populateNewContact: ', err))
                }, 10000);
                test('Save new Contact (expect error)', async () => {
                    continueTest = await module.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                    expect(continueTest).toBe(false);
                }, 10000);
                test('Detect error message notification popup', async () => {
                    await page.waitForSelector('.advisory .message')
                        .then()
                        .catch(err => logger.error('detectPopup: ', err));
                    const popupText = await page.$eval('.advisory', el => el.textContent);
                    expect(popupText).toContain('Duplicate Rule');
                }, 10000);
                test('Close notification', async () => {
                    await page.click(`.fwconfirmation-button`)
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                    await page.waitFor(() => !document.querySelector('.advisory'));
                }, 10000);
                test('Close Record', async () => {
                    await module.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            }
        });
    }

    //logoff
    test('Logoff', async () => {
        continueTest = await ModuleBase.logoff()
            .then((data) => { })
            .catch(err => logger.error('logoff: ', err));
    }, 45000);

} catch (ex) {
    logger.error('Error in catch LoginCreateNewContact', ex);
}