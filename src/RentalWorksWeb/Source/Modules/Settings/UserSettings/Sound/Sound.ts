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
        $form.find('.sound-play-button').on('click', e => {
            const soundFileName = FwFormField.getValueByDataField($form, 'FileName');
            const sound = new Audio(soundFileName);
            sound.play();
        });
        function dataURItoBlob(dataURI) {
            var byteString = atob(dataURI.split(',')[1]);

            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0]

            var ab = new ArrayBuffer(byteString.length);
            var ia = new Uint8Array(ab);
            for (var i = 0; i < byteString.length; i++) {
                ia[i] = byteString.charCodeAt(i);
            }

            var bb = new Blob([ab], { "type": mimeString });
            return bb;
        }
        //$form.find('#rll').on('change', e => {
        //    const $this = jQuery(e.currentTarget);
        //    const folder: any = $this[0];
        //    if (folder.files) {
        //        const file = folder.files;
        //        const url = URL.createObjectURL(file[0]);
        //       $form.find('#rlly').attr("src", URL.createObjectURL(file[0]));
        //        $form.find('#rlly').load(url);
        //        $form.find('#rllly').load(url);
        //        let audio: any = document.getElementById('rllly');
        //        audio.load();
            
        //        let here;

        //    }
        //    //var files = e.target.files;
        //    //$("#rlly").attr("src", URL.createObjectURL(files[0]));
        //    //document.getElementById("rllly").load();

        //});
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