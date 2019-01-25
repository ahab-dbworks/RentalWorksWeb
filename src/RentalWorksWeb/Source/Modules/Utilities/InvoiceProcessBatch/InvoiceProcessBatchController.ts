class InvoiceProcessBatch {
    Module: string = 'InvoiceProcessBatch';
    caption: string = 'Process Deal Invoices';
    nav: string = 'module/invoiceprocessbatch';
    id: string = '5DB3FB9C-6F86-4696-867A-9B99AB0D6647';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Charge Processing - Invoices', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        let today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'AsOfDate', today);
        FwFormField.setValueByDataField($form, 'ProcessInvoices', true);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.create-batch', e => {
                let request;
                let location = JSON.parse(sessionStorage.getItem('location'));
                var userId = sessionStorage.getItem('usersid');
                request = {
                    AsOfDate: FwFormField.getValueByDataField($form, 'AsOfDate')
                    , LocationId: location.locationid
                };

                FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/createbatch`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if ((response.success === true) && (response.BatchId !== '')) {
                        var batchId = response.BatchId;
                        request = {
                            BatchId: batchId
                        }
                        FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/export`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            $form.find('.export-success').show();
                            $form.find('.batch-success-message').html(`<span style="background-color: green; color:white; font-size:1.3em;">Batch ${batchId} Created Successfully.</span>`);
                        }, null, $form, userId);
                    }
                }, null, $form, userId);

            })
            .on('click', '.print-batch', e => {
                //open report front end in new tab
            })
            .on('click', '.export-historical-batches', e => {
                var batchId = FwFormField.getValueByDataField($form, 'BatchId');
                var userId = sessionStorage.getItem('usersid');
                if (batchId !== '') {
                    let request: any = {};
                    request = {
                        BatchId: batchId
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/export`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        $form.find('.export-success').show();
                        $form.find('.batch-success-message').html(`<span style="background-color: green; color:white; font-size:1.3em;">Batch ${batchId} Created Successfully.</span>`);
                    }, null, $form, userId);
                }
            })
            // enable/disable controls based on checkbox
            .on('change', '[data-datafield="ExportHistoricalBatch"] input, [data-datafield="ProcessInvoices"] input', e => {
                let $this = jQuery(e.currentTarget);
                let checkboxName = $this.parents('.fwformfield').attr('data-datafield');
                let controlsToDisable;
                let controlsToEnable;
                let isChecked = jQuery(e.currentTarget).prop('checked');
                if (checkboxName === 'ExportHistoricalBatch') {
                    checkboxName = 'ProcessInvoices';
                    if (isChecked) {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, false);
                        controlsToDisable = '.processinvoices';
                        controlsToEnable = '.export';
                    } else {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, true);
                        controlsToDisable = '.export';
                        controlsToEnable = '.processinvoices';
                    }
                } else {
                    checkboxName = 'ExportHistoricalBatch';
                    if (isChecked) {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, false);
                        controlsToDisable = '.export';
                        controlsToEnable = '.processinvoices';
                    } else {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, true);
                        controlsToDisable = '.processinvoices';
                        controlsToEnable = '.export';
                    }
                }
                FwFormField.enable($form.find(`${controlsToEnable}`));
                FwFormField.disable($form.find(`${controlsToDisable}`));
            })
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $grid, request) {
        let location = JSON.parse(sessionStorage.getItem('location'));

        request.uniqueids = {
            LocationId: location.locationid
        };
    }
    //----------------------------------------------------------------------------------------------
}
var InvoiceProcessBatchController = new InvoiceProcessBatch();