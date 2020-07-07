routes.push({ pattern: /^module\/sound$/, action: function (match: RegExpExecArray) { return SoundController.getModuleScreen(); } });

class Sound {
    Module: string = 'Sound';
    apiurl: string = 'api/v1/sound';
    caption: string = Constants.Modules.Settings.children.UserSettings.children.Sound.caption;
    nav: string = Constants.Modules.Settings.children.UserSettings.children.Sound.nav;
    id: string = Constants.Modules.Settings.children.UserSettings.children.Sound.id;
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

        FwBrowse.addLegend($browse, 'User Defined Sound', '#00FF00');
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
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

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
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
        $form.find('#soundInput').on('change', e => {
            const $this = jQuery(e.currentTarget);
            const folder: any = $this[0];
            if (folder.files) {
                // clears out any previous selection in case user abandons previous selection and saves $form
                $form.find('#soundSrc').attr("src", '');
                FwFormField.setValueByDataField($form, 'Base64Sound', '');
                const file: any = folder.files[0];
                if (file) {
                    if (file.type === 'audio/mp3' || file.type === 'audio/wav' || file.type === 'audio/ogg' || file.type === 'audio/mpeg') {
                        if (file.size < 2000000) {
                            const url = URL.createObjectURL(file);
                            $form.find('#soundSrc').attr("src", url);
                            const audioElement: any = document.getElementById('audio');
                            audioElement.load();

                            const reader = new FileReader();
                            reader.readAsDataURL(file);
                            reader.onloadend = () => {
                                const base64data = reader.result;
                                FwFormField.setValueByDataField($form, 'Base64Sound', base64data.toString().replace(/^data:audio\/(wav|mp3|ogg|mpeg);base64,/, ''));
                                $form.find('div[data-datafield="Base64Sound"]').change();
                            }
                        } else {
                            $form.find('#soundInput').val('');
                            FwNotification.renderNotification('WARNING', 'File must be less than 2MB in size.')
                        }
                    } else {
                        $form.find('#soundInput').val('');
                        FwNotification.renderNotification('WARNING', 'Only MP3, WAV or OGG file types supported.');
                    }
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if (FwFormField.getValueByDataField($form, 'SystemSound') === true) {
            FwFormField.disable($form.find('div[data-datafield="Sound"]'));
            $form.find('div.btn-row').hide();
        }
        // load audio element with blob url from base64data
        const base64Sound = FwFormField.getValueByDataField($form, 'Base64Sound');
        const blob = FwFunc.b64SoundtoBlob(base64Sound);
        const blobUrl = URL.createObjectURL(blob);
        $form.find('#soundSrc').attr("src", blobUrl);

        const audioElement: any = document.getElementById('audio');
        audioElement.load();
    }
    //----------------------------------------------------------------------------------------------
    soundsToUrl($form) {
        // This method takes a base64 string stored on a $form, creates a blob url that can be streamed on the $form, and updates the RWW url attribute used to play the sound elsewhere. 
        // It is to be invoked in afterLoad or similar $form lifecycle stage to capture any changes made for the RWW user sounds.

        // Success
        const successBase64Sound = FwFormField.getValueByDataField($form, 'SuccessBase64Sound');
        const successBlob = FwFunc.b64SoundtoBlob(successBase64Sound);
        const successBlobUrl = URL.createObjectURL(successBlob);
        $form.find(`div[data-datafield="SuccessBase64Sound"]`).attr(`data-SuccessSoundUrl`, successBlobUrl);
        jQuery('#application').attr(`data-SuccessSoundUrl`, successBlobUrl);
        // Error
        const errorBase64Sound = FwFormField.getValueByDataField($form, 'ErrorBase64Sound');
        const errorBlob = FwFunc.b64SoundtoBlob(errorBase64Sound);
        const errorBlobUrl = URL.createObjectURL(errorBlob);
        $form.find(`div[data-datafield="ErrorBase64Sound"]`).attr(`data-ErrorSoundUrl`, errorBlobUrl);
        jQuery('#application').attr(`data-ErrorSoundUrl`, errorBlobUrl);
        // Notification
        const notificationBase64Sound = FwFormField.getValueByDataField($form, 'NotificationBase64Sound');
        const notificationBlob = FwFunc.b64SoundtoBlob(notificationBase64Sound);
        const notificationBlobUrl = URL.createObjectURL(notificationBlob);
        $form.find(`div[data-datafield="NotificationBase64Sound"]`).attr(`data-NotificationSoundUrl`, notificationBlobUrl);
        jQuery('#application').attr(`data-NotificationSoundUrl`, notificationBlobUrl);
    }
    //----------------------------------------------------------------------------------------------
}

var SoundController = new Sound();