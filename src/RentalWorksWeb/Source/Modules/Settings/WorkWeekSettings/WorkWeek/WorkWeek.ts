//routes.push({ pattern: /^module\/workweek$/, action: function (match: RegExpExecArray) { return WorkWeekController.getModuleScreen(); } });

class WorkWeek {
    Module: string = 'WorkWeek';
    apiurl: string = 'api/v1/workweek';
    caption: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.caption;
    nav: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.nav;
    id: string = Constants.Modules.Settings.children.WorkWeekSettings.children.WorkWeek.id;


    getModuleScreen() {
        let screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Work Week', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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