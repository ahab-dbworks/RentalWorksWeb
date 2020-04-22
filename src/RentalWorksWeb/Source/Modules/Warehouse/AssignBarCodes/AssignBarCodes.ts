//routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });

class AssignBarCodes {
    Module: string = 'AssignBarCodes';
    apiurl: string = 'api/v1/assignbarcodes';
    caption: string = Constants.Modules.Warehouse.children.AssignBarCodes.caption;
    nav: string = Constants.Modules.Warehouse.children.AssignBarCodes.nav;
    id: string = Constants.Modules.Warehouse.children.AssignBarCodes.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
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
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        FwBrowse.renderGrid({
            nameGrid: 'POReceiveBarCodeGrid',
            gridSecurityId: 'qH0cLrQVt9avI',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 50,  // for regression test to be able to see all bar codes
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = true;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                const contractid = FwFormField.getValueByDataField($form, 'ContractId');
                request.uniqueids = {
                    WarehouseId: warehouseId,
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    ...(contractid != '') && { ReceiveContractId: contractid }
                };
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $browse.on('keydown', '[data-browsedatafield="BarCode"], [data-browsedatafield="SerialNumber"]', e => {
                    const keycode = e.keyCode || e.which;
                    if (keycode === 13) {
                        const $tr = jQuery(e.currentTarget).parents('tr');
                        let $nextRow = FwBrowse.selectNextRow($browse);
                        const nextIndex = FwBrowse.getSelectedIndex($browse);
                        FwBrowse.saveRow($browse, $tr)
                            .then((value) => {
                                if (nextIndex != -1) {
                                    $nextRow = FwBrowse.selectRowByIndex($browse, nextIndex);
                                    FwBrowse.setRowEditMode($browse, $nextRow);
                                    $browse.data('selectedfield', 'BarCode');
                                }
                            });
                    }
                });
            }
        });
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
            const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            const request: any = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                WarehouseId: warehouseId
            }
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/additems`, request, FwServices.defaultTimeout, function onSuccess(response) {
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
            const request: any = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
            }
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/assignbarcodes`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($poReceiveBarCodeGridControl);
            }, null, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'PurchaseOrderId':
                const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                request.miscfields = {
                    AssignBarCodes: true,
                    AssigningWarehouseId: warehouseId
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchaseorder`);
                break;
            case 'ContractId':
                request.uniqueids = {
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontract`);
                break;
        }

    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="assignbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Assign Bar Codes" data-hasaudit="false" data-controller="AssignBarCodesController">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bar Codes / Serial Numbers" style="flex:1 1 1400px;min-width:1400px;max-width:1400px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="flex:1 1 125px;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PODate" style="flex:1 1 125px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 350px;" data-enabled="false"></div>
                </div>
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 350px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract No." data-datafield="ContractId" data-displayfield="ContractNumber" data-validationname="ContractValidation" style="float:left; flex:0 1 200px;"></div>
                </div>
                <div class="flexrow" style="min-width:1475px;max-width:1475px;">
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