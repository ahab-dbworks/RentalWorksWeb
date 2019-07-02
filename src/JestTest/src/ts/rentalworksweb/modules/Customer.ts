import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Customer extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'Customer';
        this.moduleBtnId = '#btnModule214C6242-AA91-4498-A4CC-E0F3DCCCE71E';
        this.moduleCaption = 'Customer';
    }

    async populateNew(): Promise<string> {
        const customerName = `JEST - ${faker.company.companyName()}`;

        //wait for the form to open and find the Customer field
        await page.waitForSelector('.fwformfield[data-datafield="Customer"]', { visible: true });

        //customer data
        await this.populateTextField("Customer", customerName);
        await this.populateTextField("CustomerNumber", faker.random.alphaNumeric(8));
        await this.populateValidationField("CustomerTypeId", "CustomerTypeValidation", 1);
        await this.populateTextField("Address1", faker.address.streetAddress());
        await this.populateTextField("Address2", faker.address.secondaryAddress());
        await this.populateTextField("City", faker.address.city());
        await this.populateTextField("State", faker.address.state(true));
        await this.populateTextField("ZipCode", faker.address.zipCode("99999"));
        await this.populateTextField("Phone", faker.phone.phoneNumber());
        await this.populateTextField("Fax", faker.phone.phoneNumber());
        await this.populateTextField("WebAddress", faker.internet.url());

        //credit tab
        await this.clickTab("Credit");
        await this.populateValidationField("CreditStatusId", "CreditStatusValidation", 1);

        return customerName;
    }
}
