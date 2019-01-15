var receiptProcessingFrontEndTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport receiptprocessing" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receipt Processing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ReceiptProcessingController">
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
                <div style="float:left;margin:9px 10px 0 0;"><div class="processreceipts fwformcontrol" data-type="button">Process All Receipts</div></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-datafield="processfrom" style="float:left;width:31%;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-datafield="processto" style="float:left;width:31%;"></div>
              </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View Batch">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="" data-datafield="viewbatch" style="float:left;width:10%;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Batch No" data-datafield="batchno" data-validationname="BatchARValidation" style="float:left;width:45%;"></div>
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


class ReceiptProcessingControllerClass extends FwWebApiReport{
    Module: string = 'ReceiptProcessing';
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    };
    caption: string = 'Process Receipts';
    nav: string = 'module/receiptprocessing';
    id: string = '0BB9B45C-57FA-47E1-BC02-39CEE720792C';
    //----------------------------------------------------------------------------------------------
    constructor() {
        //this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
        super('ReceiptProcessing', '', receiptProcessingFrontEndTemplate);
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
    onLoadForm($form) {
        var request: any = {}, appOptions;
        //FwReport.load($form, this.ModuleOptions.ReportOptions);
        this.load($form, this.reportOptions);
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
    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;

        $form = $valuefield.closest('.fwform');
        switch (validationName) {
            case 'BatchAR':
                $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
                break;
        }
    };
    //----------------------------------------------------------------------------------------------
};

var ReceiptProcessingController = new ReceiptProcessingControllerClass();
