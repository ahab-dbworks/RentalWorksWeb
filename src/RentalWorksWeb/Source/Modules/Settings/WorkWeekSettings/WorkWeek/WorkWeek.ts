//routes.push({ pattern: /^module\/workweek$/, action: function (match: RegExpExecArray) { return WorkWeekController.getModuleScreen(); } });

class WorkWeek {
    Module: string = 'WorkWeek';
    apiurl: string = 'api/v1/workweek';
    caption: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.caption;
    nav: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.nav;
    id: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.id;
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

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WorkWeekId"] input').val(uniqueids.WorkWeekId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }


    afterLoad($form: any) {
    }
}

var WorkWeekController = new WorkWeek();