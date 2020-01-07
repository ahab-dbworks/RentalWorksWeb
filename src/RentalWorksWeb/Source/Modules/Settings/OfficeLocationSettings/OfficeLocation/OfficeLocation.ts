class OfficeLocation {
    Module: string = 'OfficeLocation';
    apiurl: string = 'api/v1/officelocation';
    caption: string = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.caption;
    nav: string = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.nav;
    id: string = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
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

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'SystemNumberGrid',
            gridSecurityId: 'aUMum8mzxVrWc',
            moduleSecurityId: this.id,

            $form: $form,
            pageSize: 20,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = true;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    LocationId: FwFormField.getValueByDataField($form, `LocationId`)
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="LocationId"] input').val(uniqueids.LocationId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $systemNumberGrid = $form.find('[data-name="SystemNumberGrid"]');
        FwBrowse.search($systemNumberGrid);

    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'LocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelocation`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
}

var OfficeLocationController = new OfficeLocation();