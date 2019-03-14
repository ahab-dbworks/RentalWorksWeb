﻿class SearchPreviewGrid {
     Module: string = 'SearchPreviewGrid';
     apiurl: string = 'api/v1/inventorysearchpreview';
}
//----------------------------------------------------------------------------------------------
//Refresh Availability
FwApplicationTree.clickEvents['{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}'] = function (e) {
    const $grid = jQuery(this).closest('[data-type="Grid"]');
    const onDataBind = $grid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $grid.data('ondatabind', function (request) {
            onDataBind(request);
            request.RefreshAvailability = true;
        });
    }

    FwBrowse.search($grid);
    //resets ondatabind
    $grid.data('ondatabind', onDataBind);
    jQuery(document).trigger('click');
}
//----------------------------------------------------------------------------------------------
var SearchPreviewGridController = new SearchPreviewGrid();
//----------------------------------------------------------------------------------------------