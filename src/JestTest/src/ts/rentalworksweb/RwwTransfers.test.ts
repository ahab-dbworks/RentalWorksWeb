import { BaseTest } from '../shared/BaseTest';
import { ModuleBase, NewRecordToCreate } from '../shared/ModuleBase';
import { Logging } from '../shared/Logging';
import { TestUtils } from '../shared/TestUtils';
import {
    InventoryModule, RentalInventory, SalesInventory, PurchaseOrder, User, Warehouse,
    ReceiveFromVendor, TransferOrder, AssignBarCodes, Asset, TransferOut, TransferIn, OfficeLocation
} from './modules/AllModules';


/*

01/14/2020 justin hoffman
This is a script to test the functionality of Transfer Out and Transfer In, specifically cancelling transfer-in after starting.
My plan/hope is to merge these tests into the "InventoryIntegrity" test once done.  But I'm pressed for time right now.


*/




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
    constructor() {

        this.qtyTotal = 0;
        this.qtyIn = 0;
        this.qtyQcRequired = 0;
        this.qtyInContainer = 0;
        this.qtyStaged = 0;
        this.qtyOut = 0;
        this.qtyInRepair = 0;
        this.qtyInTransit = 0;
    }
}
//---------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------
export class TransfersTest extends BaseTest {
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
    async getInventoryData(inventoryModule: InventoryModule, record: any, screenshotFileName?: string): Promise<InventoryData> {
        let invData: InventoryData = new InventoryData();

        await inventoryModule.openBrowse();
        await inventoryModule.browseSeek(record.seekObject);
        await inventoryModule.openRecord();

        let availabilityTabSelector = `div[data-type="tab"]#availabilitycalendartab1 .caption`;
        await page.waitForSelector(availabilityTabSelector);
        await page.click(availabilityTabSelector);
        await TestUtils.waitForPleaseWait();

        async function getQty(qtyType: string): Promise<number> {
            let qty: number = 0;
            let qtyFieldSelector = `div .fwformfield.totals[data-totalfield="${qtyType}"] input`;
            //let qtyString = await page.$eval(qtyFieldSelector, (e: any) => { return e.value })
            let qtyString: string = "0";
            try {
                qtyString = await page.$eval(qtyFieldSelector, (e: any) => { return e.value });
            }
            catch {
                // if field is not found on the form, then use zero
                qtyString = "0";
            };  
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
        }

        if (screenshotFileName) {
            await page.screenshot({ path: `./transfertest_${this.testToken}_${screenshotFileName}.jpg`, fullPage: true });
        }
        return invData;
    }
    //---------------------------------------------------------------------------------------
    async TestInventoryIntegrity(inventoryModule: InventoryModule, record: any, expectedInvData: InventoryData, screenshotFileName?: string) {
        await this.getInventoryData(inventoryModule, record, screenshotFileName)
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
            });
    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        this.testTimeout = 600000;
        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new OfficeLocation(), { Location: "GlobalScope.User~ME.OfficeLocation" }, true, "MINE");
        this.OpenSpecificRecord(new Warehouse(), { Warehouse: "GlobalScope.User~ME.Warehouse" }, true, "MINE");

        //temporary hack to define the "other" Office and Warehouse for this test
        this.OpenSpecificRecord(new OfficeLocation(), { Location: "NEW YORK" }, true, "OTHER");
        this.OpenSpecificRecord(new Warehouse(), { Warehouse: "NEW YORK" }, true, "OTHER");


        let qtyToPurchase: number = 20;
        let qtyToTransferOrder: number = 5;
        let qtyToTransferOut: number = 4;
        let qtyToTransferStage: number = 1;

        const quantityRentalInventoryKey: string = "QUANTITYRENTALINVENTORY";
        const quantitySalesInventoryKey: string = "QUANTITYSALESINVENTORY";
        const barCodeRentalInventoryKey: string = "BARCODERENTALINVENTORY";
        const barCodeSalesInventoryKey: string = "BARCODESALESINVENTORY";
        const rentalPurchaseOrderKey: string = "RENTALPURCHASEORDER";
        const salesPurchaseOrderKey: string = "SALESPURCHASEORDER";
        const transferOrderKey: string = "CONFIRMEDTRANSFER";
        const rentalBarCodeKey: string = "RENTALBARCODE";
        const salesBarCodeKey: string = "SALESBARCODE";

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
        let salesInventoryModule: SalesInventory = new SalesInventory();
        salesInventoryModule.newRecordsToCreate = [
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
                //rental po
                record: {
                    VendorId: 2,
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken R`,
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: true,
                    Sales: false,
                    Parts: false,
                    Miscellaneous: false,
                    Labor: false,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken R",
                },
            },
            {
                //sales po
                record: {
                    VendorId: 2,
                    Description: `${TestUtils.randomJobTitle().substring(0, 25)} GlobalScope.TestToken~1.TestToken S`,
                    ReferenceNumber: TestUtils.randomAlphanumeric(8),
                    Rental: false,
                    Sales: true,
                    Parts: false,
                    Miscellaneous: false,
                    Labor: false,
                },
                seekObject: {
                    Description: "GlobalScope.TestToken~1.TestToken S",
                },
            },
        ];
        poModule.newRecordsToCreate[0].gridRecords = [
            {
                // rental quantity 
                grid: poModule.rentalGrid,
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
            {
                //rental bar code
                grid: poModule.rentalGrid,
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
        ];
        poModule.newRecordsToCreate[1].gridRecords = [
            {
                //sales quantity
                grid: poModule.salesGrid,
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.SalesInventory~" + quantitySalesInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
            },
            {
                //sales bar code
                grid: poModule.salesGrid,
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.SalesInventory~" + barCodeSalesInventoryKey + ".ICode",
                        QuantityOrdered: qtyToPurchase.toString(),
                    }
                }
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
                    ToWarehouseCode: "GlobalScope.Warehouse~OTHER.WarehouseCode",
                    RequiredDate: TestUtils.futureDateMDY(20),
                    ShipDate: TestUtils.futureDateMDY(12),
                    PickDate: TestUtils.futureDateMDY(10),
                    Rental: true,
                    Sales: true,
                },
                seekObject: {
                    Description: `GlobalScope.TestToken~1.TestToken 1`,
                },
            },
        ];
        transferModule.newRecordsToCreate[0].gridRecords = [
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + quantityRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToTransferOrder.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[0], // rental grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.RentalInventory~" + barCodeRentalInventoryKey + ".ICode",
                        QuantityOrdered: qtyToTransferOrder.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[1], // sales grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.SalesInventory~" + quantitySalesInventoryKey + ".ICode",
                        QuantityOrdered: qtyToTransferOrder.toString(),
                    },
                },
            },
            {
                grid: transferModule.grids[1], // sales grid
                recordToCreate: {
                    record: {
                        ICode: "GlobalScope.SalesInventory~" + barCodeSalesInventoryKey + ".ICode",
                        QuantityOrdered: qtyToTransferOrder.toString(),
                    },
                },
            },
        ];
        //---------------------------------------------------------------------------------------
        let receiveFromVendorModule: ReceiveFromVendor = new ReceiveFromVendor();
        let assignBarCodesModule: AssignBarCodes = new AssignBarCodes();
        let transferOutModule: TransferOut = new TransferOut();
        let transferInModule: TransferIn = new TransferIn();
        //---------------------------------------------------------------------------------------
        describe("Transfer Inventory", () => {
            let testName: string = "";
            //---------------------------------------------------------------------------------------
            testName = "Create new Rental Inventory";
            test(testName, async () => {
                let module: ModuleBase = rentalInventoryModule;
                await this.createModuleRecord(module, module.newRecordsToCreate[0], quantityRentalInventoryKey);
                await this.createModuleRecord(module, module.newRecordsToCreate[1], barCodeRentalInventoryKey);
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Sales Inventory";
            test(testName, async () => {
                let module: ModuleBase = salesInventoryModule;
                await this.createModuleRecord(module, module.newRecordsToCreate[0], quantitySalesInventoryKey);
                await this.createModuleRecord(module, module.newRecordsToCreate[1], barCodeSalesInventoryKey);
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

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "020");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Rental Purchase Order";
            test(testName, async () => {
                let module: ModuleBase = poModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[0];
                await this.createModuleRecord(module, record, rentalPurchaseOrderKey);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + rentalPurchaseOrderKey];
                expect(obj.Status).toBe("NEW");
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Create new Sales Purchase Order";
            test(testName, async () => {
                let module: ModuleBase = poModule;
                let record: NewRecordToCreate = module.newRecordsToCreate[1];
                await this.createModuleRecord(module, record, salesPurchaseOrderKey);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + salesPurchaseOrderKey];
                expect(obj.Status).toBe("NEW");
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Receive Rental Inventory from Vendor";
            test(testName, async () => {
                await receiveFromVendorModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + rentalPurchaseOrderKey];
                await receiveFromVendorModule.loadPo(po.PurchaseOrderNumber);
                await receiveFromVendorModule.inputQuantity(1, qtyToPurchase);
                await receiveFromVendorModule.inputQuantity(2, qtyToPurchase);
                await receiveFromVendorModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Assign Rental Bar Codes";
            test(testName, async () => {
                await assignBarCodesModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + rentalPurchaseOrderKey];
                await assignBarCodesModule.loadPo(po.PurchaseOrderNumber);
                await assignBarCodesModule.assignBarCodes();
                await assignBarCodesModule.registerBarCodesToGlobal(rentalBarCodeKey);
                await assignBarCodesModule.addItems();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Receive Sales Inventory from Vendor";
            test(testName, async () => {
                await receiveFromVendorModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + salesPurchaseOrderKey];
                await receiveFromVendorModule.loadPo(po.PurchaseOrderNumber);
                await receiveFromVendorModule.inputQuantity(1, qtyToPurchase);
                await receiveFromVendorModule.inputQuantity(2, qtyToPurchase);
                await receiveFromVendorModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Assign Sales Bar Codes";
            test(testName, async () => {
                await assignBarCodesModule.openModule();

                let po: any = this.globalScopeRef[poModule.moduleName + "~" + salesPurchaseOrderKey];
                await assignBarCodesModule.loadPo(po.PurchaseOrderNumber);
                await assignBarCodesModule.assignBarCodes();
                await assignBarCodesModule.registerBarCodesToGlobal(salesBarCodeKey);
                await assignBarCodesModule.addItems();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Load Rental Items (Bar Code Assets)";
            test(testName, async () => {

                let module: ModuleBase = assetModule;
                for (let bc = 1; bc <= qtyToPurchase; bc++) {
                    let barCodeObject: any = this.globalScopeRef[rentalBarCodeKey + "~" + bc.toString()];
                    let barCode = barCodeObject.BarCode;
                    let recordKey = module.moduleName + "~" + barCodeRentalInventoryKey + bc.toString();
                    await this.openModuleRecord(module, { BarCode: barCode }, recordKey);
                }

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Load Sales Items (Bar Code Assets)";
            test(testName, async () => {

                let module: ModuleBase = assetModule;
                for (let bc = 1; bc <= qtyToPurchase; bc++) {
                    let barCodeObject: any = this.globalScopeRef[salesBarCodeKey + "~" + bc.toString()];
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

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "030");


                //barcode rental inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 20;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "040");


                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 20;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 20;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");


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

                await this.createModuleRecord(module, record, transferOrderKey, confirmTransfer);
                let obj: any = this.globalScopeRef[module.moduleName + "~" + transferOrderKey];
                expect(obj.Status).toBe("CONFIRMED");

            }, this.testTimeout);

            //---------------------------------------------------------------------------------------
            testName = "Check-Out inventory to the Transfer Order";
            test(testName, async () => {
                await transferOutModule.openModule();

                let transfer: any = this.globalScopeRef[transferModule.moduleName + "~" + transferOrderKey];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let salesInv: any = this.globalScopeRef[salesInventoryModule.moduleName + "~" + quantitySalesInventoryKey];
                let rentalBarCode3: any = this.globalScopeRef[rentalBarCodeKey + "~" + "3"];
                let rentalBarCode4: any = this.globalScopeRef[rentalBarCodeKey + "~" + "4"];
                let rentalBarCode5: any = this.globalScopeRef[rentalBarCodeKey + "~" + "5"];
                let rentalBarCode6: any = this.globalScopeRef[rentalBarCodeKey + "~" + "6"];
                let salesBarCode3: any = this.globalScopeRef[salesBarCodeKey + "~" + "3"];
                let salesBarCode4: any = this.globalScopeRef[salesBarCodeKey + "~" + "4"];
                let salesBarCode5: any = this.globalScopeRef[salesBarCodeKey + "~" + "5"];
                let salesBarCode6: any = this.globalScopeRef[salesBarCodeKey + "~" + "6"];

                await transferOutModule.loadTransfer(transfer.TransferNumber);
                await transferOutModule.stageQuantity(rentalInv.ICode, qtyToTransferOut);
                await transferOutModule.stageBarCode(rentalBarCode3.BarCode);
                await transferOutModule.stageBarCode(rentalBarCode4.BarCode);
                await transferOutModule.stageBarCode(rentalBarCode5.BarCode);
                await transferOutModule.stageBarCode(rentalBarCode6.BarCode);
                await transferOutModule.stageQuantity(salesInv.ICode, qtyToTransferOut);
                await transferOutModule.stageBarCode(salesBarCode3.BarCode);
                await transferOutModule.stageBarCode(salesBarCode4.BarCode);
                await transferOutModule.stageBarCode(salesBarCode5.BarCode);
                await transferOutModule.stageBarCode(salesBarCode6.BarCode);
                await transferOutModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Stage inventory to the Transfer Order";
            test(testName, async () => {
                await transferOutModule.openModule();

                let transfer: any = this.globalScopeRef[transferModule.moduleName + "~" + transferOrderKey];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let salesInv: any = this.globalScopeRef[salesInventoryModule.moduleName + "~" + quantitySalesInventoryKey];
                let rentalBarCode7: any = this.globalScopeRef[rentalBarCodeKey + "~" + "7"];
                let salesBarCode7: any = this.globalScopeRef[salesBarCodeKey + "~" + "7"];

                await transferOutModule.loadTransfer(transfer.TransferNumber);
                await transferOutModule.stageQuantity(rentalInv.ICode, qtyToTransferStage);
                await transferOutModule.stageBarCode(rentalBarCode7.BarCode);
                await transferOutModule.stageQuantity(salesInv.ICode, qtyToTransferStage);
                await transferOutModule.stageBarCode(salesBarCode7.BarCode);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //rental quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "050");

                //rental barcode
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "060");


                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");



            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            //this.ChangeOfficeWarehouse("GlobalScope.OfficeLocation~OTHER.Location", "GlobalScope.Warehouse~OTHER.Warehouse");
            testName = "Change to 'Other' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~OTHER"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~OTHER"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

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

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "020");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Transfer-In inventory from the Transfer Order, then cancel all items individually";
            test(testName, async () => {
                await transferInModule.openModule();

                let transfer: any = this.globalScopeRef[transferModule.moduleName + "~" + transferOrderKey];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let salesInv: any = this.globalScopeRef[salesInventoryModule.moduleName + "~" + quantitySalesInventoryKey];
                let rentalBarCode3: any = this.globalScopeRef[rentalBarCodeKey + "~" + "3"];
                let rentalBarCode4: any = this.globalScopeRef[rentalBarCodeKey + "~" + "4"];
                let rentalBarCode5: any = this.globalScopeRef[rentalBarCodeKey + "~" + "5"];
                let rentalBarCode6: any = this.globalScopeRef[rentalBarCodeKey + "~" + "6"];
                let salesBarCode3: any = this.globalScopeRef[salesBarCodeKey + "~" + "3"];
                let salesBarCode4: any = this.globalScopeRef[salesBarCodeKey + "~" + "4"];
                let salesBarCode5: any = this.globalScopeRef[salesBarCodeKey + "~" + "5"];
                let salesBarCode6: any = this.globalScopeRef[salesBarCodeKey + "~" + "6"];

                await transferInModule.loadTransfer(transfer.TransferNumber);
                await transferInModule.checkInQuantity(rentalInv.ICode, qtyToTransferOut);
                await transferInModule.checkInQuantity(salesInv.ICode, qtyToTransferOut);
                await transferInModule.checkInBarCode(rentalBarCode3.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode4.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode5.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode6.BarCode);
                await transferInModule.checkInBarCode(salesBarCode3.BarCode);
                await transferInModule.checkInBarCode(salesBarCode4.BarCode);
                await transferInModule.checkInBarCode(salesBarCode5.BarCode);
                await transferInModule.checkInBarCode(salesBarCode6.BarCode);
                await transferInModule.cancelAllItemsInGrid();
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Change to 'My' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~MINE"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~MINE"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //rental quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "050");

                //rental barcode
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "060");


                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");



            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Change to 'Other' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~OTHER"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~OTHER"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

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

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "020");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Transfer-In inventory from the Transfer Order, then cancel entire Session";
            test(testName, async () => {
                await transferInModule.openModule();

                let transfer: any = this.globalScopeRef[transferModule.moduleName + "~" + transferOrderKey];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let salesInv: any = this.globalScopeRef[salesInventoryModule.moduleName + "~" + quantitySalesInventoryKey];
                let rentalBarCode3: any = this.globalScopeRef[rentalBarCodeKey + "~" + "3"];
                let rentalBarCode4: any = this.globalScopeRef[rentalBarCodeKey + "~" + "4"];
                let rentalBarCode5: any = this.globalScopeRef[rentalBarCodeKey + "~" + "5"];
                let rentalBarCode6: any = this.globalScopeRef[rentalBarCodeKey + "~" + "6"];
                let salesBarCode3: any = this.globalScopeRef[salesBarCodeKey + "~" + "3"];
                let salesBarCode4: any = this.globalScopeRef[salesBarCodeKey + "~" + "4"];
                let salesBarCode5: any = this.globalScopeRef[salesBarCodeKey + "~" + "5"];
                let salesBarCode6: any = this.globalScopeRef[salesBarCodeKey + "~" + "6"];

                await transferInModule.loadTransfer(transfer.TransferNumber);
                await transferInModule.checkInQuantity(rentalInv.ICode, qtyToTransferOut);
                await transferInModule.checkInQuantity(salesInv.ICode, qtyToTransferOut);
                await transferInModule.checkInBarCode(rentalBarCode3.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode4.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode5.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode6.BarCode);
                await transferInModule.checkInBarCode(salesBarCode3.BarCode);
                await transferInModule.checkInBarCode(salesBarCode4.BarCode);
                await transferInModule.checkInBarCode(salesBarCode5.BarCode);
                await transferInModule.checkInBarCode(salesBarCode6.BarCode);

                await transferInModule.cancelSession();
            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "070");

                //barcode 
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "080");


                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 0;
                expectedInvData.qtyIn = 0;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");


            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            //this.ChangeOfficeWarehouse("GlobalScope.OfficeLocation~MINE.Location", "GlobalScope.Warehouse~MINE.Warehouse");
            testName = "Change to 'My' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~MINE"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~MINE"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

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
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "090");

                //barcode 
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "100");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 20;
                expectedInvData.qtyIn = 15;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 4;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");



            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            //this.ChangeOfficeWarehouse("GlobalScope.OfficeLocation~OTHER.Location", "GlobalScope.Warehouse~OTHER.Warehouse");
            testName = "Change to 'Other' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~OTHER"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~OTHER"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Transfer-In inventory from the Transfer Order";
            test(testName, async () => {
                await transferInModule.openModule();

                let transfer: any = this.globalScopeRef[transferModule.moduleName + "~" + transferOrderKey];
                let rentalInv: any = this.globalScopeRef[rentalInventoryModule.moduleName + "~" + quantityRentalInventoryKey];
                let salesInv: any = this.globalScopeRef[salesInventoryModule.moduleName + "~" + quantitySalesInventoryKey];
                let rentalBarCode3: any = this.globalScopeRef[rentalBarCodeKey + "~" + "3"];
                let rentalBarCode4: any = this.globalScopeRef[rentalBarCodeKey + "~" + "4"];
                let rentalBarCode5: any = this.globalScopeRef[rentalBarCodeKey + "~" + "5"];
                let rentalBarCode6: any = this.globalScopeRef[rentalBarCodeKey + "~" + "6"];
                let salesBarCode3: any = this.globalScopeRef[salesBarCodeKey + "~" + "3"];
                let salesBarCode4: any = this.globalScopeRef[salesBarCodeKey + "~" + "4"];
                let salesBarCode5: any = this.globalScopeRef[salesBarCodeKey + "~" + "5"];
                let salesBarCode6: any = this.globalScopeRef[salesBarCodeKey + "~" + "6"];

                await transferInModule.loadTransfer(transfer.TransferNumber);
                await transferInModule.checkInQuantity(rentalInv.ICode, qtyToTransferOut);
                await transferInModule.checkInQuantity(salesInv.ICode, qtyToTransferOut);
                await transferInModule.checkInBarCode(rentalBarCode3.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode4.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode5.BarCode);
                await transferInModule.checkInBarCode(rentalBarCode6.BarCode);
                await transferInModule.checkInBarCode(salesBarCode3.BarCode);
                await transferInModule.checkInBarCode(salesBarCode4.BarCode);
                await transferInModule.checkInBarCode(salesBarCode5.BarCode);
                await transferInModule.checkInBarCode(salesBarCode6.BarCode);
                await transferInModule.createContract();

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 4;
                expectedInvData.qtyIn = 4;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "110");

                //barcode 
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 4;
                expectedInvData.qtyIn = 4;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "120");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 4;
                expectedInvData.qtyIn = 4;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 4;
                expectedInvData.qtyIn = 4;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 0;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");



            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            //this.ChangeOfficeWarehouse("GlobalScope.OfficeLocation~MINE.Location", "GlobalScope.Warehouse~MINE.Warehouse");
            testName = "Change to 'My' Office/Warehouse";
            test(testName, async () => {
                let toOffice: string = this.globalScopeRef["OfficeLocation~MINE"].Location;
                let toWarehouse: string = this.globalScopeRef["Warehouse~MINE"].Warehouse;
                await this.ChangeOfficeWarehouse(toOffice, toWarehouse);

                let selector = `div.systembarcontrol[data-id="officelocation"] .value`;
                await page.waitForSelector(selector);
                const element = await page.$(selector);
                const officeLocation = await page.evaluate(element => element.textContent, element);
                let expectedOfficeLocation = toOffice;
                Logging.logInfo(`Validating Office Location on toolbar:\n     Expecting: "${expectedOfficeLocation}"\n     Found:     "${officeLocation}"`);
                expect(officeLocation).toBe(expectedOfficeLocation);

            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
            testName = "Test Inventory Integrity";
            test(testName, async () => {
                //quantity 
                let expectedInvData: InventoryData = new InventoryData();

                expectedInvData.qtyTotal = 16;
                expectedInvData.qtyIn = 11;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                let record: NewRecordToCreate = rentalInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "130");

                //barcode 
                expectedInvData = new InventoryData();

                expectedInvData.qtyTotal = 16;
                expectedInvData.qtyIn = 11;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = rentalInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(rentalInventoryModule, record, expectedInvData, "140");

                //barcode sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 16;
                expectedInvData.qtyIn = 11;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[0];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");

                //quantity sales inventory
                expectedInvData = new InventoryData();
                expectedInvData.qtyTotal = 16;
                expectedInvData.qtyIn = 11;
                expectedInvData.qtyQcRequired = 0;
                expectedInvData.qtyInContainer = 0;
                expectedInvData.qtyStaged = 1;
                expectedInvData.qtyOut = 0;
                expectedInvData.qtyInRepair = 0;
                expectedInvData.qtyInTransit = 0;

                record = salesInventoryModule.newRecordsToCreate[1];
                await this.TestInventoryIntegrity(salesInventoryModule, record, expectedInvData, "020");



            }, this.testTimeout);
            //---------------------------------------------------------------------------------------
        });
    }
    //---------------------------------------------------------------------------------------
}

describe('TransfersTest', () => {
    try {
        new TransfersTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
