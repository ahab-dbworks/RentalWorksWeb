class ChargeProcessing {
    Module: string = 'ChargeProcessing';
    caption: string = 'Process Deal Invoices';
    nav: string = 'module/chargeprocessing';
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
            .on('change', '[data-datafield="ExportHistoricalBatch"] input', e => {
                let isChecked = jQuery(e.currentTarget).prop('checked');
                if (isChecked) {
                    FwFormField.setValueByDataField($form, 'ProcessInvoices', false);
                    FwFormField.enable($form.find('[data-datafield="BatchId"]'));
                    FwFormField.enable($form.find('.export-historical-batches'));
                    FwFormField.disable($form.find('[data-datafield="AsOfDate"]'));
                    FwFormField.disable($form.find('.create-batch'));
                } else {
                    FwFormField.setValueByDataField($form, 'ProcessInvoices', true);
                    FwFormField.enable($form.find('[data-datafield="AsOfDate"]'));
                    FwFormField.enable($form.find('.create-batch'));
                    FwFormField.disable($form.find('[data-datafield="BatchId"]'));
                    FwFormField.disable($form.find('.export-historical-batches'));
                }
            })
    }
}
var ChargeProcessingController = new ChargeProcessing();