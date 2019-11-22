routes.push({ pattern: /^module\/inventorypurchaseutility$/, action: function (match: RegExpExecArray) { return InventoryPurchaseUtilityController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class InventoryPurchaseUtility {
    Module: string = 'InventoryPurchaseUtility';
    caption: string = Constants.Modules.Home.InventoryPurchaseUtility.caption;
    nav: string = Constants.Modules.Home.InventoryPurchaseUtility.nav;
    id: string = Constants.Modules.Home.InventoryPurchaseUtility.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');
        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);
        FwFormField.setValueByDataField($form, 'Quantity', 1);

        const $manufacturerValidation = $form.find('[data-datafield="ManufacturerVendorId"]');
        $manufacturerValidation.data('beforevalidate', ($form, $manufacturerValidation, request) => {
            request.uniqueids = {
                'RentalInventory': true
            }
        });

        const $purchaseValidation = $form.find('[data-datafield="PurchaseVendorId"]');
        $purchaseValidation.data('beforevalidate', ($form, $purchaseValidation, request) => {
            request.uniqueids = {
                'RentalInventory': true
            }
        });

        const $rentalInventoryValidation = $form.find('.icode[data-datafield="InventoryId"]');
        $rentalInventoryValidation.data('beforevalidate', ($form, $rentalInventoryValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid
            }
        });

        const $rentalInventoryDescValidation = $form.find('.description[data-datafield="InventoryId"]');
        $rentalInventoryDescValidation.data('beforevalidate', ($form, $rentalInventoryDescValidation, request) => {
            request.uniqueids = {
                'WarehouseId': warehouse.warehouseid
            }
        });

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $itemGridControl = $form.find('[data-name="InventoryPurchaseItemGrid"]');

        $form.find('.description[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'InventoryId', FwBrowse.getValueByDataField(null, $tr, 'InventoryId'), FwBrowse.getValueByDataField(null, $tr, 'ICode'));
            $form.find('.icode[data-datafield="InventoryId"]').data('onchange')($tr)
        });

        $form.find('.icode[data-datafield="InventoryId"]').data('onchange', $tr => {
            const trackedBy = FwBrowse.getValueByDataField(null, $tr, 'TrackedBy');
            if (trackedBy === 'QUANTITY') {
                $form.find('.tracked-by').hide();
            } else {
                $form.find('.tracked-by').show();
            }
            $form.find('.additems').show();
            const inventoryId = FwBrowse.getValueByDataField(null, $tr, 'InventoryId');
            const description = FwBrowse.getValueByDataField(null, $tr, 'Description');
            FwFormField.setValue2($form.find('.icode[data-datafield="InventoryId"]'), inventoryId, description);
            const unitVal = FwBrowse.getValueByDataField(null, $tr, 'UnitValue');
            FwFormField.setValueByDataField($form, 'UnitCost', unitVal);
            const aisleLoc = FwBrowse.getValueByDataField(null, $tr, 'AisleLocation');
            FwFormField.setValueByDataField($form, 'AisleLocation', aisleLoc);
            const shelfLoc = FwBrowse.getValueByDataField(null, $tr, 'ShelfLocation');
            FwFormField.setValueByDataField($form, 'ShelfLocation', shelfLoc);

        });

        $form.find('[data-datafield="WarrantyDays"]').on('change', e => {
            const days = FwFormField.getValueByDataField($form, 'WarrantyDays');
            const today = FwFunc.getDate();
            const expiration = FwFunc.getDate(today, parseInt(days));
            FwFormField.setValueByDataField($form, 'WarrantyExpiration', expiration);
        });

        $form.find('[data-datafield="ManufacturerVendorId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'CountryId', FwBrowse.getValueByDataField(null, $tr, 'CountryId'), FwBrowse.getValueByDataField(null, $tr, 'Country'));
        });


        //Add items button
        //$form.find('.additems').on('click', e => {
        //    let request: any = {};
        //    request = {
        //        PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //        , ContractId: FwFormField.getValueByDataField($form, 'ContractId')
        //    }
        //    FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/receivebarcodeadditems', request, FwServices.defaultTimeout,
        //        response => {
        //        if (response.success) {
        //            FwNotification.renderNotification('SUCCESS', `${response.ItemsAdded} items added.`);
        //            FwFormField.setValueByDataField($form, 'PurchaseOrderId', '', '');
        //            FwFormField.setValueByDataField($form, 'ContractId', '', '');
        //            FwFormField.setValueByDataField($form, 'PODate', '');
        //            FwFormField.setValueByDataField($form, 'VendorId', '', '');
        //            FwFormField.setValueByDataField($form, 'Description', '');
        //            FwFormField.setValueByDataField($form, 'ContractDate', '');
        //            FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
        //            FwFormField.enable($form.find('[data-datafield="ContractId"]'));
        //            $itemGridControl.find('tbody').empty();
        //        }
        //    }, ex => FwFunc.showError(ex), $form);
        //});

        //Assign Bar Codes button
        //$form.find('.assignbarcodes').on('click', e => {
        //    let request: any = {};
        //    request = {
        //        PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //        , ContractId: FwFormField.getValueByDataField($form, 'ContractId')
        //    }
        //    FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/assignbarcodesfromreceive', request, FwServices.defaultTimeout,
        //        response => {
        //        FwBrowse.search($itemGridControl);
        //    }, ex => FwFunc.showError(ex), $form);
        //});
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        const $itemGrid = $form.find('div[data-grid="InventoryPurchaseItemGrid"]');
        const $itemGridControl = FwBrowse.loadGridFromTemplate('InventoryPurchaseItemGrid');
        $itemGrid.empty().append($itemGridControl);
        $itemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PickListId: FwFormField.getValueByDataField($form, 'PickListId')
            };
        });
        FwBrowse.init($itemGridControl);
        FwBrowse.renderRuntimeHtml($itemGridControl);
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var InventoryPurchaseUtilityController = new InventoryPurchaseUtility();