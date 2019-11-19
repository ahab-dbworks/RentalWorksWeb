routes.push({ pattern: /^module\/sound$/, action: function (match: RegExpExecArray) { return SoundController.getModuleScreen(); } });
class Sound {
    Module: string = 'Sound';
    apiurl: string = 'api/v1/sound';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Sound', false, 'BROWSE', true);
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

        FwBrowse.addLegend($browse, 'User Defined Sound', '#00FF00');

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form, $moduleSelect;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if ($form.find('div[data-datafield="SystemSound"]').attr('data-originalvalue') === "true") {
            FwFormField.disable($form.find('div[data-datafield="Sound"]'));
            FwFormField.disable($form.find('div[data-datafield="FileName"]'));
        }

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        //var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        //let mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        //let settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule')
        //let modules = mainModules.concat(settingsModules);
        //var allModules = [];
        //for (var i = 0; i < modules.length; i++) {
        //    var moduleNav = modules[i].properties.controller.slice(0, -10);
        //    var moduleCaption = modules[i].properties.caption;
        //    if (moduleCaption === "Designer") {
        //        continue;
        //    }
        //    var moduleController = modules[i].properties.controller;
        //    if (window[moduleController].hasOwnProperty('apiurl')) {
        //        var moduleUrl = window[moduleController].apiurl;
        //        allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
        //    }
        //};

        ////Sort modules
        //function compare(a, b) {
        //    if (a.text < b.text)
        //        return -1;
        //    if (a.text > b.text)
        //        return 1;
        //    return 0;
        //}
        //allModules.sort(compare);

        //$moduleSelect = $form.find('.modules');
        //FwFormField.loadItems($moduleSelect, allModules);

        //$form.find('[data-datafield="SystemRule"]').attr('data-required', false);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SoundId"] input').val(uniqueids.SoundId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        let sound, soundFileName;

        // Sound Preview
        $form.find('.sound-play-button').on('click', e => {
            soundFileName = FwFormField.getValueByDataField($form, 'FileName');
            sound = new Audio(soundFileName);
            sound.play();
        });
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if ($form.find('div[data-datafield="SystemSound"]').attr('data-originalvalue') === "true") {
            FwFormField.disable($form.find('div[data-datafield="Sound"]'));
            FwFormField.disable($form.find('div[data-datafield="FileName"]'));
        }
    }
}

var SoundController = new Sound();