routes.push({ pattern: /^module\/exchange$/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });

class Exchange {
    Module: string = 'Exchange';
    apiurl: string = 'api/v1/exchange';
    ContractId: string = '';
    ExchangeResponse: any = {};
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        let self = this;
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Exchange', false, 'FORM', true);
            self.ContractId = '';
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form, department;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        department = JSON.parse(sessionStorage.getItem('department'));

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);

        if (department.department === 'RENTALS') {
            $form.find('div[data-validationname="RentalInventoryValidation"]').show();
        }
        if (department.department === 'SALES') {
            $form.find('div[data-validationname="SalesInventoryValidation"]').show();
        }
        this.getSoundUrls($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let self = this, errorSound, successSound, department, contractRequest = {}, exchangeRequest = {};

        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        department = JSON.parse(sessionStorage.getItem('department'));

        contractRequest['DepartmentId'] = department.departmentid;

        // Deal Id
        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue')
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue')

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    if (self.ContractId === '') {
                        self.ContractId = response.ContractId
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
        $form.find('div[data-datafield="OrderId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'DealId', $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue');
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'Description', $tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    if (self.ContractId === '') {
                        self.ContractId = response.ContractId
                    }

                    FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                    FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Check-Out
        $form.find('.out').on('keypress', function (e) {
            if (e.which === 13) {
                exchangeRequest['ContractId'] = self.ContractId;
                exchangeRequest['OutCode'] = FwFormField.getValueByDataField($form, 'BarCodeOut');
                exchangeRequest['InCode'] = FwFormField.getValueByDataField($form, 'BarCodeIn');

                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitemout", exchangeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success) {
                            successSound.play();
                            FwFormField.setValueByDataField($form, 'ICodeOut', response.ItemStatus.ICode);
                            FwFormField.setValueByDataField($form, 'DescriptionOut', response.ItemStatus.Description);
                            FwFormField.setValueByDataField($form, 'WarehouseIdOut', response.ItemStatus.WarehouseId, response.ItemStatus.Warehouse);
                            FwFormField.setValueByDataField($form, 'VendorIdOut', response.ItemStatus.VendorId, response.ItemStatus.Vendor);
                            FwFormField.setValueByDataField($form, 'PurchaseOrderIdOut', response.ItemStatus.PurchaseOrderNumberId, response.ItemStatus.PurchaseOrder);
                            FwFormField.setValueByDataField($form, 'ConsignorIdOut', response.ItemStatus.ConsignorId, response.ItemStatus.Consignor);
                            $form.find('div.error-msg-out').html('');
                            $form.find('.out').removeClass('error');
                            let fields = $form.find('.fwformfield');
                            for (var i = 0; i < fields.length; i++) {
                                if (jQuery(fields[i]).attr('data-datafield').match(/^((?!DepartmentId$|DealId$|OrderId$|Description$).)*$/g)) {
                                    FwFormField.setValue2(jQuery(fields[i]), '', '');
                                }
                            }

                            self.ExchangeResponse = response;

                            var $exchangeItemGridControl: any;
                            $exchangeItemGridControl = $form.find('[data-name="ExchangeItemGrid"]');
                            $exchangeItemGridControl.data('ondatabind', function (request) {
                                request.uniqueids = {
                                    ContractId: self.ContractId
                                }
                            })
                            FwBrowse.search($exchangeItemGridControl);
                            FwFormField.getDataField($form, 'BarCodeIn').find('input').focus();
                        } else {
                            errorSound.play();
                            FwFormField.setValueByDataField($form, 'ICodeOut', response.ItemStatus.ICode);
                            FwFormField.setValueByDataField($form, 'DescriptionOut', response.ItemStatus.Description);
                            $form.find('.out').addClass('error');
                            $form.find('div.error-msg-out').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                            FwFormField.getDataField($form, 'BarCodeOut').find('input').select();
                        }
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });
        // Check-In
        $form.find('.in').on('keypress', function (e) {
            if (e.which === 13) {
                try {
                    var inRequest = {
                        ContractId: self.ContractId,
                        InCode: jQuery(this).find('input').val()
                    }
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitemin", inRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success) {
                            if (self.ContractId === '') {
                                self.ContractId = response.ContractId
                            }
                            $form.find('div.error-msg-in').html('');
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
                            $form.find('div.error-msg-in').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
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
            if (self.ContractId !== '') {
                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/completeexchangecontract/" + self.ContractId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                        let $contractForm = ContractController.loadForm({
                            ContractId: response.ContractId
                        });
                        let fields = $form.find('.fwformfield');
                        FwModule.openSubModuleTab($form, $contractForm);
                        for (var i = 0; i < fields.length; i++) {
                            if (jQuery(fields[i]).attr('data-datafield') !== 'DepartmentId') {
                                FwFormField.setValue2(jQuery(fields[i]), '', '');
                            }
                        }
                        self.ContractId = '';
                        self.ExchangeResponse = {};
                        self.renderGrids($form);
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
    renderGrids($form: any) {
        let $exchangeItemGrid: any;
        let $exchangeItemGridControl: any;
        let self = this;

        $exchangeItemGrid = $form.find('div[data-grid="ExchangeItemGrid"]');
        $exchangeItemGridControl = jQuery(jQuery('#tmpl-grids-ExchangeItemGridBrowse').html());
        $exchangeItemGrid.empty().append($exchangeItemGridControl);
        $exchangeItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: self.ContractId
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
};
var ExchangeController = new Exchange();