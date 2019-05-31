routes.push({ pattern: /^module\/contact$/, action: function (match: RegExpExecArray) { return ContactController.getModuleScreen(); } });

class Contact {
    Module: string = 'Contact';
    apiurl: string = 'api/v1/contact';
    caption: string = Constants.Modules.Home.Contact.caption;
	nav: string = Constants.Modules.Home.Contact.nav;
	id: string = Constants.Modules.Home.Contact.id;
    ActiveView: string = 'ALL';

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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        //let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form     = FwModule.openForm($form, mode);

        FwFormField.setValueByDataField($form, 'ActiveDate', FwFunc.getDate());

        // Disable / Enable Inactive Date
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

        FwFormField.getDataField($form, 'DefaultDealId').data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield, $tr: JQuery) => {
            request.uniqueids.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwFormField.getDataField($form, 'LocationId').on('change', (e: JQuery.ChangeEvent) => {
            try {
                FwFormField.setValueByDataField($form, 'WarehouseId', '', '');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        });
        FwFormField.getDataField($form, 'WarehouseId').data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield, $tr: JQuery) => {
            request.uniqueids.LocationId = FwFormField.getValueByDataField($form, 'LocationId');
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        //Company Contact grid
        const $companyContactGrid = $form.find('div[data-grid="ContactCompanyGrid"]');
        const $companyContactGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyGridBrowse').html());
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
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/companycontact/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($companyContactGrid, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $companyContactGrid)

        } catch (ex) {
            FwFunc.showError(ex);
        }

        // Contact Node Grid
        const $contactNoteGrid        = $form.find('div[data-grid="ContactNoteGrid"]');
        const $contactNoteGridControl = jQuery(FwBrowse.loadGridFromTemplate('ContactNoteGrid'));
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

        // Personal Event Grid
        const $contactPersonalEventGrid        = $form.find('div[data-grid="ContactPersonalEventGrid"]');
        const $contactPersonalEventGridControl = jQuery(FwBrowse.loadGridFromTemplate('ContactPersonalEventGrid'));
        $contactPersonalEventGrid.empty().append($contactPersonalEventGridControl);
        $contactPersonalEventGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
            };
        })
        $contactPersonalEventGridControl.data('beforesave', function (request) {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($contactPersonalEventGridControl);
        FwBrowse.renderRuntimeHtml($contactPersonalEventGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const $contactCompanyGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($contactCompanyGrid);

        const $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);

        const $contactPersonalEventGrid = $form.find('[data-name="ContactPersonalEventGrid"]');
        FwBrowse.search($contactPersonalEventGrid);
    }
    //----------------------------------------------------------------------------------------------
}
var ContactController = new Contact();