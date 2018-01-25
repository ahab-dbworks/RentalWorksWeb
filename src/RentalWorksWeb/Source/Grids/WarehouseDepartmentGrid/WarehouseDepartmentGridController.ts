class WarehouseDepartmentGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseDepartment';
        this.apiurl = 'api/v1/warehousedepartment';
    }
}

(<any>window).WarehouseDepartmentGridController = new WarehouseDepartmentGrid();
//----------------------------------------------------------------------------------------------