//----------------------------------------------------------------------------------------------
var RwCustomerStatusController = {
    Module: 'CustomerStatus'
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwCustomerStatusController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwCustomerStatusController.openBrowse();

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
RwCustomerStatusController.openBrowse = function() {
    var $browse;

    $browse = jQuery(jQuery('#tmpl-modules-CustomerStatusBrowse').html());
    $browse = FwModule.openBrowse($browse);
    FwBrowse.init($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-CustomerStatusForm').html());
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.loadForm = function(uniqueids) {
    var $form;

    $form = RwCustomerStatusController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val(uniqueids.clientid);
    FwModule.loadForm(RwCustomerStatusController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwCustomerStatusController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
RwCustomerStatusController.afterLoad = function($form) {
    
};
//----------------------------------------------------------------------------------------------
