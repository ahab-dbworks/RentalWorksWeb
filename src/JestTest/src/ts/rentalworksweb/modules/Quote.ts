import { ModuleBase } from "../../shared/ModuleBase";
import faker from 'faker';

export class Quote extends ModuleBase {
    //---------------------------------------------------------------------------------------
    constructor() {
        super();
        this.moduleName = 'Quote';
        this.moduleBtnId = '#btnModule4D785844-BE8A-4C00-B1FA-2AA5B05183E5';
        this.moduleCaption = 'Quote';
    }
    //---------------------------------------------------------------------------------------
   // async populateFormWithRecord(quoteRecord: any): Promise<void> {

        //wait for the form to open and find the QuoteNumber field
       // await page.waitForSelector('.fwformfield[data-datafield="QuoteNumber"]', { visible: true });

        //quote description
       // await this.populateTextField("Description", quoteRecord.Description);
       // await this.populateValidationTextField("DealId", quoteRecord.Deal);
       // await this.populateTextField("Location", quoteRecord.Location);
       // await this.populateTextField("ReferenceNumber", quoteRecord.ReferenceNumber);
        //await this.clickTab("Billing");
        //await this.populateValidationField("BillingCycleId", "BillingCycleValidation", 1);

        //return quoteDescription;
   // }
    //---------------------------------------------------------------------------------------
    //async getFormRecord(): Promise<any> {
    //    let quoteRecord: any = {
    //        Deal: await this.getDataFieldText('DealId'),
    //        DealNumber: await this.getDataFieldValue('DealNumber'),
    //        QuoteNumber: await this.getDataFieldValue('QuoteNumber'),
    //        Description: await this.getDataFieldValue('Description'),
    //        Location: await this.getDataFieldValue('Location'),
    //        ReferenceNumber: await this.getDataFieldValue('ReferenceNumber')
    //    }
    //    return quoteRecord;
    //}
    //---------------------------------------------------------------------------------------
}