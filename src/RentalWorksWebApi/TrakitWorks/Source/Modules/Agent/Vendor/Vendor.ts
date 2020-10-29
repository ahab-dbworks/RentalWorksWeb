routes.push({ pattern: /^module\/vendor$/, action: function (match: RegExpExecArray) { return VendorController.getModuleScreen(); } });

class Vendor {
    Module:  string = 'Vendor';
    apiurl:  string = 'api/v1/vendor';
    caption: string = Constants.Modules.Agent.children.Vendor.caption;
	nav: string = Constants.Modules.Agent.children.Vendor.nav;
	id: string = Constants.Modules.Agent.children.Vendor.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        var self:    Vendor = this;
        var screen:  any    = {};
        screen.$view        = FwModule.getModuleControl(this.Module + 'Controller');

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
    //---------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);

        var $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.purchaseOrderSubModule').append($submodulePurchaseOrderBrowse);

        FwFormField.setValueByDataField($form, 'DefaultSubRentDaysPerWeek', 0);
        FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
        FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);

        FwFormField.loadItems($form.find('div[data-datafield="VendorNameType"]'), [
            {value:'COMPANY',    text:'Company'},
            {value:'PERSON',     text:'Person'}
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            {value:'DELIVER',  text:'Vendor Deliver'},
            {value:'SHIP',     text:'Ship'},
            {value:'PICK UP',  text:'Pick Up'}
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            {value:'DELIVER',  text:'Deliver'},
            {value:'SHIP',     text:'Ship'},
            {value:'PICK UP',  text:'Vendor Pick Up'}
        ], true);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultRate"]'), [
            {value:'DAILY',   text:'Daily Rate'},
            {value:'WEEKLY',  text:'Weekly Rate'},
            {value:'MONTHLY', text:'Monthly Rate'}
        ], true);

        return $form;
    }
    //---------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------
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
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = FwFormField.getValueByDataField($form, 'VendorId');
        FwModule.loadAudit($form, uniqueid);
    }
    //---------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $vendorTaxOptionGrid = $form.find('[data-name="VendorTaxOptionGrid"]');
        FwBrowse.search($vendorTaxOptionGrid);

        var $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
        FwBrowse.search($vendorNoteGrid);

        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        var $purchaseOrderBrowse = $form.find('#PurchaseOrderBrowse')
        FwBrowse.search($purchaseOrderBrowse);

        this.toggleType($form, FwFormField.getValueByDataField($form, 'VendorNameType'));
    }
    //---------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('change', 'div[data-datafield="VendorNameType"]', e => {
            this.toggleType($form, FwFormField.getValue2(jQuery(e.currentTarget)))
        });
    }
    //---------------------------------------------------------------------------------
    getTab($target: JQuery): JQuery {
        return $target.closest('.tabpage');
    }
    //---------------------------------------------------------------------------------
    toggleType($form: JQuery, value: string): void {
        var $companypanel = $form.find('.company_panel');
        var $personpanel  = $form.find('.person_panel');

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
    //---------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        //var $vendorTaxOptionGrid: JQuery = $form.find('div[data-grid="VendorTaxOptionGrid"]');
        //var $vendorTaxOptionControl: JQuery = FwBrowse.loadGridFromTemplate('VendorTaxOptionGrid');
        //$vendorTaxOptionGrid.empty().append($vendorTaxOptionControl);
        //$vendorTaxOptionControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //$vendorTaxOptionControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        //});
        //FwBrowse.init($vendorTaxOptionControl);
        //FwBrowse.renderRuntimeHtml($vendorTaxOptionControl);

        FwBrowse.renderGrid({
            nameGrid: 'VendorTaxOptionGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            }
        });

        //var $vendorNoteGrid: JQuery = $form.find('div[data-grid="VendorNoteGrid"]');
        //var $vendorNoteControl: JQuery = FwBrowse.loadGridFromTemplate('VendorNoteGrid');
        //$vendorNoteGrid.empty().append($vendorNoteControl);
        //$vendorNoteControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        VendorId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //FwBrowse.init($vendorNoteControl);
        //FwBrowse.renderRuntimeHtml($vendorNoteControl);

        FwBrowse.renderGrid({
            nameGrid: 'VendorNoteGrid',
            gridSecurityId: 'zuywROD73X60O',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
        });

        var nameCompanyContactGrid: string = 'CompanyContactGrid'
        var $companyContactGrid: any = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        var $companyContactControl: any = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
            }
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    }
    //---------------------------------------------------------------------------------
}
var VendorController = new Vendor();