routes.push({ pattern: /^module\/vendor$/, action: function (match) { return VendorController.getModuleScreen(); } });
class Vendor {
    constructor() {
        this.Module = 'Vendor';
        this.apiurl = 'api/v1/vendor';
        this.caption = Constants.Modules.Home.Vendor.caption;
        this.nav = Constants.Modules.Home.Vendor.nav;
        this.id = Constants.Modules.Home.Vendor.id;
    }
    getModuleScreen() {
        var self = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        this.events($form);
        var $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.purchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        FwFormField.setValueByDataField($form, 'DefaultSubRentDaysPerWeek', 0);
        FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
        FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);
        FwFormField.loadItems($form.find('div[data-datafield="VendorNameType"]'), [
            { value: 'COMPANY', text: 'Company' },
            { value: 'PERSON', text: 'Person' }
        ], true);
        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            { value: 'DELIVER', text: 'Vendor Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Pick Up' }
        ], true);
        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Vendor Pick Up' }
        ], true);
        FwFormField.loadItems($form.find('div[data-datafield="DefaultRate"]'), [
            { value: 'DAILY', text: 'Daily Rate' },
            { value: 'WEEKLY', text: 'Weekly Rate' },
            { value: 'MONTHLY', text: 'Monthly Rate' }
        ], true);
        return $form;
    }
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    openPurchaseOrderBrowse($form) {
        let $browse = PurchaseOrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PurchaseOrderController.ActiveViewFields;
            request.uniqueids = {
                VendorId: FwFormField.getValueByDataField($form, 'VendorId')
            };
        });
        return $browse;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    loadAudit($form) {
        var uniqueid = FwFormField.getValueByDataField($form, 'VendorId');
        FwModule.loadAudit($form, uniqueid);
    }
    afterLoad($form) {
        var $vendorTaxOptionGrid = $form.find('[data-name="VendorTaxOptionGrid"]');
        FwBrowse.search($vendorTaxOptionGrid);
        var $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
        FwBrowse.search($vendorNoteGrid);
        var $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);
        var $purchaseOrderBrowse = $form.find('#PurchaseOrderBrowse');
        FwBrowse.search($purchaseOrderBrowse);
        this.toggleType($form, FwFormField.getValueByDataField($form, 'VendorNameType'));
    }
    events($form) {
        $form.on('change', 'div[data-datafield="VendorNameType"]', e => {
            this.toggleType($form, FwFormField.getValue2(jQuery(e.currentTarget)));
        });
    }
    getTab($target) {
        return $target.closest('.tabpage');
    }
    toggleType($form, value) {
        var $companypanel = $form.find('.company_panel');
        var $personpanel = $form.find('.person_panel');
        $companypanel.hide();
        FwFormField.disable($companypanel.find('.fwformfield'));
        $personpanel.hide();
        FwFormField.disable($personpanel.find('.fwformfield'));
        switch (value) {
            case 'COMPANY':
                $companypanel.show();
                FwFormField.enable($companypanel.find('.fwformfield'));
                break;
            case 'PERSON':
                $personpanel.show();
                FwFormField.enable($personpanel.find('.fwformfield'));
                break;
        }
    }
    renderGrids($form) {
        var $vendorTaxOptionGrid = $form.find('div[data-grid="VendorTaxOptionGrid"]');
        var $vendorTaxOptionControl = FwBrowse.loadGridFromTemplate('VendorTaxOptionGrid');
        $vendorTaxOptionGrid.empty().append($vendorTaxOptionControl);
        $vendorTaxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
            };
        });
        $vendorTaxOptionControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        });
        FwBrowse.init($vendorTaxOptionControl);
        FwBrowse.renderRuntimeHtml($vendorTaxOptionControl);
        var $vendorNoteGrid = $form.find('div[data-grid="VendorNoteGrid"]');
        var $vendorNoteControl = FwBrowse.loadGridFromTemplate('VendorNoteGrid');
        $vendorNoteGrid.empty().append($vendorNoteControl);
        $vendorNoteControl.data('ondatabind', function (request) {
            request.uniqueids = {
                VendorId: FwFormField.getValueByDataField($form, 'VendorId')
            };
        });
        FwBrowse.init($vendorNoteControl);
        FwBrowse.renderRuntimeHtml($vendorNoteControl);
        var nameCompanyContactGrid = 'CompanyContactGrid';
        var $companyContactGrid = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        var $companyContactControl = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
            };
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    }
}
var VendorController = new Vendor();
//# sourceMappingURL=Vendor.js.map