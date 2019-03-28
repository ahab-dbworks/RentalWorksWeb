class IntegrationControllerClass {
    Module: 'Integration';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $form;

        screen            = {};
        screen.$view      = FwModule.getModuleControl(`${this.Module}Controller`);

        $form = this.loadForm();

        screen.load = function () {
            FwModule.openModuleTab($form, 'RentalWorks Integration', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode) {
        var $form, $browsedefaultrows, $applicationtheme;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm() {
        var $form, request = {};

        $form = this.openForm('EDIT');

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        var appOptions;

        appOptions = program.getApplicationOptions();

        if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
            $form.find('.qbo').attr('src', window.location.pathname + 'integration/qbointegration/qbointegration.aspx');
            $form.find('.qbointegration').show();
        }
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form, closetab, navigationpath) {
        var request = {}, $fwformfields, fields;
    };
    //----------------------------------------------------------------------------------------------
    verifyAuthToken() {
        return sessionStorage.getItem('authToken') !== '';
    };
}
//----------------------------------------------------------------------------------------------
var IntegrationController = new IntegrationControllerClass();
