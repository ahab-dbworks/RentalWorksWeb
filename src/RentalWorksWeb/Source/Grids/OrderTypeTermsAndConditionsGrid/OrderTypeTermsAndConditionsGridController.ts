class OrderTypeTermsAndConditionsGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderTypeTermsAndConditions';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

(<any>window).OrderTypeTermsAndConditionsGridController = new OrderTypeTermsAndConditionsGrid();
//----------------------------------------------------------------------------------------------