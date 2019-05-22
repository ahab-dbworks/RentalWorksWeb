class VendorInvoiceProcessBatch {
    Module: string = 'VendorInvoiceProcessBatch';
    caption: string = 'Process Vendor Invoices';
    nav: string = 'module/vendorinvoiceprocessbatch';
    id: string = '4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Process Vendor Invoices', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.setValueByDataField($form, 'Process', true);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.create-batch', e => {
                let location = JSON.parse(sessionStorage.getItem('location'));
                var userId = sessionStorage.getItem('usersid');
                const request: any = {
                    AsOfDate: FwFormField.getValueByDataField($form, 'AsOfDate')
                    , LocationId: location.locationid
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoiceprocessbatch/createbatch`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    alert(response.BatchId);
                    //submit batchid to export endpoint & show progress meter
                    //when that is successful, updated text and show print button
                    if ((response.success === true) && (response.Batch !== null)) {
                        var batch = response.Batch;
                        var batchId = batch.BatchId;
                        var batchNumber = batch.BatchNumber
                        FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                        exportBatch();
                    } else {
                        FwNotification.renderNotification('WARNING', 'There are no Approved Invoices to process.');
                    }
                }, null, $form, userId);
            })
            .on('click', '.print-batch', e => {
                try {
                    const batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
                    const batchId = FwFormField.getValueByDataField($form, 'BatchId');
                    const $report = VendorInvoiceBatchReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    FwFormField.setValueByDataField($report, 'BatchId', batchId, batchNumber);
                    $report.find('[data-datafield="BatchId"] input').change(); //sets the batchnumber and batchdate for the report
                    jQuery('.tab.submodule.active').find('.caption').html(`Print Vendor Invoice Batch`);
                    const batchTab = jQuery('.tab.submodule.active');
                    batchTab.find('.caption').html(`Print Deal Invoice Batch`);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.export-historical-batches', e => {
                exportBatch();            })
            .on('change', '[data-datafield="ExportHistoricalBatch"] input, [data-datafield="Process"] input', e => {
                let $this = jQuery(e.currentTarget);
                let checkboxName = $this.parents('.fwformfield').attr('data-datafield');
                let controlsToDisable;
                let controlsToEnable;
                let isChecked = jQuery(e.currentTarget).prop('checked');
                if (checkboxName === 'ExportHistoricalBatch') {
                    checkboxName = 'Process';
                    if (isChecked) {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, false);
                        controlsToDisable = '.process';
                        controlsToEnable = '.export';
                    } else {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, true);
                        controlsToDisable = '.export';
                        controlsToEnable = '.process';
                    }
                } else {
                    checkboxName = 'ExportHistoricalBatch';
                    if (isChecked) {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, false);
                        controlsToDisable = '.export';
                        controlsToEnable = '.process';
                    } else {
                        FwFormField.setValueByDataField($form, `${checkboxName}`, true);
                        controlsToDisable = '.process';
                        controlsToEnable = '.export';
                    }
                }
                FwFormField.enable($form.find(`${controlsToEnable}`));
                FwFormField.disable($form.find(`${controlsToDisable}`));
            })
        function exportBatch() {
            const batchId = FwFormField.getValueByDataField($form, 'BatchId');
            if (batchId !== '') {
                const request: any = {
                    BatchId: batchId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/export`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if ((response.success === true) && (response.batch !== null)) {
                        let batch = response.batch;
                        let batchNumber = batch.BatchNumber
                        const $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${response.downloadUrl}" style="display:none;"></iframe>`);
                        jQuery('#application').append($iframe);
                        setTimeout(function () {
                            $iframe.remove();
                        }, 500);

                        $form.find('.export-success').show();
                        $form.find('.success-msg').html(`<div style="margin-left:0;><span>Batch ${batchNumber} Created Successfully.</span><div>`);
                    }
                }, null, $form);
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $grid, request) {
        const location = JSON.parse(sessionStorage.getItem('location'));

        request.uniqueids = {
            LocationId: location.locationid
        };
    };
    //----------------------------------------------------------------------------------------------
}
var VendorInvoiceProcessBatchController = new VendorInvoiceProcessBatch();