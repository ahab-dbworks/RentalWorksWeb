class Crew {
    Module: string = 'Crew';
    apiurl: string = 'api/v1/crew';
    caption: string = Constants.Modules.Settings.children.LaborSettings.children.Crew.caption;
    nav: string = Constants.Modules.Settings.children.LaborSettings.children.Crew.nav;
    id: string = Constants.Modules.Settings.children.LaborSettings.children.Crew.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="ExpirePassword"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ExpireDays"]'))
            } else {
                FwFormField.disable($form.find('[data-datafield="ExpireDays"]'))
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CrewId"] input').val(uniqueids.CrewId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //const $crewPositionGrid = $form.find('div[data-grid="CrewPositionGrid"]');
        //const $crewPositionGridControl = FwBrowse.loadGridFromTemplate('CrewPositionGrid');
        //$crewPositionGrid.empty().append($crewPositionGridControl);
        //$crewPositionGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        CrewId: FwFormField.getValueByDataField($form, 'CrewId')
        //    };
        //});
        //$crewPositionGridControl.data('beforesave', request => {
        //    request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        //});
        //FwBrowse.init($crewPositionGridControl);
        //FwBrowse.renderRuntimeHtml($crewPositionGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'CrewPositionGrid',
            gridSecurityId: 'shA9rX1DYWp3',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CrewId: FwFormField.getValueByDataField($form, 'CrewId')
                };
            },
            beforeSave: (request: any) => {
                request.CrewId = FwFormField.getValueByDataField($form, 'CrewId');
            },
        });

        //const $crewLocationGrid = $form.find('div[data-grid="CrewLocationGrid"]');
        //const $crewLocationGridControl = FwBrowse.loadGridFromTemplate('CrewLocationGrid');
        //$crewLocationGrid.empty().append($crewLocationGridControl);
        //$crewLocationGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        CrewId: FwFormField.getValueByDataField($form, 'CrewId')
        //    };
        //});
        //$crewLocationGridControl.data('beforesave', request => {
        //    request.CrewId = FwFormField.getValueByDataField($form, 'CrewId')
        //});
        //FwBrowse.init($crewLocationGridControl);
        //FwBrowse.renderRuntimeHtml($crewLocationGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'CrewLocationGrid',
            gridSecurityId: 'vCrMyhsLCP7h',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CrewId: FwFormField.getValueByDataField($form, 'CrewId')
                };
            },
            beforeSave: (request: any) => {
                request.CrewId = FwFormField.getValueByDataField($form, 'CrewId');
            },
        });

        //const $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
        //const $contactNoteGridControl = FwBrowse.loadGridFromTemplate('ContactNoteGrid');
        //$contactNoteGrid.empty().append($contactNoteGridControl);
        //$contactNoteGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ContactId: FwFormField.getValueByDataField($form, 'CrewId')
        //    };
        //})
        //$contactNoteGridControl.data('beforesave', request => {
        //    request.ContactId = FwFormField.getValueByDataField($form, 'CrewId');
        //});
        //FwBrowse.init($contactNoteGridControl);
        //FwBrowse.renderRuntimeHtml($contactNoteGridControl);


        FwBrowse.renderGrid({
            nameGrid: 'ContactNoteGrid',
            gridSecurityId: 'mkJ1Ry8nqSnw',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContactId: FwFormField.getValueByDataField($form, 'CrewId')
                };
            },
            beforeSave: (request: any) => {
                request.ContactId = FwFormField.getValueByDataField($form, 'CrewId');
            },
        });
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
    //--------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
        }
    }
}
//----------------------------------------------------------------------------------------------
var CrewController = new Crew();