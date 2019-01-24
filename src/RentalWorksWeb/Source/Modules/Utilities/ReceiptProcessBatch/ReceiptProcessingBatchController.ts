class ReceiptProcessBatch {
    Module: string = 'ReceiptProcessBatch';
    caption: string = 'Process Receipts';
    nav: string = 'module/receiptprocessbatch';
    id: string = '0BB9B45C-57FA-47E1-BC02-39CEE720792C';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Receipt Processing', false, 'FORM', true);
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
        FwFormField.setValueByDataField($form, 'Process', true);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.create-batch', e => {
                let request;
                request = {
                    method: 'CreateCharge',
                    asofdate: FwFormField.getValue($form, 'div[data-datafield="AsOfDate"]')
                };
                FwReport.getData($form, request, function (response) {
                    if ((response.status == '0') && (response.chgbatchid != '')) {
                        FwFormField.setValue($form, 'div[data-datafield="BatchId"]', response.chgbatchid, response.chgbatchno, true);
                        $form.find('.batch-success-message').html(`<div style="margin-left:8px; margin-top: 10px;"><span>Batch ${response.chgbatchno} Created Successfully</span></div>`);
                        //FwFormField.setValue($form, 'div[data-datafield="exported"]', response.chgbatchdate);
                    } else if ((response.status == '0') && (response.chgbatchid == '')) {
                        FwNotification.renderNotification('WARNING', 'No Approved Invoices to Process.');
                    } else {
                        FwFunc.showError(response.msg);
                    }
                });
            })
            .on('click', '.print-batch', e => {
                //open report front end in new tab
            })
            .on('click', '.export-historical-batches', e => {
                alert("asdf");
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
var ReceiptProcessBatchController = new ReceiptProcessBatch();