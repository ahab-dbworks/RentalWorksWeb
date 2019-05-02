//routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });

class AssignBarCodes {
    Module: string = 'AssignBarCodes';
    caption: string = Constants.Modules.Home.AssignBarCodes.caption;
    nav: string = Constants.Modules.Home.AssignBarCodes.nav;
    id: string = Constants.Modules.Home.AssignBarCodes.id;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
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
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
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
    };
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
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') request.uniqueids.ReceiveContractId = contractId;
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
        $form.find('[data-datafield="ContractId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'ContractDate', $tr.find('[data-browsedatafield="ContractDate"]').attr('data-originalvalue'));
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (purchaseOrderId && contractId) {
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
                    $poReceiveBarCodeGridControl.find('tbody').empty();
                }
            }, null, $form);
        });

        //Assign Bar Codes button
        $form.find('.assignbarcodes').on('click', e => {
            let request: any = {};
            request = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                , ContractId: FwFormField.getValueByDataField($form, 'ContractId')
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
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateContractNumber($browse: any, $form: any, request: any) {
        request.uniqueIds = {
            PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        };
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="assignbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Assign Bar Codes" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="AssignBarCodesController">
          <div id="checkinform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
            </div>
            <div class="tabpages">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Assign Bar Codes">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidatePONumber" style="float:left; flex:0 1 200px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PODate" style="float:left; flex:0 1 150px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="float:left; flex:0 1 250px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="float:left; flex:1 1 250px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left; flex:0 1 225px;" data-enabled="false"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract No." data-datafield="ContractId" data-displayfield="ContractNumber" data-validationname="ContractValidation" data-formbeforevalidate="beforeValidateContractNumber" style="float:left; flex:0 1 200px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Contract Date" data-datafield="ContractDate" style="float:left;  flex:0 1 150px;" data-enabled="false"></div>
                    </div>
                    <div class="flexrow" style="min-width:1400px;">
                      <div data-control="FwGrid" data-grid="POReceiveBarCodeGrid" data-securitycaption="Purchase Order Receive Bar Code"></div>
                    </div>
                    <div class="formrow" style="min-width:1400px;">
                      <div class="fwformcontrol assignbarcodes" data-type="button" style="float:left;">Assign Bar Codes</div>
                      <div class="fwformcontrol additems" data-type="button" style="float:right;">Add Items</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
var AssignBarCodesController = new AssignBarCodes();