class ICodeGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ICodeGrid';
        this.apiurl = 'api/v1/inventorygroupinventory';
    }
}

(<any>window).ICodeGridController = new ICodeGrid();
//----------------------------------------------------------------------------------------------