class InventoryKitGrid {
    Module: string = 'InventoryPackageInventory';
    apiurl: string = 'api/v1/inventorypackageinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="DefaultQuantity"] input').val('1');
        });
    };
}

var InventoryKitGridController = new InventoryKitGrid();

//----------------------------------------------------------------------------------------------
//QuikSearch
FwApplicationTree.clickEvents[Constants.Grids.InventoryKitGrid.menuItems.QuikSearch.id] = function (e) {
    let search, $form, id;

    $form = jQuery(this).closest('.fwform');
    const controllerName = $form.attr('data-controller');
    search = new SearchInterface();

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
            (<any>window)[controllerName].saveForm($form, { closetab: false });
            $form.attr('data-opensearch', 'true');
            $form.attr('data-searchtype', gridInventoryType);
            $form.attr('data-activetabid', activeTabId);
        }
    }

    id = FwFormField.getValueByDataField($form, 'InventoryId');
    search.renderSearchPopup($form, id, 'Kit', gridInventoryType);
}
//----------------------------------------------------------------------------------------------