import { FrontEndModule } from "../../shared/FrontEndModule";
import { TestUtils } from "../../shared/TestUtils";
import { GridBase } from "../../shared/GridBase";
import { ModuleBase } from "../../shared/ModuleBase";

//---------------------------------------------------------------------------------------
export class Staging extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Staging';
        this.moduleId = 'C3B5EEC9-3654-4660-AD28-20DE8FF9044D';
        this.moduleCaption = 'Staging / Check-Out';
    }
    //---------------------------------------------------------------------------------------
    async loadOrder(orderNumber: string) {
        const orderNumberElementHandle = await page.$(`.fwformfield[data-datafield="OrderId"] .fwformfield-text`);
        await orderNumberElementHandle.click();
        await page.keyboard.sendCharacter(orderNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
    async stageQuantity(iCode: string, qty: number) {
        // input I-Code
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
        await iCodeElementHandle.click();
        await iCodeElementHandle.focus();
        await iCodeElementHandle.click({ clickCount: 3 });
        await iCodeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(iCode);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();

        // input Quantity
        const qtyElementHandle = await page.$(`.fwformfield[data-datafield="Quantity"] input`);
        await qtyElementHandle.click();
        await qtyElementHandle.focus();
        await qtyElementHandle.click({ clickCount: 3 });
        await qtyElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(qty.toString());
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
        await ModuleBase.wait(5000); // temporary
    }
    //---------------------------------------------------------------------------------------
    async createContract() {
        const createContractElementHandle = await page.$(`div .createcontract .btnmenu`);
        await createContractElementHandle.click();

        //pending items exist
        await page.waitForSelector('.advisory');
        const options = await page.$$('.advisory .fwconfirmation-button');
        await options[0].click(); // click "Continue" option
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ReceiveFromVendor extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ReceiveFromVendor';
        this.moduleId = '00539824-6489-4377-A291-EBFE26325FAD';
        this.moduleCaption = 'Receive From Vendor';

        let itemsGrid: GridBase = new GridBase("Receive Items Grid", "POReceiveItemGrid");
        this.grids.push(itemsGrid);
    }
    //---------------------------------------------------------------------------------------
    async loadPo(poNumber: string) {
        const poNumberElementHandle = await page.$(`.fwformfield[data-datafield="PurchaseOrderId"] .fwformfield-text`);
        await poNumberElementHandle.click();
        await page.keyboard.sendCharacter(poNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
    async inputQuantity(row: number, qty: number) {
        let quantityElementHandle = await page.$(`${this.grids[0].gridSelector} tr.viewmode:nth-child(${row}) .field.quantity input`);
        await quantityElementHandle.click();
        await quantityElementHandle.focus();
        await quantityElementHandle.click({ clickCount: 3 });
        await quantityElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(qty.toString());
        await page.keyboard.press('Enter');
        await ModuleBase.wait(1000);
    }
    //---------------------------------------------------------------------------------------
    async createContract() {
        const createContractElementHandle = await page.$(`div .fwformcontrol.createcontract .btnmenu`);
        await createContractElementHandle.click();
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class AssignBarCodes extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AssignBarCodes';
        this.moduleId = '4B9C17DE-7FC0-4C33-B953-26FC90F32EA0';
        this.moduleCaption = 'Assign Bar Codes';

        let itemsGrid: GridBase = new GridBase("Receive Bar Codes Grid", "POReceiveBarCodeGrid");
        this.grids.push(itemsGrid);
    }
    //---------------------------------------------------------------------------------------
    async loadPo(poNumber: string) {
        const poNumberElementHandle = await page.$(`.fwformfield[data-datafield="PurchaseOrderId"] .fwformfield-text`);
        await poNumberElementHandle.click();
        await page.keyboard.sendCharacter(poNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
    async assignBarCodes() {
        const assignBarCodesElementHandle = await page.$(`div.fwformcontrol.assignbarcodes`);
        await assignBarCodesElementHandle.click();
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
    async registerBarCodesToGlobal(barCodeKey: string) {
        let barCodeCells = await page.$$(`${this.grids[0].gridSelector} tr.viewmode .field[data-browsedatafield="BarCode"]`);
        for (let bc = 1; bc <= barCodeCells.length; bc++) {
            let barCodeCellSelector = `${this.grids[0].gridSelector} tr.viewmode:nth-child(${bc}) .field[data-browsedatafield="BarCode"]`;
            let barCode = await page.$eval(barCodeCellSelector, el => el.textContent);
            this.globalScopeRef[barCodeKey + "~" + bc.toString()] = { BarCode: barCode };
        }
    }
    //---------------------------------------------------------------------------------------
    async addItems() {
        const addItemsElementHandle = await page.$(`div.fwformcontrol.additems`);
        await addItemsElementHandle.click();
        await TestUtils.waitForPleaseWait(60000);

        try {
            let toasterCloseSelector = `.advisory .messageclose`;
            await page.waitForSelector(toasterCloseSelector, { timeout: 2000 });
            await page.click(toasterCloseSelector);
            await page.waitFor(() => !document.querySelector('.advisory'));  // wait for toaster to go away
        } catch (error) { } // assume that we missed the toaster
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
