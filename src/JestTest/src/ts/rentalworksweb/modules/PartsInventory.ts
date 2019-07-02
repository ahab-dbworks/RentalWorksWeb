import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class PartsInventory extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'PartsInventory';
        this.moduleBtnId = '#btnModule351B8A09-7778-4F06-A6A2-ED0920A5C360';
        this.moduleCaption = 'Parts Inventory';
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
        await this.populateValidationField("CategoryId", "PartsCategoryValidation");
        //units
        await this.populateValidationField("UnitId", "UnitValidation");
        await this.populateTextField("ManufacturerPartNumber", faker.random.alphaNumeric(8));
        //rank
        await this.populateValidationField("Rank", "RankValidation", 3);
    }
}
