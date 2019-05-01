class InvoiceProcessBatch {
    Module: string = 'InvoiceProcessBatch';
    caption: string = 'Process Invoices';
    nav: string = 'module/invoiceprocessbatch';
    id: string = '5DB3FB9C-6F86-4696-867A-9B99AB0D6647';
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
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const today = FwFunc.getDate();
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
                        FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                        exportBatch();
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
                    $report.find('[data-datafield="BatchId"] input').change(); //sets the batchnumber and batchdate for the report
                    jQuery('.tab.submodule.active').find('.caption').html(`Print Deal Invoice Batch`);
                    batchTab = jQuery('.tab.submodule.active');
                    batchTab.find('.caption').html(`Print Deal Invoice Batch`);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.export-historical-batches', e => {
                exportBatch();
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
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $grid, request) {
        const location = JSON.parse(sessionStorage.getItem('location'));

        request.uniqueids = {
            LocationId: location.locationid
        };
    }
    //----------------------------------------------------------------------------------------------
}
//Export Settings
FwApplicationTree.clickEvents[Constants.Modules.Utilities.InvoiceProcessBatch.form.menuItems.ExportSettings.id] = function (event) {
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