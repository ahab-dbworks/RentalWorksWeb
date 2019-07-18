require('dotenv').config()
import { ModuleBase } from '../shared/ModuleBase';
import { RentalInventory } from './modules/RentalInventory';
import { PurchaseOrder } from './modules/PurchaseOrder';

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
    let iCode: string = "";
    let poDescription: string = "";

    //login
    test('Login', async () => {
        continueTest = await ModuleBase.authenticate()
            .then((data) => { })
            .catch(err => logger.error('authenticate: ', err));
    }, 45000);

    //rental inventory
    if (continueTest) {
        const rentalInventoryModule: RentalInventory = new RentalInventory();

        describe('Create new Rental Inventory, fill out form, save record', () => {
            test('Open module', async () => {
                await rentalInventoryModule.openModule(5000, 1000) // wait 1 second after the module is opened to allow control query to load system defaults
                    .then()
                    .catch(err => logger.error('openModule: ', err));
            }, 10000);
            test('Create New record', async () => {
                await rentalInventoryModule.createNewRecord(1)
                    .then()
                    .catch(err => logger.error('createNewRecord: ', err));
            }, 10000);
            test('Fill in form data', async () => {
                iCode = await rentalInventoryModule.populateNew()
                    .then()
                    .catch(err => logger.error('populateNew: ', err))
            }, 10000);
            test('Save new', async () => {
                await rentalInventoryModule.saveRecord()
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 10000);
            test('Close', async () => {
                await rentalInventoryModule.closeRecord()
                    .then()
                    .catch(err => logger.error('closeRecord: ', err));
            }, 10000);
        });
    }

    // purchase order
    if (continueTest) {
        const poModule: PurchaseOrder = new PurchaseOrder();
        describe('Create new Purchase Order, fill out form, save record', () => {
            test('Open poModule and create', async () => {
                await poModule.openModule(5000, 1000)
                    .then()
                    .catch(err => logger.error('openModule: ', err));
            }, 10000);
            test('Create New record', async () => {
                await poModule.createNewRecord(1)
                    .then()
                    .catch(err => logger.error('createNewRecord: ', err));
            }, 10000);
            test('Fill in PO form data', async () => {
                poDescription = await poModule.populateNew()
                    .then()
                    .catch(err => logger.error('populateNewPO: ', err))
            }, 20000);
            test('Save new PO', async () => {
                await poModule.saveRecord()
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 20000);
            test('Add one grid row in rental tab', async () => {
                const fieldObject = {
                    InventoryId: iCode,
                    //QuantityOrdered: 20
                }
                await poModule.clickTab("Rental");
                await poModule.addGridRow('OrderItemGrid', 'R', null, fieldObject)
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 200000);
            test('Save new PO', async () => {
                await poModule.saveRecord()
                    .then()
                    .catch(err => logger.error('saveRecord: ', err));
            }, 10000);
            test('Close Record', async () => {
                await poModule.closeRecord()
                    .then()
                    .catch(err => logger.error('closeRecord: ', err));
            }, 10000);
        });
    }


    //logoff
    test('Logoff', async () => {
        continueTest = await ModuleBase.logoff()
            .then((data) => { })
            .catch(err => logger.error('logoff: ', err));
    }, 45000);


} catch (ex) {
    logger.error('Error in catch LoginCreateNewRentalInventory', ex);
}