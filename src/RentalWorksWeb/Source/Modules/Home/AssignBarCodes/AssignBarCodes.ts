routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });

class AssignBarCodes {
    Module: string = 'AssignBarCodes';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Assign Bar Codes', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-AssignBarCodesForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="ContractId"] input.fwformfield-value').val(parentmoduleinfo.ContractId);
            $form.find('div[data-datafield="ContractId"] input.fwformfield-text').val(parentmoduleinfo.ContractNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-text').val(parentmoduleinfo.PurchaseOrderNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-value').val(parentmoduleinfo.PurchaseOrderId);
            jQuery($form.find('[data-datafield="ContractId"] input')).trigger('change');
            jQuery($form.find('[data-datafield="PurchaseOrderId"] input')).trigger('change');
        }

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $poReceiveBarCodeGrid: any,
            $poReceiveBarCodeGridControl: any;

        $poReceiveBarCodeGrid = $form.find('div[data-grid="POReceiveBarCodeGrid"]');
        $poReceiveBarCodeGridControl = jQuery(jQuery('#tmpl-grids-POReceiveBarCodeGridBrowse').html());
        $poReceiveBarCodeGrid.empty().append($poReceiveBarCodeGridControl);
        $poReceiveBarCodeGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderReceiveBarCodeId: FwFormField.getValueByDataField($form, 'POReceiveBarCodeId')
            }
        })
        FwBrowse.init($poReceiveBarCodeGridControl);
        FwBrowse.renderRuntimeHtml($poReceiveBarCodeGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').on('change', e => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
        });
        $form.find('[data-datafield="ContractId"]').on('change', e => {
            FwFormField.disable($form.find('[data-datafield="ContractId"]'));
        });

        $form.find('.additems').on('click', e => {
            let request: any = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                , ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/receivebarcodeadditems', request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', `${response.ItemsAdded} items added.`);
                    FwFormField.setValueByDataField($form, 'PurchaseOrderId', '', '');
                    FwFormField.setValueByDataField($form, 'ContractId', '', '');
                    FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
                    FwFormField.enable($form.find('[data-datafield="ContractId"]'));
                }
            }, null, null);
        });

    }
    //----------------------------------------------------------------------------------------------
    beforeValidatePONumber($browse: any, $form: any, request: any) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            AssignBarCodes: true
            , AssigningWarehouseId: warehouseId
        }
    };
    //----------------------------------------------------------------------------------------------
}
var AssignBarCodesController = new AssignBarCodes();