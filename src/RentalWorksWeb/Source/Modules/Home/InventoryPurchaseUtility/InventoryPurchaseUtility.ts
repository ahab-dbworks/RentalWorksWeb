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

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $itemGridControl = $form.find('[data-name="InventoryPurchaseItemGrid"]');

        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            const trackedBy = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            if (trackedBy === 'QUANTITY') {
                $form.find('.itemsgrid').hide();
            } else {
                $form.find('.itemsgrid').show();
            }

            //default unit cost
            const unitVal = $tr.find('[data-browsedatafield="UnitValue"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'UnitCost', unitVal);
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