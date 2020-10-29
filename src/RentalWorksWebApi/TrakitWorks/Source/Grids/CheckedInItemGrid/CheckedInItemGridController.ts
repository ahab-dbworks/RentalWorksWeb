class CheckedInItemGrid {
    Module: string = 'CheckedInItemGrid';
    apiurl: string = 'api/v1/checkedinitem';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Swapped Item', '#dc407e');
        FwBrowse.addLegend($control, 'Sub Vendor', '#ebb58e');
        FwBrowse.addLegend($control, 'Consignor', '#857cfa');
    }
}

var CheckedInItemGridController = new CheckedInItemGrid();
//----------------------------------------------------------------------------------------------