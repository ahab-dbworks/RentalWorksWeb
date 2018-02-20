class WarehouseDepartmentGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseDepartment';
        this.apiurl = 'api/v1/warehousedepartment';
    }
}

var WarehouseDepartmentGridController = new WarehouseDepartmentGrid();
//----------------------------------------------------------------------------------------------