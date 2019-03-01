﻿class SearchPreviewGrid {
     Module: string = 'SearchPreviewGrid';
     apiurl: string = 'api/v1/inventorysearchpreview';
}
//----------------------------------------------------------------------------------------------
//Refresh Availability
FwApplicationTree.clickEvents['{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}'] = function (e) {
    const $grid = jQuery(this).closest('[data-type="Grid"]');
    const $form = jQuery(this).closest('.fwform');
    const module = $form.attr('data-controller').replace('Controller', '');
    const recType = jQuery('#searchpopup').find('[data-datafield="InventoryType"] input:checked').val();
    var defaultRows = sessionStorage.getItem('browsedefaultrows');

    const pageNumber = $grid.attr('data-pageno');
    $grid.data('ondatabind', function (request) {
        request.uniqueids = {
            OrderId: FwFormField.getValueByDataField($form, `${module}Id`),
            RecType: recType,
            RefreshAvailability: true
        };
        request.pagesize = defaultRows;
        request.pageno = pageNumber;
    });

    FwBrowse.search($grid);
    //sets refreshavailability back to its default
    $grid.data('ondatabind', function (request) {
        request.uniqueids = {
            OrderId: FwFormField.getValueByDataField($form, `${module}Id`),
            RecType: recType
        };
        request.pagesize = defaultRows;
    });
}
//----------------------------------------------------------------------------------------------
var SearchPreviewGridController = new SearchPreviewGrid();
//----------------------------------------------------------------------------------------------