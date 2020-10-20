class PickListItemGrid {
    Module: string = 'PickListItemGrid';
    apiurl: string = 'api/v1/picklistitem';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Sub Vendor', '#ebb58e');
    }
}

var PickListItemGridController = new PickListItemGrid();
//----------------------------------------------------------------------------------------------