routes.push({ pattern: /^module\/exchange$/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });

class Exchange {
    ContractId: string;
    ExchangeResponse: any;
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Exchange';
        this.apiurl = 'api/v1/exchange';
        this.ContractId = '';
        this.ExchangeResponse = {};
    }
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
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form;
        let widgets = [];
        let department = JSON.parse(sessionStorage.getItem('department'));
        let contractRequest = {};
        let exchangeRequest = {};
        let self = this;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);
        contractRequest['DepartmentId'] = department.departmentid;

        if (department.department === 'RENTALS') {
            $form.find('div[data-validationname="RentalInventoryValidation"]').show();
        }
        if (department.department === 'SALES') {
            $form.find('div[data-validationname="SalesInventoryValidation"]').show();
        }


        $form.find('div[data-datafield="OrderId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'DealId', $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="Deal"]').attr('data-originalvalue'));
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue');
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue');

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    self.ContractId = response.ContractId;
                    FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $form.find('div[data-datafield="DealId"]').data('onchange', function ($tr) {
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue')
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue')

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    self.ContractId = response.ContractId;

                    let $exchangeItemGridControl: any;
                    $exchangeItemGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                    $exchangeItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: response.ContractId
                        }
                    })
                    FwFormField.disable(FwFormField.getDataField($form, 'OrderId'));
                    FwFormField.disable(FwFormField.getDataField($form, 'DealId'));
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $form.find('.in').on('keypress', function (e) {
            if (e.which === 13) {
                FwFormField.getDataField($form, 'BarCodeOut').find('input').focus();
            }
        });

        $form.find('.out').on('keypress', function (e) {
            if (e.which === 13) { 
                exchangeRequest['ContractId'] = self.ContractId;
                exchangeRequest['OutCode'] = FwFormField.getValueByDataField($form, 'BarCodeOut');
                exchangeRequest['InCode'] = FwFormField.getValueByDataField($form, 'BarCodeIn'); 

                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitem", exchangeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        self.ExchangeResponse = response;
                    }, null, $form);

                    var $exchangeItemGridControl: any;
                    $exchangeItemGridControl = $form.find('[data-name="ExchangeItemGrid"]');
                    $exchangeItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: self.ContractId
                        }
                    })
                    FwBrowse.search($exchangeItemGridControl); 
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

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
                        $form.find('div[data-grid="ExchangeItemGrid"]').empty().append(jQuery(jQuery('#tmpl-grids-ExchangeItemGridBrowse').html()));
                        FwFormField.enable(FwFormField.getDataField($form, 'OrderId'));
                        FwFormField.enable(FwFormField.getDataField($form, 'DealId'));
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            } else {
                let $confirmation = FwConfirmation.renderConfirmation('Error', 'Please select a valid order and/or bar code.');
                let $cancel = FwConfirmation.addButton($confirmation, 'OK', true);
                FwConfirmation.addControls($confirmation, '');
            }

        });

        return $form;
    }
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
    }

}
var ExchangeController = new Exchange();