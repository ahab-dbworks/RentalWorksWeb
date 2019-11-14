import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse, NewRecordToCreate } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { RentalInventory, PurchaseOrder, User, DefaultSettings, InventorySettings, Warehouse, Quote, Order, Staging, ReceiveFromVendor } from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

export class AvailabilityData {
    theDate: Date;
    availableQuantity: number;
}


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
    async getAvailability(rentalInventoryModule: RentalInventory): Promise<AvailabilityData[]> {
        let availData: AvailabilityData[] = new Array(); 

        let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
        await rentalInventoryModule.openBrowse();
        await rentalInventoryModule.browseSeek(record.seekObject);
        await rentalInventoryModule.openFirstRecordIfAny();

        let availabilityTabSelector = `div[data-type="tab"]#availabilitycalendartab1 .caption`;
        await page.waitForSelector(availabilityTabSelector);
        await page.click(availabilityTabSelector);
        await page.waitFor(() => document.querySelector('.pleasewait'));
        await page.waitFor(() => !document.querySelector('.pleasewait'));

        let availDaysSelector = `div[data-control="FwSchedulerDetailed"] .scheduler_default_matrix .scheduler_default_event_line0`;
        //const availDays = await page.$$(availDaysSelector);
        //for (let d = 1; d <= availDays.length; d++) {
        for (let d = 1; d <= 25; d++) {
            let theDate: Date = TestUtils.futureDate(d - 1);
            let availDaySelector = availDaysSelector + `:nth-child(${d})`;
            let availQty = await page.$eval(availDaySelector, el => el.textContent);
            Logging.logInfo(`day=${d}, availQty=${availQty}`);

            let a: AvailabilityData = new AvailabilityData();
            a.theDate = theDate;
            a.availableQuantity = +availQty;
            availData.push(a);
        }

        return availData;

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
                    await page.waitFor(() => document.querySelector('.pleasewait'));
                    await page.waitFor(() => !document.querySelector('.pleasewait'));
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
                    await page.waitFor(() => document.querySelector('.pleasewait'));
                    await page.waitFor(() => !document.querySelector('.pleasewait'));
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
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: 60000 });


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
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: 60000 });

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Check Availabilty 01";
            test(testName, async () => {

                await this.getAvailability(rentalInventoryModule)
                    .then(actualAvailQuantities => {

                        let expectedAvailQuantities: number[] = new Array();
                        expectedAvailQuantities.push(20);    //00
                        expectedAvailQuantities.push(8);     //01
                        expectedAvailQuantities.push(8);     //02
                        expectedAvailQuantities.push(5);     //03
                        expectedAvailQuantities.push(-5);    //04
                        expectedAvailQuantities.push(-5);    //05
                        expectedAvailQuantities.push(-5);    //06
                        expectedAvailQuantities.push(5);     //07
                        expectedAvailQuantities.push(5);     //08
                        expectedAvailQuantities.push(5);     //09
                        expectedAvailQuantities.push(11);    //10
                        expectedAvailQuantities.push(14);    //11
                        expectedAvailQuantities.push(14);    //12
                        expectedAvailQuantities.push(14);    //13
                        expectedAvailQuantities.push(20);    //14
                        expectedAvailQuantities.push(20);    //15
                        expectedAvailQuantities.push(20);    //16
                        expectedAvailQuantities.push(20);    //17
                        expectedAvailQuantities.push(20);    //18
                        expectedAvailQuantities.push(20);    //19
                        expectedAvailQuantities.push(20);    //20
                        expectedAvailQuantities.push(20);    //21
                        expectedAvailQuantities.push(20);    //22
                        expectedAvailQuantities.push(20);    //23
                        expectedAvailQuantities.push(20);    //24
                        //expectedAvailQuantities.push(20);    //25
                        //expectedAvailQuantities.push(20);    //26
                        //expectedAvailQuantities.push(20);    //27
                        //expectedAvailQuantities.push(20);    //28
                        //expectedAvailQuantities.push(20);    //29
                        //expectedAvailQuantities.push(20);    //30
                        //expectedAvailQuantities.push(20);    //31
                        //expectedAvailQuantities.push(20);    //32
                        //expectedAvailQuantities.push(20);    //33
                        //expectedAvailQuantities.push(20);    //34

                        Logging.logInfo(`about to check availability array length, ${actualAvailQuantities.length.toString()} vs ${expectedAvailQuantities.length.toString()}`);
                        expect(actualAvailQuantities.length).toBe(expectedAvailQuantities.length);
                        for (let q = 0; q < actualAvailQuantities.length; q++) {
                            Logging.logInfo(`about to check availability quantity, ${actualAvailQuantities[q].availableQuantity.toString()} vs ${expectedAvailQuantities[q].toString()}`);
                            expect(actualAvailQuantities[q].availableQuantity).toEqual(expectedAvailQuantities[q]);
                        }

                    });

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
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));


                // input I-Code
                const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
                await iCodeElementHandle.click();
                await iCodeElementHandle.focus();
                await iCodeElementHandle.click({ clickCount: 3 });
                await iCodeElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(rentalInv.ICode);
                await page.keyboard.press('Enter');
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));


                // input Quantity
                const qtyElementHandle = await page.$(`.fwformfield[data-datafield="Quantity"] input`);
                await qtyElementHandle.click();
                await qtyElementHandle.focus();
                await qtyElementHandle.click({ clickCount: 3 });
                await qtyElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(qtyToCheckOut1.toString());
                await page.keyboard.press('Enter');
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));

                // create contract
                const createContractElementHandle = await page.$(`div .createcontract .btnmenu`);
                await createContractElementHandle.click();

                //pending items exist
                await page.waitForSelector('.advisory');
                const options = await page.$$('.advisory .fwconfirmation-button');
                await options[0].click(); // click "Continue" option
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'), { timeout: 60000 });

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
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));


                // input I-Code
                const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
                await iCodeElementHandle.click();
                await iCodeElementHandle.focus();
                await iCodeElementHandle.click({ clickCount: 3 });
                await iCodeElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(rentalInv.ICode);
                await page.keyboard.press('Enter');
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));


                // input Quantity
                const qtyElementHandle = await page.$(`.fwformfield[data-datafield="Quantity"] input`);
                await qtyElementHandle.click();
                await qtyElementHandle.focus();
                await qtyElementHandle.click({ clickCount: 3 });
                await qtyElementHandle.press('Backspace');
                await page.keyboard.sendCharacter(qtyToStage1.toString());
                await page.keyboard.press('Enter');
                await page.waitFor(() => document.querySelector('.pleasewait'));
                await page.waitFor(() => !document.querySelector('.pleasewait'));

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------

        });
    }
    //---------------------------------------------------------------------------------------
}

new AvailabilityTest().Run();
