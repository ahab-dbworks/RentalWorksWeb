class FloorGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FloorGrid';
        this.apiurl = 'api/v1/floor';
    }
}

(<any>window).FloorGridController = new FloorGrid();
//----------------------------------------------------------------------------------------------