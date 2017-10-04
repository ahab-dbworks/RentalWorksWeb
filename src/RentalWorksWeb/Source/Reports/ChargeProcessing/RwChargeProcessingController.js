RwChargeProcessingController = {
    Module: 'ChargeProcessing',
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    }
};
RwChargeProcessingController.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, RwChargeProcessingController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwChargeProcessingController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;
    screen            = {};
    screen.$view      = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwChargeProcessingController.openForm();

    screen.load = function () {
        FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
    };
    screen.unload = function () {
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwChargeProcessingController.openForm = function() {
    var $form, batchid, batchno;
    
    batchid = "";

    $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
    $form.data('getexportrequest', function(request) {
        request.parameters = FwReport.getParameters($form);
        return request;
    });

    $form
        .on('click', '.createbatch', function() {
            var request;
            request = {
                method:   'CreateCharge',
                asofdate: FwFormField.getValue($form, 'div[data-datafield="asofdate"]')
            };
            FwReport.getData($form, request, function(response) {
                if ((response.status == '0') && (response.chgbatchid != '')) {
                    FwFormField.setValue($form, 'div[data-datafield="batchno"]', response.chgbatchid, response.chgbatchno, true);
                    FwFormField.setValue($form, 'div[data-datafield="exported"]', response.chgbatchdate);
                } else if ((response.status == '0') && (response.chgbatchid == '')) {
                    FwNotification.renderNotification('WARNING', 'No Approved Invoices to Process.');
                } else {
                    FwFunc.showError(response.msg);
                }
            });
        })
        .on('change', 'div[data-datafield="viewbatch"] input.fwformfield-value', function() {
            if (jQuery(this).is(':checked')) {
                $form.focusbatch();
            } else {
                $form.focusdates();
            }
        })
        .on('change', 'div[data-datafield="viewdates"] input.fwformfield-value', function() {
            if (jQuery(this).is(':checked')) {
                $form.focusdates();
            } else {
                $form.focusbatch();
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
                                html.push('<thead><tr><th style="width:90px;">Invoice No</th><th>Status</th></tr></thead>')
                                html.push('<tbody>');
                                for (var i = 0; i < response.export.invoices.length; i++) {
                                    html.push('<tr><th>' + response.export.invoices[i].invoiceno + '</th><th>' + response.export.invoices[i].message + '</th></tr>');
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
        $form.focusbatch = function() {
            FwFormField.enable($form.find('div[data-datafield="batchno"]'));
            $form.find('div[data-datafield="batchno"]').attr('data-required', true);
            FwFormField.disable($form.find('div[data-datafield="batchfrom"]'));
            $form.find('div[data-datafield="batchfrom"]').attr('data-required', false).removeClass('error');
            FwFormField.disable($form.find('div[data-datafield="batchto"]'));
            $form.find('div[data-datafield="batchto"]').attr('data-required', false).removeClass('error');

            FwFormField.setValue($form, 'div[data-datafield="viewdates"]', false);
            FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', true);
        };
        $form.focusdates = function() {
            FwFormField.disable($form.find('div[data-datafield="batchno"]'));
            $form.find('div[data-datafield="batchno"]').attr('data-required', false).removeClass('error');
            FwFormField.enable($form.find('div[data-datafield="batchfrom"]'));
            $form.find('div[data-datafield="batchfrom"]').attr('data-required', true);
            FwFormField.enable($form.find('div[data-datafield="batchto"]'));
            $form.find('div[data-datafield="batchto"]').attr('data-required', true);

            FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', false);
            FwFormField.setValue($form, 'div[data-datafield="viewdates"]', true);
        };

    return $form;
};
//----------------------------------------------------------------------------------------------
RwChargeProcessingController.onLoadForm = function($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwChargeProcessingController.ModuleOptions.ReportOptions);
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
    FwFormField.setValue($form, 'div[data-datafield="asofdate"]', FwFunc.getDate());

    if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
        $form.find('.qbointegration').show();
    }
};
//----------------------------------------------------------------------------------------------
RwChargeProcessingController.loadRelatedValidationFields = function(validationName, $valuefield, $tr) {
    var $form;

    $form = $valuefield.closest('.fwform');
    switch (validationName) {
        case 'BatchInvoices':
            $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
            break;
    }
};
//----------------------------------------------------------------------------------------------
