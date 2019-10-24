class DocumentBarCodeSettings {
    Module: string = 'DocumentBarCodeSettings';
    apiurl: string = 'api/v1/documentbarcodesettings';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Document Bar Code Settings', false, 'BROWSE', true);
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

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DocumentBarCodeSettingsId"] input').val(uniqueids.DocumentBarCodeSettingsId);
        FwModule.loadForm(this.Module, $form);

        $form.find('[data-datafield="DocumentBarCodeStyle"] .fwformfield-control').css({'display': 'flex', 'height' : '7em'})
        $form.find('[data-datafield="DocumentBarCodeStyle"] div[data-value="1D"]')
            .css({ 'margin': '25px', 'max-width': '100px' })
            .append(`<div style="margin-top:10px;"><span style="font-family:'Libre Barcode 39 Text';font-size:50px;">BARCODE</span></div>`);
       
        $form.find('[data-datafield="DocumentBarCodeStyle"] div[data-value="2D"]')
            .css({ 'margin': '36px 25px 25px', 'max-width':'65px' })
            .append(`<img src="./theme/images/qrcodesample.jpg" alt="Image" class="image" style="width:60px; margin-top:10px;">`);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {}
}
//----------------------------------------------------------------------------------------------
var DocumentBarCodeSettingsController = new DocumentBarCodeSettings();