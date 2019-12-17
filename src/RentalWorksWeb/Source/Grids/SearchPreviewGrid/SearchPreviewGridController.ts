﻿class SearchPreviewGrid {
     Module: string = 'SearchPreviewGrid';
     apiurl: string = 'api/v1/inventorysearchpreview';

     generateRow($control, $generatedtr) {
         FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
             const availabilityState = $generatedtr.find('[data-browsedatafield="AvailabilityState"]').attr('data-originalvalue');
             $generatedtr.find('[data-browsedatafield="QuantityAvailable"]').attr('data-state', availabilityState);

             const parentIdIndex = dt.ColumnIndex.ParentId;
             const grandParentIdIndex = dt.ColumnIndex.GrandParentId;
             const parentId = dt.Rows[rowIndex][parentIdIndex];
             const grandParentId = dt.Rows[rowIndex][grandParentIdIndex];
             const iCodeColumn = $tr.find('[data-browsedatafield="ICode"]');
             const descriptionColumn = $tr.find('[data-browsedatafield="Description"]');

             if (parentId != "") {
                 if (grandParentId != "") {
                     iCodeColumn.css('text-indent', '2em');
                     descriptionColumn.css('text-indent', '2em');
                 } else {
                     iCodeColumn.css('text-indent', '1em');
                     descriptionColumn.css('text-indent', '1em');
                 }
             }
         });
     }
}
//----------------------------------------------------------------------------------------------
////Refresh Availability
//FwApplicationTree.clickEvents[Constants.Grids.SearchPreviewGrid.menuItems.RefreshAvailability.id] = function (e: JQuery.ClickEvent) {
//    const $grid = jQuery(this).closest('[data-type="Grid"]');
//    const onDataBind = $grid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $grid.data('ondatabind', function (request) {
//            onDataBind(request);
//            request.RefreshAvailability = true;
//        });
//    }

//    FwBrowse.search($grid);
//    //resets ondatabind
//    $grid.data('ondatabind', onDataBind);
//    jQuery(document).trigger('click');
//}
////----------------------------------------------------------------------------------------------
var SearchPreviewGridController = new SearchPreviewGrid();
//----------------------------------------------------------------------------------------------