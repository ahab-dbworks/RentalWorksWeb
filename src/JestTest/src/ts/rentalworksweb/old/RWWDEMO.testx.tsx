require('dotenv').config()
import { ModuleBase } from '../shared/ModuleBase';
import { Customer } from './modules/Customer';
import { Deal } from './modules/Deal';
import { Quote } from './modules/Quote';
import { Vendor } from './modules/Vendor';
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
    let customerName: string = "";
    let dealName: string = "";
    let quoteDescription: string = "";
    let testToken: string = "";

    if (process.env.TEST_TOKEN !== undefined) testToken = process.env.TEST_TOKEN;

    //login
    test('Login', async () => {
        continueTest = await ModuleBase.authenticate()
            .then((data) => { })
            .catch(err => logger.error('authenticate: ', err));
    }, 45000);
    for (let i = 0; i < 4; i++) {
        //customer
        if (continueTest) {
            const customerModule: Customer = new Customer();
            describe('Create new Customer, fill out form, save record', () => {
                test('Open customerModule and create', async () => {
                    await customerModule.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await customerModule.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Customer form data', async () => {
                    customerName = await customerModule.populateNew()
                        .then()
                        .catch(err => logger.error('populateNewCustomer: ', err))
                }, 10000);
                test('Save new Customer', async () => {
                    await customerModule.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await customerModule.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            });
        }
        //deal
        if (continueTest) {
            const dealModule: Deal = new Deal();
            describe('Create new Deal, fill out form, save record', () => {
                test('Open dealModule and create', async () => {
                    await dealModule.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await dealModule.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Deal form data', async () => {
                    dealName = await dealModule.populateNew(customerName)
                        .then()
                        .catch(err => logger.error('populateNewDeal: ', err))
                }, 10000);
                test('Save new Deal', async () => {
                    await dealModule.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await dealModule.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            });
        }
        //quote
        if (continueTest) {
            const quoteModule: Quote = new Quote();
            describe('Create new Quote, fill out form, save record', () => {
                test('Open quoteModule and create', async () => {
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
                test('Close Record', async () => {
                    await quoteModule.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            });
        }
        // Vendor
        if (continueTest) {
            describe('Create new Vendor, fill out form, save record', () => {
                const vendorModule: Vendor = new Vendor();
                test('Open module and create', async () => {
                    await vendorModule.openModule()
                        .then()
                        .catch(err => logger.error('openModule: ', err));
                }, 10000);
                test('Create New record', async () => {
                    await vendorModule.createNewRecord(1)
                        .then()
                        .catch(err => logger.error('createNewRecord: ', err));
                }, 10000);
                test('Fill in Vendor form data', async () => {
                    await vendorModule.populateNewVendor()
                        .then()
                        .catch(err => logger.error('populateNewVendor: ', err))
                }, 10000);
                test('Save new Vendor', async () => {
                    await vendorModule.saveRecord()
                        .then()
                        .catch(err => logger.error('saveRecord: ', err));
                }, 10000);
                test('Close Record', async () => {
                    await vendorModule.closeRecord()
                        .then()
                        .catch(err => logger.error('closeRecord: ', err));
                }, 10000);
            });
        }
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
        //sales inventory
        if (continueTest) {
            const module: SalesInventory = new SalesInventory();
            describe('Create new Sales Inventory, fill out form, save record', () => {
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
                    await module.populateNew()
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
        //Parts inventory
        if (continueTest) {
            const module: PartsInventory = new PartsInventory();
            describe('Create new Parts Inventory, fill out form, save record', () => {
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
                    await module.populateNew()
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
    }
    //logoff
    test('Logoff', async () => {
        continueTest = await ModuleBase.logoff()
            .then((data) => { })
            .catch(err => logger.error('logoff: ', err));
    }, 45000);

} catch (ex) {
    logger.error('Error in catch DEMO', ex);
}