routes.push({ pattern: /^module\/deal$/, action: function (match: RegExpExecArray) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return DealController.getModuleScreen(filter); } });

class Deal {
    Module:  string = 'Deal';
    apiurl:  string = 'api/v1/deal';
    caption: string = 'Deal';
    nav:     string = 'module/deal';
    id:      string = '393DE600-2911-4753-85FD-ABBC4F0B1407';
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var self          = this;
        var screen: any   = {};
        screen.$view      = FwModule.getModuleControl(`${this.Module}Controller`);

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        //var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse     = FwModule.openBrowse($browse);

        let hasDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        if (!hasDefaults) {
            FwAppData.apiMethod(true, 'GET', `api/v1/control/1`, null, FwServices.defaultTimeout, function onSuccess(res) {
                let ControlDefaults = {
                    defaultdealstatusid:       res.DefaultDealStatusId,
                    defaultdealstatus:         res.DefaultDealStatus,
                    defaultcustomerstatusid:   res.DefaultCustomerStatusId,
                    defaultcustomerstatus:     res.DefaultCustomerStatus,
                    defaultdealbillingcycleid: res.DefaultDealBillingCycleId,
                    defaultdealbillingcycle:   res.DefaultDealBillingCycle
                }
                sessionStorage.setItem('controldefaults', JSON.stringify(ControlDefaults));
            }, null, null);
        }

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?: any) {
        //var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form     = FwModule.openForm($form, mode);

        this.events($form);

        if (mode === 'NEW') {
            let officeLocation = JSON.parse(sessionStorage.getItem('location'));
            let dealDefaults   = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="LocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="DealStatusId"]', dealDefaults.defaultcustomerstatusid, dealDefaults.defaultdealstatus);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', dealDefaults.defaultdealbillingcycleid, dealDefaults.defaultdealbillingcycle);
        }
        // SUBMODULES
        //var $submoduleQuoteBrowse = this.openQuoteBrowse($form);
        //$form.find('.quote').append($submoduleQuoteBrowse);
        //$submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        //$submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
        //    var $quoteForm, controller, $browse, quoteFormData: any = {};
        //    $browse = jQuery(this).closest('.fwbrowse');
        //    controller = $browse.attr('data-controller');
        //    quoteFormData.DealId = FwFormField.getValueByDataField($form, 'DealId');
        //    quoteFormData.Deal = FwFormField.getValueByDataField($form, 'Deal');
        //    quoteFormData.RateTypeId = FwFormField.getValueByDataField($form, 'DefaultRate');
        //    quoteFormData.RateType = FwFormField.getTextByDataField($form, 'DefaultRate');
        //    quoteFormData.BillingCycleId = FwFormField.getValueByDataField($form, 'BillingCycleId');
        //    quoteFormData.BillingCycle = FwFormField.getTextByDataField($form, 'BillingCycleId');
        //    if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
        //    if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
        //    $quoteForm = window[controller]['openForm']('NEW', quoteFormData);
        //    FwModule.openSubModuleTab($browse, $quoteForm);
        //});

        var $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order').append($submoduleOrderBrowse);
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var orderFormData: any = {};
            var $browse            = jQuery(this).closest('.fwbrowse');
            var controller         = $browse.attr('data-controller');
            var orderFormData: any = {
                DealId:          FwFormField.getValueByDataField($form, 'DealId'),
                Deal:            FwFormField.getValueByDataField($form, 'Deal'),
                RateTypeId:      FwFormField.getValueByDataField($form, 'DefaultRate'),
                RateType:        FwFormField.getTextByDataField($form, 'DefaultRate'),
                BillingCycleId:  FwFormField.getValueByDataField($form, 'BillingCycleId'),
                BillingCycle:    FwFormField.getTextByDataField($form, 'BillingCycleId')
            }
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            var $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="CustomerId"]', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);


        $form.find('.contractSubModule').append(this.openContractBrowse($form));
        //$form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        //$form.find('.receiptSubModule').append(this.openReceiptBrowse($form));

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        let $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    //openInvoiceBrowse($form) {
    //    let dealId = FwFormField.getValueByDataField($form, 'DealId');
    //    let $browse;
    //    $browse = InvoiceController.openBrowse();
    //    $browse.data('ondatabind', function (request) {
    //        request.activeviewfields = InvoiceController.ActiveViewFields;
    //        request.uniqueids = {
    //            DealId: dealId
    //        };
    //    });
    //    return $browse;
    //}
    //---------------------------------------------------------------------------------------------
    //openReceiptBrowse($form) {
    //    let dealId = FwFormField.getValueByDataField($form, 'DealId');
    //    let $browse;
    //    $browse = ReceiptController.openBrowse();
    //    $browse.data('ondatabind', function (request) {
    //        request.activeviewfields = ReceiptController.ActiveViewFields;
    //        request.uniqueids = {
    //            DealId: dealId
    //        };
    //    });
    //    return $browse;
    //}
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any):void {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any):void {
        let uniqueid = FwFormField.getValueByDataField($form, 'DealId');
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        var val_ship = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
        this.toggleShippingAddressInfo($form, val_ship);

        this.shippingAddressTypeChange($form);
        this.transferDealAddressValues($form);

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            const tabname        = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage        = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        // If user changes customer, update corresponding address fields in other tabs
        $form.find('div[data-datafield="CustomerId"]').data('onchange', e => {
            this.customerChange($form);
        });
        // If user updates general address info
        $form.find('.deal_address input').on('change', e => {
            this.transferDealAddressValues($form);
        });
        //Shipping Address Type Change
        $form.find('div[data-datafield="ShippingAddressType"]').on('change', e => {
            var val = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
            this.toggleShippingAddressInfo($form, val);
            this.shippingAddressTypeChange($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    shippingAddressTypeChange($form: any): void {
        switch (FwFormField.getValueByDataField($form, 'ShippingAddressType')) {
            case 'CUSTOMER':
                const CUSTOMERID = FwFormField.getValueByDataField($form, 'CustomerId');
                FwAppData.apiMethod(true, 'GET', `api/v1/customer/${CUSTOMERID}`, null, FwServices.defaultTimeout, response => {
                    this.loadCustomerShippingValues($form, response);
                }, null, null);
                break;
            case 'DEAL':
                FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
                FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
                FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
                FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
                FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
                FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
                break;
            //case 'OTHER':
            //    FwFormField.enable($form.find('div[data-datafield="ShipToAttention"]'));
            //    break;
        }
    }
    //----------------------------------------------------------------------------------------------
    transferDealAddressValues($form: any): void {
        setTimeout(() => { // Wrapped in a setTimeout because text value in Country validation was not resetting prior to setting values
            // Shipping Tab
            if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'DEAL') {
                FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
                FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
                FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
                FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
                FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
                FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
            }
        }, 1000)
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
        const $dealNoteGrid    = $form.find('div[data-grid="DealNoteGrid"]');
        const $dealNoteControl = FwBrowse.loadGridFromTemplate('DealNoteGrid');
        $dealNoteGrid.empty().append($dealNoteControl);
        $dealNoteControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId:  FwFormField.getValueByDataField($form, 'DealId')
            }
        });
        $dealNoteControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
        })
        FwBrowse.init($dealNoteControl);
        FwBrowse.renderRuntimeHtml($dealNoteControl);
        // ----------
        const $dealShipperGrid        = $form.find('div[data-grid="DealShipperGrid"]');
        const $dealShipperGridControl = FwBrowse.loadGridFromTemplate('DealShipperGrid');
        $dealShipperGrid.empty().append($dealShipperGridControl);
        $dealShipperGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            }
        });
        $dealShipperGridControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId')
        });

        FwBrowse.init($dealShipperGridControl);
        FwBrowse.renderRuntimeHtml($dealShipperGridControl);
        // ----------
        const $companyContactGrid:    any = $form.find(`div[data-grid="CompanyContactGrid"]`);
        const $companyContactControl: any = FwBrowse.loadGridFromTemplate('CompanyContactGrid');
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'DealId')
            }
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    }
    //----------------------------------------------------------------------------------------------
    customerChange($form: any): void {
        const CUSTOMERID = FwFormField.getValueByDataField($form, 'CustomerId');
        FwAppData.apiMethod(true, 'GET', `api/v1/customer/${CUSTOMERID}`, null, FwServices.defaultTimeout, response => {
            // Deal tab
            FwFormField.setValueByDataField($form, 'Address1', response.Address1);
            FwFormField.setValueByDataField($form, 'Address2', response.Address2);
            FwFormField.setValueByDataField($form, 'City', response.City);
            FwFormField.setValueByDataField($form, 'State', response.State);
            FwFormField.setValueByDataField($form, 'ZipCode', response.ZipCode);
            FwFormField.setValueByDataField($form, 'Phone', response.Phone);
            FwFormField.setValueByDataField($form, 'Phone800', response.Phone800);
            FwFormField.setValueByDataField($form, 'Fax', response.Fax);
            FwFormField.setValueByDataField($form, 'PhoneOther', response.OtherPhone);
            FwFormField.setValue($form, 'div[data-datafield="CountryId"]', response.CountryId, response.Country);
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', response.PaymentTermsId, response.PaymentTerms);
            // Shipping Address tab defaults
            if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'CUSTOMER') {
                this.loadCustomerShippingValues($form, response);
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    loadCustomerShippingValues($form: any, response: any): void {
        FwFormField.setValueByDataField($form, 'ShipAddress1', '');
        FwFormField.setValueByDataField($form, 'ShipAddress2', '');
        FwFormField.setValueByDataField($form, 'ShipCity', '');
        FwFormField.setValueByDataField($form, 'ShipState', '');
        FwFormField.setValueByDataField($form, 'ShipZipCode', '');
        FwFormField.setValueByDataField($form, 'ShipCountryId', '', '');

        FwFormField.setValueByDataField($form, 'ShipAddress1', response.ShipAddress1);
        FwFormField.setValueByDataField($form, 'ShipAddress2', response.ShipAddress2);
        FwFormField.setValueByDataField($form, 'ShipCity', response.ShipCity);
        FwFormField.setValueByDataField($form, 'ShipState', response.ShipState);
        FwFormField.setValueByDataField($form, 'ShipZipCode', response.ShipZipCode);
        FwFormField.setValueByDataField($form, 'ShipCountryId', response.ShipCountryId, response.ShipCountry);
    }
    //----------------------------------------------------------------------------------------------
    openQuoteBrowse($form) {
        const $browse = QuoteController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openOrderBrowse($form) {
        const $browse = OrderController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    toggleShippingAddressInfo($form: JQuery, isOther: boolean) {
        var list = [
            'ShipAddress1',
            'ShipAddress2',
            'ShipCity',
            'ShipState',
            'ShipZipCode',
            'ShipCountryId'];

        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    disableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.disable($form.find(`[data-datafield="${e}"]`)); });
    }
    //----------------------------------------------------------------------------------------------
    enableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.enable($form.find(`[data-datafield="${e}"]`)); });
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Deal" data-control="FwBrowse" data-type="Browse" id="DealBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="DealController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="DealId" data-browsedatatype="key" ></div>
          </div>
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Number" data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Type" data-datafield="DealType" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Status" data-datafield="DealStatus" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="dealform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="DealController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="DealId"></div>
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="dealtab" class="tab" data-tabpageid="dealtabpage" data-caption="Deal"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Order"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="dealtabpage" class="tabpage" data-tabid="dealtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Deal section -->
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Deal" style="flex:1 1 300px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="DealNumber" data-required="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-required="true" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DealTypeId" data-displayfield="DealType" data-validationname="DealTypeValidation" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Classification" data-datafield="DealClassificationId" data-displayfield="DealClassification" data-validationname="DealClassificationValidation" style="flex:1 1 200px;"></div>
                          <!--<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Production Type" data-datafield="ProductionTypeId" data-displayfield="ProductionType" data-validationname="ProductionTypeValidation" style="flex:1 1 200px;"></div>-->
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="DealStatusId" data-displayfield="DealStatus" data-validationname="DealStatusValidation" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expected Wrap Date" data-datafield="ExpectedWrapDate" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Address section -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="City" data-datafield="City" style="flex:2 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield deal_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Contact Numbers section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="800 Phone" data-datafield="Phone800" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="PhoneOther" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- CSR section -->
                    <div class="flexcolumn" style="flex:1 1 275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer Service Representative">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="CSR" data-datafield="CsrId" data-displayfield="Csr" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Default Agent / Project Manager section -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Agent / Project Manager">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="DefaultAgentId" data-displayfield="DefaultAgent" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="DefaultProjectManagerId" data-displayfield="DefaultProjectManager" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            
              <!-- ##### ORDER tab ##### -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order" data-tabid="ordertab"></div>

              <!-- ##### SHIPPING tab ##### -->
              <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 275px;">
                      <!-- Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield shipping_address_type_radio" data-caption="" data-datafield="ShippingAddressType" style="flex:1 1 250px;">
                            <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                            <div data-value="DEAL" data-caption="Use Deal"></div>
                            <div data-value="OTHER" data-caption="Use Other"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Deliver section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Outgoing Delivery" style="margin-top:13px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 250px;">
                            <div data-value="DELIVER" data-caption="Deliver to Customer"></div>
                            <div data-value="SHIP" data-caption="Ship to Customer"></div>
                            <div data-value="PICK UP" data-caption="Customer Pick Up"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Delivery Return section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Incoming Delivery" style="margin-top:12px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 250px;">
                            <div data-value="DELIVER" data-caption="Customer Deliver"></div>
                            <div data-value="SHIP" data-caption="Customer Ship"></div>
                            <div data-value="PICK UP" data-caption="Pick Up from Customer"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 550px;">
                      <!-- Default Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="ShipAttention" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="ShipCountryId" data-displayfield="ShipCountry" data-validationname="CountryValidation" data-enabled="false" style="float:left;width:175px;"></div>
                        </div>
                      </div>
                      <!-- Carrier section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Carrier" style="flex:1 1 550px;">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="DealShipperGrid" data-securitycaption="Deal Shipper"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### CONTACTS ##### tab -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Deal Contacts"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### NOTES tab ##### -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="DealNoteGrid" data-securitycaption="Deal Notes"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
}

var DealController = new Deal();