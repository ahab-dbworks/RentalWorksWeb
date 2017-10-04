class RateLocationTaxGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'RateLocationTaxGrid';
        this.apiurl = 'api/v1/ratelocationtax';
    }
}

(<any>window).RateLocationTaxGridController = new RateLocationTaxGrid();
//----------------------------------------------------------------------------------------------