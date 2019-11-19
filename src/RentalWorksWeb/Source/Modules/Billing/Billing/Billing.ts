routes.push({ pattern: /^module\/billing$/, action: function (match: RegExpExecArray) { return BillingController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class Billing {
    Module: string = 'Billing';
    apiurl: string = 'api/v1/billing';
    caption: string = Constants.Modules.Home.Billing.caption;
    nav: string = Constants.Modules.Home.Billing.nav;
    id: string = Constants.Modules.Home.Billing.id;
    ActiveView: string = 'ALL';
    SessionId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            this.renderBrowseFilterPopup($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    renderBrowseFilterPopup($browse) {
        let $popup = jQuery(`
                <div id="billingSearchPopup" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" style="background-color:white; padding:0px 0px 10px 0px; border:2px solid gray; min-width:350px;">
                  <div style="background-color:#2196F3;height:60px;width:412px;color:white;"><span style="position:absolute;top:5px;padding:15px;font-size:1.1em">Billing Search</span> <div class="close-modal" style="position:absolute; right:5px; top:5px; cursor:pointer;"><i class="material-icons">clear</i></div></div>
                  <div class="flexpage">
                    <div class="flexrow">
                      <div class="flexcolumn">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" >
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Bill As Of" data-datafield="BillAsOfDate" data-required="true" style="max-width:150px;"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="UserId" data-displayfield="User" data-validationname="UserValidation"></div>
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="max-width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Orders with a Pending PO" data-datafield="ShowOrdersWithPendingPO"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="If Order is Complete, show even if beyond Bill As of Date" data-datafield="BillIfComplete"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Multiple Billing Periods on One Invoice" data-datafield="CombinePeriods"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Calculate Invoice Totals while searching" data-datafield="IncludeTotals"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div data-type="button" class="fwformcontrol billingSearchButton" style="flex:0 0 60px; margin:auto;">Search</div>
                    </div>
                  </div>
                </div>`);
        FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
        $popup = FwPopup.renderPopup($popup, { 'ismodal': true });
        FwFormField.setValueByDataField($popup, 'ShowOrdersWithPendingPO', "T");
        FwPopup.showPopup($popup);

        $browse.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });

        $popup.data('fields', $popup.find('.fwformfield'));

        //Adds "Search" button to the menu bar 
        $browse.find('.buttonbar').append(`<div class="openBrowseFilter btn" style="display:flex; padding:0 10px; cursor:pointer;"><i class="material-icons">filter_list</i><div class="btn-text">Search</div></div>`);

        $browse.on('click', 'div.openBrowseFilter', e => {
            $popup.show();
            $popup.find('[data-datafield="DealId"] input').focus();
        });

        //selects all checkboxes/records
        $browse.data('afterdatabindcallback', () => {
            $browse.find('thead .cbselectrow').click();
            if ($browse.find('tbody tr').length > 0) {
                $browse.find('.buttonbar .createInvoices').css({ 'pointer-events': '', 'color': '' });
            } else {
                $browse.find('.buttonbar .createInvoices').css({ 'pointer-events': 'none', 'color': 'gray' });
            }
        });

        //populate browse
        let $billingMessageBrowse;
        let $msgTab;
        $popup.on('click', 'div.billingSearchButton', e => {
            let request: any = {};
            const isValid = FwModule.validateForm($popup);

            const $messagesTab = $browse.closest('.tabpages').siblings().find('.tab[data-caption="Search Messages"]');
            if ($messagesTab.length < 1) {
                //Creates Search Messages tab
                $billingMessageBrowse = BillingMessageController.openBrowse();
                $billingMessageBrowse.attr('data-newtab', 'false');
                FwModule.openModuleTab($billingMessageBrowse, 'Search Messages', true, 'BROWSE', false);
                $msgTab = FwTabs.getTabByElement($billingMessageBrowse);
                $msgTab.css({ 'background-color': '#e08080', 'color': '#000077', 'display': 'none' });
            }

            if (isValid === true) {
                $popup.hide();
                request = {
                    BillAsOfDate: FwFormField.getValueByDataField($popup, 'BillAsOfDate')
                    , OfficeLocationId: FwFormField.getValueByDataField($popup, 'OfficeLocationId')
                    , CustomerId: FwFormField.getValueByDataField($popup, 'CustomerId')
                    , DealId: FwFormField.getValueByDataField($popup, 'DealId')
                    , DepartmentId: FwFormField.getValueByDataField($popup, 'DepartmentId')
                    , AgentId: FwFormField.getValueByDataField($popup, 'UserId')
                    , OrderId: FwFormField.getValueByDataField($popup, 'OrderId')
                    , ShowOrdersWithPendingPO: (FwFormField.getValueByDataField($popup, 'ShowOrdersWithPendingPO') == 'T' ? true : false)
                    , BillIfComplete: (FwFormField.getValueByDataField($popup, 'BillIfComplete') == 'T' ? true : false)
                    , CombinePeriods: (FwFormField.getValueByDataField($popup, 'CombinePeriods') == 'T' ? true : false)
                    , IncludeTotals: (FwFormField.getValueByDataField($popup, 'IncludeTotals') == 'T' ? true : false)
                };
                FwAppData.apiMethod(true, 'POST', `api/v1/billing/populate`, request, FwServices.defaultTimeout, response => {
                    //load browse with sessionId
                    const max = 9999;
                    $browse.data('ondatabind', request => {
                        request.uniqueids = {
                            SessionId: response.SessionId
                        }
                        request.pagesize = max;
                    });

                    FwBrowse.search($browse);
                    this.SessionId = response.SessionId;

                    if (response.BillingMessages > 0) {
                        $msgTab.show();
                        $billingMessageBrowse.data('ondatabind', request => {
                            request.uniqueids = {
                                SessionId: this.SessionId
                            }
                        });
                        const msgRequest: any = {};
                        msgRequest.uniqueids = {
                            SessionId: this.SessionId
                        }
                        FwAppData.apiMethod(true, 'POST', `api/v1/billingmessage/browse`, msgRequest, FwServices.defaultTimeout, response => {
                            FwBrowse.search($billingMessageBrowse);
                        }, ex => FwFunc.showError(ex), $browse);
                    } else {
                        $msgTab.hide();
                    }
                }, ex => FwFunc.showError(ex), $browse);
            }
        });

        $popup.on('click', 'div.close-modal', e => {
            $popup.hide();
        });

        //defaults bill as of date to today
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($popup, 'BillAsOfDate', today);

        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValueByDataField($popup, 'OfficeLocationId', location.locationid, location.location);

        $browse
            .off('dblclick', 'tbody tr')
            .on('dblclick', 'tbody tr', e => {
                let $this = jQuery(e.currentTarget);
                let orderId = $this.find('[data-browsedatafield="OrderId"]').attr('data-originalvalue');
                if (typeof orderId !== 'undefined') {
                    let orderInfo: any = {};
                    let $orderForm;
                    let orderNumber = $this.find('[data-browsedatafield="OrderNumber"]').attr('data-originalvalue');
                    let orderDescription = $this.find('[data-browsedatafield="OrderDescription"]').attr('data-originalvalue');
                    orderInfo.OrderId = orderId;
                    $orderForm = OrderController.loadForm(orderInfo);
                    FwModule.openModuleTab($orderForm, `${orderNumber} ${orderDescription}`, true, 'FORM', true);
                }
            });

        $popup.find('[data-datafield="DealId"] input').focus();
        this.createInvoicesEvents($browse);
    }
    //----------------------------------------------------------------------------------------------
    createInvoicesEvents($browse) {
        //Adds "Create Invoices" button to the menu bar 
        $browse.find('.buttonbar').append(`<div class="createInvoices btn" style="display:flex; padding:0 10px; cursor:pointer;"><i class="material-icons">add</i><div class="btn-text">Create Invoices</div></div>`);

        $browse.on('click', '.createInvoices', e => {
            let $selectedCheckBoxes;
            let ids: any = [];
            let request: any = {};
            $selectedCheckBoxes = $browse.find('tbody .cbselectrow:checked');
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let $this = jQuery($selectedCheckBoxes[i]);
                let id;
                id = $this.closest('tr').find('div[data-browsedatafield="BillingId"]').attr('data-originalvalue');
                ids.push(id);
            };

            request = {
                SessionId: this.SessionId
                , BillingIds: ids
            }

            FwAppData.apiMethod(true, 'POST', `api/v1/billing/createinvoices`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    //Open new invoice browse tab
                    let $invoiceBrowse = InvoiceController.openBrowse();
                    //set data-newtab to false to prevent "+" button from being added
                    $invoiceBrowse.attr('data-newtab', 'false');
                    $invoiceBrowse.data('ondatabind', function (request) {
                        request.uniqueids = {
                            InvoiceCreationBatchId: response.InvoiceCreationBatchId
                        }
                        request.pagesize = 999;
                    });
                    FwModule.openModuleTab($invoiceBrowse, 'Invoice', true, 'BROWSE', true);
                    FwBrowse.search($invoiceBrowse);

                    //clear browse and add click event to billing tab to open up the search when clicked
                    let $billingBrowseTab = FwTabs.getTabByElement($browse);
                    $browse.data('ondatabind', function (request) {
                        request.uniqueids = {
                            SessionId: ''
                        }
                    });
                    FwBrowse.search($browse);
                    $billingBrowseTab.off('click');
                    $billingBrowseTab.on('click', e => {
                        $browse.find('.openBrowseFilter').click();
                    });
                } else {
                    FwNotification.renderNotification('ERROR', response.msg);
                }
            }, null, $browse, this.SessionId);
        });
    }

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        $browse.find('.pager .col2, .pager .col3').hide();

        FwBrowse.addLegend($browse, 'Missing Crew Times', '#FF9D9D');
        FwBrowse.addLegend($browse, 'Missing Break Times', '#B7B7FF');
        FwBrowse.addLegend($browse, 'No Charge', '#FF6F6F');
        FwBrowse.addLegend($browse, 'Outside Order Billing Dates', '#00FF00');
        FwBrowse.addLegend($browse, 'Flat PO', '#8888FF');
        FwBrowse.addLegend($browse, 'Repair', '#5EAEAE');
        FwBrowse.addLegend($browse, 'Rebill Adds', '#F709DF');
        FwBrowse.addLegend($browse, 'Has Billing Note', '#00FFFF');
        FwBrowse.addLegend($browse, 'PO Pending', '#EEA011');
        FwBrowse.addLegend($browse, 'L&D', '#3C0040');
        FwBrowse.addLegend($browse, 'Hiatus', '#00B95C');

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingId"] input').val(uniqueids.BillingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
};
//----------------------------------------------------------------------------------------------
var BillingController = new Billing();