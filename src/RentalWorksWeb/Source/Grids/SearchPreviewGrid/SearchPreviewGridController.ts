﻿﻿class SearchPreviewGrid {
     Module: string = 'SearchPreviewGrid';
     apiurl: string = 'api/v1/inventorysearchpreview';

     generateRow($control, $generatedtr) {
         FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
             const availabilityState = $generatedtr.find('[data-browsedatafield="AvailabilityState"]').attr('data-originalvalue');
             $generatedtr.find('[data-browsedatafield="QuantityAvailable"]').attr('data-state', availabilityState);
         });
     }
}
//----------------------------------------------------------------------------------------------
//Refresh Availability
FwApplicationTree.clickEvents[Constants.Grids.SearchPreviewGrid.menuItems.RefreshAvailability.id] = function (e: JQuery.ClickEvent) {
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