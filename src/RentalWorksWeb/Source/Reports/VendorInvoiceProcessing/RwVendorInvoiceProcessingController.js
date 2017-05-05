RwVendorInvoiceProcessingController = {
    Module: 'VendorInvoiceProcessing',
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    }
};
RwVendorInvoiceProcessingController.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, RwVendorInvoiceProcessingController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwVendorInvoiceProcessingController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;
    screen            = {};
    screen.$view      = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwVendorInvoiceProcessingController.openForm();

    screen.load = function () {
        FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
    };
    screen.unload = function () {
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwVendorInvoiceProcessingController.openForm = function() {
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
                method:   'CreateCharge'
            };
            FwReport.getData($form, request, function(response) {
                if (response.chgbatchid != '') {
                    FwFormField.setValue($form, 'div[data-datafield="batchno"]', response.chgbatchid, response.chgbatchno, true);
                    FwFormField.setValue($form, 'div[data-datafield="exported"]', response.chgbatchdate);
                } else if (response.chgbatchid == '') {
                    FwNotification.renderNotification('WARNING', 'No approved vendor invoices to process.');
                } else {
                    FwFunc.showError(response.msg);
                }
            });
        })
        .on('click', '.exportqbo', function() {
            var request, run;
            if (jQuery(this).hasClass('disabled')) {
                FwNotification.renderNotification('WARNING', 'Not currently connected to Quickbooks Online.');
            } else {
                run = true;
                request = {
                    method:    'ExportToQBO',
                    batchno:   FwFormField.getValue($form, 'div[data-datafield="batchno"]')
                }

                if (request.batchno == '') {
                    $form.find('div[data-datafield="batchno"]').addClass('error');
                    run = false;
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
                                for (var i = 0; i < response.export.vendorinvoices.length; i++) {
                                    html.push('<tr><th>' + response.export.vendorinvoices[i].invoiceno + '</th><th>' + response.export.vendorinvoices[i].message + '</th></tr>');
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
    ;

    return $form;
};
//----------------------------------------------------------------------------------------------
RwVendorInvoiceProcessingController.onLoadForm = function($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwVendorInvoiceProcessingController.ModuleOptions.ReportOptions);
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

    if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
        $form.find('.qbointegration').show();
    }
};
//----------------------------------------------------------------------------------------------
RwVendorInvoiceProcessingController.loadRelatedValidationFields = function(validationName, $valuefield, $tr) {
    var $form;

    $form = $valuefield.closest('.fwform');
    switch (validationName) {
        case 'BatchVendorInvoice':
            $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
            break;
    }
};
//----------------------------------------------------------------------------------------------
