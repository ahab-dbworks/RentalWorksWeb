class InventoryContainerItemGrid {
    Module: string = 'InventoryContainerItemGrid';
    apiurl: string = 'api/v1/inventorycontaineritem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val(1);
            $generatedtr.find('.field[data-browsedatafield="TrackedBy"]').text($tr.find('.field[data-browsedatafield="TrackedBy"]').attr('data-originalvalue'));
        });
    };
}
//----------------------------------------------------------------------------------------------
var InventoryContainerItemGridController = new InventoryContainerItemGrid();
//----------------------------------------------------------------------------------------------
//QuikSearch
FwApplicationTree.clickEvents[Constants.Grids.InventoryContainerItemGrid.menuItems.QuikSearch.id] = function (e) {
    const $form = jQuery(this).closest('.fwform');
    const controllerName = $form.attr('data-controller');
    const search = new SearchInterface();

    let gridInventoryType;
    if (controllerName === 'RentalInventoryController') {
        gridInventoryType = 'Rental';
    } else if (controllerName === 'SalesInventoryController') {
        gridInventoryType = 'Sales';
    }

    if ($form.attr('data-mode') === 'NEW') {
        let isValid = FwModule.validateForm($form);
        if (isValid) {
            let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
            window[controllerName].saveForm($form, { closetab: false });
            $form.attr('data-opensearch', 'true');
            $form.attr('data-searchtype', gridInventoryType);
            $form.attr('data-activetabid', activeTabId);
        }
    }

    const id = FwFormField.getValueByDataField($form, 'InventoryId');
    search.renderSearchPopup($form, id, 'Container', gridInventoryType);
}
//----------------------------------------------------------------------------------------------
