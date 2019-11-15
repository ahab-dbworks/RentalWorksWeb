import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse, NewRecordToCreate } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { RentalInventory, PurchaseOrder, User, DefaultSettings, InventorySettings, Warehouse, Quote, Order, RepairOrder, Staging, ReceiveFromVendor } from './modules/AllModules';
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

export class AvailabilityData {
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
}
//---------------------------------------------------------------------------------------
export class AvailabilityTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async RelogAsCopyOfUser() {

        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();  // uses new login account

    }
    //---------------------------------------------------------------------------------------
    async createModuleRecord(module: ModuleBase, record: any, registerWithRecordKey: string, doBeforeClosing?: () => void) {
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

        await module.closeRecord();
        await module.browseSeek(record.seekObject);
        await module.openFirstRecordIfAny(true, registerWithRecordKey);
        await module.closeRecord();

    }
    //---------------------------------------------------------------------------------------
    async getAvailability(rentalInventoryModule: RentalInventory): Promise<AvailabilityData> {
        let availData: AvailabilityData = new AvailabilityData();

        let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
        await rentalInventoryModule.openBrowse();
        await rentalInventoryModule.browseSeek(record.seekObject);
        await rentalInventoryModule.openFirstRecordIfAny();

        let availabilityTabSelector = `div[data-type="tab"]#availabilitycalendartab1 .caption`;
        await page.waitForSelector(availabilityTabSelector);
        await page.click(availabilityTabSelector);
        await TestUtils.waitForPleaseWait();

        async function getQty(qtyType: string): Promise<number> {
            let qty: number = 0;
            let qtyFieldSelector = `div .fwformfield.totals[data-caption="${qtyType}"] input`;
            let qtyString = await page.$eval(qtyFieldSelector, (e: any) => { return e.value })
            qty = +qtyString;
            return qty;
        }
        availData.qtyTotal = await getQty("Total");
        availData.qtyIn = await getQty("In");
        availData.qtyQcRequired = await getQty("QC  Req'd");
        availData.qtyInContainer = await getQty("In Container");
        availData.qtyStaged = await getQty("Staged");
        availData.qtyOut = await getQty("Out");
        availData.qtyInRepair = await getQty("In Repair");
        availData.qtyInTransit = await getQty("In Transit");



        let availDaysSelector = `div[data-control="FwSchedulerDetailed"] .scheduler_default_matrix .scheduler_default_event_line0`;
        for (let d = 1; d <= 25; d++) {
            let theDate: Date = TestUtils.futureDate(d - 1);
            let availDaySelector = availDaysSelector + `:nth-child(${d})`;
            let availQty = await page.$eval(availDaySelector, el => el.textContent);
            Logging.logInfo(`day=${d}, availQty=${availQty}`);
            availData.dates.push(new AvailabilityDate(theDate, +availQty));
        }

        return availData;

    }
    //---------------------------------------------------------------------------------------
    async TestAvailability(rentalInventoryModule: RentalInventory, expectedAvailData: AvailabilityData) {
        await this.getAvailability(rentalInventoryModule)
            .then(actualAvailData => {

                let expectedStr: string = "";
                let actualStr: string = "";

                actualStr = `Total Qty: ${actualAvailData.qtyTotal.toString()}`; 
                expectedStr = `Total Qty: ${expectedAvailData.qtyTotal.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Qty: ${actualAvailData.qtyIn.toString()}`;
                expectedStr = `In Qty: ${expectedAvailData.qtyIn.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `QC Required Qty: ${actualAvailData.qtyQcRequired.toString()}`;
                expectedStr = `QC Required Qty: ${expectedAvailData.qtyQcRequired.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Container Qty: ${actualAvailData.qtyInContainer.toString()}`;
                expectedStr = `In Container Qty: ${expectedAvailData.qtyInContainer.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `Staged Qty: ${actualAvailData.qtyStaged.toString()}`;
                expectedStr = `Staged Qty: ${expectedAvailData.qtyStaged.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `Out Qty: ${actualAvailData.qtyOut.toString()}`;
                expectedStr = `Out Qty: ${expectedAvailData.qtyOut.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Repair Qty: ${actualAvailData.qtyInRepair.toString()}`;
                expectedStr = `In Repair Qty: ${expectedAvailData.qtyInRepair.toString()}`;
                expect(actualStr).toBe(expectedStr);

                actualStr = `In Transit Qty: ${actualAvailData.qtyInTransit.toString()}`;
                expectedStr = `In Transit Qty: ${expectedAvailData.qtyInTransit.toString()}`;
                expect(actualStr).toBe(expectedStr);

                Logging.logInfo(`about to check availability array length, ${actualAvailData.dates.length.toString()} vs ${expectedAvailData.dates.length.toString()}`);
                expect(actualAvailData.dates.length).toBe(expectedAvailData.dates.length);
                for (let q = 0; q < actualAvailData.dates.length; q++) {
                    Logging.logInfo(`about to check availability quantity, ${actualAvailData.dates[q].availableQuantity.toString()} vs ${expectedAvailData.dates[q].availableQuantity.toString()}`);

                    actualStr = `Available ${TestUtils.dateMDY(actualAvailData.dates[q].theDate)}: ${actualAvailData.dates[q].availableQuantity.toString()}`;
                    expectedStr = `Available ${TestUtils.dateMDY(expectedAvailData.dates[q].theDate)}: ${expectedAvailData.dates[q].availableQuantity.toString()}`;
                    expect(actualStr).toBe(expectedStr);

                }
            });
    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //prerequisites

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        this.testTimeout = 600000;

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

        //---------------------------------------------------------------------------------------
        describe('Setup new Rental I-Codes', () => {
            let testName: string = 'Create new Rental I-Code using i-Code mask, if any';
            test(testName, async () => {

                let iCodeMask: string = this.globalScopeRef["InventorySettings~1"].ICodeMask;  // ie. "aaaaa-"  or "aaaaa-aa"
                let newICode: string = TestUtils.randomAlphanumeric((iCodeMask.toUpperCase().split("A").length - 1)); // count the A's
                iCodeMask = iCodeMask.trim();
                let maskedICode: string = newICode;

                if ((iCodeMask.includes("-")) && (!iCodeMask.endsWith("-"))) {
                    let hyphenIndex: number = iCodeMask.indexOf("-");
                    let iCodeStart: string = newICode.toUpperCase().substr(0, hyphenIndex);
                    let iCodeEnd: string = newICode.toUpperCase().substr(hyphenIndex);
                    maskedICode = iCodeStart + '-' + iCodeEnd;
                }

                let newICodeObject: any = {};
                newICodeObject.newICode = newICode.toUpperCase();
                newICodeObject.maskedICode = maskedICode.toUpperCase();
                this.globalScopeRef["RentalInventory~NEWICODE"] = newICodeObject;

                expect(1).toBe(1);
            }, this.testTimeout);
        });
        //---------------------------------------------------------------------------------------
        let rentalInventoryModule: RentalInventory = new RentalInventory();
        rentalInventoryModule.newRecordsToCreate = [
            {
                record: {
                    ICode: "GlobalScope.RentalInventory~NEWICODE.newICode",
                    Description: `${TestUtils.randomProductName()} GlobalScope.TestToken~1.TestToken`,
                    InventoryTypeId: 1,
                    CategoryId: 1,
                    UnitId: 1,
                    ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                    Rank: 1,
                    TrackedBy: "QUANTITY",
                    IsFixedAsset: false,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
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
                    ICode: "GlobalScope.RentalInventory~NEWICODE.newICode",
                    Quantity: qtyToRepair.toString(),
                    DueDate: TestUtils.futureDateMDY(8),
                },
                seekObject: {
                    ICode: "GlobalScope.RentalInventory~NEWICODE.newICode",
                    Quantity: qtyToRepair.toString(),
                },
            }
        ];
        //---------------------------------------------------------------------------------------
        let stagingModule: Staging = new Staging();
        //---------------------------------------------------------------------------------------
        let receiveFromVendorModule: ReceiveFromVendor = new ReceiveFromVendor();
        //---------------------------------------------------------------------------------------
        describe("Rental Inventory Availability", () => {
            let testName: string = "";
            //---------------------------------------------------------------------------------------
            testName = "Create new Rental Inventory";
            test(testName, async () => {
                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                let recordKey: string = "RENTALINVENTORY1";
                await this.createModuleRecord(rentalInventoryModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check Availability";
            test(testName, async () => {
                let expectedAvailData: AvailabilityData = new AvailabilityData();

                expectedAvailData.qtyTotal = 0;
                expectedAvailData.qtyIn = 0;
                expectedAvailData.qtyQcRequired = 0;
                expectedAvailData.qtyInContainer = 0;
                expectedAvailData.qtyStaged = 0;
                expectedAvailData.qtyOut = 0;
                expectedAvailData.qtyInRepair = 0;
                expectedAvailData.qtyInTransit = 0;

                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(0), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(1), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(2), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(3), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(4), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(5), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(6), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(7), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(8), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(9), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(10), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(11), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(12), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(13), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(14), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(15), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(16), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(17), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(18), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(19), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(20), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(21), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(22), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(23), 0));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(24), 0));

                await this.TestAvailability(rentalInventoryModule, expectedAvailData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Purchase Order";
            test(testName, async () => {
                let record: NewRecordToCreate = poModule.newRecordsToCreate[0];
                let recordKey: string = "PO1";
                await this.createModuleRecord(poModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and Reserve it";
            test(testName, async () => {
                let record: NewRecordToCreate = quoteModule.newRecordsToCreate[0];
                let recordKey: string = "RESERVEDQUOTE";

                //reserve this quote
                async function reserveQuote() {
                    await page.waitForSelector(quoteModule.getFormMenuSelector());
                    await page.click(quoteModule.getFormMenuSelector());
                    let reserveMenuOptionSelector = `div [data-securityid="C122C2C5-9D68-4CDF-86C9-E37CB70C57A0"]`;
                    await page.waitForSelector(reserveMenuOptionSelector);
                    await page.click(reserveMenuOptionSelector);
                    await page.waitForSelector('.advisory');
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[0].click(); // click "Reserve" option
                    await TestUtils.waitForPleaseWait();
                }

                await this.createModuleRecord(quoteModule, record, recordKey, reserveQuote);

                let quote: any = this.globalScopeRef[quoteModule.moduleName + "~" + recordKey];
                expect(quote.Status).toBe("RESERVED");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and keep it Active";
            test(testName, async () => {
                let record: NewRecordToCreate = quoteModule.newRecordsToCreate[1];
                let recordKey: string = "ACTIVEQUOTE";
                await this.createModuleRecord(quoteModule, record, recordKey);

                let quote: any = this.globalScopeRef[quoteModule.moduleName + "~" + recordKey];
                expect(quote.Status).toBe("ACTIVE");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Prospect Quote";
            test(testName, async () => {
                let record: NewRecordToCreate = quoteModule.newRecordsToCreate[2];
                let recordKey: string = "PROSPECTQUOTE";
                await this.createModuleRecord(quoteModule, record, recordKey);
                let quote: any = this.globalScopeRef[quoteModule.moduleName + "~" + recordKey];
                expect(quote.Status).toBe("PROSPECT");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote and Cancel it";
            test(testName, async () => {
                let record: NewRecordToCreate = quoteModule.newRecordsToCreate[3];
                let recordKey: string = "CANCELLEDQUOTE";

                //cancel this quote
                async function cancelQuote() {
                    await page.waitForSelector(quoteModule.getFormMenuSelector());
                    await page.click(quoteModule.getFormMenuSelector());
                    let cancelMenuOptionSelector = `div [data-securityid="BF633873-8A40-4BD6-8ED8-3EAC27059C84"]`;
                    await page.waitForSelector(cancelMenuOptionSelector);
                    await page.click(cancelMenuOptionSelector);
                    await page.waitForSelector('.advisory');
                    const options = await page.$$('.advisory .fwconfirmation-button');
                    await options[0].click(); // click "Cancel" option
                    await TestUtils.waitForPleaseWait();
                }

                await this.createModuleRecord(quoteModule, record, recordKey, cancelQuote);
                let quote: any = this.globalScopeRef[quoteModule.moduleName + "~" + recordKey];
                expect(quote.Status).toBe("CANCELLED");
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create first new Order";
            test(testName, async () => {
                let record: NewRecordToCreate = orderModule.newRecordsToCreate[0];
                let recordKey: string = "ORDER1";
                await this.createModuleRecord(orderModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create second new Order";
            test(testName, async () => {
                let record: any = orderModule.newRecordsToCreate[1];
                let recordKey: string = "ORDER2";
                await this.createModuleRecord(orderModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create third new Order";
            test(testName, async () => {
                let record: any = orderModule.newRecordsToCreate[2];
                let recordKey: string = "ORDER3";
                await this.createModuleRecord(orderModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Receive inventory from Vendor";
            test(testName, async () => {
                await receiveFromVendorModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~PO1"];

                //input PO Number
                const poNumberElementHandle = await page.$(`.fwformfield[data-datafield="PurchaseOrderId"] .fwformfield-text`);
                await poNumberElementHandle.click();
                await page.keyboard.sendCharacter(po.PurchaseOrderNumber);
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait(60000);


                // find I-Code Quantity in the grid (first row)
                const quantityElementHandle = await page.$(`${receiveFromVendorModule.grids[0].gridSelector} tr.viewmode .field.quantity input`);
                await quantityElementHandle.click();
                await quantityElementHandle.focus();
                await quantityElementHandle.click({ clickCount: 3 });
                await quantityElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(qtyToPurchase.toString());
                await page.keyboard.press('Enter');
                await ModuleBase.wait(1000);

                // create contract
                const createContractElementHandle = await page.$(`div .fwformcontrol.createcontract[data-type="button"]`);
                await createContractElementHandle.click();
                await TestUtils.waitForPleaseWait(60000);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check Availability";
            test(testName, async () => {
                let expectedAvailData: AvailabilityData = new AvailabilityData();

                expectedAvailData.qtyTotal = 20;
                expectedAvailData.qtyIn = 20;
                expectedAvailData.qtyQcRequired = 0;
                expectedAvailData.qtyInContainer = 0;
                expectedAvailData.qtyStaged = 0;
                expectedAvailData.qtyOut = 0;
                expectedAvailData.qtyInRepair = 0;
                expectedAvailData.qtyInTransit = 0;

                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(0), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(1), 8));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(2), 8));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(3), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(4), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(5), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(6), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(7), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(8), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(9), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(10), 11));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(11), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(12), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(13), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(14), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(15), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(16), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(17), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(18), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(19), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(20), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(21), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(22), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(23), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(24), 20));

                await this.TestAvailability(rentalInventoryModule, expectedAvailData);


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check-Out inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~ORDER1"];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~RENTALINVENTORY1"];

                //input Order Number
                const orderNumberElementHandle = await page.$(`.fwformfield[data-datafield="OrderId"] .fwformfield-text`);
                await orderNumberElementHandle.click();
                await page.keyboard.sendCharacter(order.OrderNumber);
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();


                // input I-Code
                const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
                await iCodeElementHandle.click();
                await iCodeElementHandle.focus();
                await iCodeElementHandle.click({ clickCount: 3 });
                await iCodeElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(rentalInv.ICode);
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();


                // input Quantity
                const qtyElementHandle = await page.$(`.fwformfield[data-datafield="Quantity"] input`);
                await qtyElementHandle.click();
                await qtyElementHandle.focus();
                await qtyElementHandle.click({ clickCount: 3 });
                await qtyElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(qtyToCheckOut1.toString());
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();
                await ModuleBase.wait(5000); // temporary

                // create contract
                const createContractElementHandle = await page.$(`div .createcontract .btnmenu`);
                await createContractElementHandle.click();

                //pending items exist
                await page.waitForSelector('.advisory');
                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click(); // click "Continue" option
                await TestUtils.waitForPleaseWait(60000);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Stage inventory to the Order";
            test(testName, async () => {
                await stagingModule.openModule();

                let order: any = this.globalScopeRef[orderModule.moduleName + "~ORDER1"];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~RENTALINVENTORY1"];

                //input Order Number
                const orderNumberElementHandle = await page.$(`.fwformfield[data-datafield="OrderId"] .fwformfield-text`);
                await orderNumberElementHandle.click();
                await page.keyboard.sendCharacter(order.OrderNumber);
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();


                // input I-Code
                const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
                await iCodeElementHandle.click();
                await iCodeElementHandle.focus();
                await iCodeElementHandle.click({ clickCount: 3 });
                await iCodeElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(rentalInv.ICode);
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();


                // input Quantity
                const qtyElementHandle = await page.$(`.fwformfield[data-datafield="Quantity"] input`);
                await qtyElementHandle.click();
                await qtyElementHandle.focus();
                await qtyElementHandle.click({ clickCount: 3 });
                await qtyElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(qtyToStage1.toString());
                await page.keyboard.press('Enter');
                await TestUtils.waitForPleaseWait();
                await ModuleBase.wait(5000); // temporary

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check Availability";
            test(testName, async () => {
                let expectedAvailData: AvailabilityData = new AvailabilityData();

                expectedAvailData.qtyTotal = 20;
                expectedAvailData.qtyIn = 15;
                expectedAvailData.qtyQcRequired = 0;
                expectedAvailData.qtyInContainer = 0;
                expectedAvailData.qtyStaged = 1;
                expectedAvailData.qtyOut = 4;
                expectedAvailData.qtyInRepair = 0;
                expectedAvailData.qtyInTransit = 0;

                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(0), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(1), 8));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(2), 8));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(3), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(4), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(5), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(6), -5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(7), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(8), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(9), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(10), 11));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(11), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(12), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(13), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(14), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(15), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(16), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(17), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(18), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(19), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(20), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(21), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(22), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(23), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(24), 20));

                await this.TestAvailability(rentalInventoryModule, expectedAvailData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Repair Order";
            test(testName, async () => {
                let record: NewRecordToCreate = repairModule.newRecordsToCreate[0];
                let recordKey: string = "REPAIRORDER1";
                await this.createModuleRecord(repairModule, record, recordKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check Availability";
            test(testName, async () => {
                let expectedAvailData: AvailabilityData = new AvailabilityData();

                expectedAvailData.qtyTotal = 20;
                expectedAvailData.qtyIn = 13;
                expectedAvailData.qtyQcRequired = 0;
                expectedAvailData.qtyInContainer = 0;
                expectedAvailData.qtyStaged = 1;
                expectedAvailData.qtyOut = 4;
                expectedAvailData.qtyInRepair = 2;
                expectedAvailData.qtyInTransit = 0;

                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(0), 12));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(1), 6));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(2), 6));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(3), 3));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(4), -7));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(5), -7));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(6), -7));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(7), 3));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(8), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(9), 5));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(10), 11));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(11), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(12), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(13), 14));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(14), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(15), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(16), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(17), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(18), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(19), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(20), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(21), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(22), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(23), 20));
                expectedAvailData.dates.push(new AvailabilityDate(TestUtils.futureDate(24), 20));

                await this.TestAvailability(rentalInventoryModule, expectedAvailData);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
}

new AvailabilityTest().Run();
