class CheckedInItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CheckedInItemGrid';
        this.apiurl = 'api/v1/checkedinitem';
    }
}

var CheckedInItemGridController = new CheckedInItemGrid();
//----------------------------------------------------------------------------------------------