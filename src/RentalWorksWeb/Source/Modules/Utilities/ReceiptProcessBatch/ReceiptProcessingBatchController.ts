﻿class ReceiptProcessBatch {
    Module:  string = 'ReceiptProcessBatch';
    apiurl:  string = 'api/v1/receiptprocessbatch'
    caption: string = Constants.Modules.Utilities.children.ReceiptProcessBatch.caption;
    nav:     string = Constants.Modules.Utilities.children.ReceiptProcessBatch.nav;
    id:      string = Constants.Modules.Utilities.children.ReceiptProcessBatch.id;
    exporttype: string = 'ReceiptBatchExport';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

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

        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', today);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.setValueByDataField($form, 'Process', true);

        const request: any = {};
        request.uniqueids = {
            ExportType: this.exporttype,
            DefaultFormat: true
        }
        FwAppData.apiMethod(true, 'POST', `api/v1/dataexportformat/browse`, request, FwServices.defaultTimeout, response => {
            if (response.TotalRows > 0) {
                const idIndex = response.ColumnIndex.DataExportFormatId;
                const defaultExportFormatId = response.Rows[0][idIndex];
                const exportDescIndex = response.ColumnIndex.Description;
                const desc = response.Rows[0][exportDescIndex];
                FwFormField.setValueByDataField($form, 'DataExportFormatId', defaultExportFormatId, desc);
            }
        }, ex => FwFunc.showError(ex), $form);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.create-batch', e => {
                let request;
                var userId = sessionStorage.getItem('usersid');
                const office = JSON.parse(sessionStorage.getItem('location'));
                request = {
                    OfficeLocationId: office.locationid,
                    FromDate: FwFormField.getValueByDataField($form, 'FromDate'),
                    ToDate: FwFormField.getValueByDataField($form, 'ToDate')
                };

                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/createbatch`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    //if (response.Batch !== null) {
                    if (response.success === true) {
                        var batch = response.Batch;
                        var batchId = batch.BatchId;
                        var batchNumber = batch.BatchNumber

                        FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                        exportBatch();
                    } else {
                        //FwNotification.renderNotification('WARNING', 'There are no Receipts to process.');
                        FwNotification.renderNotification('WARNING', response.msg);
                    }
                }, null, $form, userId);
            })
            .on('click', '.print-batch', e => {
                let $report, batchNumber, batchId;
                try {
                    batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
                    batchId = FwFormField.getValueByDataField($form, 'BatchId');
                    $report = ReceiptBatchReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    FwFormField.setValueByDataField($report, 'BatchId', batchId, batchNumber);
                    $report.find('[data-datafield="BatchId"] input').change(); //sets the batchnumber and batchdate for the report
                    const $tab = FwTabs.getTabByElement($report);
                    $tab.find('.caption').html(`Print Receipt Batch`);
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

       const exportBatch = () => {
           const batchId = FwFormField.getValueByDataField($form, 'BatchId');
           const dataExportFormatId = FwFormField.getValueByDataField($form, 'DataExportFormatId');
            if (batchId) {
                let request: any = {};
                request = {
                    BatchId: batchId,
                    DataExportFormatId: dataExportFormatId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/receiptbatchexport/export`, request, FwServices.defaultTimeout,
                    response => {
                    if (response.downloadUrl != "") {
                        let batchNumber = response.BatchNumber
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
                    }, ex => FwFunc.showError(ex), $form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        let location = JSON.parse(sessionStorage.getItem('location'));

        request.uniqueids = {
            LocationId: location.locationid
        };
        switch (datafield) {
            case 'BatchId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebatch`);
                break;
            case 'DataExportFormatId':
                request.uniqueids = {
                    ExportType: this.exporttype
                };
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
}

var ReceiptProcessBatchController = new ReceiptProcessBatch();