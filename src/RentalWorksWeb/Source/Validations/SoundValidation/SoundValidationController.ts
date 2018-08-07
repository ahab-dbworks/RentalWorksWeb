class SoundValidation {
    Module: string = 'SoundValidation';
    apiurl: string = 'api/v1/sound';

    addLegend($control) {
        FwBrowse.addLegend($control, 'User Defined Sound', '#00FF00');
    }
}

var SoundValidationController = new SoundValidation();