import { ModuleBase } from "../../shared/ModuleBase";

export class Customer extends ModuleBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Customer';
        this.moduleBtnId = '#btnModule214C6242-AA91-4498-A4CC-E0F3DCCCE71E';
        this.moduleCaption = 'Customer';
    }
    //---------------------------------------------------------------------------------------
    //async populateFormWithRecord(customerRecord: any): Promise<void> {

    //    //wait for the form to open and find the Customer field
    //    await page.waitForSelector('.fwformfield[data-datafield="Customer"]', { visible: true });

    //    //customer data
    //    await this.populateTextField("Customer", customerRecord.Customer);
    //    await this.populateTextField("CustomerNumber", customerRecord.CustomerNumber);
    //    await this.populateValidationField("CustomerTypeId", "CustomerTypeValidation", 1);
    //    await this.populateTextField("Address1", customerRecord.Address1);
    //    await this.populateTextField("Address2", customerRecord.Address2);
    //    await this.populateTextField("City", customerRecord.City);
    //    await this.populateTextField("State", customerRecord.State);
    //    await this.populateTextField("ZipCode", customerRecord.ZipCode);
    //    await this.populateTextField("Phone", customerRecord.Phone);
    //    await this.populateTextField("Fax", customerRecord.Fax);
    //    await this.populateTextField("WebAddress", customerRecord.WebAddress);

    //    //credit tab
    //    await this.clickTab("Credit");
    //    await this.populateValidationField("CreditStatusId", "CreditStatusValidation", 1);
    //}
    //---------------------------------------------------------------------------------------
    //async getFormRecord(): Promise<any> {
    //    let customerRecord: any = {
    //        Customer: await this.getDataFieldValue('Customer'),
    //        CustomerNumber: await this.getDataFieldValue('CustomerNumber'),
    //        CustomerTypeId: await this.getDataFieldValue('CustomerTypeId'),
    //        CustomerType: await this.getDataFieldText('CustomerTypeId'),
    //        Address1: await this.getDataFieldValue('Address1'),
    //        Address2: await this.getDataFieldValue('Address2'),
    //        City: await this.getDataFieldValue('City'),
    //        State: await this.getDataFieldValue('State'),
    //        ZipCode: await this.getDataFieldValue('ZipCode'),
    //        Phone: await this.getDataFieldValue('Phone'),
    //        Fax: await this.getDataFieldValue('Fax'),
    //        WebAddress: await this.getDataFieldValue('WebAddress'),
    //        CreditStatusId: await this.getDataFieldValue('CreditStatusId'),
    //        CreditStatus: await this.getDataFieldText('CreditStatusId')
    //    }
    //    return customerRecord;
    //}
    //---------------------------------------------------------------------------------------
}
