class SpaceWarehouseRateGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SpaceWarehouseRateGrid';
        this.apiurl = 'api/v1/ratewarehouse';
    }
}

(<any>window).SpaceWarehouseRateGridController = new SpaceWarehouseRateGrid();
//----------------------------------------------------------------------------------------------