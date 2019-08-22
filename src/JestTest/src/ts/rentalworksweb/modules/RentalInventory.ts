import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class RentalInventory extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'RentalInventory';
        this.moduleBtnId = '#btnModuleFCDB4C86-20E7-489B-A8B7-D22EE6F85C06';
        this.moduleCaption = 'Rental Inventory';
    }

    async populateNew(testToken: string): Promise<string> {
        //wait for the form to open and find the I-Code field
        await page.waitForSelector('.fwformfield[data-datafield="ICode"]', { visible: true });

        //faker
        let iCode: string = faker.random.alphaNumeric(7);
        let description: string = `JEST - ${testToken} - ${faker.commerce.productName()}`;
        let mfgPartNo: string = faker.random.alphaNumeric(8);

        //populate fields
        await this.populateTextField("ICode", iCode);
        await this.populateTextField("Description", description);
        await this.populateValidationField("InventoryTypeId", "InventoryTypeValidation", 2);
        await this.populateValidationField("CategoryId", "RentalCategoryValidation");
        await this.populateValidationField("UnitId", "UnitValidation");
        await this.populateTextField("ManufacturerPartNumber", mfgPartNo);
        await this.populateValidationField("Rank", "RankValidation", 3);

        let iCodeWithHyphen: string = await this.getDataFieldValue('ICode');
        console.log('iCode', iCodeWithHyphen);

        return iCodeWithHyphen;
    }
}
