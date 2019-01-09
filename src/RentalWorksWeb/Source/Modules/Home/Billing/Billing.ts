routes.push({ pattern: /^module\/billing$/, action: function (match: RegExpExecArray) { return BillingController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class Billing {
    Module: string = 'Billing';
    apiurl: string = 'api/v1/billing';
    caption: string = 'Billing';
    nav: string = 'module/billing';
    id: string = '34E0472E-9057-4C66-8CC2-1938B3222569';
    ActiveView: string = 'ALL';
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            //FwBrowse.databind($browse);
            //FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    renderBrowseFilterPopup($browse) {
        let html, $popup;
        html = `<div id="billingSearchPopup" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-rendermode="template" style="background-color:white; padding:15px 0px; border:2px solid gray;">
                  <div class="close-modal" style="position:absolute; right:5px; top:5px; cursor:pointer;"><i class="material-icons">clear</i></div>
                  <div class="flexpage">
                    <div class="flexrow">
                      <div class="flexcolumn">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Bill As Of" data-datafield="BillAsOfDate" data-required="true"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="UserId" data-displayfield="User" data-validationname="UserValidation"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div data-type="button" class="fwformcontrol billingSearchButton" style="flex:0 0 60px; margin:auto;">Search</div>
                    </div>
                  </div>
                </div>`;
        html = jQuery(html);
        FwControl.renderRuntimeControls(html.find('.fwcontrol'));
        $popup = FwPopup.renderPopup(html, { 'ismodal': true });
        FwPopup.showPopup($popup);

        $popup.data('fields', $popup.find('.fwformfield'));

        //Adds "Search" button to the menu bar 
        $browse.find('.buttonbar').append(`<div class="openBrowseFilter" style="display:flex; padding:0 10px; cursor:pointer;"><i class="material-icons">filter_list</i><div class="btn-text">Search</div></div>`);

        $browse.on('click', 'div.openBrowseFilter', e => {
            $popup.show();
        });

        $popup.on('click', 'div.billingSearchButton', e => {
            let request: any = {};
            let isValid;
            isValid = FwModule.validateForm($popup);
            if (isValid === true) {
                $popup.hide();
                request = {
                    BillAsOfDate: FwFormField.getValueByDataField($popup, 'BillAsOfDate')
                    , OfficeLocationId: FwFormField.getValueByDataField($popup, 'OfficeLocationId')
                    , CustomerId: FwFormField.getValueByDataField($popup, 'CustomerId')
                    , DealId: FwFormField.getValueByDataField($popup, 'DealId')
                    , DepartmentId: FwFormField.getValueByDataField($popup, 'DepartmentId')
                    , OrderId: FwFormField.getValueByDataField($popup, 'OrderId')
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/billing/populate`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    //load browse with sessionId 
                    $browse.data('ondatabind', function (request) {
                        request.uniqueids = {
                            SessionId: response
                        }
                    });
                    FwBrowse.search($browse);
                }, null, $browse);
            }
        });

        $popup.on('click', 'div.close-modal', e => {
            $popup.hide();
        });
        }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        this.renderBrowseFilterPopup($browse);
        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingId"] input').val(uniqueids.BillingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        //let $vendorInvoiceItemGrid: any,
        //    $vendorInvoiceItemGridControl: any;

        //$vendorInvoiceItemGrid = $form.find('div[data-grid="VendorInvoiceItemGrid"]');
        //$vendorInvoiceItemGridControl = jQuery(jQuery('#tmpl-grids-VendorInvoiceItemGridBrowse').html());
        //$vendorInvoiceItemGrid.empty().append($vendorInvoiceItemGridControl);
        //$vendorInvoiceItemGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //    }
        //})
        //FwBrowse.init($vendorInvoiceItemGridControl);
        //FwBrowse.renderRuntimeHtml($vendorInvoiceItemGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //let $vendorInvoiceItemGridControl = $form.find('[data-name="VendorInvoiceItemGrid"]');
        //FwBrowse.search($vendorInvoiceItemGridControl);
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
     
    };
};
//----------------------------------------------------------------------------------------------
var BillingController = new Billing();