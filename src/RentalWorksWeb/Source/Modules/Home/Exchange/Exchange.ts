class Exchange {
    Module: string = 'Exchange';
    apiurl: string = 'api/v1/exchange';
    caption: string = Constants.Modules.Home.Exchange.caption;
    nav: string = Constants.Modules.Home.Exchange.nav;
    id: string = Constants.Modules.Home.Exchange.id;
    ContractId: string = '';
    ExchangeResponse: any = {};
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    Type: string = 'Order';

    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, `${this.caption}`, false, 'FORM', true);
            this.ContractId = '';
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        const department = JSON.parse(sessionStorage.getItem('department'));

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);

        if (department.department === 'RENTALS') {
            $form.find('div[data-validationname="RentalInventoryValidation"]').show();
        }
        if (department.department === 'SALES') {
            $form.find('div[data-validationname="SalesInventoryValidation"]').show();
        }

        const cancelMenuOptionId = Constants.Modules.Home.Exchange.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
        $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'false');

        this.getSoundUrls($form);
        this.getSuspendedSessions($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/exchange/suspendedsessionsexist', null, FwServices.defaultTimeout,
                response => {
                    if (response) {
                        $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                    }
                }, ex => FwFunc.showError(ex), $form);

            $form.on('click', '.suspendedsession', e => {
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid
                        , SessionType: 'EXCHANGE'
                        , OrderType: 'O'
                    }
                });
                FwBrowse.search($browse);

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    let dealId = $this.find(`[data-browsedatafield="DealId"]`).attr('data-originalvalue');
                    let dealNumber = $this.find(`[data-browsedatafield="DealNumber"]`).attr('data-originalvalue');
                    this.ContractId = $this.find(`[data-browsedatafield="ContractId"]`).attr('data-originalvalue');
                    if (dealId !== "") {
                        FwFormField.setValueByDataField($form, 'DealId', dealId, dealNumber);
                    }
                    FwFormField.setValueByDataField($form, 'OrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="OrderId"] input').change();
                    FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                    FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                    $form.find('.suspendedsession').hide();

                    const $exchangeItemGrid = $form.find('div[data-name="ExchangeItemGrid"]');
                    FwBrowse.search($exchangeItemGrid);
                });
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form: JQuery): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        let errorSound, successSound, department, contractRequest = {}, exchangeRequest = {};

        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        department = JSON.parse(sessionStorage.getItem('department'));

        contractRequest['DepartmentId'] = department.departmentid;

        const cancelMenuOptionId = Constants.Modules.Home.Exchange.form.menuItems.Cancel.id.replace('{', '').replace('}', '');

        // Deal Id
        $form.find('div[data-datafield="DealId"]').data('onchange', $tr => {
            contractRequest['OrderId'] = FwFormField.getValueByDataField($form, "OrderId");
            contractRequest['DealId'] = FwFormField.getValueByDataField($form, "DealId");

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, response => {
                    if (this.ContractId === '') {
                        this.ContractId = response.ContractId
                        $form.find('.suspendedsession').hide();
                        $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'true');
                    }

                    let $exchangeItemGridControl: any;
                    $exchangeItemGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                    $exchangeItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: response.ContractId
                        }
                    })
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                    FwFormField.getDataField($form, 'OrderId').find('input').focus();
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
                errorSound.play();
            }
        });
        // Order Id
        $form.find('div[data-datafield="OrderId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'DealId', $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            contractRequest['OrderId'] = FwFormField.getValueByDataField($form, "OrderId");
            contractRequest['DealId'] = FwFormField.getValueByDataField($form, "DealId");
            FwFormField.setValueByDataField($form, 'Description', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));

            if (this.ContractId === '') {
                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, response => {
                        if (this.ContractId === '') {
                            this.ContractId = response.ContractId
                            $form.find('.suspendedsession').hide();
                            $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'true');
                        }

                        FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                        FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                        FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        // Check-Out
        $form.find('.out').on('keypress', e => {
            if (e.which === 13) {
                exchangeRequest['ContractId'] = this.ContractId;
                exchangeRequest['OutCode'] = FwFormField.getValueByDataField($form, 'BarCodeOut');
                exchangeRequest['InCode'] = FwFormField.getValueByDataField($form, 'BarCodeIn');

                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitemout", exchangeRequest, FwServices.defaultTimeout, response => {
                        if (response.success) {
                            successSound.play();
                            FwFormField.setValueByDataField($form, 'ICodeOut', response.ItemStatus.ICode);
                            FwFormField.setValueByDataField($form, 'DescriptionOut', response.ItemStatus.Description);
                            FwFormField.setValueByDataField($form, 'WarehouseIdOut', response.ItemStatus.WarehouseId, response.ItemStatus.Warehouse);
                            FwFormField.setValueByDataField($form, 'VendorIdOut', response.ItemStatus.VendorId, response.ItemStatus.Vendor);
                            FwFormField.setValueByDataField($form, 'PurchaseOrderIdOut', response.ItemStatus.PurchaseOrderNumberId, response.ItemStatus.PurchaseOrder);
                            FwFormField.setValueByDataField($form, 'ConsignorIdOut', response.ItemStatus.ConsignorId, response.ItemStatus.Consignor);
                            $form.find('div.error-msg.check-out').html('');
                            $form.find('.out').removeClass('error');
                            let fields = $form.find('.fwformfield');
                            for (let i = 0; i < fields.length; i++) {
                                if (jQuery(fields[i]).attr('data-datafield').match(/^((?!DepartmentId$|DealId$|OrderId$|Description$).)*$/g)) {
                                    FwFormField.setValue2(jQuery(fields[i]), '', '');
                                }
                            }

                            this.ExchangeResponse = response;

                            var $exchangeItemGridControl: any;
                            $exchangeItemGridControl = $form.find('[data-name="ExchangeItemGrid"]');
                            $exchangeItemGridControl.data('ondatabind', request => {
                                request.uniqueids = {
                                    ContractId: this.ContractId
                                }
                            })
                            FwBrowse.search($exchangeItemGridControl);
                            FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                        } else {
                            errorSound.play();
                            FwFormField.setValueByDataField($form, 'ICodeOut', response.ItemStatus.ICode);
                            FwFormField.setValueByDataField($form, 'DescriptionOut', response.ItemStatus.Description);
                            $form.find('.out').addClass('error');
                            $form.find('div.error-msg.check-out').html(`<div><span>${response.msg}</span></div>`);
                            FwFormField.getDataField($form, 'BarCodeOut').find('input').select();
                        }
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        // Check-In
        $form.find('.in').on('keypress', e => {
            if (e.which === 13) {
                try {
                    const inRequest: any = {
                        ContractId: this.ContractId,
                        InCode: jQuery(e.currentTarget).find('input').val()
                    }
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitemin", inRequest, FwServices.defaultTimeout, response => {
                        if (response.success) {
                            if (this.ContractId === '') {
                                this.ContractId = response.ContractId;
                                $form.find('.suspendedsession').hide();
                                $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'true');
                            }
                            $form.find('div.error-msg.check-in').html('');
                            $form.find('.in').removeClass('error');
                            successSound.play();
                            FwFormField.setValueByDataField($form, 'DealId', response.DealId, response.Deal);
                            FwFormField.setValueByDataField($form, 'OrderId', response.OrderId, response.OrderNumber);
                            FwFormField.setValueByDataField($form, 'Description', response.OrderDescription);
                            FwFormField.setValueByDataField($form, 'ICodeIn', response.ItemStatus.ICode);
                            FwFormField.setValueByDataField($form, 'DescriptionIn', response.ItemStatus.Description);
                            FwFormField.setValueByDataField($form, 'WarehouseIdIn', response.ItemStatus.WarehouseId, response.ItemStatus.Warehouse);
                            FwFormField.setValueByDataField($form, 'VendorIdIn', response.ItemStatus.VendorId, response.ItemStatus.Vendor);
                            FwFormField.setValueByDataField($form, 'PurchaseOrderIdIn', response.ItemStatus.PurchaseOrderNumberId, response.ItemStatus.PurchaseOrder);
                            FwFormField.setValueByDataField($form, 'ConsignorIdIn', response.ItemStatus.ConsignorId, response.ItemStatus.Consignor);
                            FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                            FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                            FwFormField.getDataField($form, 'BarCodeOut').find('input').focus();
                        } else {
                            FwFormField.setValueByDataField($form, 'DescriptionIn', response.ItemStatus.Description);
                            FwFormField.setValueByDataField($form, 'ICodeIn', response.ItemStatus.ICode);
                            $form.find('.in').addClass('error');
                            errorSound.play();
                            $form.find('div.error-msg.check-in').html(`<div><span>${response.msg}</span></div>`);
                            FwFormField.getDataField($form, 'BarCodeIn').find('input').select();
                        }
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        // Create Contract
        $form.find('.createcontract').on('click', () => {
            if (this.ContractId !== '') {
                try {
                    FwAppData.apiMethod(true, 'POST', `api/v1/exchange/completeexchangecontract/${this.ContractId}`, null, FwServices.defaultTimeout, response => {
                        let $contractForm = ContractController.loadForm({
                            ContractId: response.ContractId
                        });
                        FwModule.openSubModuleTab($form, $contractForm);
                        this.resetForm($form);
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            } else {
                event.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select a valid Order and/or Bar Code.')
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const $exchangeItemGrid = $form.find('div[data-grid="ExchangeItemGrid"]');
        const $exchangeItemGridControl = FwBrowse.loadGridFromTemplate('ExchangeItemGrid');
        $exchangeItemGrid.empty().append($exchangeItemGridControl);
        $exchangeItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContractId: this.ContractId
            };
        })
        FwBrowse.init($exchangeItemGridControl);
        FwBrowse.renderRuntimeHtml($exchangeItemGridControl);
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateOrder($browse, $grid, request) {
        const $form = $grid.closest('.fwform');
        const DealId: string = FwFormField.getValueByDataField($form, 'DealId');
        if (DealId.length > 0) {
            request.uniqueids = {
                'DealId': DealId
            }
        }

        let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        let warehouseId = warehouse.warehouseid;
        request.miscfields = {
            Exchange: true,
            ExchangeWarehouseId: warehouseId
        }

    };
    //----------------------------------------------------------------------------------------------
    resetForm($form) {
        const fields = $form.find('.fwformfield');
        for (let i = 0; i < fields.length; i++) {
            if (jQuery(fields[i]).attr('data-datafield') !== 'DepartmentId') {
                FwFormField.setValue2(jQuery(fields[i]), '', '');
            }
        }
        this.ContractId = '';
        const cancelMenuOptionId = Constants.Modules.Home.Exchange.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
        $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'false');
        this.ExchangeResponse = {};
        this.renderGrids($form);
        FwFormField.enable(FwFormField.getDataField($form, 'OrderId'));
        FwFormField.enable(FwFormField.getDataField($form, 'DealId'));

        $form.find('.suspendedsession').show();
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        let typeHTML;
        switch (this.Module) {
            case 'Exchange':
                typeHTML = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:1 1 125px;" data-formbeforevalidate="beforeValidateOrder"></div>`;
                break;
            case 'ExchangeContainerItem':
                typeHTML = `<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container Item" data-datafield="ItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="flex:1 1 125px;"></div>`;
                break;
        }
        return `
        <div id="exchangeform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="${this.caption}" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="${this.Module}Controller">
          <div class="flexpage">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                <div class="flexrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield forTypeOrder" data-caption="${Constants.Modules.Home.Deal.caption}" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:1 1 175px;"></div>
                ${typeHTML}
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield forTypeOrder" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;" data-enabled="false"></div>
                </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-In">
                <div class="flexrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield in" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeIn" style="flex:0 1 200px;"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeIn" style="flex:1 1 125px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionIn" style="flex:1 1 250px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdIn" data-displayfield="WarehouseIn" data-validationname="WarehouseValidation" style="flex:1 1 125px;" data-enabled="false"></div>
                </div>
                <div class="flexrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;display:none;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorIdIn" data-displayfield="VendorIn" data-validationname="VendorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdIn" data-displayfield="PoNumberIn" data-validationname="PurchaseOrderValidation" style="flex:1 1 50px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdIn" data-displayfield="ConsignorIn" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                </div>
                <div class="flexrow error-msg check-in" style="margin-top:8px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-Out">
                <div class="flexrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield out" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeOut" style="flex:0 1 200px;"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeOut" style="flex:1 1 125px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionOut" style="flex:1 1 250px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdOut" data-displayfield="WarehouseOut" data-validationname="WarehouseValidation" style="flex:1 1 125px;" data-enabled="false"></div>
                </div>
                <div class="flexrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;display:none;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorIdOut" data-displayfield="VendorOut" data-validationname="VendorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdOut" data-displayfield="PoNumberOut" data-validationname="PurchaseOrderValidation" style="flex:1 1 50px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdOut" data-displayfield="ConsignorOut" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                </div>
                <div class="flexrow error-msg check-out" style="margin-top:8px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                <div class="flexcolumn" style="max-width:1600px">
                <div class="flexrow" style="max-width:1600px">
                    <div data-control="FwGrid" data-grid="ExchangeItemGrid" data-securitycaption="Exchange Item"></div>
                </div>
                <div class="formrow" style="max-width:1600px;margin-left:auto;">
                    <div class="createcontract fwformcontrol" data-type="button" style="flex: 0 1 140px;">Create Contract</div>
                </div>
                </div>
            </div>
          </div>
        </div>`;
    }
};
//----------------------------------------------------------------------------------------------
//Cancel
FwApplicationTree.clickEvents[Constants.Modules.Home.Exchange.form.menuItems.Cancel.id] = function (event: JQuery.ClickEvent) {
    const $form = jQuery(this).closest('.fwform');
    const contractId = ExchangeController.ContractId;
    const $confirmation = FwConfirmation.renderConfirmation('Cancel Exchange', 'Cancelling this Exchange Session will cause all transacted items to be cancelled.  Continue?');
    FwConfirmation.addControls($confirmation, '');
    const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
    FwConfirmation.addButton($confirmation, 'No', true);

    $yes.on('click', () => {
        try {
            const request: any = {};
            request.ContractId = contractId;
            FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout,
                response => {
                    FwConfirmation.destroyConfirmation($confirmation);
                    ExchangeController.resetForm($form);
                    FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
                },
                ex => FwFunc.showError(ex),
                $confirmation.find('.fwconfirmationbox'));
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    });
};
//----------------------------------------------------------------------------------------------
var ExchangeController = new Exchange();