routes.push({ pattern: /^module\/billing$/, action: function (match: RegExpExecArray) { return BillingController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class Billing {
    Module: string = 'Billing';
    apiurl: string = 'api/v1/billing';
    caption: string = Constants.Modules.Billing.children.Billing.caption;
    nav: string = Constants.Modules.Billing.children.Billing.nav;
    id: string = Constants.Modules.Billing.children.Billing.id;
    ActiveView: string = 'ALL';
    SessionId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasEdit = false;
        options.hasDelete = false;
        options.hasFind = false;
        FwMenu.addBrowseMenuButtons(options);

        //FwMenu.addSubMenuItem(options.$groupOptions, `XXXXXXXXXXx`, `pnHZApj7X6kKU`, (e: JQuery.ClickEvent) => {
        //    //try {
        //    //    this.xxxxxxxxxxxx($browse);
        //    //} catch (ex) {
        //    //    FwFunc.showError(ex);
        //    //}
        //});
    }
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
    //getSecurityDependencies() {
    //    var dependencies: SecurityDependency[] = [];
    //    dependencies.push({
    //        id: 'Va6jzxcGVll8',
    //        name: 'Agent>Order',
    //        description: 'Required to view Orders.'
    //    });
    //    dependencies.push({
    //        id: 'QHbwnxEN2Ud9',
    //        name: 'Billing>Invoice',
    //        description: 'Required to Create Invoices.'
    //    });
    //    return dependencies;
    //}
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'UserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
            case 'OrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    renderBrowseFilterPopup($browse) {
        let $popup = jQuery(`
                <div id="billingSearchPopup" class="fwcontrol fwcontainer fwform" data-controller="BillingController" data-control="FwContainer" data-type="form" style="background-color:white; padding:0px 0px 10px 0px; border:2px solid gray; min-width:350px;">
                  <div style="background-color:#2196F3;height:60px;width:494px;color:white;"><span style="position:absolute;top:5px;padding:15px;font-size:1.1em">Billing Search</span> <div class="close-modal" style="position:absolute; right:5px; top:5px; cursor:pointer;"><i class="material-icons">clear</i></div></div>
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
                          <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Orders with a Pending PO" data-datafield="ShowOrdersWithPendingPO"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Completed Orders even if their Billing Cycle is not yet complete" data-datafield="BillIfComplete"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Combine Multiple Billing Periods on One Invoice" data-datafield="CombinePeriods"></div>
                          <!--<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Calculate Invoice Totals while searching" data-datafield="IncludeTotals"></div>-->
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
        FwFormField.setValueByDataField($popup, 'BillIfComplete', "T");
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
                    , ShowOrdersWithPendingPO: FwFormField.getValueByDataField($popup, 'ShowOrdersWithPendingPO')
                    , BillIfComplete: FwFormField.getValueByDataField($popup, 'BillIfComplete')
                    , CombinePeriods: FwFormField.getValueByDataField($popup, 'CombinePeriods')
                    //, IncludeTotals: FwFormField.getValueByDataField($popup, 'IncludeTotals')
                };
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/populate`, request, FwServices.defaultTimeout, response => {
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
                            request.pagesize = max;
                        });
                        $billingMessageBrowse.find('.pager .col2, .pager .col3').hide();
                        const msgRequest: any = {};
                        msgRequest.uniqueids = {
                            SessionId: this.SessionId
                        }
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/billingmessage/browse`, msgRequest, FwServices.defaultTimeout, response => {
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
        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($popup, 'BillAsOfDate', today);

        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValueByDataField($popup, 'OfficeLocationId', location.locationid, location.location);

        $browse
            .data('onrowdblclick', e => {
                let $this = jQuery(e.currentTarget);
                let orderId = $this.find('[data-browsedatafield="OrderId"]').attr('data-originalvalue');
                if (typeof orderId !== 'undefined') {
                    const orderInfo: any = { OrderId: orderId };

                    const orderNumber = $this.find('[data-browsedatafield="OrderNumber"]').attr('data-originalvalue');
                    const orderDescription = $this.find('[data-browsedatafield="OrderDescription"]').attr('data-originalvalue');
                    if (FwApplicationTree.isVisibleInSecurityTree('Va6jzxcGVll8')) {
                        const $orderForm = OrderController.loadForm(orderInfo);
                        FwModule.openModuleTab($orderForm, `${orderNumber} ${orderDescription}`, true, 'FORM', true);
                    } else {
                        FwFunc.showMessage('Viewing Orders is disabled for your security group.');
                    }
                }
            })
            .off('keydown', 'tbody tr')
            .on('keydown', 'tbody tr', e => {
                var code;
                try {
                    code = e.keyCode || e.which;
                    switch (code) {
                        case 13: //Enter Key
                            const $this = jQuery(e.currentTarget);
                            const orderId = $this.find('[data-browsedatafield="OrderId"]').attr('data-originalvalue');
                            if (typeof orderId !== 'undefined') {
                                const orderInfo: any = { OrderId: orderId };
                                if (FwApplicationTree.isVisibleInSecurityTree('Va6jzxcGVll8')) {
                                    const $orderForm = OrderController.loadForm(orderInfo);
                                    const orderNumber = $this.find('[data-browsedatafield="OrderNumber"]').attr('data-originalvalue');
                                    const orderDescription = $this.find('[data-browsedatafield="OrderDescription"]').attr('data-originalvalue');
                                    FwModule.openModuleTab($orderForm, `${orderNumber} ${orderDescription}`, true, 'FORM', true);
                                } else {
                                    FwFunc.showMessage('Viewing Orders is disabled for your security group.');
                                }
                            }
                            break;
                        case 37: //Left Arrow Key
                            if ($browse.attr('data-type') === 'Browse') {
                                FwBrowse.prevPage($browse);
                            }
                            return false;
                        case 38: //Up Arrow Key
                            if ($browse.attr('data-type') === 'Browse') {
                                FwBrowse.selectPrevRow($browse);
                                return false;
                            }
                        case 39: //Right Arrow Key
                            if ($browse.attr('data-type') === 'Browse') {
                                FwBrowse.nextPage($browse);
                            }
                            return false;
                        case 40: //Down Arrow Key
                            if ($browse.attr('data-type') === 'Browse') {
                                FwBrowse.selectNextRow($browse);
                                return false;
                            }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        $popup.find('[data-datafield="DealId"] input').focus();
        this.createInvoicesEvents($browse);
    }
    //----------------------------------------------------------------------------------------------
    createInvoicesEvents($browse) {
        //Adds "Create Invoices" button to the menu bar 
        if (FwApplicationTree.isVisibleInSecurityTree('QHbwnxEN2Ud9')) {
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
                }

                request = {
                    SessionId: this.SessionId,
                    BillingIds: ids
                }

                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/createinvoices`, request, FwServices.defaultTimeout, function onSuccess(response) {
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.find('.pager .col2, .pager .col3').hide();

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }


        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingId"] input').val(uniqueids.BillingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var BillingController = new Billing();