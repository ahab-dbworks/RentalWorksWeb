routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarcodesController.getModuleScreen(); } });

class AssignBarcodes {
    Module:                    string = 'AssignBarcodes';
    caption:                   string = 'Assign Barcodes';
    nav:                       string = 'module/assignbarcodes';
    id:                        string = '81B0D93C-9765-4340-8B40-63040E0343B8';
    successSoundFileName:      string;
    errorSoundFileName:        string;
    notificationSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Assign Barcodes', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            //$form.find('div[data-datafield="ContractId"] input.fwformfield-value').val(parentmoduleinfo.ContractId);
            //$form.find('div[data-datafield="ContractId"] input.fwformfield-text').val(parentmoduleinfo.ContractNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-text').val(parentmoduleinfo.PurchaseOrderNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-value').val(parentmoduleinfo.PurchaseOrderId);
            //jQuery($form.find('[data-datafield="ContractId"] input')).trigger('change');
            jQuery($form.find('[data-datafield="PurchaseOrderId"] input')).trigger('change');
        }
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $poReceiveBarCodeGrid: any,
            $poReceiveBarCodeGridControl: any;

        $poReceiveBarCodeGrid = $form.find('div[data-grid="POReceiveBarCodeGrid"]');
        $poReceiveBarCodeGridControl = FwBrowse.loadGridFromTemplate('POReceiveBarCodeGrid');
        $poReceiveBarCodeGrid.empty().append($poReceiveBarCodeGridControl);
        $poReceiveBarCodeGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
            }
            //const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            //if (contractId != '') request.uniqueids.ReceiveContractId = contractId;
        })
        FwBrowse.init($poReceiveBarCodeGridControl);
        FwBrowse.renderRuntimeHtml($poReceiveBarCodeGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $poReceiveBarCodeGridControl = $form.find('[data-name="POReceiveBarCodeGrid"]');

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
            FwBrowse.search($poReceiveBarCodeGridControl);

        });
        //Contract No. Change
        //$form.find('[data-datafield="ContractId"]').data('onchange', $tr => {
        //    FwFormField.setValueByDataField($form, 'ContractDate', $tr.find('[data-browsedatafield="ContractDate"]').attr('data-originalvalue'));
        //    const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
        //    const contractId = FwFormField.getValueByDataField($form, 'ContractId');
        //    if (purchaseOrderId && contractId) {
        //        FwBrowse.search($poReceiveBarCodeGridControl);
        //    }
        //});

        //Add items button
        $form.find('.additems').on('click', e => {
            let request: any = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                //, ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/receivebarcodeadditems', request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {
                    FwNotification.renderNotification('SUCCESS', `${response.ItemsAdded} items added.`);
                    FwFormField.setValueByDataField($form, 'PurchaseOrderId', '', '');
                    //FwFormField.setValueByDataField($form, 'ContractId', '', '');
                    FwFormField.setValueByDataField($form, 'PODate', '');
                    FwFormField.setValueByDataField($form, 'VendorId', '', '');
                    FwFormField.setValueByDataField($form, 'Description', '');
                    //FwFormField.setValueByDataField($form, 'ContractDate', '');
                    FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
                    //FwFormField.enable($form.find('[data-datafield="ContractId"]'));
                    $poReceiveBarCodeGridControl.find('tbody').empty();
                }
            }, null, $form);
        });

        //Assign Bar Codes button
        $form.find('.assignbarcodes').on('click', e => {
            let request: any = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                //, ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/assignbarcodesfromreceive', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($poReceiveBarCodeGridControl);
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
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateContractNumber($browse: any, $form: any, request: any) {
        request.uniqueIds = {
            PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        };
    }
    //----------------------------------------------------------------------------------------------
}
var AssignBarcodesController = new AssignBarcodes();
