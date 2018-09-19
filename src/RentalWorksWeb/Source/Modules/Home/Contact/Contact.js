routes.push({ pattern: /^module\/contact$/, action: function (match) { return ContactController.getModuleScreen(); } });
class Contact {
    constructor() {
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = 'Contact';
        this.ActiveView = 'ALL';
    }
    getModuleScreen() {
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
    }
    ;
    openBrowse() {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    ;
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'ActiveDate', today);
            $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);
                if ($this.prop('checked') === true) {
                    FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', today);
                }
                else {
                    FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', "");
                }
            });
        }
        return $form;
    }
    ;
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    ;
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    renderGrids($form) {
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
        var $contactPersonalEventGrid;
        var $contactPersonalEventGridControl;
        $contactPersonalEventGrid = $form.find('div[data-grid="ContactPersonalEventGrid"]');
        $contactPersonalEventGridControl = jQuery(jQuery('#tmpl-grids-ContactPersonalEventGridBrowse').html());
        $contactPersonalEventGrid.empty().append($contactPersonalEventGridControl);
        $contactPersonalEventGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
            };
        });
        $contactPersonalEventGridControl.data('beforesave', function (request) {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($contactPersonalEventGridControl);
        FwBrowse.renderRuntimeHtml($contactPersonalEventGridControl);
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
    }
    ;
    addLegend($form) {
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.addLegend($companyContactGrid, 'Lead', '#ff8040');
        FwBrowse.addLegend($companyContactGrid, 'Prospect', '#ff0080');
        FwBrowse.addLegend($companyContactGrid, 'Customer', '#ffff80');
        FwBrowse.addLegend($companyContactGrid, 'Deal', '#03de3a');
        FwBrowse.addLegend($companyContactGrid, 'Vendor', '#20b7ff');
    }
    afterLoad($form) {
        var $contactNoteGrid;
        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);
        var $contactPersonalEventGrid;
        $contactPersonalEventGrid = $form.find('[data-name="ContactPersonalEventGrid"]');
        FwBrowse.search($contactPersonalEventGrid);
        var $companyContactGrid;
        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($companyContactGrid);
        $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                const today = FwFunc.getDate();
                FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', today);
            }
            else {
                FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', "");
            }
        });
    }
    ;
}
var ContactController = new Contact();
//# sourceMappingURL=Contact.js.map