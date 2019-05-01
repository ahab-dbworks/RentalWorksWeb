class ReceiptProcessBatch {
    Module: string = 'ReceiptProcessBatch';
    caption: string = 'Process Receipts';
    nav: string = 'module/receiptprocessbatch';
    id: string = '0BB9B45C-57FA-47E1-BC02-39CEE720792C';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Process Receipts', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', today);

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
                let request;
                var userId = sessionStorage.getItem('usersid');
                const office = JSON.parse(sessionStorage.getItem('location'));
                request = {
                    OfficeLocationId: office.locationid
                    , FromDate: FwFormField.getValueByDataField($form, 'FromDate')
                    , ToDate: FwFormField.getValueByDataField($form, 'ToDate')
                };

                FwAppData.apiMethod(true, 'POST', `api/v1/receiptprocessbatch/createbatch`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.Batch !== null) {
                        var batch = response.Batch;
                        var batchId = batch.BatchId;
                        var batchNumber = batch.BatchNumber

                        FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                        exportBatch();
                    } else {
                        FwNotification.renderNotification('WARNING', 'There are no Receipts to process.');
                    }
                }, null, $form, userId);
            })
            .on('click', '.print-batch', e => {
                let $report, batchNumber, batchId, batchTab;
                try {
                    batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
                    batchId = FwFormField.getValueByDataField($form, 'BatchId');
                    $report = RwReceiptBatchReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    FwFormField.setValueByDataField($report, 'BatchId', batchId, batchNumber);
                    $report.find('[data-datafield="BatchId"] input').change(); //sets the batchnumber and batchdate for the report
                    jQuery('.tab.submodule.active').find('.caption').html(`Print Receipt Batch`);
                    batchTab = jQuery('.tab.submodule.active');
                    batchTab.find('.caption').html(`Print Receipt Batch`);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.export-historical-batches', e => {
                exportBatch();
            })
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
            });

        function exportBatch() {
            let batchId = FwFormField.getValueByDataField($form, 'BatchId');
            if (batchId !== '') {
                let request: any = {};
                request = {
                    BatchId: batchId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/receiptprocessbatch/export`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if ((response.success === true) && (response.batch !== null)) {
                        let batch = response.batch;
                        let batchNumber = batch.BatchNumber
                        let $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${response.downloadUrl}" style="display:none;"></iframe>`);
                        jQuery('#application').append($iframe);
                        setTimeout(function () {
                            $iframe.remove();
                        }, 500);

                        $form.find('.export-success').show();
                        $form.find('.success-msg').html(`<div style="margin-left:0;><span>Batch ${batchNumber} Created Successfully.</span><div>`);
                    } else {
                        FwNotification.renderNotification('WARNING', 'Batch could not be exported.');
                    }
                }, null, $form);
            }
        }
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
FwApplicationTree.clickEvents[Constants.Modules.Utilities.ReceiptProcessBatch.form.menuItems.ExportSettings.id] = function (event) {
    try {
        let $exportSettingsBrowse = ExportSettingsController.openBrowse();
        $exportSettingsBrowse.data('ondatabind', function (request) {
            request.miscfields = {
                Invoices: false
                , Receipts: true
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
var ReceiptProcessBatchController = new ReceiptProcessBatch();