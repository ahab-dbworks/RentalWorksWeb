//require('dotenv').config()
//import { ModuleBase } from '../shared/ModuleBase';
//import { RentalInventory } from './modules/RentalInventory';
//import { PurchaseOrder } from './modules/PurchaseOrder';

//const { createLogger, format, transports } = require('winston');
//const { combine, timestamp, label, printf } = format;
//const myFormat: any = printf(({ level, message, label, timestamp }) => {
//    return `${timestamp} ${level}: ${message}`;
//});
//const logger = createLogger({
//    format: combine(
//        timestamp(),
//        myFormat
//    ),
//    defaultMeta: { service: 'user-service' },
//    transports: [
//        new transports.Console(),
//        new transports.File({ filename: 'error.log', level: 'error' }),
//        new transports.File({ filename: 'combined.log', level: 'info' }),
//    ]
//});
//try {
//    if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';
//    if (process.env.RW_EMAIL === undefined) throw 'Please add a line to the .env file such as RW_EMAIL=\'TEST\'';

//    //globals
//    let testToken: string = "";
//    let continueTest: boolean = true;
//    let iCode1: string = "";
//    let iCode2: string = "";
//    let iCode3: string = "";
//    let poDescription: string = "";

//    if (process.env.TEST_TOKEN !== undefined) testToken = process.env.TEST_TOKEN;

//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    //login
//    describe('System Login', () => {
//        test('Login to RentalWorksWeb', async () => {
//            continueTest = await ModuleBase.authenticate()
//                .then((data) => { })
//                .catch(err => logger.error('authenticate: ', err));
//        }, 45000);
//    });
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    //rental inventory 1
//    if (continueTest) {
//        const rentalInventoryModule: RentalInventory = new RentalInventory();

//        describe('Create new Rental Inventory 1', () => {
//            test('Open Rental Inventory module', async () => {
//                await rentalInventoryModule.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
//                    .then()
//                    .catch(err => logger.error('openModule: ', err));
//            }, 10000);
//            test('Create new record', async () => {
//                await rentalInventoryModule.createNewRecord(1)
//                    .then()
//                    .catch(err => logger.error('createNewRecord: ', err));
//            }, 10000);
//            test('Fill in form data', async () => {
//                iCode1 = await rentalInventoryModule.populateNew(testToken)
//                    .then()
//                    .catch(err => logger.error('populateNew: ', err));
//            }, 10000);

//            test(`Save new record, ICode: ${iCode1}`, async () => {
//                await rentalInventoryModule.saveRecord()
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));
//            }, 10000);
//            test('Close Rental Inventory record', async () => {
//                await rentalInventoryModule.closeRecord()
//                    .then()
//                    .catch(err => logger.error('closeRecord: ', err));
//            }, 10000);
//        });
//    }
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    //rental inventory 2
//    if (continueTest) {
//        const rentalInventoryModule: RentalInventory = new RentalInventory();

//        describe('Create new Rental Inventory 2', () => {
//            test('Open Rental Inventory module', async () => {
//                await rentalInventoryModule.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
//                    .then()
//                    .catch(err => logger.error('openModule: ', err));
//            }, 10000);
//            test('Create new record', async () => {
//                await rentalInventoryModule.createNewRecord(1)
//                    .then()
//                    .catch(err => logger.error('createNewRecord: ', err));
//            }, 10000);
//            test('Fill in form data', async () => {
//                iCode2 = await rentalInventoryModule.populateNew(testToken)
//                    .then()
//                    .catch(err => logger.error('populateNew: ', err));
//            }, 10000);

//            test(`Save new record, ICode: ${iCode2}`, async () => {
//                await rentalInventoryModule.saveRecord()
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));
//            }, 10000);
//            test('Close Rental Inventory record', async () => {
//                await rentalInventoryModule.closeRecord()
//                    .then()
//                    .catch(err => logger.error('closeRecord: ', err));
//            }, 10000);
//        });
//    }
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    //rental inventory 3
//    if (continueTest) {
//        const rentalInventoryModule: RentalInventory = new RentalInventory();

//        describe('Create new Rental Inventory 3', () => {
//            test('Open Rental Inventory module', async () => {
//                await rentalInventoryModule.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
//                    .then()
//                    .catch(err => logger.error('openModule: ', err));
//            }, 10000);
//            test('Create new record', async () => {
//                await rentalInventoryModule.createNewRecord(1)
//                    .then()
//                    .catch(err => logger.error('createNewRecord: ', err));
//            }, 10000);
//            test('Fill in form data', async () => {
//                iCode3 = await rentalInventoryModule.populateNew(testToken)
//                    .then()
//                    .catch(err => logger.error('populateNew: ', err));
//            }, 10000);

//            test(`Save new record, ICode: ${iCode3}`, async () => {
//                await rentalInventoryModule.saveRecord()
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));
//            }, 10000);
//            test('Close Rental Inventory record', async () => {
//                await rentalInventoryModule.closeRecord()
//                    .then()
//                    .catch(err => logger.error('closeRecord: ', err));
//            }, 10000);
//        });
//    }
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    // purchase order
//    if (continueTest) {
//        const poModule: PurchaseOrder = new PurchaseOrder();
//        describe('Create new Purchase Order', () => {
//            test('Open Purchase Order Module', async () => {
//                await poModule.openModule(5000, 1000)
//                    .then()
//                    .catch(err => logger.error('openModule: ', err));
//            }, 10000);
//            test('Create new record', async () => {
//                await poModule.createNewRecord(1)
//                    .then()
//                    .catch(err => logger.error('createNewRecord: ', err));
//            }, 10000);
//            test('Fill in form data', async () => {
//                poDescription = await poModule.populateNew(testToken)
//                    .then()
//                    .catch(err => logger.error('populateNewPO: ', err))
//            }, 20000);
//            test(`Save new PO, Description: ${poDescription}`, async () => {
//                await poModule.saveRecord()
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));
//            }, 20000);
//            test('Add grid rows in rental tab', async () => {
//                await poModule.clickTab("Rental Inventory");

//                // row 1
//                const fieldObject1 = {
//                    InventoryId: iCode1,
//                    ManufacturerPartNumber: '111',
//                    QuantityOrdered: '10' // all values must be string type
//                }
//                // row 2
//                const fieldObject2 = {
//                    InventoryId: iCode2,
//                    ManufacturerPartNumber: '2222',
//                    QuantityOrdered: '20' // all values must be string type
//                }
//                // row 3
//                const fieldObject3 = {
//                    InventoryId: iCode3,
//                    ManufacturerPartNumber: '3333',
//                    QuantityOrdered: '30' // all values must be string type
//                }

//                await poModule.addGridRow('OrderItemGrid', 'R', null, fieldObject1)
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));

//                await poModule.addGridRow('OrderItemGrid', 'R', null, fieldObject2)
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));

//                await poModule.addGridRow('OrderItemGrid', 'R', null, fieldObject3)
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));


//            }, 200000);
//            test('Save PO again', async () => {
//                await poModule.saveRecord()
//                    .then()
//                    .catch(err => logger.error('saveRecord: ', err));
//            }, 10000);
//            test('Close PO Record', async () => {
//                await poModule.closeRecord()
//                    .then()
//                    .catch(err => logger.error('closeRecord: ', err));
//            }, 10000);
//        });
//    }
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//    //logoff
//    describe('System Logout', () => {
//        test('Logout of RentalWorksWeb', async () => {
//            continueTest = await ModuleBase.logoff()
//                .then((data) => { })
//                .catch(err => logger.error('logoff: ', err));
//        }, 45000);
//    });
//    //-----------------------------------------------------------------------------------------------------------------------------------------------
//} catch (ex) {
//    logger.error('Error in catch LoginCreateNewRentalInventory', ex);
//}