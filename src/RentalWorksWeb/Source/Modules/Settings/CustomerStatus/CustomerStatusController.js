//----------------------------------------------------------------------------------------------
var CustomerStatusController = {
    Module: 'CustomerStatus',
    apiurl: 'api/v1/customerstatus'
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('CustomerStatusController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = CustomerStatusController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Customer Status', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.openBrowse = function() {
    var $browse;

    $browse = jQuery(jQuery('#tmpl-modules-CustomerStatusBrowse').html());
    $browse = FwModule.openBrowse($browse);
    FwBrowse.init($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-CustomerStatusForm').html());
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.loadForm = function(uniqueids) {
    var $form;

    $form = CustomerStatusController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="CustomerStatusId"] input').val(uniqueids.CustomerStatusId);
    FwModule.loadForm(CustomerStatusController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(CustomerStatusController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="CustomerStatusId"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
CustomerStatusController.afterLoad = function($form) {
    
};
//----------------------------------------------------------------------------------------------
