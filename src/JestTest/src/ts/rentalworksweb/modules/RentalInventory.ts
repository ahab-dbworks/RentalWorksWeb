import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class RentalInventory extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'RentalInventory';
        this.moduleBtnId = '#btnModuleFCDB4C86-20E7-489B-A8B7-D22EE6F85C06';
        this.moduleCaption = 'Rental Inventory';
    }

    async populateNew(): Promise<string> {
        //wait for the form to open and find the I-Code field
        await page.waitForSelector('.fwformfield[data-datafield="ICode"]', { visible: true });

        //icode and description
        let iCode: string = faker.random.alphaNumeric(5);
        //let iCode: string = faker.random.alphaNumeric(7);
        await this.populateTextField("ICode", iCode);
        await this.populateTextField("Description", `JEST - ${faker.commerce.productName()}`);

        //inventory type
        await this.populateValidationField("InventoryTypeId", "InventoryTypeValidation", 2);

        //category
        await this.populateValidationField("CategoryId", "RentalCategoryValidation");

        //unit
        await this.populateValidationField("UnitId", "UnitValidation");
        await this.populateTextField("ManufacturerPartNumber", faker.random.alphaNumeric(8));

        //rank
        await this.populateValidationField("Rank", "RankValidation", 3);

        iCode = await this.getDataFieldValue('ICode');
        //iCode = await this.getDataFieldText('ICode');
        console.log('iCode', iCode);

        return iCode;

    }
}
