class WarehouseOfficeLocationGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseOfficeLocation';
        this.apiurl = 'api/v1/warehouselocation';
    }
}

(<any>window).WarehouseOfficeLocationGridController = new WarehouseOfficeLocationGrid();
//----------------------------------------------------------------------------------------------