class SoundValidation {
    constructor() {
        this.Module = 'SoundValidation';
        this.apiurl = 'api/v1/sound';
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'User Defined Sound', '#00FF00');
    }
}
var SoundValidationController = new SoundValidation();
//# sourceMappingURL=SoundValidationController.js.map