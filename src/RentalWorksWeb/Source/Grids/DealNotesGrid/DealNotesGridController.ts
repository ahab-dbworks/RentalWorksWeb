class DealNotesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Generator Type Warehouse';
        this.apiurl = 'api/v1/dealnote';
    }
}

(<any>window).DealNotesGridController = new DealNotesGrid();
//----------------------------------------------------------------------------------------------