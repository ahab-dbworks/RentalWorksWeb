﻿class InvoiceProcessBatch {
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
                    if ((response.success === true) && (response.Batch !== null)) {
                        var batch = response.Batch;
                        var batchId = batch.BatchId;
                        var batchNumber = batch.BatchNumber
                        request = {
                            BatchId: batchId
                        }
                        FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/export`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            $form.find('.export-success').show();
                            FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                            $form.find('.batch-success-message').html(`<span style="background-color: green; color:white; font-size:1.3em;">Batch ${batchNumber} Created Successfully.</span>`);
                        }, null, $form, userId);
                    } else {
                        FwNotification.renderNotification('WARNING', 'There are no Approved Invoices to process.');
                    }
                }, null, $form, userId);

            })
            .on('click', '.print-batch', e => {
                let $report, batchNumber, batchId, batchTab;
                try {
                    batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
                    batchId = FwFormField.getValueByDataField($form, 'BatchId');
                    $report = RwDealInvoiceBatchReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    FwFormField.setValueByDataField($report, 'BatchId', batchId, batchNumber);
                    jQuery('.tab.submodule.active').find('.caption').html(`Print Deal Invoice Batch`);
                    batchTab = jQuery('.tab.submodule.active');
                    batchTab.find('.caption').html(`Print Deal Invoice Batch`);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.export-historical-batches', e => {
                var batchId = FwFormField.getValueByDataField($form, 'BatchId');
                var userId = sessionStorage.getItem('usersid');
                if (batchId !== '') {
                    let request: any = {};
                    request = {
                        BatchId: batchId
                    }
                    let timeout = 7200;
                    FwAppData.apiMethod(true, 'POST', `api/v1/invoiceprocessbatch/export`, request, timeout, function onSuccess(response) {

                    if ((response.success === true) && (response.batch !== null)) {
                        var batch = response.batch;
                        var batchNumber = batch.BatchNumber
                        var downloadUrl = response.downloadUrl;

                        $form.find('.export-success').show();
                        $form.find('.batch-success-message').html(`<span style="background-color: green; color:white; font-size:1.3em;">Batch ${batchNumber} Exported Successfully.</span>`);

                        // at this point we want to initiate the download process using "downloadUrl" from the response. 
                        // please loop in Josh at this point as he did a lot of work on the "Download Excel" process for reports.  I think this will be similar
     
                        let $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${downloadUrl}" style="display:none;"></iframe>`);
                        jQuery('#application').append($iframe);
                        setTimeout(function () {
                            $iframe.remove();
                        }, 500);

                    } else {
                        FwNotification.renderNotification('WARNING', 'Batch could not be exported.');
                    }

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
//Export Settings
FwApplicationTree.clickEvents['{28D5F4EF-9A60-4D7F-B294-4B302B88413F}'] = function (event) {
    try {
        let $exportSettingsBrowse = ExportSettingsController.openBrowse();
        $exportSettingsBrowse.data('ondatabind', function (request) {
            request.miscfields = {
                Invoices: true
                , Receipts: false
                , VendorInvoices: false
            }
        });
        FwModule.openModuleTab($exportSettingsBrowse, 'Export Settings', true, 'BROWSE', true);
        FwBrowse.search($exportSettingsBrowse);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

var InvoiceProcessBatchController = new InvoiceProcessBatch();