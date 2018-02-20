class OrderTypeTermsAndConditionsGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderTypeTermsAndConditions';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

var OrderTypeTermsAndConditionsGridController = new OrderTypeTermsAndConditionsGrid();
//----------------------------------------------------------------------------------------------