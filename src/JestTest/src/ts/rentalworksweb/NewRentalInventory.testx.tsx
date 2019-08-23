require('dotenv').config()
import { ModuleBase } from '../shared/ModuleBase';
import { RentalInventory } from './modules/RentalInventory';
import { SalesInventory } from './modules/SalesInventory';
import { PartsInventory } from './modules/PartsInventory';

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
    let testToken: string = "";

    if (process.env.TEST_TOKEN !== undefined) testToken = process.env.TEST_TOKEN;

    //login
    test('Login', async () => {
        continueTest = await ModuleBase.authenticate()
            .then((data) => { })
            .catch(err => logger.error('authenticate: ', err));
    }, 45000);
    //for (let i = 0; i < 5; i++) {

        //rental inventory
        if (continueTest) {
            const module: RentalInventory = new RentalInventory();

            describe('Create new Rental Inventory, fill out form, save record', () => {
                test('Open module', async () => {
                    await module.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await module.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in form data', async () => {
                    await module.populateNew(testToken)
                        .then()
                        .catch(err => logger.error('populateNew: ', err))
                }, 10000);
                test('Save new', async () => {
                    await module.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close', async () => {
                    await module.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            });
        }
    //    //sales inventory
    //    if (continueTest) {
    //        const module: SalesInventory = new SalesInventory();
    //        describe('Create new Sales Inventory, fill out form, save record', () => {
    //            test('Open module', async () => {
    //                await module.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
    //                    .then()
    //                    .catch(err => logger.error('openModule: ', err));
    //            }, 10000);
    //            test('Create New record', async () => {
    //                await module.createNewRecord(1)
    //                    .then()
    //                    .catch(err => logger.error('createNewRecord: ', err));
    //            }, 10000);
    //            test('Fill in form data', async () => {
    //                await module.populateNew()
    //                    .then()
    //                    .catch(err => logger.error('populateNew: ', err))
    //            }, 10000);
    //            test('Save new', async () => {
    //                await module.saveRecord()
    //                    .then()
    //                    .catch(err => logger.error('saveRecord: ', err));
    //            }, 10000);
    //            test('Close', async () => {
    //                await module.closeRecord()
    //                    .then()
    //                    .catch(err => logger.error('closeRecord: ', err));
    //            }, 10000);
    //        });
    //    }
    //    //Parts inventory
    //    if (continueTest) {
    //        const module: PartsInventory = new PartsInventory();
    //        describe('Create new Parts Inventory, fill out form, save record', () => {
    //            test('Open module', async () => {
    //                await module.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
    //                    .then()
    //                    .catch(err => logger.error('openModule: ', err));
    //            }, 10000);
    //            test('Create New record', async () => {
    //                await module.createNewRecord(1)
    //                    .then()
    //                    .catch(err => logger.error('createNewRecord: ', err));
    //            }, 10000);
    //            test('Fill in form data', async () => {
    //                await module.populateNew()
    //                    .then()
    //                    .catch(err => logger.error('populateNew: ', err))
    //            }, 10000);
    //            test('Save new', async () => {
    //                await module.saveRecord()
    //                    .then()
    //                    .catch(err => logger.error('saveRecord: ', err));
    //            }, 10000);
    //            test('Close', async () => {
    //                await module.closeRecord()
    //                    .then()
    //                    .catch(err => logger.error('closeRecord: ', err));
    //            }, 10000);
    //        });
    //    }
    //}

    //logoff
    test('Logoff', async () => {
        continueTest = await ModuleBase.logoff()
            .then((data) => { })
            .catch(err => logger.error('logoff: ', err));
    }, 45000);


} catch (ex) {
    logger.error('Error in catch LoginCreateNewRentalInventory', ex);
}