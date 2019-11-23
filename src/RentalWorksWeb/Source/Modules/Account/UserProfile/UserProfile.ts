﻿class UserProfile{
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
        //const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        //const mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        //const settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        //const modules = mainModules.concat(settingsModules);
        //const allModules = [];
        //const sortableModules = [];
        //for (let i = 0; i < modules.length; i++) {
        //    if (modules[i].properties.visible === "T") {
        //        const moduleGUID = modules[i].id;
        //        const moduleCaption = modules[i].properties.caption;
        //        const moduleController = modules[i].properties.controller;
        //        if (typeof window[moduleController] !== 'undefined') {
        //            if (window[moduleController].hasOwnProperty('nav')) {
        //                const moduleNav = (<any>window)[moduleController].nav;
        //                allModules.push({ value: moduleGUID, text: moduleCaption, nav: moduleNav });
        //                sortableModules.push({ value: moduleNav, text: moduleCaption, selected: 'T' });
        //            }
        //        }
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

        // Load Default Home Page
        const allModules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('nav')) {
                modules.push({ value: moduleController.id, text: moduleCaption, nav: moduleController.nav });
            }
        });
        FwApplicationTree.sortModules(allModules);
        const $defaultHomePage = $form.find('.default-home-page');
        FwFormField.loadItems($defaultHomePage, allModules, true);

        // Load Available Modules
        const sortableModules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('nav')) {
                modules.push({ value: moduleController.nav, text: moduleCaption, selected: 'T'});
            }
        });
        FwApplicationTree.sortModules(allModules);
        const $availModules = $form.find('.available-modules');
        FwFormField.loadItems($availModules, sortableModules, true);

        //sortableModules.sort(compare);
        //const $availModules = $form.find('.available-modules');
        //FwFormField.loadItems($availModules, sortableModules, true);

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
            request.ToolBarJson = JSON.stringify(modules);
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
        $form.find('div.default-home-page').on("change", e => {
            const moduleUrl = jQuery(e.currentTarget).find(':selected').attr('data-nav')
            FwFormField.setValueByDataField($form, 'HomeMenuPath', moduleUrl)
        });

    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        try {
            parameters.afterCloseForm = () => {
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
            FwModule.saveForm(this.Module, $form, parameters);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) {
        const toolBarJson = FwFormField.getValueByDataField($form, 'ToolBarJson')
        sessionStorage.setItem('toolbar', toolBarJson);

        //remove unchecked modules
        $form.find('.selected-modules li[data-selected="F"]').remove();
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const browseRows = sessionStorage.getItem('browsedefaultrows');
        const theme = sessionStorage.getItem('applicationtheme');
        jQuery($form.find('div.fwformfield[data-datafield="BrowseDefaultRows"] select')).val(browseRows);
        jQuery($form.find('div.fwformfield[data-datafield="ApplicationTheme"] select')).val(theme);

        let selectedModules = FwFormField.getValueByDataField($form, 'ToolBarJson');
        if (selectedModules.length > 0) {
            selectedModules = JSON.parse(selectedModules);
            FwFormField.loadItems($form.find('.selected-modules'), selectedModules);

            //remove duplicates(selected modules) from available modules list 
            const $availableModules = $form.find('.available-modules');
            for (let i = 0; i < selectedModules.length; i++) {
                $availableModules.find(`[data-value="${selectedModules[i].value}"]`).remove();
            }
        }
    }
    //----------------------------------------------------------------------------------------------
}
var UserProfileController = new UserProfile();