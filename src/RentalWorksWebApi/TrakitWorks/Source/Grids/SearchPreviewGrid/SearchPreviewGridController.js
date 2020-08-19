class SearchPreviewGrid {
    constructor() {
        this.Module = 'SearchPreviewGrid';
        this.apiurl = 'api/v1/inventorysearchpreview';
    }
}
FwApplicationTree.clickEvents[Constants.Grids.SearchPreviewGrid.menuItems.RefreshAvailability.id] = function (e) {
    const $grid = jQuery(this).closest('[data-type="Grid"]');
    const onDataBind = $grid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $grid.data('ondatabind', function (request) {
            onDataBind(request);
            request.RefreshAvailability = true;
        });
    }
    FwBrowse.search($grid);
    $grid.data('ondatabind', onDataBind);
    jQuery(document).trigger('click');
};
var SearchPreviewGridController = new SearchPreviewGrid();
//# sourceMappingURL=SearchPreviewGridController.js.map