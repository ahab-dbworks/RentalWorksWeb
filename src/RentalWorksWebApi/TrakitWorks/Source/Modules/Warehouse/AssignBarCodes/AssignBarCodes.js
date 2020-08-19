class AssignBarCodes {
    constructor() {
        this.Module = 'AssignBarCodes';
        this.apiurl = 'api/v1/assignbarcodes';
        this.caption = Constants.Modules.Warehouse.children.AssignBarCodes.caption;
        this.nav = Constants.Modules.Warehouse.children.AssignBarCodes.nav;
        this.id = Constants.Modules.Warehouse.children.AssignBarCodes.id;
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    getModuleScreen() {
        var screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
    openForm(mode, parentmoduleinfo) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');
        this.events($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="ContractId"] input.fwformfield-value').val(parentmoduleinfo.ContractId);
            $form.find('div[data-datafield="ContractId"] input.fwformfield-text').val(parentmoduleinfo.ContractNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-text').val(parentmoduleinfo.PurchaseOrderNumber);
            $form.find('div[data-datafield="PurchaseOrderId"] input.fwformfield-value').val(parentmoduleinfo.PurchaseOrderId);
            jQuery($form.find('[data-datafield="ContractId"] input')).trigger('change');
            jQuery($form.find('[data-datafield="PurchaseOrderId"] input')).trigger('change');
        }
        return $form;
    }
    ;
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'POReceiveBarCodeGrid',
            gridSecurityId: 'qH0cLrQVt9avI',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                let contractid = FwFormField.getValueByDataField($form, 'ContractId');
                request.uniqueids = Object.assign({ PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId') }, (contractid != '') && { ReceiveContractId: contractid });
            }
        });
    }
    events($form) {
        const $poReceiveBarCodeGridControl = $form.find('[data-name="POReceiveBarCodeGrid"]');
        let department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);
        FwFormField.disable($form.find('[data-datafield="DepartmentId"]'));
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'PODate', $tr.find('[data-browsedatafield="PurchaseOrderDate"]').attr('data-originalvalue'));
            FwBrowse.search($poReceiveBarCodeGridControl);
        });
        $form.find('[data-datafield="ContractId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'ContractDate', $tr.find('[data-browsedatafield="ContractDate"]').attr('data-originalvalue'));
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (purchaseOrderId && contractId) {
                FwBrowse.search($poReceiveBarCodeGridControl);
            }
        });
        $form.find('.additems').on('click', e => {
            let request = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            };
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
                    $poReceiveBarCodeGridControl.find('tbody').empty();
                }
            }, null, $form);
        });
        $form.find('.assignbarcodes').on('click', e => {
            let request = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                ContractId: FwFormField.getValueByDataField($form, 'ContractId')
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/assignbarcodesfromreceive', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($poReceiveBarCodeGridControl);
            }, null, $form);
        });
    }
    beforeValidatePONumber($browse, $form, request) {
        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            AssignBarCodes: true,
            AssigningWarehouseId: warehouseId
        };
    }
    ;
    beforeValidateContractNumber($browse, $form, request) {
        request.uniqueIds = {
            PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        };
    }
    getFormTemplate() {
        return `
        <div id="assignbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Assign Bar Codes" data-hasaudit="false" data-controller="AssignBarCodesController">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Barcodes / Serial Numbers" style="flex:1 1 750px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidatePONumber" style="flex:1 1 125px;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PODate" style="flex:1 1 125px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 350px;" data-enabled="false"></div>
                </div>
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 350px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract No." data-datafield="ContractId" data-displayfield="ContractNumber" data-validationname="ContractValidation" data-formbeforevalidate="beforeValidateContractNumber" style="float:left; flex:0 1 200px;"></div>
                </div>
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="POReceiveBarCodeGrid" data-securitycaption="Purchase Order Receive Bar Code"></div>
                </div>
                <div class="flexrow" style="margin-top:15px;justify-content:center;">
                  <div class="fwformcontrol assignbarcodes" data-type="button" style="flex: 0 0 150px;">Assign Barcodes</div>
                  <div class="fwformcontrol additems" data-type="button" style="flex: 0 0 150px;margin:0 0 0 20px;">Add Items</div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
var AssignBarCodesController = new AssignBarCodes();
//# sourceMappingURL=AssignBarCodes.js.map