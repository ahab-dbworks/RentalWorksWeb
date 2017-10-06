class VehicleTypeWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VehicleTypeWarehouseGrid';
        this.apiurl = 'api/v1/vehicletypewarehouse';
    }
}

(<any>window).VehicleTypeWarehouseGridController = new VehicleTypeWarehouseGrid();
//----------------------------------------------------------------------------------------------