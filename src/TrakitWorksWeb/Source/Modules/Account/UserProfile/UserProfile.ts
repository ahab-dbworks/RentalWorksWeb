routes.push({ pattern: /^module\/userprofile$/, action: function (match: RegExpExecArray) { return UserProfileController.getModuleScreen(); } });

class UserProfile {
    Module: string = 'UserProfile';
    apiurl: string = 'api/v1/userprofile';
    id: string = 'DrTcbvvUw92V';
    caption: string = 'User Profile';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm('NEW');

        screen.load = function () {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form, $browsedefaultrows, $applicationtheme, $moduleSelect
            , node
            , mainModules
            , settingsModules
            , modules
            , allModules;

        const userId = JSON.parse(sessionStorage.getItem('userid'));

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $browsedefaultrows = $form.find('.browsedefaultrows');
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

        $applicationtheme = $form.find('.applicationtheme');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-material', text: 'Material' }
        ], true);

        // Load Default Home Page
        const defaultHomePages = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('nav')) {
                modules.push({ value: moduleController.id, text: moduleCaption, nav: moduleController.nav });
            }
        });
        FwApplicationTree.sortModules(defaultHomePages);
        const $defaultHomePage = $form.find('.default-home-page');
        FwFormField.loadItems($defaultHomePage, defaultHomePages, true);

        // Load Available Modules
        //const toolbarModules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
        //    if (moduleController.hasOwnProperty('nav')) {
        //        modules.push({ value: moduleController.nav, text: moduleCaption, selected: 'T'});
        //    }
        //});
        //FwApplicationTree.sortModules(toolbarModules);
        //const $availModules = $form.find('.available-modules');
        //FwFormField.loadItems($availModules, toolbarModules, true);

        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        let successSound, successSoundFileName, errorSound, errorSoundFileName, notificationSound, notificationSoundFileName;

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
            successSoundFileName = FwFormField.getValueByDataField($form, 'SuccessSoundFileName');
            successSound = new Audio(successSoundFileName);
            successSound.play();
        });
        $form.find('.error-play-button').on('click', e => {
            errorSoundFileName = FwFormField.getValueByDataField($form, 'ErrorSoundFileName');
            errorSound = new Audio(errorSoundFileName);
            errorSound.play();
        });
        $form.find('.notification-play-button').on('click', e => {
            notificationSoundFileName = FwFormField.getValueByDataField($form, 'NotificationSoundFileName');
            notificationSound = new Audio(notificationSoundFileName);
            notificationSound.play();
        });
        $form.find('div.default-home-page').on("change", function () {
            let moduleUrl = jQuery(this).find(':selected').attr('data-apiurl')
            FwFormField.setValueByDataField($form, 'HomeMenuPath', moduleUrl)
        });
     
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        let sounds: any = {}, browseDefaultRows, applicationTheme, successSoundFileName, errorSoundFileName, notificationSoundFileName, homePage:any = {};
        FwModule.saveForm(this.Module, $form, parameters);

        browseDefaultRows = jQuery($form.find('[data-datafield="BrowseDefaultRows"] select')).val().toString();
        applicationTheme = jQuery($form.find('[data-datafield="ApplicationTheme"] select')).val().toString();
        successSoundFileName = FwFormField.getValueByDataField($form, 'SuccessSoundFileName').toString();
        errorSoundFileName = FwFormField.getValueByDataField($form, 'ErrorSoundFileName').toString();
        notificationSoundFileName = FwFormField.getValueByDataField($form, 'NotificationSoundFileName').toString();
        homePage.guid = FwFormField.getValueByDataField($form, 'HomeMenuGuid');
        homePage.path = FwFormField.getValueByDataField($form, 'HomeMenuPath');
        sounds.successSoundFileName = successSoundFileName;
        sounds.errorSoundFileName = errorSoundFileName;
        sounds.notificationSoundFileName = notificationSoundFileName;

        sessionStorage.setItem('browsedefaultrows', browseDefaultRows);
        sessionStorage.setItem('applicationtheme', applicationTheme);
        sessionStorage.setItem('sounds', JSON.stringify(sounds));
        sessionStorage.setItem('homePage', JSON.stringify(homePage));
        setTimeout(function () {
            location.reload();
        }, 1000);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const browserows = sessionStorage.getItem('browsedefaultrows');
        const theme = sessionStorage.getItem('applicationtheme');
        jQuery($form.find('div.fwformfield[data-datafield="BrowseDefaultRows"] select')).val(browserows);
        jQuery($form.find('div.fwformfield[data-datafield="ApplicationTheme"] select')).val(theme);
    }
    //----------------------------------------------------------------------------------------------
}
var UserProfileController = new UserProfile();