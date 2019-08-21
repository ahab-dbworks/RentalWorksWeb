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
                    await module.populateNew()
                        .then()
                        .catch(err => logger.error('populateNew: ', err))
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