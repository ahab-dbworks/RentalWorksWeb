var Contact = /** @class */ (function () {
    function Contact() {
        //----------------------------------------------------------------------------------------------
        this.renderGrids = function ($form) {
            var $contactNoteGrid;
            var $contactNoteGridControl;
            $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
            $contactNoteGridControl = jQuery(jQuery('#tmpl-grids-ContactNoteGridBrowse').html());
            $contactNoteGrid.empty().append($contactNoteGridControl);
            $contactNoteGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
                };
            });
            $contactNoteGridControl.data('beforesave', function (request) {
                request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
            });
            FwBrowse.init($contactNoteGridControl);
            FwBrowse.renderRuntimeHtml($contactNoteGridControl);
            //Company Contact grid
            var $companyContactGrid;
            var $companyContactGridControl;
            $companyContactGrid = $form.find('div[data-grid="CompanyContactGrid"]');
            $companyContactGridControl = jQuery(jQuery('#tmpl-grids-CompanyContactGridBrowse').html());
            $companyContactGrid.empty().append($companyContactGridControl);
            $companyContactGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
                };
            });
            $companyContactGridControl.data('beforesave', function (request) {
                request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
            });
            FwBrowse.init($companyContactGridControl);
            FwBrowse.renderRuntimeHtml($companyContactGridControl);
            this.addLegend($form);
        };
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = 'Contact';
        this.ActiveView = 'ALL';
    }
    Contact.prototype.getModuleScreen = function () {
        var me = this;
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
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
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.loadForm = function (uniqueids) {
        var $form = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    ;
    //----------------------------------------------------------------------------------------------
    Contact.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    ;
    //----------------------------------------------------------------------------------------------
    //addGridFilter($form: any) {
    //    var $menuObject = $form.find('[data-name="CompanyContactGrid"] .fwmenu');
    //    var self = this;
    //    var $all: JQuery = FwGridMenu.generateDropDownViewBtn('All Contacts', true);
    //    var $lead: JQuery = FwGridMenu.generateDropDownViewBtn('Lead Contacts', false);
    //    var $prospect: JQuery = FwGridMenu.generateDropDownViewBtn('Prospect Contacts', false);
    //    var $customer: JQuery = FwGridMenu.generateDropDownViewBtn('Customer Contacts', false);
    //    var $deal: JQuery = FwGridMenu.generateDropDownViewBtn('Deal Contacts', false);
    //    var $vendor: JQuery = FwGridMenu.generateDropDownViewBtn('Vendor Contacts', false);
    //    $all.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'ALL';
    //        FwBrowse.databind($browse);
    //    });
    //    $lead.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'LEAD';
    //        FwBrowse.databind($browse);
    //    });
    //    $prospect.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'PROSPECT';
    //        FwBrowse.databind($browse);
    //    });
    //    $customer.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'CUSTOMER';
    //        FwBrowse.databind($browse);
    //    });
    //    $deal.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'DEAL';
    //        FwBrowse.databind($browse);
    //    });
    //    $vendor.on('click', function () {
    //        var $browse;
    //        $browse = jQuery(this).closest('.fwbrowse');
    //        self.ActiveView = 'VENDOR';
    //        FwBrowse.databind($browse);
    //    });
    //    FwGridMenu.addVerticleSeparator($menuObject);
    //    var viewSubitems: Array<JQuery> = [];
    //    viewSubitems.push($all);
    //    viewSubitems.push($lead);
    //    viewSubitems.push($prospect);
    //    viewSubitems.push($customer);
    //    viewSubitems.push($deal);
    //    viewSubitems.push($vendor);
    //    var $view;
    //    $view = FwGridMenu.addViewBtn($menuObject, 'View', viewSubitems);
    //};
    //---------------------------------------------------------------------------------------------- 
    Contact.prototype.addLegend = function ($form) {
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.addLegend($companyContactGrid, 'Lead', '#ff8040');
        FwBrowse.addLegend($companyContactGrid, 'Prospect', '#ff0080');
        FwBrowse.addLegend($companyContactGrid, 'Customer', '#ffff80');
        FwBrowse.addLegend($companyContactGrid, 'Deal', '#03de3a');
        FwBrowse.addLegend($companyContactGrid, 'Vendor', '#20b7ff');
    };
    //----------------------------------------------------------------------------------------------
    Contact.prototype.afterLoad = function ($form) {
        var $contactNoteGrid;
        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);
        //this.addGridFilter($form);
    };
    ;
    return Contact;
}());
window.ContactController = new Contact();
//# sourceMappingURL=Contact.js.map