class POReceiveBarCodeGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'POReceiveBarCodeGrid';
        this.apiurl = 'api/v1/purchaseorderreceivebarcode';
    }
}

var POReceiveBarCodeGridController = new POReceiveBarCodeGrid();
//----------------------------------------------------------------------------------------------