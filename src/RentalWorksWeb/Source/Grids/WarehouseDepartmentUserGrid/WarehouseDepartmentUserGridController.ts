class WarehouseDepartmentUserGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseDepartmentUser';
        this.apiurl = 'api/v1/warehousedepartment';
    }
}

(<any>window).WarehouseDepartmentUserGridController = new WarehouseDepartmentUserGrid();
//----------------------------------------------------------------------------------------------