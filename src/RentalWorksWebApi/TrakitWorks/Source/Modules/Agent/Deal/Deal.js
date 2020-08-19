routes.push({ pattern: /^module\/deal$/, action: function (match) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal\/(\w+)\/(\S+)/, action: function (match) { var filter = { datafield: match[1], search: match[2] }; return DealController.getModuleScreen(filter); } });
class Deal {
    constructor() {
        this.Module = 'Deal';
        this.apiurl = 'api/v1/deal';
        this.caption = Constants.Modules.Agent.children.Deal.caption;
        this.nav = Constants.Modules.Agent.children.Deal.nav;
        this.id = Constants.Modules.Agent.children.Deal.id;
    }
    getModuleScreen(filter) {
        var self = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('');
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
    openBrowse() {
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        let hasDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        if (!hasDefaults) {
            FwAppData.apiMethod(true, 'GET', `api/v1/control/1`, null, FwServices.defaultTimeout, function onSuccess(res) {
                let ControlDefaults = {
                    defaultdealstatusid: res.DefaultDealStatusId,
                    defaultdealstatus: res.DefaultDealStatus,
                    defaultcustomerstatusid: res.DefaultCustomerStatusId,
                    defaultcustomerstatus: res.DefaultCustomerStatus,
                    defaultdealbillingcycleid: res.DefaultDealBillingCycleId,
                    defaultdealbillingcycle: res.DefaultDealBillingCycle
                };
                sessionStorage.setItem('controldefaults', JSON.stringify(ControlDefaults));
            }, null, null);
        }
        return $browse;
    }
    openForm(mode, parentmoduleinfo) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        this.events($form);
        if (mode === 'NEW') {
            let officeLocation = JSON.parse(sessionStorage.getItem('location'));
            let dealDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="LocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="DealStatusId"]', dealDefaults.defaultcustomerstatusid, dealDefaults.defaultdealstatus);
        }
        var $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order').append($submoduleOrderBrowse);
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var orderFormData = {};
            var $browse = jQuery(this).closest('.fwbrowse');
            var controller = $browse.attr('data-controller');
            var orderFormData = {
                DealId: FwFormField.getValueByDataField($form, 'DealId'),
                Deal: FwFormField.getValueByDataField($form, 'Deal')
            };
            if (typeof window[controller] !== 'object')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function')
                throw 'Missing javascript function: ' + controller + '.openForm';
            var $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });
        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="CustomerId"]', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
        }
        FwFormField.loadItems($form.find('div[data-datafield="ShippingAddressType"]'), [
            { value: 'DEAL', caption: 'Use Job', checked: true },
            { value: 'OTHER', caption: 'Use Other' }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Deliver to Job', checked: true },
            { value: 'SHIP', caption: 'Ship to Job', },
            { value: 'PICK UP', caption: 'Job Pick Up' }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Job Deliver', checked: true },
            { value: 'SHIP', caption: 'Job Ship' },
            { value: 'PICK UP', caption: 'Pick Up from Job' }
        ]);
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
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    loadAudit($form) {
        let uniqueid = FwFormField.getValueByDataField($form, 'DealId');
        FwModule.loadAudit($form, uniqueid);
    }
    afterLoad($form) {
        var $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);
        var val_ship = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
        this.toggleShippingAddressInfo($form, val_ship);
        this.shippingAddressTypeChange($form);
        this.transferDealAddressValues($form);
        FwTabs.enableTab($form.find('.tab.orders'));
        FwTabs.enableTab($form.find('.tab.contacts'));
        FwTabs.enableTab($form.find('.tab.notes'));
    }
    events($form) {
        $form.find('div[data-datafield="CustomerId"]').data('onchange', e => {
            this.customerChange($form);
        });
        $form.find('.deal_address input').on('change', e => {
            this.transferDealAddressValues($form);
        });
        $form.find('div[data-datafield="ShippingAddressType"]').on('change', e => {
            var val = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
            this.toggleShippingAddressInfo($form, val);
            this.shippingAddressTypeChange($form);
        });
    }
    shippingAddressTypeChange($form) {
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
        }
    }
    transferDealAddressValues($form) {
        setTimeout(() => {
            if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'DEAL') {
                FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
                FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
                FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
                FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
                FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
                FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
            }
        }, 1000);
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'DealNoteGrid',
            gridSecurityId: 'Est6dgMdThwL',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options) => {
            },
            onDataBind: (request) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request) => {
                request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'DealShipperGrid',
            gridSecurityId: 'MFYWJzFkOeQU',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request) => {
                request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'CompanyContactGrid',
            gridSecurityId: '1rdUfYSzLHzj',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
    }
    customerChange($form) {
        const CUSTOMERID = FwFormField.getValueByDataField($form, 'CustomerId');
        FwAppData.apiMethod(true, 'GET', `api/v1/customer/${CUSTOMERID}`, null, FwServices.defaultTimeout, response => {
            FwFormField.setValueByDataField($form, 'Address1', response.Address1);
            FwFormField.setValueByDataField($form, 'Address2', response.Address2);
            FwFormField.setValueByDataField($form, 'City', response.City);
            FwFormField.setValueByDataField($form, 'State', response.State);
            FwFormField.setValueByDataField($form, 'ZipCode', response.ZipCode);
            FwFormField.setValueByDataField($form, 'Phone', response.Phone);
            FwFormField.setValueByDataField($form, 'PhoneTollFree', response.PhoneTollFree);
            FwFormField.setValueByDataField($form, 'Fax', response.Fax);
            FwFormField.setValueByDataField($form, 'PhoneOther', response.OtherPhone);
            FwFormField.setValue($form, 'div[data-datafield="CountryId"]', response.CountryId, response.Country);
            FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', response.PaymentTermsId, response.PaymentTerms);
            if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'CUSTOMER') {
                this.loadCustomerShippingValues($form, response);
            }
        }, null, null);
    }
    loadCustomerShippingValues($form, response) {
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
    openQuoteBrowse($form) {
        const $browse = QuoteController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            };
        });
        return $browse;
    }
    ;
    openOrderBrowse($form) {
        const $browse = OrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                DealId: FwFormField.getValueByDataField($form, 'DealId')
            };
        });
        return $browse;
    }
    ;
    toggleShippingAddressInfo($form, isOther) {
        var list = [
            'ShipAddress1',
            'ShipAddress2',
            'ShipCity',
            'ShipState',
            'ShipZipCode',
            'ShipCountryId'
        ];
        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    }
    disableFields($form, fields) {
        fields.forEach((e, i) => { FwFormField.disable($form.find(`[data-datafield="${e}"]`)); });
    }
    enableFields($form, fields) {
        fields.forEach((e, i) => { FwFormField.enable($form.find(`[data-datafield="${e}"]`)); });
    }
}
var DealController = new Deal();
//# sourceMappingURL=Deal.js.map