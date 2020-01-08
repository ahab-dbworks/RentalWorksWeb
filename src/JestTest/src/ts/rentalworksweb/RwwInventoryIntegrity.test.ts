import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse, NewRecordToCreate } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import {
    RentalInventory, PurchaseOrder, User, DefaultSettings, InventorySettings, Warehouse, Quote, Order,
    RepairOrder, Staging, ReceiveFromVendor, TransferOrder, AssignBarCodes, Asset, Exchange
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

        this.qtyTotal = 0;
        this.qtyIn = 0;
        this.qtyQcRequired = 0;
        this.qtyInContainer = 0;
        this.qtyStaged = 0;
        this.qtyOut = 0;
        this.qtyInRepair = 0;
        this.qtyInTransit = 0;

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
        await module.openRecord(1, true, registerWithRecordKey);
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
            await module.openRecord(1, true, registerWithRecordKey);
            await module.closeRecord();
        }

    }
    //---------------------------------------------------------------------------------------
    async getInventoryData(rentalInventoryModule: RentalInventory, record: any, screenshotFileName?: string): Promise<InventoryData> {
        let invData: InventoryData = new InventoryData();

        await rentalInventoryModule.openBrowse();
        await rentalInventoryModule.browseSeek(record.seekObject);
        await rentalInventoryModule.openRecord();

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

        if (screenshotFileName) {
            await page.screenshot({ path: `./inventoryintegrity_${this.testToken}_${screenshotFileName}.jpg`, fullPage: true });
        }
        return invData;
    }
    //---------------------------------------------------------------------------------------
    async TestInventoryIntegrity(rentalInventoryModule: RentalInventory, record: any, expectedInvData: InventoryData, screenshotFileName?: string) {
        await this.getInventoryData(rentalInventoryModule, record, screenshotFileName)
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
        let qtyToNewTransfer1: number = 50;
        let qtyToCancelTransfer1: number = 60;
        let qtyToConfirmTransfer1: number = 4;

        const quantityRentalInventoryKey: string = "QUANTITYRENTALINVENTORY";
        const barCodeRentalInventoryKey: string = "BARCODERENTALINVENTORY";
        const purchaseOrderKey: string = "PURCHASEORDER";
        const reservedQuoteKey: string = "RESERVEDQUOTE";
        const activeQuoteKey: string = "ACTIVEQUOTE";
        const prospectQuoteKey: string = "PROSPECTQUOTE";
        const cancelledQuoteKey: string = "CANCELLEDQUOTE";
        const orderKey1: string = "ORDER1";
        const orderKey2: string = "ORDER2";
        const orderKey3: string = "ORDER3";
        const repairOrderKey1: string = "REPAIRORDER1";
        const repairOrderKey2: string = "REPAIRORDER2";
        const repairOrderKey3: string = "REPAIRORDER3";
        const repairOrderKey4: string = "REPAIRORDER4";
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
                //quantity
                grid: poModule.rentalGrid,
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
            {
                //bar code
                grid: poModule.rentalGrid,
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
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: quoteModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
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
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToOrder3.toString(),
                    }
                }
            },
        ];
        //---------------------------------------------------------------------------------------
        let repairModule: RepairOrder = new RepairOrder();
        repairModule.newRecordsToCreate = [
            {
                //quantity
                record: {
                    ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                    Quantity: qtyToRepair.toString(),
                    DueDate: TestUtils.futureDateMDY(8),
                },
                seekObject: {
                    ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                    Quantity: qtyToRepair.toString(),
                },
            },
            {
                //bar code 1
                record: {
                    BarCode: "GlobalScope." + barCodeKey + "~1.BarCode",
                    DueDate: TestUtils.futureDateMDY(8),
                },
                seekObject: {
                    BarCode: "GlobalScope." + barCodeKey + "~1.BarCode",
                },
            },
            {
                //bar code 2
                record: {
                    BarCode: "GlobalScope." + barCodeKey + "~2.BarCode",
                    DueDate: TestUtils.futureDateMDY(8),
                },
                seekObject: {
                    BarCode: "GlobalScope." + barCodeKey + "~2.BarCode",
                },
            },
            {
                //bar code (pending)
                record: {
                    BarCode: "GlobalScope." + barCodeKey + "~3.BarCode",
                    DueDate: TestUtils.futureDateMDY(8),
                    PendingRepair: true,
                },
                seekObject: {
                    BarCode: "GlobalScope." + barCodeKey + "~3.BarCode",
                },
            },
        ];
        //---------------------------------------------------------------------------------------
        let transferModule: TransferOrder = new TransferOrder();
        transferModule.newRecordsToCreate = [
            //confirm
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
            },
            //new
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 2`,
                    FromWarehouseCode: "GlobalScope.Warehouse~MINE.WarehouseCode",
                    ToWarehouseId: 2,
                    RequiredDate: TestUtils.futureDateMDY(20),
                    ShipDate: TestUtils.futureDateMDY(12),
                    PickDate: TestUtils.futureDateMDY(10),
                    Rental: true,
                    Sales: false,
                },
                seekObject: {
                    Description: `GlobalScope.TestToken~1.TestToken 2`,
                },
            },
            //cancel
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 3`,
                    FromWarehouseCode: "GlobalScope.Warehouse~MINE.WarehouseCode",
                    ToWarehouseId: 2,
                    RequiredDate: TestUtils.futureDateMDY(20),
                    ShipDate: TestUtils.futureDateMDY(12),
                    PickDate: TestUtils.futureDateMDY(10),
                    Rental: true,
                    Sales: false,
                },
                seekObject: {
                    Description: `GlobalScope.TestToken~1.TestToken 3`,
                },
            },
        ];
        //confirm
        transferModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToConfirmTransfer1.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToConfirmTransfer1.toString(),
                    },
                },
            },
        ];
        //new
        transferModule.newRecordsToCreate[1].gridRecords = [
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToNewTransfer1.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToNewTransfer1.toString(),
                    },
                },
            },
        ];
        //cancel
        transferModule.newRecordsToCreate[2].gridRecords = [
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToCancelTransfer1.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToCancelTransfer1.toString(),
                    },
                },
            },
        ];

        //---------------------------------------------------------------------------------------
        let stagingModule: Staging = new Staging();
        let receiveFromVendorModule: ReceiveFromVendor = new ReceiveFromVendor();
        let assignBarCodesModule: AssignBarCodes = new AssignBarCodes();
        let exchangeModule: Exchange = new Exchange();
        //---------------------------------------------------------------------------------------
        describe("Rental Inventory Integrity", () => {
            let testName: string = "";
            //---------------------------------------------------------------------------------------
            testName = "Create new Rental Inventory";
            test(testName, async () => {
                let module: ModuleBase = rentalInventoryModule;
                await this.createModuleRecord(module, module.newRecordsToCreate[0], quantityRentalInventoryKey);
                await this.createModuleRecord(module, module.newRecordsToCreate[1], barCodeRentalInventoryKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity rental inventory
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

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "010");

                //barcode rental inventory
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

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "020");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Purchase Order";
            test(testName, async () => {
                let module: ModuleBase = poModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, purchaseOrderKey);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + purchaseOrderKey];
                expect(obj.Status).toBe("NEW");
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and Reserve it";
            test(testName, async () => {
                let module: ModuleBase = quoteModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];

                //reserve this quote
                async function reserveQuote() {
                    let moduleMenuSelector: string = module.getFormMenuSelector();
                    let functionMenuSelector = `div .fwform [data-securityid="1oBE7m2rBjxhm"]`;
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
                    let functionMenuSelector = `div .fwform [data-securityid="dpH0uCuEp3E89"]`;
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
                //quantity inventory
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 20;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 20", "  8", "  8", "  5", " -5", " -5", " -5", "  5", "  5", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "030");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check-Out inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~" + orderKey1];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let barCode3: any = this.globalScopeRef[barCodeKey + "~" + "3"];
                let barCode4: any = this.globalScopeRef[barCodeKey + "~" + "4"];
                let barCode5: any = this.globalScopeRef[barCodeKey + "~" + "5"];
                let barCode6: any = this.globalScopeRef[barCodeKey + "~" + "6"];

                await stagingModule.loadOrder(order.OrderNumber);
                await stagingModule.stageQuantity(rentalInv.ICode, qtyToCheckOut1);
                await stagingModule.stageBarCode(barCode3.BarCode);
                await stagingModule.stageBarCode(barCode4.BarCode);
                await stagingModule.stageBarCode(barCode5.BarCode);
                await stagingModule.stageBarCode(barCode6.BarCode);
                await stagingModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Stage inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~" + orderKey1];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let barCode7: any = this.globalScopeRef[barCodeKey + "~" + "7"];

                await stagingModule.loadOrder(order.OrderNumber);
                await stagingModule.stageQuantity(rentalInv.ICode, qtyToStage1);
                await stagingModule.stageBarCode(barCode7.BarCode);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 14", "  8", "  8", "  5", " -5", " -5", " -5", "  5", "  5", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "040");

                //barcode 
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 14", "  8", "  8", "  5", " -5", " -5", " -5", "  5", "  5", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "041");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Repair Order for Quantity inventory";
            test(testName, async () => {
                let module: ModuleBase = repairModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, repairOrderKey1);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Repair Orders for Bar Code inventory";
            test(testName, async () => {
                let module: ModuleBase = repairModule;
                await this.createModuleRecord(module, module.newRecordsToCreate[1], repairOrderKey2);
                await this.createModuleRecord(module, module.newRecordsToCreate[2], repairOrderKey3);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Pending Repair Order for Bar Code inventory";
            test(testName, async () => {
                let module: ModuleBase = repairModule;
                await this.createModuleRecord(module, module.newRecordsToCreate[3], repairOrderKey4);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "050");

                //barcode
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", " 11", " 14", " 14", " 14", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20", " 20",]);

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "051");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Transfer and Confirm it";
            test(testName, async () => {
                let module: ModuleBase = transferModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];

                //confirm this transfer
                async function confirmTransfer() {
                    let moduleMenuSelector = module.getFormMenuSelector();
                    let functionMenuSelector = `div .fwform [data-securityid="VHP1qrNmwB4"]`;
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
            testName = "Create new Transfer and keep it New";
            test(testName, async () => {
                let module: ModuleBase = transferModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[1];

                await this.createModuleRecord(module, record, confirmedTransferOrderKey);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + confirmedTransferOrderKey];
                expect(obj.Status).toBe("NEW");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            //testName = "Create new Transfer and Cancel it";
            //test(testName, async () => {
            //    let module: ModuleBase = transferModule;
            //    let record: NewRecordToCreate = module.newRecordsToCreate[2];
            //
            //    //cancel this transfer
            //    async function cancelTransfer() {
            //        let moduleMenuSelector = module.getFormMenuSelector();
            //        let functionMenuSelector = `div .fwform [data-securityid="XXXXXXXXXXXXXXXXXXXXXXX"]`;
            //        let toasterCloseSelector = `.advisory .messageclose`;
            //        await page.waitForSelector(moduleMenuSelector);
            //        await page.click(moduleMenuSelector);
            //        await page.waitForSelector(functionMenuSelector);
            //        await page.click(functionMenuSelector);
            //        await page.waitForSelector('.advisory');
            //        const options = await page.$$('.advisory .fwconfirmation-button');
            //        await options[0].click();
            //        await TestUtils.waitForPleaseWait();
            //        try {
            //            let toasterCloseSelector = `.advisory .messageclose`;
            //            await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
            //            await page.click(toasterCloseSelector);
            //            await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
            //        } catch (error) { } // assume that we missed the toaster
            //    }
            //
            //    await this.createModuleRecord(module, record, confirmedTransferOrderKey, cancelTransfer);
            //    let obj: any = this.globalScopeRef[module.moduleName + "~" + confirmedTransferOrderKey];
            //    expect(obj.Status).toBe("CANCELLED");
            //
            //}, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", "  7", " 10", " 10", " 10", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "060");

                //barcode
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 13;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 4;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 12", "  6", "  6", "  3", " -7", " -7", " -7", "  3", "  3", "  5", "  7", " 10", " 10", " 10", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16", " 16",]);

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "061");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Pending Exchange";
            test(testName, async () => {
                await exchangeModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~" + orderKey1];
                let barCode8: any = this.globalScopeRef[barCodeKey + "~" + "8"];

                await exchangeModule.loadDeal(order.Deal);
                await exchangeModule.checkOutBarCode(barCode8.BarCode);
                await exchangeModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                ////quantity
                //let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                //await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "060");

                //barcode
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 12;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 5;
                expectedInvData.qtyInRepair = 2;
                expectedInvData.qtyInTransit = 0;
                //                                    00     01     02     03     04     05     06     07     08     09     10     11     12     13     14     15     16     17     18     19     20     21     22     23     24
                expectedInvData.populateAvailDates([" 11", "  5", "  5", "  2", " -8", " -8", " -8", "  2", "  2", "  4", "  6", "  9", "  9", "  9", " 15", " 15", " 15", " 15", " 15", " 15", " 15", " 15", " 15", " 15", " 15",]);

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "070");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
}

describe('InventoryIntegrityTest', () => {
    try {
        new InventoryIntegrityTest().Run();
    } catch(ex) {
        fail(ex);
    }
});
