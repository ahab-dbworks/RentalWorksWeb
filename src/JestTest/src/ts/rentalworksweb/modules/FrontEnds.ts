import { FrontEndModule } from "../../shared/FrontEndModule";
import { TestUtils } from "../../shared/TestUtils";
import { GridBase } from "../../shared/GridBase";
import { ModuleBase } from "../../shared/ModuleBase";

//---------------------------------------------------------------------------------------
export abstract class StagingBase extends FrontEndModule {
    async stageBarCode(barCode: string) {
        // input Bar Code
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="Code"] input`);
        await iCodeElementHandle.click();
        await iCodeElementHandle.focus();
        await iCodeElementHandle.click({ clickCount: 3 });
        await iCodeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(barCode);
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
    }
    //---------------------------------------------------------------------------------------
    async createContract() {
        await ModuleBase.wait(5000); // temporary
        const createContractElementHandle = await page.$(`div .createcontract .btnmenu`);
        await createContractElementHandle.click();

        // need to wait here for please wait to pop and go away - new staging messages
        await TestUtils.waitForPleaseWait();

        //if pending items exist and pop-up occurs, click "Continue" to proceed:
        var popUp;
        try {
            popUp = await page.waitForSelector('.advisory', { timeout: 5000 });
        } catch (error) { } // no pop-up

        if (popUp !== undefined) {
            //await page.waitForSelector('.advisory');
            const options = await page.$$('.advisory .fwconfirmation-button');
            await options[0].click(); // click "Continue" option
        }
        await TestUtils.waitForPleaseWait(10000);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class Staging extends StagingBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Staging';
        this.moduleId = 'H0sf3MFhL0VK';
        this.moduleCaption = 'Staging / Check-Out';
    }
    //---------------------------------------------------------------------------------------
    async loadOrder(orderNumber: string) {
        const orderNumberElementHandle = await page.$(`.fwformfield[data-datafield="OrderId"] .fwformfield-text`);
        await orderNumberElementHandle.click();
        await page.keyboard.sendCharacter(orderNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();

        // need to wait here for please wait to pop and go away - new staging messages
        await TestUtils.waitForPleaseWait();

    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class TransferOut extends StagingBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferOut';
        this.moduleId = 'uxIAX8VBtAwD';
        this.moduleCaption = 'Transfer Out';
    }
    //---------------------------------------------------------------------------------------
    async loadTransfer(transferNumber: string) {
        const transferNumberElementHandle = await page.$(`.fwformfield[data-datafield="TransferId"] .fwformfield-text`);
        await transferNumberElementHandle.click();
        await page.keyboard.sendCharacter(transferNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();

        // need to wait here for please wait to pop and go away - new staging messages
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class ReceiveFromVendor extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'ReceiveFromVendor';
        this.moduleId = 'MtgBxCKWVl7m';
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
export abstract class CheckInBase extends FrontEndModule {
    async checkInBarCode(barCode: string) {
        // input Bar Code
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="BarCode"] input`);
        await iCodeElementHandle.click();
        await iCodeElementHandle.focus();
        await iCodeElementHandle.click({ clickCount: 3 });
        await iCodeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(barCode);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
    async checkInQuantity(iCode: string, qty: number) {
        // input I-Code
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="BarCode"] input`);
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
    }
    //---------------------------------------------------------------------------------------
    async createContract(): Promise<string> {
        await ModuleBase.wait(5000); // temporary
        const createContractElementHandle = await page.$(`div .createcontract`);
        await createContractElementHandle.click();
        await TestUtils.waitForPleaseWait();

        await ModuleBase.wait(3000); 
        const contractNumberSelector = `div[data-datafield="ContractNumber"] .fwformfield-value`;
        const contractNumber = await page.$eval(contractNumberSelector, (e: any) => { return e.value });

        return contractNumber;

    }
    //---------------------------------------------------------------------------------------
    async cancelSession() {
        await this.clickMenuWithConfirmation('S8ybdjuN7MU');
    }
    //---------------------------------------------------------------------------------------
    async cancelAllItemsInGrid() {
        let gridAllRowsBoxSelector = `div [data-name="CheckedInItemGrid"] .divselectrow`;
        await page.click(gridAllRowsBoxSelector);
        await ModuleBase.wait(2000);

        let gridMenuSelector = `div [data-name="CheckedInItemGrid"] .submenubutton`;
        await page.click(gridMenuSelector);
        await ModuleBase.wait(1000);

        let cancelItemsSelector = `div [data-name="CheckedInItemGrid"] [data-securityid="8bSrfYlth57y"]`;
        await page.click(cancelItemsSelector);
        //await TestUtils.waitForPleaseWait();
        await ModuleBase.wait(10000);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
export class CheckIn extends CheckInBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'CheckIn';
        this.moduleId = 'krnJWTUs4n5U';
        this.moduleCaption = 'Check-In';
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
}
//---------------------------------------------------------------------------------------
export class TransferIn extends CheckInBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'TransferIn';
        this.moduleId = 'aVOT6HR8knES';
        this.moduleCaption = 'Transfer In';
    }
    //---------------------------------------------------------------------------------------
    async loadTransfer(transferNumber: string) {
        const transferNumberElementHandle = await page.$(`.fwformfield[data-datafield="TransferId"] .fwformfield-text`);
        await transferNumberElementHandle.click();
        await page.keyboard.sendCharacter(transferNumber);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------
export class AssignBarCodes extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'AssignBarCodes';
        this.moduleId = '7UU96BApz2Va';
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
export class Exchange extends FrontEndModule {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Exchange';
        this.moduleId = 'IQS4rxzIVFl';
        this.moduleCaption = 'Exchange';
    }
    //---------------------------------------------------------------------------------------
    async loadDeal(deal: string) {
        const poNumberElementHandle = await page.$(`.fwformfield[data-datafield="DealId"] .fwformfield-text`);
        await poNumberElementHandle.click();
        await page.keyboard.sendCharacter(deal);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
    async checkInBarCode(barCode: string) {
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="BarCodeIn"] input`);
        await iCodeElementHandle.click();
        await iCodeElementHandle.focus();
        await iCodeElementHandle.click({ clickCount: 3 });
        await iCodeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(barCode);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
    async checkOutBarCode(barCode: string) {
        const iCodeElementHandle = await page.$(`.fwformfield[data-datafield="BarCodeOut"] input`);
        await iCodeElementHandle.click();
        await iCodeElementHandle.focus();
        await iCodeElementHandle.click({ clickCount: 3 });
        await iCodeElementHandle.press('Backspace');
        await page.keyboard.sendCharacter(barCode);
        await page.keyboard.press('Enter');
        await TestUtils.waitForPleaseWait();
    }
    //---------------------------------------------------------------------------------------
    async createContract() {
        await ModuleBase.wait(5000); // temporary
        const createContractElementHandle = await page.$(`div .fwformcontrol.createcontract`);
        await createContractElementHandle.click();
        await TestUtils.waitForPleaseWait(60000);
    }
    //---------------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------------
