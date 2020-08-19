routes.push({ pattern: /^module\/contact$/, action: function (match) { return ContactController.getModuleScreen(); } });
class Contact {
    constructor() {
        this.Module = 'Contact';
        this.apiurl = 'api/v1/contact';
        this.caption = Constants.Modules.Home.Contact.caption;
        this.nav = Constants.Modules.Home.Contact.nav;
        this.id = Constants.Modules.Home.Contact.id;
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
    openBrowse() {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        FwFormField.setValueByDataField($form, 'ActiveDate', FwFunc.getDate());
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
        FwFormField.getDataField($form, 'DefaultDealId').data('beforevalidate', ($validationbrowse, $object, request, datafield, $tr) => {
            request.uniqueids.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwFormField.getDataField($form, 'LocationId').on('change', (e) => {
            try {
                FwFormField.setValueByDataField($form, 'WarehouseId', '', '');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFormField.getDataField($form, 'WarehouseId').data('beforevalidate', ($validationbrowse, $object, request, datafield, $tr) => {
            request.uniqueids.LocationId = FwFormField.getValueByDataField($form, 'LocationId');
        });
        return $form;
    }
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);
        $form.find('.orderSubModule').append(this.openOrderBrowse($form));
        $form.find('.quoteSubModule').append(this.openQuoteBrowse($form));
        return $form;
    }
    openOrderBrowse($form) {
        const contactId = FwFormField.getValueByDataField($form, 'ContactId');
        const $browse = OrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                ContactId: contactId
            };
        });
        return $browse;
    }
    openQuoteBrowse($form) {
        const contactId = FwFormField.getValueByDataField($form, 'ContactId');
        const $browse = QuoteController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                ContactId: contactId
            };
        });
        return $browse;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    renderGrids($form) {
        const $companyContactGrid = $form.find('div[data-grid="ContactCompanyGrid"]');
        const $companyContactGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyGridBrowse').html());
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
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/companycontact/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($companyContactGrid, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $companyContactGrid);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
        const $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
        const $contactNoteGridControl = jQuery(FwBrowse.loadGridFromTemplate('ContactNoteGrid'));
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
        const $contactPersonalEventGrid = $form.find('div[data-grid="ContactPersonalEventGrid"]');
        const $contactPersonalEventGridControl = jQuery(FwBrowse.loadGridFromTemplate('ContactPersonalEventGrid'));
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
    }
    afterLoad($form) {
        const $contactCompanyGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($contactCompanyGrid);
        const $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);
        const $contactPersonalEventGrid = $form.find('[data-name="ContactPersonalEventGrid"]');
        FwBrowse.search($contactPersonalEventGrid);
    }
}
var ContactController = new Contact();
//# sourceMappingURL=Contact.js.map