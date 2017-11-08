class TermsAndConditionsGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'TermsAndConditions';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

(<any>window).TermsAndConditionsGridController = new TermsAndConditionsGrid();
//----------------------------------------------------------------------------------------------