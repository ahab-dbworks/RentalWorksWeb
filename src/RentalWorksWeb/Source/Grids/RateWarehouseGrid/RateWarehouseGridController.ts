class RateWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'RateWarehouseGrid';
        this.apiurl = 'api/v1/ratewarehouse';
    }
}

(<any>window).RateWarehouseGridController = new RateWarehouseGrid();
//----------------------------------------------------------------------------------------------