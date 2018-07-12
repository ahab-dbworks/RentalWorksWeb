routes.push({ pattern: /^module\/exchange/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });

class Exchange {
    static ContractId: any;
    static ExchangeResponse: any;
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Exchange';
        this.apiurl = 'api/v1/exchange';
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Exchange', false, 'FORM', true);
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
            contractRequest['OrderId'] = $tr.find('.field[data-browsedatafield="OrderId"]').attr('data-originalvalue')
            contractRequest['DealId'] = $tr.find('.field[data-browsedatafield="DealId"]').attr('data-originalvalue')

            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startexchangecontract", contractRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    Exchange.ContractId = response.ContractId;
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
                    Exchange.ContractId = response.ContractId;

                    let $exchangeItemGridControl: any;
                    $exchangeItemGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                    $exchangeItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: response.ContractId
                        }
                    })
                    FwBrowse.search($exchangeItemGridControl);
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $form.find('.in').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'VendorIn', $tr.find('.field[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            if (FwFormField.getValueByDataField($form, 'RentalBarCodeOutId') !== '' || FwFormField.getValueByDataField($form, 'SalesBarCodeOutId') !== '') {
                exchangeRequest['ContractId'] = Exchange.ContractId;
                exchangeRequest['InCode'] = $tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue');

                if (FwFormField.getValueByDataField($form, 'RentalBarCodeOutId') !== '') {
                    exchangeRequest['OutCode'] = FwFormField.getValueByDataField($form, 'RentalBarCodeOutId');
                } else {
                    exchangeRequest['OutCode'] = FwFormField.getValueByDataField($form, 'SalesBarCodeOutId');
                }
                
                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitem", exchangeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        Exchange.ExchangeResponse = response;
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });

        $form.find('.out').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'VendorIn', $tr.find('.field[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            if (FwFormField.getValueByDataField($form, 'RentalBarCodeInId') !== '' || FwFormField.getValueByDataField($form, 'SalesBarCodeInId') !== '') {
                exchangeRequest['ContractId'] = Exchange.ContractId;
                exchangeRequest['OutCode'] = $tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue');

                if (FwFormField.getValueByDataField($form, 'RentalBarCodeInId') !== '') {
                    exchangeRequest['InCode'] = FwFormField.getValueByDataField($form, 'RentalBarCodeInId');
                } else {
                    exchangeRequest['InCode'] = FwFormField.getValueByDataField($form, 'SalesBarCodeInId');
                }

                try {
                    FwAppData.apiMethod(true, 'POST', "api/v1/exchange/exchangeitem", exchangeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                        Exchange.ExchangeResponse = response;
                    }, null, $form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }
        });


        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        let $exchangeItemGrid: any;
        let $exchangeItemGridControl: any;

        $exchangeItemGrid = $form.find('div[data-grid="ExchangeItemGrid"]');
        $exchangeItemGridControl = jQuery(jQuery('#tmpl-grids-ExchangeItemGridBrowse').html());
        $exchangeItemGrid.empty().append($exchangeItemGridControl);
        $exchangeItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: Exchange.ContractId
            };
        })
        FwBrowse.init($exchangeItemGridControl);
        FwBrowse.renderRuntimeHtml($exchangeItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        //FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: 'home' });
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    //afterLoad($form: any) {

    //}
    //----------------------------------------------------------------------------------------------

}
let ExchangeController = new Exchange();