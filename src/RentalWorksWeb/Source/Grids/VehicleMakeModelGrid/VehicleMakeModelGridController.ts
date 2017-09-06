class VehicleMakeModelGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VehicleMakeModelGrid';
        this.apiurl = 'api/v1/vehiclemodel';
    }
}

(<any>window).VehicleMakeModelGridController = new VehicleMakeModelGrid();
//----------------------------------------------------------------------------------------------