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
        // Sound Preview
        //$form.find('.sound-play-button').on('click', e => {
        //    const soundFileName = FwFormField.getValueByDataField($form, 'FileName');
        //    const sound = new Audio(soundFileName);
        //    sound.play();
        //});

        $form.find('#soundInput').on('change', e => {
            // if NEW vs EDIT
            const $this = jQuery(e.currentTarget);
            const folder: any = $this[0];
            if (folder.files) {
                $form.find('#soundSrc').attr("src", '');
                const file: any = folder.files[0];
                if (file) { //possible must handle clear out file and leave blank?
                    if (file.type === 'audio/mp3' || file.type === 'audio/wav' || file.type === 'audio/ogg') {
                        const url = URL.createObjectURL(file);
                        $form.find('#soundSrc').attr("src", url);
                        const audioElement: any = document.getElementById('audio');
                        audioElement.load();
                        FwFormField.setValueByDataField($form, 'FileName', url);

                        const reader = new FileReader();
                        reader.readAsDataURL(file);
                        reader.onloadend = () => {
                            const base64data = reader.result;
                            FwFormField.setValueByDataField($form, 'Base64Sound', base64data.toString()); //possible need of replacing audio tags in the str like done in b64toBlob
                            $form.find('div[data-datafield="Base64Sound"]').change();

                            // next steps:

                            // figure out how to stream files when played elsewhere i.e fwfunc.playerrorSound() etc this method currently requires a filename
                            // add issystemsound flag to responseGetUserSettings to be used in base.ts
                            // in base.ts - if not sys sound, load base64 and url into #application, else load filename
                            // on change evt in user and user profile, reload #application attr
                            // add check in fwfunc.play... to see if attr is empty (browser refresh)
                            // limit size and length of new sounds
                        }
                    } else {
                        $form.find('#soundInput').val('');
                        FwNotification.renderNotification('WARNING', 'Only MP3, WAV or OGG file types supported.')
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
            // load audio element with file url from local
            const fileName = FwFormField.getValueByDataField($form, 'FileName');
            $form.find('#soundSrc').attr("src", fileName);
            const audioElement: any = document.getElementById('audio');
            audioElement.load();
        } else {
            // getting base64data from page load and loading a blob on the page
            const base64Sound = FwFormField.getValueByDataField($form, 'Base64Sound');
            const blob = this.b64toBlob(base64Sound);
            const blobUrl = URL.createObjectURL(blob);
            $form.find('#soundSrc').attr("src", blobUrl);
            jQuery('#application').attr('data-errsoundurl', blobUrl);

            const audioElement: any = document.getElementById('audio');
            audioElement.load();
        }
    }
    //----------------------------------------------------------------------------------------------
    b64toBlob(b64Data, contentType = '', sliceSize = 512) {
        const byteCharacters = atob(b64Data.replace(/^data:audio\/(wav|mp3|ogg);base64,/, ''));
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            const slice = byteCharacters.slice(offset, offset + sliceSize);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }
}

var SoundController = new Sound();