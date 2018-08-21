class CheckedInItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CheckedInItemGrid';
        this.apiurl = 'api/v1/checkedinitem';
    }

    addLegend($control) {
        FwBrowse.addLegend($control, 'Swapped Item', '#dc407e');
    }
}

var CheckedInItemGridController = new CheckedInItemGrid();
//----------------------------------------------------------------------------------------------