require('dotenv').config()
import { ModuleBase } from '../shared/ModuleBase';
import { Quote } from './modules/Quote';

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
    let quoteDescription: string = "";
    let dealName: string = 'DED RENTALS';
    //login
    test('Login', async () => {
        continueTest = await ModuleBase.authenticate()
            .then((data) => { })
            .catch(err => logger.error('authenticate: ', err));
    }, 45000);

    //Quote
    if (continueTest) {
        const quoteModule: Quote = new Quote();
        describe('Create new Quote, fill out form, save record', () => {
            test('Open QuoteModule and create', async () => {
                await quoteModule.openModule()
                    .then()
                    .catch(err => logger.error('openModule: ', err));
            }, 10000);
            test('Create New record', async () => {
                await quoteModule.createNewRecord(1)
                    .then()
                    .catch(err => logger.error('createNewRecord: ', err));
            }, 10000);
            test('Fill in Quote form data', async () => {
                quoteDescription = await quoteModule.populateNew(dealName)
                    .then()
                    .catch(err => logger.error('populateNewQuote: ', err))
            }, 10000);
            test('Save new Quote', async () => {
                await quoteModule.saveRecord()
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 10000);
            test('Add one grid row in rental tab', async () => {
                const fieldObject = {
                    InventoryId: '100006-C03' // I-Code
                }
                //await page.waitFor(4000); // wait for save action to complete
                await quoteModule.clickTab("Rental");
                await quoteModule.addGridRow('OrderItemGrid', 'R', null, fieldObject)
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 200000);
            test('Save new Quote', async () => {
                await quoteModule.saveRecord()
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 10000);
            // test('Close Record', async () => {
            //     await quoteModule.closeRecord()
            //         .then()
            //         .catch(err => logger.error('closeRecord: ', err));
            // }, 10000);
        });
    }
    //logoff
    // test('Logoff', async () => {
    //     continueTest = await ModuleBase.logoff()
    //         .then((data) => { })
    //         .catch(err => logger.error('logoff: ', err));
    // }, 45000);

} catch (ex) {
    logger.error('Error in catch AddGrid', ex);
}