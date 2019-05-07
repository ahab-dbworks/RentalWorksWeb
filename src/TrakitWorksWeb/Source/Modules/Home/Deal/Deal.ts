routes.push({ pattern: /^module\/deal$/, action: function (match: RegExpExecArray) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return DealController.getModuleScreen(filter); } });

class Deal {
    Module:  string = 'Deal';
    apiurl:  string = 'api/v1/deal';
    caption: string = Constants.Modules.Home.Deal.caption;
    nav:     string = Constants.Modules.Home.Deal.nav;
    id:      string = Constants.Modules.Home.Deal.id;
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
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
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
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        this.events($form);

        if (mode === 'NEW') {
            let officeLocation = JSON.parse(sessionStorage.getItem('location'));
            let dealDefaults   = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="LocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="DealStatusId"]', dealDefaults.defaultcustomerstatusid, dealDefaults.defaultdealstatus);
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
                Deal:            FwFormField.getValueByDataField($form, 'Deal')
            }
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            var $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="CustomerId"]', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
        }

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            if ($form.data('mode') !== 'NEW') {
                const tabpageid = jQuery(e.currentTarget).data('tabpageid');

                const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                if ($gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    }
                }

                const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                if ($browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
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

        FwTabs.enableTab($form.find('.tab.orders'));
        FwTabs.enableTab($form.find('.tab.contacts'));
        FwTabs.enableTab($form.find('.tab.notes'));
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
}

var DealController = new Deal();