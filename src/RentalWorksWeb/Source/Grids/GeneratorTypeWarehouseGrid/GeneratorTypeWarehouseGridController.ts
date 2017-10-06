class GeneratorTypeWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Generator Type Warehouse';
        this.apiurl = 'api/v1/generatortypewarehouse';
    }
}

(<any>window).GeneratorTypeWarehouseGridController = new GeneratorTypeWarehouseGrid();
//----------------------------------------------------------------------------------------------