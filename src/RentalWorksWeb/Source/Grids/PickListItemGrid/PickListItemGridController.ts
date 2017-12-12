class PickListItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PickListItemGrid';
        this.apiurl = 'api/v1/picklistitem';
    }
}

(<any>window).PickListItemGridController = new PickListItemGrid();
//----------------------------------------------------------------------------------------------