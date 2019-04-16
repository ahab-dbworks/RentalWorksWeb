class Crew {
    Module: string = 'Crew';
    apiurl: string = 'api/v1/crew';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Crew', false, 'BROWSE', true);
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
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="ExpirePassword"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ExpireDays"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ExpireDays"]'))
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CrewId"] input').val(uniqueids.CrewId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CrewId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $crewPositionGrid = $form.find('div[data-grid="CrewPositionGrid"]');
        const $crewPositionGridControl = FwBrowse.loadGridFromTemplate('CrewPositionGrid');
        $crewPositionGrid.empty().append($crewPositionGridControl);
        $crewPositionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                CrewId: FwFormField.getValueByDataField($form, 'CrewId')
            };
        });
        $crewPositionGridControl.data('beforesave', request => {
            request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        });
        FwBrowse.init($crewPositionGridControl);
        FwBrowse.renderRuntimeHtml($crewPositionGridControl);

        const $crewLocationGrid = $form.find('div[data-grid="CrewLocationGrid"]');
        const $crewLocationGridControl = FwBrowse.loadGridFromTemplate('CrewLocationGrid');
        $crewLocationGrid.empty().append($crewLocationGridControl);
        $crewLocationGridControl.data('ondatabind', request => {
            request.uniqueids = {
                CrewId: FwFormField.getValueByDataField($form, 'CrewId')
            };
        });
        $crewLocationGridControl.data('beforesave', request => {
            request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        });
        FwBrowse.init($crewLocationGridControl);
        FwBrowse.renderRuntimeHtml($crewLocationGridControl);

        const $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
        const $contactNoteGridControl = FwBrowse.loadGridFromTemplate('ContactNoteGrid');
        $contactNoteGrid.empty().append($contactNoteGridControl);
        $contactNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContactId: FwFormField.getValueByDataField($form, 'CrewId')
            };
        })
        $contactNoteGridControl.data('beforesave', request => {
            request.ContactId = FwFormField.getValueByDataField($form, 'CrewId');
        });
        FwBrowse.init($contactNoteGridControl);
        FwBrowse.renderRuntimeHtml($contactNoteGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $crewPositionGrid = $form.find('[data-name="CrewPositionGrid"]');
        FwBrowse.search($crewPositionGrid);

        const $crewLocationGrid = $form.find('[data-name="CrewLocationGrid"]');
        FwBrowse.search($crewLocationGrid);

        const $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);

        if ($form.find('[data-datafield="ExpirePassword"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ExpireDays"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ExpireDays"]'))
        }
    }
}
//----------------------------------------------------------------------------------------------
var CrewController = new Crew();