import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class SalesInventory extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'SalesInventory';
        this.moduleBtnId = '#btnModuleB0CF2E66-CDF8-4E58-8006-49CA68AE38C2';
        this.moduleCaption = 'Sales Inventory';
    }

    async populateNew(): Promise<void> {
        //wait for the form to open and find the I-Code field
        await page.waitForSelector('.fwformfield[data-datafield="ICode"]', { visible: true });
        //icode and description
        await this.populateTextField("ICode", faker.random.alphaNumeric(8));
        await this.populateTextField("Description", `JEST - ${faker.commerce.productName()}`);
        //inventory type
        await this.populateValidationField("InventoryTypeId", "InventoryTypeValidation", 1);
        //category
        await this.populateValidationField("CategoryId", "SalesCategoryValidation");
        //units
        await this.populateValidationField("UnitId", "UnitValidation");
        await this.populateTextField("ManufacturerPartNumber", faker.random.alphaNumeric(8));
        //rank
        await this.populateValidationField("Rank", "RankValidation", 3);
    }
}
