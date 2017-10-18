class ItemQcGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ItemQcGrid';
        this.apiurl = 'api/v1/itemqc';
    }
}

(<any>window).ItemQcGridController = new ItemQcGrid();
//----------------------------------------------------------------------------------------------