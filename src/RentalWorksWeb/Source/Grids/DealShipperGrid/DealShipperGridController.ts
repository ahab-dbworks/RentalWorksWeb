class DealShipperGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DealShipperGrid';
        this.apiurl = 'api/v1/dealshipper';
    }
}

(<any>window).DealShipperGridController = new DealShipperGrid();
//----------------------------------------------------------------------------------------------