import { ModuleBase } from "../../shared/ModuleBase";
//import faker from 'faker';

export class Vendor extends ModuleBase {

    constructor() {
        super();
        this.moduleName = 'Vendor';
        this.moduleBtnId = '#btnModuleAE4884F4-CB21-4D10-A0B5-306BD0883F19';
        this.moduleCaption = 'Vendor';
    }

    //async populateNewVendor(): Promise<void> {
    //    await page.waitForSelector('.fwformfield[data-datafield="Vendor"]', {visible:true});
    //    await this.populateTextField("Vendor", `JEST - ${faker.company.companyName()}`);
    //    await this.populateTextField("VendorNumber", faker.random.alphaNumeric(10));
    //    await this.populateTextField("Address1", faker.address.streetAddress());
    //    await this.populateTextField("Address2", faker.address.secondaryAddress());
    //    await this.populateTextField("City", faker.address.city());
    //    await this.populateTextField("State", faker.address.state());
    //    await this.populateTextField("ZipCode", faker.address.zipCode());
    //    await this.populateTextField("Phone", faker.phone.phoneNumber());
    //    await this.populateTextField("Fax", faker.phone.phoneNumber());
    //    await this.populateTextField("WebAddress", faker.internet.url());
    //    await this.populateValidationField("OfficeLocationId", "OfficeLocationValidation", 1);
    //}

    //async populateNewVendorWithStaticVendorNo(): Promise<void> {
    //    await page.waitForSelector('.fwformfield[data-datafield="Vendor"]', {visible:true});
    //    await this.populateTextField("Vendor", `JEST - ${faker.company.companyName()}`);
    //    await this.populateTextField("VendorNumber", "1111111");
    //    await this.populateTextField("Address1", faker.address.streetAddress());
    //    await this.populateTextField("Address2", faker.address.secondaryAddress());
    //    await this.populateTextField("City", faker.address.city());
    //    await this.populateTextField("State", faker.address.state());
    //    await this.populateTextField("ZipCode", faker.address.zipCode());
    //    await this.populateTextField("Phone", faker.phone.phoneNumber());
    //    await this.populateTextField("Fax", faker.phone.phoneNumber());
    //    await this.populateTextField("WebAddress", faker.internet.url());
    //    await this.populateValidationField("OfficeLocationId", "OfficeLocationValidation", 1);
    //}
    //async populateNewVendorWithoutVendor(): Promise<void> {
    //    await page.waitForSelector('.fwformfield[data-datafield="Vendor"]', {visible:true});
    //    //await this.populateTextField("Vendor", `JEST - ${faker.company.companyName()}`);
    //    await this.populateTextField("VendorNumber", faker.random.alphaNumeric(10));
    //    await this.populateTextField("Address1", faker.address.streetAddress());
    //    await this.populateTextField("Address2", faker.address.secondaryAddress());
    //    await this.populateTextField("City", faker.address.city());
    //    await this.populateTextField("State", faker.address.state());
    //    await this.populateTextField("ZipCode", faker.address.zipCode());
    //    await this.populateTextField("Phone", faker.phone.phoneNumber());
    //    await this.populateTextField("Fax", faker.phone.phoneNumber());
    //    await this.populateTextField("WebAddress", faker.internet.url());
    //    await this.populateValidationField("OfficeLocationId", "OfficeLocationValidation", 1);
    //}
}