
var vendorInvoiceProcessingFrontEndTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport vendorinvoiceprocessingexport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor Invoice Processing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorInvoiceProcessingController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
      <div id="exporttab" class="tab exporttab" data-tabpageid="exporttabpage" data-caption="Export"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="formcolumn">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Batch">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div style="float:left;margin:9px 10px 0 0;"><div class="createbatch fwformcontrol" data-type="button">Process All Approved Vendor Invoices</div></div>
              </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View Batch">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Batch No" data-datafield="batchno" data-validationname="BatchVendorInvoiceValidation" data-required="true" style="float:left;width:45%;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Exported" data-datafield="exported" data-enabled="false" style="float:left;width:45%;"></div>
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

class VendorInvoiceProcessingControllerClass extends FwWebApiReport {
    Module: string = 'VendorInvoiceProcessing';
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    };
    caption: string = 'Process Vendor Invoices';
    nav: string = 'module/vendorinvoiceprocessing';
    id: string = '4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240';
    //----------------------------------------------------------------------------------------------
    constructor() {
        //this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
        super('VendorInvoiceProcessing', '', vendorInvoiceProcessingFrontEndTemplate);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $form;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');

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
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });

        $form
            .on('click', '.createbatch', function () {
                var request;
                request = {
                    method: 'CreateCharge'
                };
                FwReport.getData($form, request, function (response) {
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
            .on('click', '.exportqbo', function () {
                var request, run;
                if (jQuery(this).hasClass('disabled')) {
                    FwNotification.renderNotification('WARNING', 'Not currently connected to Quickbooks Online.');
                } else {
                    run = true;
                    request = {
                        method: 'ExportToQBO',
                        batchno: FwFormField.getValue($form, 'div[data-datafield="batchno"]')
                    }

                    if (request.batchno == '') {
                        $form.find('div[data-datafield="batchno"]').addClass('error');
                        run = false;
                    }

                    if (run) {
                        var $confirm = FwConfirmation.renderConfirmation('Export batch to Quickbooks', 'Running please wait...');
                        FwReport.getData($form, request, function (response) {
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
                            } catch (ex) {
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
    onLoadForm($form) {
        var request: any = {}, appOptions;
        //FwReport.load($form, this.ModuleOptions.ReportOptions);
        this.load($form, this.reportOptions);
        appOptions = program.getApplicationOptions();

        request.method = "LoadForm";
        FwReport.getData($form, request, function (response) {
            try {
                FwFormField.loadItems($form.find('div[data-datafield="orderbylist"]'), response.orderbylist);
                if (response.qbo.connected == true) {
                    $form.find('.exportqbo').removeClass('disabled');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
            $form.find('.qbointegration').show();
        }
    };
    //----------------------------------------------------------------------------------------------
    loadRelatedValidationFields(validationName, $valuefield, $tr) {
        var $form;

        $form = $valuefield.closest('.fwform');
        switch (validationName) {
            case 'BatchVendorInvoice':
                $form.find('div[data-datafield="exported"] input.fwformfield-value').val($tr.find('.field[data-browsedatafield="chgbatchdate"]').attr('data-originalvalue'));
                break;
        }
    };
    //----------------------------------------------------------------------------------------------
};

var VendorInvoiceProcessingController = new VendorInvoiceProcessingControllerClass();