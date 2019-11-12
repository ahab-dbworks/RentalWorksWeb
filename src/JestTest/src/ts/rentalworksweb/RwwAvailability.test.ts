import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, OpenRecordResponse } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import { RentalInventory, PurchaseOrder, User, DefaultSettings, InventorySettings, Warehouse, Quote, Order, Staging, ReceiveFromVendor } from './modules/AllModules';
import { SettingsModule } from '../shared/SettingsModule';

export class AvailabilityTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    async RelogAsCopyOfUser() {

        this.LoadMyUserGlobal(new User());
        this.CopyMyUserRegisterGlobal(new User());
        this.DoLogoff();
        this.DoLogin();  // uses new login account

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

        let qtyToPurchase: number = 20;
        let qtyToReserve: number = 3;
        let qtyToOrder: number = 6;
        let qtyToCheckOut: number = 4;
        let qtyToStage: number = 1;

        //---------------------------------------------------------------------------------------
        describe('Setup new Rental I-Codes', () => {
            let testName: string = 'Create new Rental I-Code using i-Code mask, if any';
            test(testName, async () => {

                let iCodeMask: string = this.globalScopeRef["InventorySettings~1"].ICodeMask;  // ie. "aaaaa-"  or "aaaaa-aa"
                let newICode: string = TestUtils.randomAlphanumeric((iCodeMask.split("a").length - 1)); // count the a's
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
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken`,
                    //Deal: dealInputs.Deal,
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
                    Description: "GlobalScope.TestToken~1.TestToken",
                },
            }
        ];
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
        //---------------------------------------------------------------------------------------
        let orderModule: Order = new Order();
        orderModule.newRecordsToCreate = [
            {
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 1`,
                    //Deal: dealInputs.Deal,
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
                record: {
                    Description: `${TestUtils.randomJobTitle().substring(0, 20)} GlobalScope.TestToken~1.TestToken 2`,
                    //Deal: dealInputs.Deal,
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
        ];
        orderModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
                        QuantityOrdered: qtyToOrder.toString(),
                    }
                }
            },
        ];
        orderModule.newRecordsToCreate[1].gridRecords = [
            {
                grid: orderModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~NEWICODE.maskedICode",
                        QuantityOrdered: qtyToOrder.toString(),
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
                await rentalInventoryModule.openBrowse();
                await rentalInventoryModule.createNewRecord();
                await rentalInventoryModule.populateFormWithRecord(rentalInventoryModule.newRecordsToCreate[0].record);
                await rentalInventoryModule.saveRecord(true);
                await rentalInventoryModule.closeRecord();

                await rentalInventoryModule.browseSeek(rentalInventoryModule.newRecordsToCreate[0].seekObject);
                await rentalInventoryModule.openFirstRecordIfAny(true, "RENTALINVENTORY1");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Purchase Order";
            test(testName, async () => {
                await poModule.openBrowse();
                await poModule.createNewRecord();
                await poModule.populateFormWithRecord(poModule.newRecordsToCreate[0].record);
                await poModule.saveRecord(true);

                for (let grid of poModule.grids) {
                    if (grid.canNew) {
                        for (let gridRecord of poModule.newRecordsToCreate[0].gridRecords) {
                            if (gridRecord.grid === grid) {
                                await grid.addGridRow(gridRecord.recordToCreate.record, true);
                            }
                        }
                    }
                }
                await poModule.closeRecord();

                await poModule.browseSeek(poModule.newRecordsToCreate[0].seekObject);
                await poModule.openFirstRecordIfAny(true, "PO1");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Quote";
            test(testName, async () => {
                await quoteModule.openBrowse();
                await quoteModule.createNewRecord();
                await quoteModule.populateFormWithRecord(quoteModule.newRecordsToCreate[0].record);
                await quoteModule.saveRecord(true);

                for (let grid of quoteModule.grids) {
                    if (grid.canNew) {
                        for (let gridRecord of quoteModule.newRecordsToCreate[0].gridRecords) {
                            if (gridRecord.grid === grid) {
                                await grid.addGridRow(gridRecord.recordToCreate.record, true);
                            }
                        }
                    }
                }

                //reserve this quote
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

                await quoteModule.closeRecord();

                await quoteModule.browseSeek(quoteModule.newRecordsToCreate[0].seekObject);
                await quoteModule.openFirstRecordIfAny(true, "QUOTE1");

                let quote: any = this.globalScopeRef[quoteModule.moduleName + "~QUOTE1"];
                expect(quote.Status).toBe("RESERVED");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create first new Order";
            test(testName, async () => {
                await orderModule.openBrowse();
                await orderModule.createNewRecord();
                await orderModule.populateFormWithRecord(orderModule.newRecordsToCreate[0].record);
                await orderModule.saveRecord(true);

                for (let grid of orderModule.grids) {
                    if (grid.canNew) {
                        for (let gridRecord of orderModule.newRecordsToCreate[0].gridRecords) {
                            if (gridRecord.grid === grid) {
                                await grid.addGridRow(gridRecord.recordToCreate.record, true);
                            }
                        }
                    }
                }
                await orderModule.closeRecord();

                await orderModule.browseSeek(orderModule.newRecordsToCreate[0].seekObject);
                await orderModule.openFirstRecordIfAny(true, "ORDER1");

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create second new Order";
            test(testName, async () => {
                await orderModule.openBrowse();
                await orderModule.createNewRecord();
                await orderModule.populateFormWithRecord(orderModule.newRecordsToCreate[1].record);
                await orderModule.saveRecord(true);

                for (let grid of orderModule.grids) {
                    if (grid.canNew) {
                        for (let gridRecord of orderModule.newRecordsToCreate[1].gridRecords) {
                            if (gridRecord.grid === grid) {
                                await grid.addGridRow(gridRecord.recordToCreate.record, true);
                            }
                        }
                    }
                }
                await orderModule.closeRecord();

                await orderModule.browseSeek(orderModule.newRecordsToCreate[1].seekObject);
                await orderModule.openFirstRecordIfAny(true, "ORDER2");

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
                await page.keyboard.sendCharacter(qtyToCheckOut.toString());
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
                await page.keyboard.sendCharacter(qtyToStage.toString());
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
