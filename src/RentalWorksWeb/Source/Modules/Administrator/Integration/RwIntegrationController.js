//----------------------------------------------------------------------------------------------
var RwIntegrationController = {
    Module: 'Integration'
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwIntegrationController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwIntegrationController.loadForm();

    screen.load = function () {
        FwModule.openModuleTab($form, 'RentalWorks Integration', false, 'FORM', true);
    };
    screen.unload = function () {
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.openForm = function(mode) {
    var $form, $browsedefaultrows, $applicationtheme;

    $form = FwModule.loadFormFromTemplate(this.Module);
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.loadForm = function() {
    var $form, request = {};
    
    $form = RwIntegrationController.openForm('EDIT');

    return $form;
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.onLoadForm = function($form) {
    var appOptions;

    appOptions = program.getApplicationOptions();

    if ((typeof appOptions['quickbooks'] != 'undefined') && (appOptions['quickbooks'].enabled)) {
        $form.find('.qbo').attr('src', window.location.pathname + 'integration/qbointegration/qbointegration.aspx');
        $form.find('.qbointegration').show();
    }
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.saveForm = function($form, closetab, navigationpath) {
    var request = {}, $fwformfields, fields;
};
//----------------------------------------------------------------------------------------------
RwIntegrationController.verifyAuthToken = function() {
    return sessionStorage.authToken != '';
};
//----------------------------------------------------------------------------------------------