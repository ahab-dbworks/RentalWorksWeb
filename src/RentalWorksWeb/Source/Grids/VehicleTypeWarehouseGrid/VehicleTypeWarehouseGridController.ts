class VehicleTypeWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Vehicle Type Warehouse';
        this.apiurl = 'api/v1/vehicletypewarehouse';
    }
}

(<any>window).VehicleTypeWarehouseGridController = new VehicleTypeWarehouseGrid();
//----------------------------------------------------------------------------------------------