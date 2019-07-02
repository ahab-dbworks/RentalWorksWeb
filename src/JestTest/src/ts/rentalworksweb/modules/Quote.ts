import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Quote extends ModuleBase {
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleBtnId = '#btnModule4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
        this.moduleCaption = 'Quote';
    }

    async populateNew(dealName: string): Promise<string> {

        //wait for the form to open and find the QuoteNumber field
        await page.waitForSelector('.fwformfield[data-datafield="QuoteNumber"]', { visible: true });

        //quote description
        const quoteDescription = `JEST - ${faker.name.jobTitle()}`;
        await this.populateTextField("Description", quoteDescription);

        //deal data
        await this.populateValidationTextField("DealId", dealName);
        await this.populateTextField("Location", faker.address.streetName());
        await this.populateTextField("ReferenceNumber", faker.random.alphaNumeric(8));

        //Billing tab
        await this.clickTab("Billing");
        await this.populateValidationField("BillingCycleId", "BillingCycleValidation", 1);

        return quoteDescription;
    }
}