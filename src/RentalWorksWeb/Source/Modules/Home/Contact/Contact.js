var Contact = (function () {
    function Contact() {
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
            var $companyContactGrid;
            var $companyContactGridControl;
            $companyContactGrid = $form.find('div[data-grid="ContactCompanyGrid"]');
            $companyContactGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyGridBrowse').html());
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
    Contact.prototype.openBrowse = function () {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    ;
    Contact.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    ;
    Contact.prototype.loadForm = function (uniqueids) {
        var $form = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    ;
    Contact.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
    ;
    Contact.prototype.addLegend = function ($form) {
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.addLegend($companyContactGrid, 'Lead', '#ff8040');
        FwBrowse.addLegend($companyContactGrid, 'Prospect', '#ff0080');
        FwBrowse.addLegend($companyContactGrid, 'Customer', '#ffff80');
        FwBrowse.addLegend($companyContactGrid, 'Deal', '#03de3a');
        FwBrowse.addLegend($companyContactGrid, 'Vendor', '#20b7ff');
    };
    Contact.prototype.afterLoad = function ($form) {
        var $contactNoteGrid;
        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($companyContactGrid);
    };
    ;
    return Contact;
}());
window.ContactController = new Contact();
//# sourceMappingURL=Contact.js.map