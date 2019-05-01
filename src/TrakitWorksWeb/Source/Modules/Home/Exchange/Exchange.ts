routes.push({ pattern: /^module\/exchange$/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });

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

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, 'Exchange', false, 'FORM', true);
            this.ContractId = '';
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let department;

        let $form = FwModule.loadFormFromTemplate(this.Module);
        //let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        department = JSON.parse(sessionStorage.getItem('department'));

        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);

        if (department.department === 'RENTALS') {
            $form.find('div[data-validationname="RentalInventoryValidation"]').show();
        }
        if (department.department === 'SALES') {
            $form.find('div[data-validationname="SalesInventoryValidation"]').show();
        }
        this.getSoundUrls($form);
        this.getSuspendedSessions($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        let showSuspendedSessions = $form.attr('data-showsuspendedsessions');

        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/exchange/suspendedsessionsexist', null, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
            }, null, $form);

            $form.on('click', '.suspendedsession', e => {
                let html = `<div>
                             <div style="background-color:white; padding-right:10px; text-align:right;" class="close-modal"><i style="cursor:pointer;" class="material-icons">clear</i></div>
                             <div id="suspendedSessions" style="max-width:90vw;max-height:90vh;overflow:auto;"></div>
                            </div>`;

                let $popup = FwPopup.renderPopup(jQuery(html), { ismodal: true });

                let $browse = SuspendedSessionController.openBrowse();
                let officeLocationId = JSON.parse(sessionStorage.getItem('location'));
                officeLocationId = officeLocationId.locationid;
                $browse.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OfficeLocationId: officeLocationId
                        , SessionType: 'EXCHANGE'
                        , OrderType: 'O'
                    }
                });

                FwPopup.showPopup($popup);
                jQuery('#suspendedSessions').append($browse);
                FwBrowse.search($browse);

                $popup.find('.close-modal > i').one('click', function (e) {
                    FwPopup.destroyPopup($popup);
                    jQuery(document).find('.fwpopup').off('click');
                    jQuery(document).off('keydown');
                });

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    let dealId = $this.find(`[data-browsedatafield="DealId"]`).attr('data-originalvalue');
                    let dealNumber = $this.find(`[data-browsedatafield="DealNumber"]`).attr('data-originalvalue');
                    if (dealId !== "") {
                        FwFormField.setValueByDataField($form, 'DealId', dealId, dealNumber);
                    }
                    FwFormField.setValueByDataField($form, 'OrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="OrderId"] input').change();
                    $form.find('.suspendedsession').hide();
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

        // Deal Id
        $form.find('div[data-datafield="DealId"]').data('onchange', $tr => {
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue')
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue')

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, response => {
                    if (this.ContractId === '') {
                        this.ContractId = response.ContractId
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
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue');
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'Description', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, response => {
                    if (this.ContractId === '') {
                        this.ContractId = response.ContractId
                    }

                    FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                    FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
            $form.find('.suspendedsession').hide();
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
                    var inRequest = {
                        ContractId: this.ContractId,
                        InCode: jQuery(this).find('input').val()
                    }
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitemin", inRequest, FwServices.defaultTimeout,  response => {
                        if (response.success) {
                            if (this.ContractId === '') {
                                this.ContractId = response.ContractId
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
                        let fields = $form.find('.fwformfield');
                        FwModule.openSubModuleTab($form, $contractForm);
                        for (let i = 0; i < fields.length; i++) {
                            if (jQuery(fields[i]).attr('data-datafield') !== 'DepartmentId') {
                                FwFormField.setValue2(jQuery(fields[i]), '', '');
                            }
                        }
                        this.ContractId = '';
                        this.ExchangeResponse = {};
                        this.renderGrids($form);
                        FwFormField.enable(FwFormField.getDataField($form, 'OrderId'));
                        FwFormField.enable(FwFormField.getDataField($form, 'DealId'));
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
        let $exchangeItemGrid: any;
        let $exchangeItemGridControl: any;

        $exchangeItemGrid = $form.find('div[data-grid="ExchangeItemGrid"]');
        $exchangeItemGridControl = jQuery(jQuery('#tmpl-grids-ExchangeItemGridBrowse').html());
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
        var DealId = jQuery($grid.find('[data-validationname="DealValidation"] input')).val();

        request.uniqueids = {
            'DealId': DealId
        }
    };
    //----------------------------------------------------------------------------------------------
}
var ExchangeController = new Exchange();
