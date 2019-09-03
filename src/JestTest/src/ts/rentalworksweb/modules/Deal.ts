import { ModuleBase } from "../../shared/ModuleBase";

export class Deal extends ModuleBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Deal';
        this.moduleBtnId = '#btnModuleC67AD425-5273-4F80-A452-146B2008B41C';
        this.moduleCaption = 'Deal';
    }
    //---------------------------------------------------------------------------------------
   // async populateFormWithRecord(dealRecord: any): Promise<void> {

        //console.log(dealRecord, "dealRec")
        ////wait for the form to open and find the Deal field
        //await page.waitForSelector('.fwformfield[data-datafield="Deal"]', { visible: true });

        ////deal data
        //await this.populateTextField("Deal", dealRecord.Deal);
        //await this.populateTextField("DealNumber", dealRecord.DealNumber);
        //await this.populateValidationTextField("CustomerId", dealRecord.Customer);
        //await this.populateValidationField("DealTypeId", "DealTypeValidation", 1);

        //await this.clearInputField("Address2");
        //await this.populateTextField("Address2", dealRecord.Address2);

        //await this.clearInputField("Phone");
        //await this.populateTextField("Phone", dealRecord.Phone);
        //await this.clearInputField("Fax");
        //await this.populateTextField("Fax", dealRecord.Fax);

        ////billing tab
        //await this.clickTab("Billing");
        //await this.populateValidationField("DefaultRate", "RateTypeValidation", 2);

        ////credit tab
        //await this.clickTab("Credit");
        //await this.populateValidationField("CreditStatusId", "CreditStatusValidation", 1);

        //return dealRecord;
   // }
    //---------------------------------------------------------------------------------------
    //async getFormRecord(): Promise<any> {
    //    let dealRecord: any = {
    //        Deal: await this.getDataFieldValue('Deal'),
    //        DealNumber: await this.getDataFieldValue('DealNumber'),
    //        DealTypeId: await this.getDataFieldValue('DealTypeId'),
    //        DealType: await this.getDataFieldText('DealTypeId'),
    //        CustomerId: await this.getDataFieldValue('CustomerId'),
    //        Customer: await this.getDataFieldText('CustomerId'),
    //        Address1: await this.getDataFieldValue('Address1'),
    //        Address2: await this.getDataFieldValue('Address2'),
    //        City: await this.getDataFieldValue('City'),
    //        State: await this.getDataFieldValue('State'),
    //        ZipCode: await this.getDataFieldValue('ZipCode'),
    //        Phone: await this.getDataFieldValue('Phone'),
    //        Fax: await this.getDataFieldValue('Fax'),
    //    }
    //    return dealRecord;
    //}
    //---------------------------------------------------------------------------------------
}
