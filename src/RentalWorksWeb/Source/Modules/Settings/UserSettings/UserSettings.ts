class UserSettings {
    Module: string = 'UserSettings';
    apiurl: string = 'api/v1/usersettings';
    id: string = '510F9B3F-601F-4912-833B-56E649962B0D';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('NEW');

        screen.load = function () {
            FwModule.openModuleTab($form, 'User Settings', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        const $browsedefaultrows = $form.find('.browsedefaultrows');
        FwFormField.loadItems($browsedefaultrows, [
            { value: '5', text: '5' },
            { value: '10', text: '10' },
            { value: '15', text: '15' },
            { value: '20', text: '20' },
            { value: '25', text: '25' },
            { value: '30', text: '30' },
            { value: '35', text: '35' },
            { value: '40', text: '40' },
            { value: '45', text: '45' },
            { value: '50', text: '50' },
            { value: '100', text: '100' },
            { value: '200', text: '200' },
            { value: '500', text: '500' },
            { value: '1000', text: '1000' }
        ], true);

        const $applicationtheme = $form.find('.applicationtheme');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-material', text: 'Material' }
        ], true);

        //load App Modules for Home Page
        const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        const mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        const settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        const modules = mainModules.concat(settingsModules);
        const allModules = [];
        const sortableModules = [];
        for (let i = 0; i < modules.length; i++) {
            if (modules[i].properties.visible === "T") {
                const moduleGUID = modules[i].id;
                const moduleCaption = modules[i].properties.caption;
                const moduleController = modules[i].properties.controller;
                if (typeof window[moduleController] !== 'undefined') {
                    if (window[moduleController].hasOwnProperty('apiurl')) {
                        const moduleNav = window[moduleController].nav;
                        if (moduleNav) {
                            allModules.push({ value: moduleGUID, text: moduleCaption, apiurl: moduleNav });
                            sortableModules.push({ value: moduleNav, text: moduleCaption });
                        }
                    }
                }
            }
        };
        allModules.push({ value: 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705', text: 'Dashboard', apiurl: 'module/dashboard' });
        //Sort modules
        function compare(a, b) {
            if (a.text < b.text)
                return -1;
            if (a.text > b.text)
                return 1;
            return 0;
        }
        allModules.sort(compare);
        const $defaultHomePage = $form.find('.default-home-page');
        FwFormField.loadItems($defaultHomePage, allModules, true);

        sortableModules.sort(compare);
        const $availModules = $form.find('.available-modules');
        FwFormField.loadItems($availModules, sortableModules, true);

        const userId = JSON.parse(sessionStorage.getItem('userid'));
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        $form.data('beforesave', request => {
            const $selectedModules = FwFormField.getValue2($form.find('.selected-modules'));
            const modules: any = [];
            for (let i = 0; i < $selectedModules.length; i++) {
                modules.push({
                    text: $selectedModules[i].text
                    , value: $selectedModules[i].value
                });
            }
            request.ToolBarJson = JSON.stringify(modules);
        });

        //first sortable list (not sure if it can be combined)
        Sortable.create($form.find('.sortable').get(0), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        //second sortable list
        Sortable.create($form.find('.sortable').get(1), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        // Sound Validation
        $form.find('div[data-datafield="SuccessSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="SuccessSoundFileName"]', $tr.find('.field[data-formdatafield="FileName"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="ErrorSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ErrorSoundFileName"]', $tr.find('.field[data-formdatafield="FileName"]').attr('data-originalvalue'));
        });
        $form.find('div[data-datafield="NotificationSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="NotificationSoundFileName"]', $tr.find('.field[data-formdatafield="FileName"]').attr('data-originalvalue'));
        });
        // Sound Preview
        $form.find('.success-play-button').on('click', e => {
            const successSoundFileName = FwFormField.getValueByDataField($form, 'SuccessSoundFileName');
            const successSound = new Audio(successSoundFileName);
            successSound.play();
        });
        $form.find('.error-play-button').on('click', e => {
            const errorSoundFileName = FwFormField.getValueByDataField($form, 'ErrorSoundFileName');
            const errorSound = new Audio(errorSoundFileName);
            errorSound.play();
        });
        $form.find('.notification-play-button').on('click', e => {
            const notificationSoundFileName = FwFormField.getValueByDataField($form, 'NotificationSoundFileName');
            const notificationSound = new Audio(notificationSoundFileName);
            notificationSound.play();
        });
        $form.find('div.default-home-page').on("change", function () {
            const moduleUrl = jQuery(this).find(':selected').attr('data-apiurl')
            FwFormField.setValueByDataField($form, 'HomeMenuPath', moduleUrl)
        });

    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);

        const homePage: any = {};
        homePage.guid = FwFormField.getValueByDataField($form, 'HomeMenuGuid');
        homePage.path = FwFormField.getValueByDataField($form, 'HomeMenuPath');

        const sounds: any = {};
        const successSoundFileName = FwFormField.getValueByDataField($form, 'SuccessSoundFileName').toString();
        sounds.successSoundFileName = successSoundFileName;
        const errorSoundFileName = FwFormField.getValueByDataField($form, 'ErrorSoundFileName').toString();
        sounds.errorSoundFileName = errorSoundFileName;
        const notificationSoundFileName = FwFormField.getValueByDataField($form, 'NotificationSoundFileName').toString();
        sounds.notificationSoundFileName = notificationSoundFileName;

        const browseDefaultRows = jQuery($form.find('[data-datafield="BrowseDefaultRows"] select')).val().toString();
        sessionStorage.setItem('browsedefaultrows', browseDefaultRows);
        const applicationTheme = jQuery($form.find('[data-datafield="ApplicationTheme"] select')).val().toString();
        sessionStorage.setItem('applicationtheme', applicationTheme);
        sessionStorage.setItem('sounds', JSON.stringify(sounds));
        sessionStorage.setItem('homePage', JSON.stringify(homePage));

        setTimeout(function () { location.reload(); }, 1000);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const browseRows = sessionStorage.getItem('browsedefaultrows');
        const theme = sessionStorage.getItem('applicationtheme');
        jQuery($form.find('div.fwformfield[data-datafield="BrowseDefaultRows"] select')).val(browseRows);
        jQuery($form.find('div.fwformfield[data-datafield="ApplicationTheme"] select')).val(theme);

        const selectedModules = JSON.parse(FwFormField.getValueByDataField($form, 'ToolBarJson'));
        if (selectedModules.length > 0) {
            FwFormField.loadItems($form.find('.selected-modules'), selectedModules);
        } else {
            //no modules selected text
        }

    }
    //----------------------------------------------------------------------------------------------
}
var UserSettingsController = new UserSettings();