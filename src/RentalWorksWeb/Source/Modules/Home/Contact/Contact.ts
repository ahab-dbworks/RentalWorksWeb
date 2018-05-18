routes.push({ pattern: /^module\/contact$/, action: function (match: RegExpExecArray) { return ContactController.getModuleScreen(); } });

class Contact {
    Module: string = 'Contact';
    apiurl: string = 'api/v1/contact';
    caption: string = 'Contact';
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
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            const today = new Date(Date.now()).toLocaleString().split(',')[0];
            
            FwFormField.setValueByDataField($form, 'ActiveDate', today);

            // Disable / Enable Inactive Date 
            $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);
                if ($this.prop('checked') === true) {
                    const today = new Date(Date.now()).toLocaleString().split(',')[0];
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
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
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

        $companyContactGrid = $form.find('div[data-grid="ContactCompanyGrid"]');
        $companyContactGridControl = jQuery(jQuery('#tmpl-grids-ContactCompanyGridBrowse').html());
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
        this.addLegend($form);
    };
    //---------------------------------------------------------------------------------------------- 
    addLegend($form: any) {
        var $companyContactGrid: any;
        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');

        FwBrowse.addLegend($companyContactGrid, 'Lead', '#ff8040');
        FwBrowse.addLegend($companyContactGrid, 'Prospect', '#ff0080');
        FwBrowse.addLegend($companyContactGrid, 'Customer', '#ffff80');
        FwBrowse.addLegend($companyContactGrid, 'Deal', '#03de3a');
        FwBrowse.addLegend($companyContactGrid, 'Vendor', '#20b7ff');
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        var $contactNoteGrid: any;

        $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);

        var $companyContactGrid: any;

        $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($companyContactGrid);

        // Disable / Enable Inactive Date 
        $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                const today = new Date(Date.now()).toLocaleString().split(',')[0];
                FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', today);
            }
            else {
                FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', "");
            }
        });
    };
    //--------------------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------------------
var ContactController = new Contact();