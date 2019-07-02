import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Deal extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'Deal';
        this.moduleBtnId = '#btnModuleC67AD425-5273-4F80-A452-146B2008B41C';
        this.moduleCaption = 'Deal';
    }

    async populateNew(customerName: string): Promise<string> {
        const dealName = `JEST - ${faker.company.companyName()}`;

        //wait for the form to open and find the Deal field
        await page.waitForSelector('.fwformfield[data-datafield="Deal"]', { visible: true });

        //deal data
        await this.populateTextField("Deal", dealName);
        await this.populateTextField("DealNumber", faker.random.alphaNumeric(8));
        await this.populateValidationTextField("CustomerId", customerName);
        await this.populateValidationField("DealTypeId", "DealTypeValidation", 1);

        // address fields are defaulted from customer
        //await this.populateTextField("Address1", faker.address.streetAddress());
        //await this.populateTextField("Address2", faker.address.secondaryAddress());
        //await this.populateTextField("City", faker.address.city());
        //await this.populateTextField("State", faker.address.state(true));
        //await this.populateTextField("ZipCode", faker.address.zipCode("99999"));

        await this.clearInputField("Address2");
        await this.populateTextField("Address2", faker.address.secondaryAddress());

        await this.populateTextField("Phone", faker.phone.phoneNumber());
        await this.populateTextField("Fax", faker.phone.phoneNumber());

        //billing tab
        await this.clickTab("Billing");
        await this.populateValidationField("DefaultRate", "RateTypeValidation", 2);

        ////credit tab
        //await this.clickTab("Credit");
        //await this.populateValidationField("CreditStatusId", "CreditStatusValidation", 1);

        return dealName;
    }
}