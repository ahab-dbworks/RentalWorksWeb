class UserProfile {
    Module: string = 'UserProfile';
    apiurl: string = 'api/v1/userprofile';
    id: string = 'DrTcbvvUw92V';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, 'User Profile', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        const $browsedefaultrows = $form.find('div[data-datafield="BrowseDefaultRows"]');
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
        // First Day of Week select
        const $firstDayofWeek = $form.find('div[data-datafield="FirstDayOfWeek"]');
        FwFormField.loadItems($firstDayofWeek, [
            { value: '0', text: 'Sunday' },
            { value: '1', text: 'Monday' },
            { value: '2', text: 'Tuesday' },
            { value: '3', text: 'Wednesday' },
            { value: '4', text: 'Thursday' },
            { value: '5', text: 'Friday' },
            { value: '6', text: 'Saturday' },
        ], true);

        const $applicationtheme = $form.find('div[data-datafield="ApplicationTheme"]');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-material', text: 'Material' }
        ], true);

        // Load Default Home Page Options, Exclude Settings Modules.
        const defaultHomePages = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            const settingsString = 'settings';
            if (moduleController.hasOwnProperty('nav') && moduleController.nav.indexOf(settingsString) === -1) {
                if (nodeModule) {
                    if (nodeModule.hasOwnProperty('properties')) {
                        if (FwApplicationTree.isVisibleInSecurityTree(moduleController.id)) {
                            modules.push({ value: moduleController.id, text: moduleCaption, nav: moduleController.nav });
                        }
                    }
                }
            }
        });
        //defaultHomePages.push({ value: 'DF8111F5-F022-40B4-BAE6-23B2C6CF3705', text: 'Dashboard', nav: 'module/dashboard' }); // Dashboard will be excluded without this
        FwApplicationTree.sortModules(defaultHomePages);
        const $defaultHomePage = $form.find('div[data-datafield="HomeMenuGuid"]');
        FwFormField.loadItems($defaultHomePage, defaultHomePages, true);

        // Load Available Modules
        const favoritesModules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('nav')) {
                if (nodeModule) {
                    if (nodeModule.hasOwnProperty('properties')) {
                        if (FwApplicationTree.isVisibleInSecurityTree(moduleController.id)) {
                            if (moduleController.nav.startsWith('settings')) {
                                modules.push({ value: `module/${moduleController.Module}`, text: moduleCaption, selected: 'T' });
                            } else {
                                modules.push({ value: moduleController.nav, text: moduleCaption, selected: 'T' });
                            }
                        }
                    }
                }
            }
        });
        FwApplicationTree.sortModules(favoritesModules);
        const $availModules = $form.find('.available-modules');
        FwFormField.loadItems($availModules, favoritesModules, true);

        const userId = JSON.parse(sessionStorage.getItem('userid'));
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        $form.data('beforesave', request => {
            const $selectedModules = FwFormField.getValue2($form.find('.selected-modules'));
            const modules: any = [];
            for (let i = 0; i < $selectedModules.length; i++) {
                if ($selectedModules[i].selected === "true") {
                    modules.push({
                        text: $selectedModules[i].text
                        , value: $selectedModules[i].value
                        , selected: 'T'
                    });
                }
            }
            request.FavoritesJson = JSON.stringify(modules);
        });

        //first sortable list
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

                //hide checkboxes when moving item from "selected modules" to "available modules"
                const $parentField = jQuery(evt.item.parentElement).parents('[data-type="checkboxlist"]');
                if ($parentField.hasClass('available-modules')) {
                    jQuery(evt.item).find('input').css('display', 'none');
                }
            }
        });

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        // Sound Validation
        $form.find('div[data-datafield="SuccessSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="SuccessSoundFileName"]', FwBrowse.getValueByDataField($form, $tr, 'FileName'));
        });
        $form.find('div[data-datafield="ErrorSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ErrorSoundFileName"]', FwBrowse.getValueByDataField($form, $tr, 'FileName'));
        });
        $form.find('div[data-datafield="NotificationSoundId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="NotificationSoundFileName"]', FwBrowse.getValueByDataField($form, $tr, 'FileName'));
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
        $form.find('div[data-datafield="HomeMenuGuid"]').on("change", e => {
            const dataNav = jQuery(e.currentTarget).find(':selected').attr('data-nav');
            FwFormField.setValueByDataField($form, 'HomeMenuPath', dataNav);
        });

    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        try {
            //parameters.closetab = true;
            //parameters.afterCloseForm = () => {

            //};
            FwModule.saveForm(this.Module, $form, parameters);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        const homePage: any = {
            guid: FwFormField.getValueByDataField($form, 'HomeMenuGuid'),
            path: FwFormField.getValueByDataField($form, 'HomeMenuPath'),
        };
        sessionStorage.setItem('homePage', JSON.stringify(homePage));

        const sounds: any = {
            successSoundFileName: FwFormField.getValueByDataField($form, 'SuccessSoundFileName'),
            errorSoundFileName: FwFormField.getValueByDataField($form, 'ErrorSoundFileName'),
            notificationSoundFileName: FwFormField.getValueByDataField($form, 'NotificationSoundFileName'),
        };
        sessionStorage.setItem('sounds', JSON.stringify(sounds));

        //FirstDayOfWeek set to sessionStorage
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        userid.firstdayofweek = +FwFormField.getValueByDataField($form, 'FirstDayOfWeek');
        sessionStorage.setItem('userid', JSON.stringify(userid));

        sessionStorage.setItem('browsedefaultrows', FwFormField.getValueByDataField($form, 'BrowseDefaultRows'));
        sessionStorage.setItem('applicationtheme', FwFormField.getValueByDataField($form, 'ApplicationTheme'));
        sessionStorage.setItem('favorites', FwFormField.getValueByDataField($form, 'FavoritesJson'));

        //remove unchecked modules
        $form.find('.selected-modules li[data-selected="F"]').remove();
        window.location.reload(false);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //const browseRows = sessionStorage.getItem('browsedefaultrows');
        //const theme = sessionStorage.getItem('applicationtheme');
        //jQuery($form.find('div.fwformfield[data-datafield="BrowseDefaultRows"] select')).val(browseRows);
        //jQuery($form.find('div.fwformfield[data-datafield="ApplicationTheme"] select')).val(theme);

        let selectedModules = FwFormField.getValueByDataField($form, 'FavoritesJson');
        if (selectedModules.length > 0) {
            selectedModules = JSON.parse(selectedModules);
            FwFormField.loadItems($form.find('.selected-modules'), selectedModules);

            //remove duplicates(selected modules) from available modules list 
            const $availableModules = $form.find('.available-modules');
            for (let i = 0; i < selectedModules.length; i++) {
                $availableModules.find(`[data-value="${selectedModules[i].value}"]`).remove();
            }
        }

        //if there is no theme set, selects the first option
        const theme = FwFormField.getValueByDataField($form, 'ApplicationTheme');
        if ($form.find(`[data-datafield="ApplicationTheme"] option[value="${theme}"]`).length === 0) FwFormField.setValueByDataField($form, 'ApplicationTheme', $form.find('[data-datafield="ApplicationTheme"] option').eq(0).val());
    }
    //----------------------------------------------------------------------------------------------
}
var UserProfileController = new UserProfile();