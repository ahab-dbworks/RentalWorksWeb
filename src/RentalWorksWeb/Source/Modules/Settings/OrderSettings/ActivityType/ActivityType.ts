class ActivityType {
    Module: string = 'ActivityType';
    apiurl: string = 'api/v1/activitytype';
    caption: string = Constants.Modules.Settings.children.OrderSettings.children.ActivityType.caption;
    nav: string = Constants.Modules.Settings.children.OrderSettings.children.ActivityType.nav;
    id: string = Constants.Modules.Settings.children.OrderSettings.children.ActivityType.id;
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
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ActivityTypeId"] input').val(uniqueids.ActivityTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        FwBrowse.renderGrid({
            nameGrid: 'ActivityStatusGrid',
            gridSecurityId: 'E7cf8EVeQXuUY',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,

            onDataBind: (request: any) => {
                request.uniqueids = {
                    ActivityTypeId: FwFormField.getValueByDataField($form, 'ActivityTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.ActivityTypeId = FwFormField.getValueByDataField($form, 'ActivityTypeId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const $activityStatusGrid = $form.find('[data-name="ActivityStatusGrid"]');
        FwBrowse.search($activityStatusGrid);
    }
    //----------------------------------------------------------------------------------------------
}

var ActivityTypeController = new ActivityType();