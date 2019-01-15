var chargeProcessingFrontEndTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport chargeprocessingexport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Charge Processing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ChargeProcessingController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
      <div id="exporttab" class="tab exporttab" data-tabpageid="exporttabpage" data-caption="Export"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="formcolumn">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Batch">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div style="float:left;margin:9px 10px 0 0;"><div class="createbatch fwformcontrol" data-type="button">Process All Approved Invoices</div></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of" data-datafield="asofdate" style="float:left;width:46%;"></div>
              </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View Batch">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-datafield="viewbatch" style="float:left;width:10%;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Batch No" data-datafield="batchno" data-validationname="BatchInvoicesValidation" style="float:left;width:45%;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Exported" data-datafield="exported" data-enabled="false" style="float:left;width:45%;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-datafield="viewdates" style="float:left;width:10%;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date Range From" data-datafield="batchfrom" data-enabled="false" style="float:left;width:45%;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date Range To" data-datafield="batchto" data-enabled="false" style="float:left;width:45%;"></div>
              </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Data By">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-datafield="orderbylist" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-sortable="true" data-orderby="true" style="float:left;width:300px;margin-left:10px;"></div>
              </div>
            </div>
          </div>
          <div class="formcolumn">
            <div class="fwcontrol fwcontainer fwform-section qbointegration" data-control="FwContainer" data-type="section" data-caption="QuickBooks Online Integration" style="display:none;">
              <div style="text-align:center;margin-top:9px;"><div class="exportqbo qbobutton disabled">Export batch to Quickbooks</div></div>
            </div>
          </div>
        </div>
      </div>
      <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab">
      </div>
    </div>
  </div>
</div>
`;

class ChargeProcessingControllerClass extends FwWebApiReport {
    Module: string = 'ChargeProcessing';
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    };
    caption: string = 'Process Deal Invoices';
    nav: string = 'module/chargeprocessing';
    id: string = '5DB3FB9C-6F86-4696-867A-9B99AB0D6647';
    //----------------------------------------------------------------------------------------------
    constructor() {
        //this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
        super('ChargeProcessing', '', chargeProcessingFrontEndTemplate);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $form;
        screen            = {};
        screen.$view      = FwModule.getModuleControl(this.Module + 'Controller');

        $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm() {
        var $form, batchid, batchno;
    
        batchid = "";

        //$form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form = this.getFrontEnd();
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
    onLoadForm($form) {
        var request: any = {}, appOptions;
        //FwReport.load($form, this.ModuleOptions.ReportOptions);
        this.load($form, this.reportOptions);
        appOptions = program.getApplicationOptions();

        request.method = "LoadForm";
        FwReport.getData($form, request, function(response: any) {
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
    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;

        $form = $valuefield.closest('.fwform');
        switch (validationName) {
            case 'BatchInvoices':
                $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
                break;
        }
    };
    //----------------------------------------------------------------------------------------------
};
var ChargeProcessingController = new ChargeProcessingControllerClass();

