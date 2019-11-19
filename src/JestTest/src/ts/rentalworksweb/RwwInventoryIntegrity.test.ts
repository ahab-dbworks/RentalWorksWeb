import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse, NewRecordToCreate } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import {
    RentalInventory, PurchaseOrder, User, DefaultSettings, InventorySettings, Warehouse, Quote, Order,
    RepairOrder, Staging, ReceiveFromVendor, TransferOrder, AssignBarCodes, Asset
} from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

//---------------------------------------------------------------------------------------
export class AvailabilityDate {
    theDate: Date;
    availableQuantity: number;
    constructor(d: Date, qty: number) {
        this.theDate = d;
        this.availableQuantity = qty;
    }
}
//---------------------------------------------------------------------------------------
export class InventoryData {
    qtyTotal: number;
    qtyIn: number;
    qtyQcRequired: number;
    qtyInContainer: number;
    qtyStaged: number;
    qtyOut: number;
    qtyInRepair: number;
    qtyInTransit: number;
    dates: AvailabilityDate[];
    constructor() {
        this.dates = new Array();
    }
    populateAvailDates(availData: string[]) {
        this.dates = new Array();
        for (let d: number = 0; d < availData.length; d++) {
            this.dates.push(new AvailabilityDate(TestUtils.futureDate(d), +availData[d]));
        }
    }
}
//---------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------
export class InventoryIntegrityTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async RelogAsCopyOfUser() {
        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();
    }
    //---------------------------------------------------------------------------------------
    async openModuleRecord(module: ModuleBase, seekObject: any, registerWithRecordKey?: string) {
        await module.openBrowse();
        await module.browseSeek(seekObject);
        await module.openFirstRecordIfAny(true, registerWithRecordKey);
        await module.closeRecord();
    }
    //---------------------------------------------------------------------------------------
    async createModuleRecord(module: ModuleBase, record: any, registerWithRecordKey?: string, doBeforeClosing?: () => void) {
        await module.openBrowse();
        await module.createNewRecord();
        await module.populateFormWithRecord(record.record);
        await module.saveRecord(true);

        if (record.gridRecords) {
            if (module.grids) {
                for (let grid of module.grids) {
                    if (grid.canNew) {
                        for (let gridRecord of record.gridRecords) {
                            if (gridRecord.grid === grid) {
                                await grid.addGridRow(gridRecord.recordToCreate.record, true);
                            }
                        }
                    }
                }
            }
        }

        if (typeof doBeforeClosing === 'function') {
            await doBeforeClosing();
        }

        if (registerWithRecordKey) {
            await module.closeRecord();
            await module.browseSeek(record.seekObject);
            await module.openFirstRecordIfAny(true, registerWithRecordKey);
            await module.closeRecord();
        }

    }
    //---------------------------------------------------------------------------------------
    async getInventoryData(rentalInventoryModule: RentalInventory, record: any): Promise<InventoryData> {
        let invData: InventoryData = new InventoryData();

        await rentalInventoryModule.openBrowse();
        await rentalInventoryModule.browseSeek(record.seekObject);
        await rentalInventoryModule.openFirstRecordIfAny();

        let availabilityTabSelector = `div[data-type="tab"]#availabilitycalendartab1 .caption`;
        await page.waitForSelector(availabilityTabSelector);
        await page.click(availabilityTabSelector);
        await TestUtils.waitForPleaseWait();

        async function getQty(qtyType: string): Promise<number> {
            let qty: number = 0;
            let qtyFieldSelector = `div .fwformfield.totals[data-totalfield="${qtyType}"] input`;
            let qtyString = await page.$eval(qtyFieldSelector, (e: any) => { return e.value })
            qty = +qtyString;
            return qty;
        }
        invData.qtyTotal = await getQty("Total");
        invData.qtyIn = await getQty("In");
        invData.qtyQcRequired = await getQty("QcRequired");
        invData.qtyInContainer = await getQty("InContainer");
        invData.qtyStaged = await getQty("Staged");
        invData.qtyOut = await getQty("Out");
        invData.qtyInRepair = await getQty("InRepair");
        invData.qtyInTransit = await getQty("InTransit");

        let availDaysSelector = `div[data-control="FwSchedulerDetailed"] .scheduler_default_matrix .scheduler_default_event_line0`;
        for (let d = 1; d <= 25; d++) {
            let theDate: Date = TestUtils.futureDate(d - 1);
            let availDaySelector = availDaysSelector + `:nth-child(${d})`;
            let availQty = await page.$eval(availDaySelector, el => el.textContent);
            Logging.logInfo(`day=${d}, availQty=${availQty}`);
            invData.dates.push(new AvailabilityDate(theDate, +availQty));
        }

        return invData;
    }
    //---------------------------------------------------------------------------------------
    async TestInventoryIntegrity(rentalInventoryModule: RentalInventory, record: any, expectedInvData: InventoryData) {
        await this.getInventoryData(rentalInventoryModule, record)
            .then(actualInvData => {

                let expectedStr: string = "";
                let actualStr: string = "";

                actualStr = `Total Qty: ${actualInvData.qtyTotal.toString()}`;
                expectedStr = `Total Qty: ${expectedInvData.qtyTotal.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Qty: ${actualInvData.qtyIn.toString()}`;
                expectedStr = `In Qty: ${expectedInvData.qtyIn.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `QC Required Qty: ${actualInvData.qtyQcRequired.toString()}`;
                expectedStr = `QC Required Qty: ${expectedInvData.qtyQcRequired.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Container Qty: ${actualInvData.qtyInContainer.toString()}`;
                expectedStr = `In Container Qty: ${expectedInvData.qtyInContainer.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `Staged Qty: ${actualInvData.qtyStaged.toString()}`;
                expectedStr = `Staged Qty: ${expectedInvData.qtyStaged.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `Out Qty: ${actualInvData.qtyOut.toString()}`;
                expectedStr = `Out Qty: ${expectedInvData.qtyOut.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Repair Qty: ${actualInvData.qtyInRepair.toString()}`;
                expectedStr = `In Repair Qty: ${expectedInvData.qtyInRepair.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Transit Qty: ${actualInvData.qtyInTransit.toString()}`;
                expectedStr = `In Transit Qty: ${expectedInvData.qtyInTransit.toString()}`;
                expect(actualStr).toBe(expectedStr);

                Logging.logInfo(`about to check availability array length, ${actualInvData.dates.length.toString()} vs ${expectedInvData.dates.length.toString()}`);
                expect(actualInvData.dates.length).toBe(expectedInvData.dates.length);
                for (let q = 0; q < actualInvData.dates.length; q++) {
                    Logging.logInfo(`about to check availability quantity, ${actualInvData.dates[q].availableQuantity.toString()} vs ${expectedInvData.dates[q].availableQuantity.toString()}`);

                    actualStr = `Available ${TestUtils.dateMDY(actualInvData.dates[q].theDate)}: ${actualInvData.dates[q].availableQuantity.toString()}`;
                    expectedStr = `Available ${TestUtils.dateMDY(expectedInvData.dates[q].theDate)}: ${expectedInvData.dates[q].availableQuantity.toString()}`;
                    expect(actualStr).toBe(expectedStr);

                }
            });
    }
    //---------------------------------------------------------------------------------------
    //async clickModuleFunctionWithConfirm(moduleMenuSelector: string, functionMenuSelector: string) {
    //    await page.waitForSelector(moduleMenuSelector);
    //    await page.click(moduleMenuSelector);
    //    await page.waitForSelector(functionMenuSelector);
    //    await page.click(functionMenuSelector);
    //    await page.waitForSelector('.advisory');
    //    const options = await page.$$('.advisory .fwconfirmation-button');
    //    await options[0].click();
    //    await TestUtils.waitForPleaseWait();
    //    try {
    //        let toasterCloseSelector = `.advisory .messageclose`;
    //        await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
    //        await page.click(toasterCloseSelector);
    //        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
    //    } catch (error) { } // assume that we missed the toaster
    //}
    ////---------------------------------------------------------------------------------------
    async PerformTests() {
        this.testTimeout = 600000;
        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new Warehouse(), { Warehouse: "GlobalScope.User~ME.Warehouse" }, true, "MINE");

        let qtyToPurchase: number = 20;
        let qtyToReserve: number = 3;
        let qtyToPutOnActiveQuote: number = 50;
        let qtyToPutOnProspectQuote: number = 100;
        let qtyToPutOnCancelledQuote: number = 80;
        let qtyToOrder1: number = 6;
        let qtyToCheckOut1: number = 4;
        let qtyToStage1: number = 1;
        let qtyToOrder2: number = 6;
        let qtyToOrder3: number = 10;
        let qtyToRepair: number = 2;
        let qtyToTransfer1: number = 4;

        const quantityRentalInventoryKey: string = "QUANTITYRENTALINVENTORY";
        const barCodeRentalInventoryKey: string = "BARCODERENTALINVENTORY";
        const purchaseOrderKey: string = "PURCHASE_ORDER";
        const reservedQuoteKey: string = "RESERVEDQUOTE";
        const activeQuoteKey: string = "ACTIVEQUOTE";
        const prospectQuoteKey: string = "PROSPECTQUOTE";
        const cancelledQuoteKey: string = "CANCELLEDQUOTE";
        const orderKey1: string = "ORDER1";
        const orderKey2: string = "ORDER2";
        const orderKey3: string = "ORDER3";
        const repairOrderKey1: string = "REPAIRORDER1";
        const confirmedTransferOrderKey: string = "CONFIRMEDTRANSFER";
        const barCodeKey: string = "BARCODE";

        //---------------------------------------------------------------------------------------
        let rentalInventoryModule: RentalInventory = new RentalInventory();
        rentalInventoryModule.newRecordsToCreate = [
            {
                record: {
                    ICode: TestUtils.randomAlphanumeric(6),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken Q`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    TrackedBy: "QUANTITY",
                    IsFixedAsset: false,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken Q",
                },
            },
            {
                record: {
                    ICode: TestUtils.randomAlphanumeric(6),
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken BC`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    TrackedBy: "BARCODE",
                    IsFixedAsset: false,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken BC",
                },
            },
        ];
        //---------------------------------------------------------------------------------------
        let assetModule: Asset = new Asset();
        //---------------------------------------------------------------------------------------
        let poModule: PurchaseOrder = new PurchaseOrder();
        poModule.newRecordsToCreate = [
            {
                record: {
                    VendorId: 2,
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Parts: true,
                    Miscellaneous: true,
                    Labor: true,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
        poModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: poModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
            {
                grid: poModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
        ];
        //---------------------------------------------------------------------------------------
        let quoteModule: Quote = new Quote();
        quoteModule.newRecordsToCreate = [
            {
                // reserved quote
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken 1`,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 1",
                },
            },
            {
                // active quote (unreserved)
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken 2`,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 2",
                },
            },
            {
                // prospect quote (unreserved, no deal)
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken 3`,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 3",
                },
            },
            {
                // cancel quote 
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken 4`,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(10),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(3),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 4",
                },
            },
        ];
        //reserved
        quoteModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToReserve.toString(),
                    }
                }
            },
        ];
        //active
        quoteModule.newRecordsToCreate[1].gridRecords = [
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPutOnActiveQuote.toString(),
                    }
                }
            },
        ];
        //prospect
        quoteModule.newRecordsToCreate[2].gridRecords = [
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPutOnProspectQuote.toString(),
                    }
                }
            },
        ];
        //cancel
        quoteModule.newRecordsToCreate[3].gridRecords = [
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPutOnCancelledQuote.toString(),
                    }
                }
            },
        ];
        //---------------------------------------------------------------------------------------
        let orderModule: Order = new Order();
        orderModule.newRecordsToCreate = [
            {
                //order 1
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 1`,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(9),
                    EstimatedStartDate: TestUtils.futureDateMDY(4),
                    PickDate: TestUtils.futureDateMDY(1),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 1",
                },
            },
            {
                //order 2
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 2`,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(13),
                    EstimatedStartDate: TestUtils.futureDateMDY(4),
                    PickDate: TestUtils.futureDateMDY(1),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 2",
                },
            },
            {
                //order 3
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 3`,
                    DealId: 1,
                    Location: TestUtils.randomStreetName(),
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: true,
                    Miscellaneous: true,
                    Labor: true,
                    EstimatedStopDate: TestUtils.futureDateMDY(6),
                    EstimatedStartDate: TestUtils.futureDateMDY(5),
                    PickDate: TestUtils.futureDateMDY(4),
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken 3",
                },
            },
        ];
        //order 1
        orderModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToOrder1.toString(),
                    }
                }
            },
        ];
        //order 2
        orderModule.newRecordsToCreate[1].gridRecords = [
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToOrder2.toString(),
                    }
                }
            },
        ];
        //order 3
        orderModule.newRecordsToCreate[2].gridRecords = [
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToOrder3.toString(),
                    }
                }
            },
        ];
        //---------------------------------------------------------------------------------------
        let repairModule: RepairOrder = new RepairOrder();
        repairModule.newRecordsToCreate = [
            {
                record: {
                    ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                    Quantity: qtyToRepair.toString(),
                    DueDate: TestUtils.futureDateMDY(8),
                },
                seekObject: {
                    ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                    Quantity: qtyToRepair.toString(),
                },
            }
        ];
        //---------------------------------------------------------------------------------------
        let transferModule: TransferOrder = new TransferOrder();
        transferModule.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 1`,
                    FromWarehouseCode: "GlobalScope.Warehouse~MINE.WarehouseCode",
                    ToWarehouseId: 2,
                    RequiredDate: TestUtils.futureDateMDY(20),
                    ShipDate: TestUtils.futureDateMDY(12),
                    PickDate: TestUtils.futureDateMDY(10),
                    Rental: true,
                    Sales: false,
                },
                seekObject: {
                    Description: `GlobalScope.TestToken~1.TestToken 1`,
                },
            }
        ];
        transferModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToTransfer1.toString(),
                    },
                },
            },
        ];

        //---------------------------------------------------------------------------------------
        let stagingModule: Staging = new Staging();
        let receiveFromVendorModule: ReceiveFromVendor = new ReceiveFromVendor();
        let assignBarCodesModule: AssignBarCodes = new AssignBarCodes();
        //---------------------------------------------------------------------------------------
        describe("Rental Inventory Integrity", () => {
            let testName: string = "";
            //---------------------------------------------------------------------------------------
            testName = "Create new Rental Inventory";
            test(testName, async () => {
                let module: ModuleBase = rentalInventoryModule;
                let recordQ: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, recordQ, quantityRentalInventoryKey);

                let recordBc: NewRecordToCreate = module.newRecordsToCreate[1];
                await this.createModuleRecord(module, recordBc, barCodeRentalInventoryKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity rental inventory
                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates(["  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0",]);
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);

                //barcode rental inventory
                record = rentalInventoryModule.newRecordsToCreate[1];
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates(["  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0", "  0",]);
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Purchase Order";
            test(testName, async () => {
                let module: ModuleBase = poModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, purchaseOrderKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and Reserve it";
            test(testName, async () => {
                let module: ModuleBase = quoteModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];

                //reserve this quote
                async function reserveQuote() {
                    let moduleMenuSelector: string = module.getFormMenuSelector();
                    let functionMenuSelector = `div [data-securityid="C122C2C5-9D68-4CDF-86C9-E37CB70C57A0"]`;
                    await page.waitForSelector(moduleMenuSelector);
                    await page.click(moduleMenuSelector);
                    await page.waitForSelector(functionMenuSelector);
                    await page.click(functionMenuSelector);
                    await page.waitForSelector('.advisory');
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[0].click();
                    await TestUtils.waitForPleaseWait();
                    try {
                        let toasterCloseSelector = `.advisory .messageclose`;
                        await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
                        await page.click(toasterCloseSelector);
                        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
                    } catch (error) { } // assume that we missed the toaster
                }

                await this.createModuleRecord(module, record, reservedQuoteKey, reserveQuote);

                let obj: any = this.globalScopeRef[module.moduleName + "~" + reservedQuoteKey];
                expect(obj.Status).toBe("RESERVED");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and keep it Active";
            test(testName, async () => {
                let module: ModuleBase = quoteModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[1];
                await this.createModuleRecord(module, record, activeQuoteKey);

                let obj: any = this.globalScopeRef[module.moduleName + "~" + activeQuoteKey];
                expect(obj.Status).toBe("ACTIVE");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Prospect Quote";
            test(testName, async () => {
                let module: ModuleBase = quoteModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[2];
                await this.createModuleRecord(module, record, prospectQuoteKey);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + prospectQuoteKey];
                expect(obj.Status).toBe("PROSPECT");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and Cancel it";
            test(testName, async () => {
                let module: ModuleBase = quoteModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[3];

                //cancel this quote
                async function cancelQuote() {
                    let moduleMenuSelector: string = module.getFormMenuSelector();
                    let functionMenuSelector = `div [data-securityid="BF633873-8A40-4BD6-8ED8-3EAC27059C84"]`;
                    await page.waitForSelector(moduleMenuSelector);
                    await page.click(moduleMenuSelector);
                    await page.waitForSelector(functionMenuSelector);
                    await page.click(functionMenuSelector);
                    await page.waitForSelector('.advisory');
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[0].click();
                    await TestUtils.waitForPleaseWait();
                    try {
                        let toasterCloseSelector = `.advisory .messageclose`;
                        await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
                        await page.click(toasterCloseSelector);
                        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
                    } catch (error) { } // assume that we missed the toaster
                }

                await this.createModuleRecord(module, record, cancelledQuoteKey, cancelQuote);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + cancelledQuoteKey];
                expect(obj.Status).toBe("CANCELLED");
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create first new Order";
            test(testName, async () => {
                let module: ModuleBase = orderModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, orderKey1);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create second new Order";
            test(testName, async () => {
                let module: ModuleBase = orderModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[1];
                await this.createModuleRecord(module, record, orderKey2);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create third new Order";
            test(testName, async () => {
                let module: ModuleBase = orderModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[2];
                await this.createModuleRecord(module, record, orderKey3);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Receive Inventory from Vendor";
            test(testName, async () => {
                await receiveFromVendorModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + purchaseOrderKey];
                await receiveFromVendorModule.loadPo(po.PurchaseOrderNumber);
                await receiveFromVendorModule.inputQuantity(1, qtyToPurchase);
                await receiveFromVendorModule.inputQuantity(2, qtyToPurchase);
                await receiveFromVendorModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Assign Bar Codes";
            test(testName, async () => {
                await assignBarCodesModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + purchaseOrderKey];
                await assignBarCodesModule.loadPo(po.PurchaseOrderNumber);
                await assignBarCodesModule.assignBarCodes();
                await assignBarCodesModule.registerBarCodesToGlobal(barCodeKey);
                await assignBarCodesModule.addItems();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Load Rental Items (Assets)";
            test(testName, async () => {

                let module: ModuleBase = assetModule;
                for (let bc = 1; bc <= qtyToPurchase; bc++) {
                    let barCodeObject: any = this.globalScopeRef[barCodeKey + "~" + bc.toString()];
                    let barCode = barCodeObject.BarCode;
                    let recordKey = module.moduleName + "~" + barCodeRentalInventoryKey + bc.toString();
                    await this.openModuleRecord(module, { BarCode: barCode }, recordKey);
                }

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 20;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09    10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 20", "  8", "  8", "  5", " -5", " -5", " -5", "  5", "  5", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check-Out inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~" + orderKey1];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];

                await stagingModule.loadOrder(order.OrderNumber);
                await stagingModule.stageQuantity(rentalInv.ICode, qtyToCheckOut1);
                await stagingModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Stage inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~" + orderKey1];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];

                await stagingModule.loadOrder(order.OrderNumber);
                await stagingModule.stageQuantity(rentalInv.ICode, qtyToStage1);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09    10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 14", "  8", "  8", "  5", " -5", " -5", " -5", "  5", "  5", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Repair Order";
            test(testName, async () => {
                let module: ModuleBase = repairModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, repairOrderKey1);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09    10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);
                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Transfer and Confirm it";
            test(testName, async () => {
                let module: ModuleBase = transferModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];

                //confirm this transfer
                async function confirmTransfer() {
                    let moduleMenuSelector = module.getFormMenuSelector();
                    let functionMenuSelector = `div [data-securityid="A35F0AAD-81B5-4A0C-8970-D448A67D5A82"]`;
                    let toasterCloseSelector = `.advisory .messageclose`;
                    await page.waitForSelector(moduleMenuSelector);
                    await page.click(moduleMenuSelector);
                    await page.waitForSelector(functionMenuSelector);
                    await page.click(functionMenuSelector);
                    await page.waitForSelector('.advisory');
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[0].click();
                    await TestUtils.waitForPleaseWait();
                    try {
                        let toasterCloseSelector = `.advisory .messageclose`;
                        await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
                        await page.click(toasterCloseSelector);
                        await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
                    } catch (error) { } // assume that we missed the toaster
                }

                await this.createModuleRecord(module, record, confirmedTransferOrderKey, confirmTransfer);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + confirmedTransferOrderKey];
                expect(obj.Status).toBe("CONFIRMED");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                     00     01     02     03     04     05     06     07     08     09    10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", " 7", " 10", " 10", " 10", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16",]);
                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
}

new InventoryIntegrityTest().Run();