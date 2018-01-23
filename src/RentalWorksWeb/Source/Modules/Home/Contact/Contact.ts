declare var FwModule: any;
declare var FwBrowse: any;
declare var FwServices: any;
declare var FwMenu: any;
declare var Mustache: any;
declare var FwFunc: any;
declare var FwNotification: any;

class Contact {
    Module: string;
    apiurl: string;
    caption: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = 'Contact';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var me: Contact = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, me.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids = function ($form: JQuery) {
        var $contactNoteGrid: any;
        var $contactNoteGridControl: any;

        $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
        $contactNoteGridControl = jQuery(jQuery('#tmpl-grids-ContactNoteGridBrowse').html());
        $contactNoteGrid.empty().append($contactNoteGridControl);
        $contactNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
            };
        })
        $contactNoteGridControl.data('beforesave', function (request) {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($contactNoteGridControl);
        FwBrowse.renderRuntimeHtml($contactNoteGridControl);

        //Company Contact grid
        var $companyContactGrid: any;
        var $companyContactGridControl: any;

        $companyContactGrid = $form.find('div[data-grid="ContactCompanyContactGrid"]');
        $companyContactGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyContactGridBrowse').html());
        $companyContactGrid.empty().append($companyContactGridControl);
        $companyContactGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
            };
        })
        $companyContactGridControl.data('beforesave', function (request) {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });

        FwBrowse.init($companyContactGridControl);
        FwBrowse.renderRuntimeHtml($companyContactGridControl);
    };
    //----------------------------------------------------------------------------------------------
    addGridFilter($form: any) {
        var $menuObject = $form.find('[data-name="ContactCompanyContactGrid"] .fwmenu');
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All Contacts', true);
        var $lead: JQuery = FwMenu.generateDropDownViewBtn('Lead Contacts', false);
        var $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect Contacts', false);
        var $customer: JQuery = FwMenu.generateDropDownViewBtn('Customer Contacts', false);
        var $deal: JQuery = FwMenu.generateDropDownViewBtn('Deal Contacts', false);
        var $vendor: JQuery = FwMenu.generateDropDownViewBtn('Vendor Contacts', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.databind($browse);
        });
        $lead.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LEAD';
            FwBrowse.databind($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.databind($browse);
        });
        $customer.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CUSTOMER';
            FwBrowse.databind($browse);
        });
        $deal.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'DEAL';
            FwBrowse.databind($browse);
        });
        $vendor.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'VENDOR';
            FwBrowse.databind($browse);
        });

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($lead);
        viewSubitems.push($prospect);
        viewSubitems.push($customer);
        viewSubitems.push($deal);
        viewSubitems.push($vendor);

        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

    };
    //---------------------------------------------------------------------------------------------- 
    afterLoad($form: JQuery) {
        var $contactNoteGrid: any;

        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);

        var $companyContactGrid: any;

        $companyContactGrid = $form.find('[data-name="ContactCompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        FwBrowse.addLegend($companyContactGrid, 'Lead', '#ff8040');
        FwBrowse.addLegend($companyContactGrid, 'Prospect', '#ff0080');
        FwBrowse.addLegend($companyContactGrid, 'Customer', '#ffff80');
        FwBrowse.addLegend($companyContactGrid, 'Deal', '#03de3a');
        FwBrowse.addLegend($companyContactGrid, 'Vendor', '#20b7ff');

        this.addGridFilter($form);
    };
}

(window as any).ContactController = new Contact();