RwReceiptProcessingController = {
    Module: 'ReceiptProcessing',
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    }
};
RwReceiptProcessingController.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, RwReceiptProcessingController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwReceiptProcessingController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;
    screen            = {};
    screen.$view      = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwReceiptProcessingController.openForm();

    screen.load = function () {
        FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
    };
    screen.unload = function () {
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwReceiptProcessingController.openForm = function() {
    var $form, batchid, batchno;
    
    batchid = "";

    $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
    $form.data('getexportrequest', function(request) {
        request.parameters = FwReport.getParameters($form);
        return request;
    });

    $form
        .on('click', '.processreceipts', function() {
            var request;
            request = {
                method:      'ProcessReceipts',
                processfrom: FwFormField.getValue($form, 'div[data-datafield="processfrom"]'),
                processto:   FwFormField.getValue($form, 'div[data-datafield="processto"]')
            };
            FwReport.getData($form, request, function(response) {
                if (response.chgbatchid != '') {
                    FwFormField.setValue($form, 'div[data-datafield="batchno"]', response.chgbatchid, response.chgbatchno, true);
                    FwFormField.setValue($form, 'div[data-datafield="exported"]', response.chgbatchdate);
                } else if (response.chgbatchid == '') {
                    FwNotification.renderNotification('WARNING', 'No receipts to process.');
                } else {
                    FwFunc.showError(response.msg);
                }
            });
        })
        .on('change', 'div[data-datafield="viewbatch"] input.fwformfield-value', function() {
            if (jQuery(this).is(':checked')) {
                $form.data('focusbatch')();
            } else {
                $form.data('focusdates')();
            }
        })
        .on('change', 'div[data-datafield="viewdates"] input.fwformfield-value', function() {
            if (jQuery(this).is(':checked')) {
                $form.data('focusdates')();
            } else {
                $form.data('focusbatch')();
            }
        })
        .on('click', '.exportqbo', function() {
            var request, run;
            if (jQuery(this).hasClass('disabled')) {
                FwNotification.renderNotification('WARNING', 'Not currently connected to Quickbooks Online.');
            } else {
                run = true;
                request = {
                    method:    'ExportToQBO',
                    viewbatch: FwFormField.getValue($form, 'div[data-datafield="viewbatch"]'),
                    batchno:   FwFormField.getValue($form, 'div[data-datafield="batchno"]'),
                    viewdates: FwFormField.getValue($form, 'div[data-datafield="viewdates"]'),
                    batchfrom: FwFormField.getValue($form, 'div[data-datafield="batchfrom"]'),
                    batchto:   FwFormField.getValue($form, 'div[data-datafield="batchto"]')
                }

                if ((request.viewbatch == 'T') && (request.batchno == '')) {
                    $form.find('div[data-datafield="batchno"]').addClass('error');
                    run = false;
                } else if (request.viewdates == 'T') {
                    if (request.batchfrom == '') {
                        $form.find('div[data-datafield="batchfrom"]').addClass('error');
                        run = false;
                    }
                    if (request.batchto == '') {
                        $form.find('div[data-datafield="batchto"]').addClass('error');
                        run = false;
                    }
                }

                if (run) {
                    var $confirm = FwConfirmation.renderConfirmation('Export batch to Quickbooks', 'Running please wait...');
                    FwReport.getData($form, request, function(response) {
                        try {
                           if (response.export.status == "0") {
                                var html = [];
                                html.push('<table style="table-layout:fixed;border-collapse:collapse;text-align:left;font-size:13px;">');
                                html.push('<thead><tr><th style="width:90px;">Receipt</th><th>Status</th></tr></thead>')
                                html.push('<tbody>');
                                for (var i = 0; i < response.export.receipts.length; i++) {
                                    html.push('<tr><th>' + response.export.receipts[i].receiptno + '</th><th>' + response.export.receipts[i].message + '</th></tr>');
                                }
                                html.push('</tbody>');
                                html.push('</table>');
                                $confirm.find('.message').html(html.join(''));
                            } else {
                                $confirm.find('.message').html(response.export.message);
                            }
                            FwConfirmation.addButton($confirm, 'Ok', true);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            }
        })
        .data({
            focusbatch: function() {
                FwFormField.enable($form.find('div[data-datafield="batchno"]'));
                $form.find('div[data-datafield="batchno"]').attr('data-required', true);
                FwFormField.disable($form.find('div[data-datafield="batchfrom"]'));
                $form.find('div[data-datafield="batchfrom"]').attr('data-required', false).removeClass('error');
                FwFormField.disable($form.find('div[data-datafield="batchto"]'));
                $form.find('div[data-datafield="batchto"]').attr('data-required', false).removeClass('error');

                FwFormField.setValue($form, 'div[data-datafield="viewdates"]', false);
                FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', true);
            },
            focusdates: function() {
                FwFormField.disable($form.find('div[data-datafield="batchno"]'));
                $form.find('div[data-datafield="batchno"]').attr('data-required', false).removeClass('error');
                FwFormField.enable($form.find('div[data-datafield="batchfrom"]'));
                $form.find('div[data-datafield="batchfrom"]').attr('data-required', true);
                FwFormField.enable($form.find('div[data-datafield="batchto"]'));
                $form.find('div[data-datafield="batchto"]').attr('data-required', true);

                FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', false);
                FwFormField.setValue($form, 'div[data-datafield="viewdates"]', true);
            }
        })
    ;

    return $form;
};
//----------------------------------------------------------------------------------------------
RwReceiptProcessingController.onLoadForm = function($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwReceiptProcessingController.ModuleOptions.ReportOptions);
    appOptions = program.getApplicationOptions();

    request.method = "LoadForm";
    FwReport.getData($form, request, function(response) {
        try {
            FwFormField.loadItems($form.find('div[data-datafield="orderbylist"]'), response.orderbylist);
            if (response.qbo.connected == true) {
                $form.find('.exportqbo').removeClass('disabled');
            }
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', true, '', true);
    FwFormField.setValue($form, 'div[data-datafield="processfrom"]', FwFunc.getDate());
    FwFormField.setValue($form, 'div[data-datafield="processto"]', FwFunc.getDate());

    if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
        //$form.find('.qbo').attr('src', window.location.pathname + 'integration/qbointegration/qbointegration.aspx');
        $form.find('.qbointegration').show();
    }
};
//----------------------------------------------------------------------------------------------
RwReceiptProcessingController.loadRelatedValidationFields = function(validationName, $valuefield, $tr) {
    var $form;

    $form = $valuefield.closest('.fwform');
    switch (validationName) {
        case 'BatchAR':
            $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
            break;
    }
};
//----------------------------------------------------------------------------------------------
