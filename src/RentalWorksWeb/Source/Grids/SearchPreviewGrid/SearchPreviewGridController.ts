﻿class SearchPreviewGrid {
     Module: string = 'SearchPreviewGrid';
     apiurl: string = 'api/v1/inventorysearchpreview';
}
//----------------------------------------------------------------------------------------------
//Refresh Availability
FwApplicationTree.clickEvents['{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}'] = function (e) {
    const $grid = jQuery(this).closest('[data-type="Grid"]');
    const $popup = jQuery('#searchpopup');
    const parentId = FwFormField.getValueByDataField($popup, 'ParentFormId');
    $grid.data('ondatabind', function (request) {
        request.SessionId = parentId;
        request.RefreshAvailability = true;
    });

    FwBrowse.search($grid);
    //sets refreshavailability back to its default
    $grid.data('ondatabind', function (request) {
        request.SessionId = parentId;
    });
}
//----------------------------------------------------------------------------------------------
var SearchPreviewGridController = new SearchPreviewGrid();
//----------------------------------------------------------------------------------------------