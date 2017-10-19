declare var FwModule: any;
declare var FwBrowse: any;

class Customer {
    Module: string;
    apiurl: string;
    caption: string;
    thisModule: Customer;

    constructor() {
        this.Module = 'Customer';
        this.apiurl = 'api/v1/customer';
        this.caption = 'Customer';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse : any = this.openBrowse();

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

    events($form: JQuery): void {

        $form.on('click', '#companytaxgrid .selected', (e) => {
            this.updateExternalInputsWithGridValues(e.currentTarget);
        });
        
    }

    updateExternalInputsWithGridValues(target: Element): void {
        var $row = jQuery(target);
        $row.find('.column > .field').each((i, e) => {
            var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');

            if (value == undefined || null) {
                jQuery('.' + id).find(':input').val(0);
            } else {
                jQuery('.' + id).find(':input').val(value);
            }

        });
    }

    openBrowse() {
        var $browse: any = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="DisableQuoteOrderActivity"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.quote-order [data-type="checkbox"]'))
            } else {
                FwFormField.disable($form.find('.quote-order [data-type="checkbox"]'))
            }
        });

        this.events($form);

        
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form : any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'CustomerId', uniqueids.CustomerId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid : string = FwFormField.getValueByDataField($form, 'CustomerId');
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        // ----------
        var nameCustomerResaleGrid: string = 'CustomerResaleGrid';
        var $customerResaleGrid: any = $customerResaleGrid = $form.find('div[data-grid="' + nameCustomerResaleGrid + '"]');
        var $customerResaleGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerResaleGrid);

        $customerResaleGrid.empty().append($customerResaleGridControl);
        $customerResaleGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
            };
        });
        $customerResaleGridControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId')
        });
        FwBrowse.init($customerResaleGridControl);
        FwBrowse.renderRuntimeHtml($customerResaleGridControl);

        // ----------
        var nameCustomerNoteGrid: string = 'CustomerNoteGrid';
        var $customerNoteGrid: any = $customerNoteGrid = $form.find('div[data-grid="' + nameCustomerNoteGrid + '"]');
        var $customerNoteGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerNoteGrid);
        $customerNoteGrid.empty().append($customerNoteGridControl);
        $customerNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CustomerId: FwFormField.getValueByDataField($form, 'CustomerId')
            };
        });
        FwBrowse.init($customerNoteGridControl);
        FwBrowse.renderRuntimeHtml($customerNoteGridControl);
        

        // ----------
        var nameCompanyTaxGrid: string = 'CompanyTaxGrid'
        var $companyTaxGrid: any = $companyTaxGrid = $form.find('div[data-grid="' + nameCompanyTaxGrid + '"]');
        var $companyTaxControl: any = FwBrowse.loadGridFromTemplate(nameCompanyTaxGrid);
        $companyTaxGrid.empty().append($companyTaxControl);
        $companyTaxControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
            }
        });
        FwBrowse.init($companyTaxControl);
        FwBrowse.renderRuntimeHtml($companyTaxControl);
    }

    afterLoad($form: any) {
        var $customerResaleGrid: any = $form.find('[data-name="CustomerResaleGrid"]');
        FwBrowse.search($customerResaleGrid);

        var $customerNoteGrid: any = $form.find('[data-name="CustomerNoteGrid"]');
        FwBrowse.search($customerNoteGrid);

        var $companyTaxGrid: any = $form.find('[data-name="CompanyTaxGrid"]');
        FwBrowse.search($companyTaxGrid);
    }
}

(window as any).CustomerController = new Customer();