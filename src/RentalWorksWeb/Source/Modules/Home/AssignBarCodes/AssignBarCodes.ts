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
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                , ReceiveContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($poReceiveBarCodeGridControl);
        FwBrowse.renderRuntimeHtml($poReceiveBarCodeGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Default Department
        let department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);
        FwFormField.disable($form.find('[data-datafield="DepartmentId"]'));
        //PO No. Change
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'PODate', $tr.find('[data-browsedatafield="PurchaseOrderDate"]').attr('data-originalvalue'));

            let purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (purchaseOrderId && contractId) {
                let $poReceiveBarCodeGridControl: any;
                $poReceiveBarCodeGridControl = $form.find('[data-name="POReceiveBarCodeGrid"]');
                FwBrowse.search($poReceiveBarCodeGridControl);
            }
        });
        //Contract No. Change
        $form.find('[data-datafield="ContractId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="ContractId"]'));
            FwFormField.setValueByDataField($form, 'ContractDate', $tr.find('[data-browsedatafield="ContractDate"]').attr('data-originalvalue'));
            let purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (purchaseOrderId && contractId) {
                let $poReceiveBarCodeGridControl: any;
                $poReceiveBarCodeGridControl = $form.find('[data-name="POReceiveBarCodeGrid"]');
                FwBrowse.search($poReceiveBarCodeGridControl);
            }
        });

        //Add items button
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
                    FwFormField.setValueByDataField($form, 'PODate', '');
                    FwFormField.setValueByDataField($form, 'VendorId', '', '');
                    FwFormField.setValueByDataField($form, 'Description', '');
                    FwFormField.setValueByDataField($form, 'ContractDate', '');
                    FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
                    FwFormField.enable($form.find('[data-datafield="ContractId"]'));
                    let $poReceiveBarCodeGridControl: any;
                    $poReceiveBarCodeGridControl = $form.find('[data-name="POReceiveBarCodeGrid"]');
                    $poReceiveBarCodeGridControl.find('tbody').empty();
                }
            }, null, $form);
        });

    }
    //----------------------------------------------------------------------------------------------
    beforeValidatePONumber($browse: any, $form: any, request: any) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            AssignBarCodes: true
            , AssigningWarehouseId: warehouseId
        };
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateContractNumber($browse: any, $form: any, request: any) {
        request.uniqueIds = {
            PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        };
    }
    //----------------------------------------------------------------------------------------------
}
var AssignBarCodesController = new AssignBarCodes();