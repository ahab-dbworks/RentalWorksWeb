//----------------------------------------------------------------------------------------------
var RwOrderTypeController = {
    Module: 'OrderType'
};
RwOrderTypeController.ModuleOptions = jQuery.extend({}, FwModule.ModuleOptions, RwOrderTypeController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwOrderTypeController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwOrderTypeController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwOrderTypeController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Order Type', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.openBrowse = function() {
    var $browse;

    $browse = jQuery(jQuery('#tmpl-modules-OrderTypeBrowse').html());
    $browse = FwModule.openBrowse($browse, RwOrderTypeController.ModuleOptions.BrowseOptions);
    FwBrowse.init($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-OrderTypeForm').html());
    $form = FwModule.openForm($form, RwOrderTypeController.ModuleOptions.FormOptions, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.loadForm = function(uniqueids) {
    var $form;

    $form = RwOrderTypeController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="ordertype.ordertypeid"] input').val(uniqueids.ordertypeid);
    FwModule.loadForm(RwOrderTypeController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwOrderTypeController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="ordertype.ordertypeid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.renderGrids = function($form) {
    var $customerContactGrid,  $customerContactGridControl,
        $customerProjectGrid,  $customerProjectGridControl;

    // load Customer Contact Grid
    $customerContactGrid = $form.find('.customerContactGrid');
    $customerContactGridControl = jQuery(jQuery('#tmpl-grids-CustomerContactBrowse').html());
    $customerContactGrid.empty().append($customerContactGridControl);
    $customerContactGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            clientid: $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val()
        };
    });
    FwBrowse.init($customerContactGridControl);
    FwBrowse.renderRuntimeHtml($customerContactGridControl);
    FwBrowse.addLegend($customerContactGridControl, 'Primary Contact', '#0072ff');

    // load Customer Project Grid
    $customerProjectGrid = $form.find('.customerProjectGrid');
    $customerProjectGridControl = jQuery(jQuery('#tmpl-grids-CustomerProjectBrowse').html());
    $customerProjectGrid.empty().append($customerProjectGridControl);
    $customerProjectGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            clientid: $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val()
        };
    });
    FwBrowse.init($customerProjectGridControl);
    FwBrowse.renderRuntimeHtml($customerProjectGridControl);
};
//----------------------------------------------------------------------------------------------
RwOrderTypeController.afterLoad = function($form) {
    var $customerContactGrid,
        $customerProjectGrid;

    $customerContactGrid = $form.find('#customerContactBrowse');
    FwBrowse.search($customerContactGrid);

    $customerProjectGrid = $form.find('#customerProjectBrowse');
    FwBrowse.search($customerProjectGrid);
};
//----------------------------------------------------------------------------------------------
