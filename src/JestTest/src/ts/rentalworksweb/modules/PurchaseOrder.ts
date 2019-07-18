import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class PurchaseOrder extends ModuleBase {
    constructor() {
        super();
        this.moduleName = 'PurchaseOrder';
        this.moduleBtnId = '#btnModule67D8C8BB-CF55-4231-B4A2-BB308ADF18F0';
        this.moduleCaption = 'Purchase Order';
    }

    async populateNew(): Promise<string> {

        //wait for the form to open and find the PurchaseOrderNumber field
        await page.waitForSelector('.fwformfield[data-datafield="PurchaseOrderNumber"]', { visible: true });

        //Vendor field
        await this.populateValidationField("VendorId", "VendorValidation", 2);

        //PO description
        const poDescription = `JEST - ${faker.name.jobTitle()}`;
        await this.populateTextField("Description", poDescription);


        return poDescription;
    }
}