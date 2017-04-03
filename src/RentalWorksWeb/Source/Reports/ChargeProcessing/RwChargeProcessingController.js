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
RwChargeProcessingController.onLoadForm = function($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwChargeProcessingController.ModuleOptions.ReportOptions);
    appOptions = program.getApplicationOptions();

    request.method = "LoadForm";
    FwReport.getData($form, request, function(response) {
        try {
            FwFormField.loadItems($form.find('div[data-datafield="orderbylist"]'), response.orderbylist);
            $form.data('locationid', response.locationid);
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    FwFormField.setValue($form, 'div[data-datafield="viewbatch"]', true, '', true);
    FwFormField.setValue($form, 'div[data-datafield="asofdate"]', FwFunc.getDate());

    if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
        $form.find('.qbo').attr('src', window.location.pathname + 'integration/qbointegration/qbointegration.aspx');
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
RwChargeProcessingController.verifyAuthToken = function() {
    return sessionStorage.authToken != '';
};
//----------------------------------------------------------------------------------------------
RwChargeProcessingController.getFormData = function() {
    var formdata = {}, $form;

    $form               = jQuery('.chargeprocessingexport');
    formdata.locationid = $form.data('locationid');
    formdata.viewbatch  = FwFormField.getValue($form, 'div[data-datafield="viewbatch"]');
    formdata.batchno    = FwFormField.getValue($form, 'div[data-datafield="batchno"]');
    formdata.viewdates  = FwFormField.getValue($form, 'div[data-datafield="viewdates"]');
    formdata.batchfrom  = FwFormField.getValue($form, 'div[data-datafield="batchfrom"]');
    formdata.batchto    = FwFormField.getValue($form, 'div[data-datafield="batchto"]');
    formdata.run        = true;


    if ((formdata.viewbatch == 'T') && (formdata.batchno == '')) {
        $form.find('div[data-datafield="batchno"]').addClass('error');
        formdata.run = false;
    } else if (formdata.viewdates == 'T') {
        if (formdata.batchfrom == '') {
            $form.find('div[data-datafield="batchfrom"]').addClass('error');
            formdata.run = false;
        }
        if (formdata.batchto == '') {
            $form.find('div[data-datafield="batchto"]').addClass('error');
            formdata.run = false;
        }
    }

    return formdata;
};
//----------------------------------------------------------------------------------------------
