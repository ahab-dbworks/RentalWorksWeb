class InventorySequenceItemsGrid {
    Module: string = 'InventorySequenceItemsGrid';
    apiurl: string = 'api/v1/generalitem';
    //----------------------------------------------------------------------------------------------
    sortItems($form: any, $grid: any, sortByField: string) {
        if (!$grid.hasClass('sort-mode')) {
            $grid.find('div.btn-manualsort').click();
        }
        const $items = $grid.find('tbody tr');
        const $sortedArray = $items.sort((a, b) => jQuery(a).find(`[data-browsedatafield="${sortByField}"]`).attr('data-originalvalue')
                            > jQuery(b).find(`[data-browsedatafield="${sortByField}"]`).attr('data-originalvalue')
                            ? 1 : -1);

        $grid.find('tbody').append($sortedArray);
        const $gridMenu = $grid.find('[data-control="FwMenu"]');
        $gridMenu.find('.sorting').show();
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var InventorySequenceItemsGridController = new InventorySequenceItemsGrid();
//----------------------------------------------------------------------------------------------