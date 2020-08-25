class ReturnToVendor {
    constructor() {
        this.Module = 'ReturnToVendor';
        this.apiurl = 'api/v1/returntovendor';
        this.caption = Constants.Modules.Warehouse.children.ReturnToVendor.caption;
        this.nav = Constants.Modules.Warehouse.children.ReturnToVendor.nav;
        this.id = Constants.Modules.Warehouse.children.ReturnToVendor.id;
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Return To Vendor', '', (e) => {
            try {
                this.CancelReturnToVendor(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    getModuleScreen() {
        var screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $form = this.openForm('EDIT');
        screen.load = function () {
            FwModule.openModuleTab($form, 'Return To Vendor', false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    openForm(mode, parentmoduleinfo) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');
        $form.find('div.caption:contains(Cancel Return To Vendor)').parent().attr('data-enabled', 'false');
        let date = new Date(), currentDate = date.toLocaleString(), currentTime = date.toLocaleTimeString();
        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);
        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber);
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
            $form.attr('data-showsuspendedsessions', 'false');
        }
        this.getItems($form);
        this.events($form);
        this.getSuspendedSessions($form);
        return $form;
    }
    CancelReturnToVendor($form) {
        try {
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Return To Vendor', 'Cancelling this Return To Vendor Session will cause all transacted items to be cancelled. Continue?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no = FwConfirmation.addButton($confirmation, 'No', true);
                $yes.on('click', () => {
                    try {
                        const request = {};
                        request.ContractId = contractId;
                        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout, response => {
                            FwConfirmation.destroyConfirmation($confirmation);
                            ReturnToVendorController.resetForm($form);
                            FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
                        }, ex => FwFunc.showError(ex), $confirmation.find('.fwconfirmationbox'));
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    getSuspendedSessions($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        let sessionType = 'RETURN';
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', `api/v1/purchaseorder/returnsuspendedsessionsexist?warehouseId=${warehouse.warehouseid}`, null, FwServices.defaultTimeout, response => {
                if (response) {
                    $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                }
            }, ex => FwFunc.showError(ex), $form);
            $form.on('click', '.suspendedsession', e => {
                SuspendedSessionController.sessionType = sessionType;
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        SessionType: sessionType,
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                    };
                });
                FwBrowse.search($browse);
                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="PurchaseOrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    const contractId = $this.find(`[data-browsedatafield="ContractId"]`).attr('data-originalvalue');
                    FwFormField.setValueByDataField($form, 'ContractId', contractId);
                    FwFormField.setValueByDataField($form, 'PurchaseOrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="PurchaseOrderId"] input').change();
                    $form.find('.suspendedsession').hide();
                    const $returnItemGrid = $form.find('div[data-name="POReturnItemGrid"]');
                    FwBrowse.search($returnItemGrid);
                    const $returnBarcodeGrid = $form.find('div[data-name="POReturnBarCodeGrid"]');
                    FwBrowse.search($returnBarcodeGrid);
                });
            });
        }
    }
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId.length === 0) {
                let request = {
                    PurchaseOrderId: purchaseOrderId
                };
                FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/startreturncontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                    let contractId = response.ContractId, $pOReturnItemGridControl;
                    FwFormField.setValueByDataField($form, 'ContractId', contractId);
                    $form.find('.suspendedsession').hide();
                    $form.find('div.caption:contains(Cancel Return To Vendor)').parent().attr('data-enabled', 'true');
                    $pOReturnItemGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                    FwBrowse.search($pOReturnItemGridControl);
                }, null, null);
            }
        });
    }
    ;
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'POReturnItemGrid',
            gridSecurityId: 'wND2psEV3OEia',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 9999,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                };
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'POReturnBarCodeGrid',
            gridSecurityId: 'JkwkAFQ4tL7q0',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    ReturnContractId: FwFormField.getValueByDataField($form, 'ContractId')
                };
            }
        });
    }
    events($form) {
        let self = this;
        let errorMsg = $form.find('.error-msg:not(.qty)');
        $form.find('.createcontract').on('click', e => {
            let date = new Date(), currentDate = date.toLocaleString(), currentTime = date.toLocaleTimeString();
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/completereturncontract/${contractId}`, null, FwServices.defaultTimeout, response => {
                try {
                    let contractInfo = {}, $contractForm;
                    contractInfo.ContractId = contractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);
                    this.resetForm($form);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
                FwFormField.setValueByDataField($form, 'Date', currentDate);
                FwFormField.setValueByDataField($form, 'Time', currentTime);
            }, null, $form);
        });
        $form.find('.selectnone').on('click', e => {
            let request = {}, quantity;
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn = $form.find('.POReturnItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityColumn = $form.find('.POReturnItemGrid [data-browsedatafield="Quantity"]');
            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($returnItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        $form.find('.selectall').on('click', e => {
            let request = {};
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($returnItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form, contractId);
        });
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                errorMsg.html('');
                const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                const $returnBarCodeGridControl = $form.find('div[data-name="POReturnBarCodeGrid"]');
                let request = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    BarCode: FwFormField.getValueByDataField($form, 'BarCode')
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/returnitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwBrowse.search($returnBarCodeGridControl);
                        FwBrowse.search($returnItemsGridControl);
                    }
                    else {
                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                    }
                    $form.find('[data-datafield="BarCode"] input').select();
                }, null, $form);
            }
        });
    }
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        switch (validationName) {
            case 'PurchaseOrderValidation':
                request.miscfields = {
                    ReturnToVendor: true,
                    ReturningWarehouseId: warehouse.warehouseid,
                };
                break;
        }
        ;
    }
    resetForm($form) {
        const errorMsg = $form.find('.error-msg:not(.qty)');
        $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
        FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
        $form.find('div[data-name="POReturnItemGrid"] tr.viewmode').empty();
        $form.find('div[data-name="POReturnBarCodeGrid"] tr.viewmode').empty();
        errorMsg.html('');
        $form.find('div.caption:contains(Cancel Return To Vendor)').parent().attr('data-enabled', 'false');
        $form.find('.suspendedsession').show();
    }
    getFormTemplate() {
        return `
        <div id="returntovendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return To Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ReturnToVendorController">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Return To Vendor">
                <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 735px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:1 1 300px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:0 1 125px;" data-enabled="false"></div>
                    </div>
                    <div class="flexrow" style="margin-left:174px;">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 100px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:0 1 125px;" data-enabled="false"></div>
                    </div>
                    </div>
                </div>
              </div>
            </div>
            <div class="flexrow" style="max-width:1800px;">
              <div class="flexcolumn" style="max-width:1225px;">
                <div class="POReturnItemGrid">
                  <div data-control="FwGrid" data-grid="POReturnItemGrid" data-securitycaption="Return Items"></div>
                </div>
              </div>
              <div class="flexcolumn" style="max-width: 575px;">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / Serial Number" data-datafield="BarCode" style="width:200px;"></div>
                <div class="error-msg"></div>
                <div class="POReturnBarCodeGrid" data-control="FwGrid" data-grid="POReturnBarCodeGrid" data-securitycaption="Bar Code Items"></div>
              </div>
            </div>
            <div class="formrow" style="max-width:1200px;">
              <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
              <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
              <div class="createcontract fwformcontrol" data-type="button" style="float:right;">Create Contract</div>
            </div>
          </div>
        </div>
        `;
    }
}
var ReturnToVendorController = new ReturnToVendor();
//# sourceMappingURL=ReturnToVendor.js.map