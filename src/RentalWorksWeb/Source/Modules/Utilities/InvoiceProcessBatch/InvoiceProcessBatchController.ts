class InvoiceProcessBatch {
    Module:     string = 'InvoiceProcessBatch';
    apiurl:     string = 'api/v1/invoiceprocessbatch';
    caption:    string = Constants.Modules.Utilities.children.InvoiceProcessBatch.caption;
    nav:        string = Constants.Modules.Utilities.children.InvoiceProcessBatch.nav;
    id:         string = Constants.Modules.Utilities.children.InvoiceProcessBatch.id;
    exporttype: string = 'InvoiceBatchExport';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Export Settings', '', (e: JQuery.ClickEvent) => {
            try {
                this.ExportSettings(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
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
    ExportSettings($form: JQuery): void {
        try {
            let $exportSettingsBrowse = ExportSettingsController.openBrowse();
            $exportSettingsBrowse.data('ondatabind', function (request) {
                request.miscfields = {
                    Invoices:       true,
                    Receipts:       false,
                    VendorInvoices: false
                }
            });
            FwModule.openModuleTab($exportSettingsBrowse, 'Export Settings', true, 'BROWSE', true);
            FwBrowse.search($exportSettingsBrowse);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.create-batch', e => {
                let request;
                let location = JSON.parse(sessionStorage.getItem('location'));
                var userId = sessionStorage.getItem('usersid');
                request = {
                    AsOfDate: FwFormField.getValueByDataField($form, 'AsOfDate'),
                    LocationId: location.locationid
                };

                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/createbatch`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if ((response.success === true) && (response.Batch !== null)) {
                        var batch = response.Batch;
                        var batchId = batch.BatchId;
                        var batchNumber = batch.BatchNumber
                        FwFormField.setValueByDataField($form, 'BatchId', batchId, batchNumber);
                        exportBatch();
                    } else {
                        //FwNotification.renderNotification('WARNING', 'There are no Approved Invoices to process.');
                        FwNotification.renderNotification('WARNING', response.msg);
                    }
                }, null, $form, userId);

            })
            .on('click', '.print-batch', e => {
                let $report, batchNumber, batchId;
                try {
                    batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
                    batchId = FwFormField.getValueByDataField($form, 'BatchId');
                    $report = DealInvoiceBatchReportController.openForm();
                    FwModule.openSubModuleTab($form, $report);
                    FwFormField.setValueByDataField($report, 'BatchId', batchId, batchNumber);
                    $report.find('[data-datafield="BatchId"] input').change(); //sets the batchnumber and batchdate for the report
                    const $tab = FwTabs.getTabByElement($report);
                    $tab.find('.caption').html(`Print Deal Invoice Batch`);
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

        const exportBatch = () => {
            const batchId = FwFormField.getValueByDataField($form, 'BatchId');
            const dataExportFormatId = FwFormField.getValueByDataField($form, 'DataExportFormatId');
            if (batchId) {
                const request: any = {
                    BatchId: batchId,
                    DataExportFormatId: dataExportFormatId
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/invoicebatchexport/export`, request, FwServices.defaultTimeout,
                    response => {
                        if (response.downloadUrl != "") {
                            let batchNumber = response.BatchNumber
                            const $iframe = jQuery(`<iframe src="${applicationConfig.apiurl}${response.downloadUrl}" style="display:none;"></iframe>`);
                            jQuery('#application').append($iframe);
                            setTimeout(function () {
                                $iframe.remove();
                            }, 500);

                            $form.find('.export-success').show();
                            $form.find('.success-msg').html(`<div style="margin-left:0;><span>Batch ${batchNumber} Created Successfully.</span><div>`);
                        }
                    }, ex => FwFunc.showError(ex), $form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const location = JSON.parse(sessionStorage.getItem('location'));
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
//----------------------------------------------------------------------------------------------
var InvoiceProcessBatchController = new InvoiceProcessBatch();