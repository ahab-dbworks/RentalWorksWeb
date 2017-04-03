//----------------------------------------------------------------------------------------------
var RwCustomerController = {
    Module: 'Customer'
};
//----------------------------------------------------------------------------------------------
RwCustomerController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwCustomerController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwCustomerController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Customer', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwCustomerController.openBrowse = function() {
    var $browse;

    $browse = jQuery(jQuery('#tmpl-modules-CustomerBrowse').html());
    $browse = FwModule.openBrowse($browse);
    FwBrowse.init($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwCustomerController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-CustomerForm').html());
    $form = FwModule.openForm($form, mode);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwCustomerController.loadForm = function(uniqueids) {
    var $form;

    $form = RwCustomerController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val(uniqueids.clientid);
    FwModule.loadForm(RwCustomerController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwCustomerController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwCustomerController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwCustomerController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
RwCustomerController.renderGrids = function($form) {
    var $customerNoteGrid,     $customerNoteGridControl,
        $customerDocumentGrid, $customerDocumentGridControl,
        $customerContactGrid,  $customerContactGridControl,
        $customerProjectGrid,  $customerProjectGridControl;

    // load Customer Note Grid
    $customerNoteGrid = $form.find('.customerNoteGrid');
    $customerNoteGridControl = jQuery(jQuery('#tmpl-grids-CustomerNoteBrowse').html());
    $customerNoteGrid.empty().append($customerNoteGridControl);
    $customerNoteGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            clientid: $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val()
        };
    });
    FwBrowse.init($customerNoteGridControl);
    FwBrowse.renderRuntimeHtml($customerNoteGridControl);

    // load Customer Document Grid
    $customerDocumentGrid = $form.find('.customerDocumentGrid');
    $customerDocumentGridControl = jQuery(jQuery('#tmpl-grids-CustomerDocumentBrowse').html());
    $customerDocumentGrid.empty().append($customerDocumentGridControl);
    $customerDocumentGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            clientid: $form.find('div.fwformfield[data-datafield="customer.customerid"] input').val()
        };
    });
    FwBrowse.init($customerDocumentGridControl);
    FwBrowse.renderRuntimeHtml($customerDocumentGridControl);

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
RwCustomerController.afterLoad = function($form) {
    var $customerNoteGrid,
        $customerDocumentGrid,
        $customerContactGrid,
        $customerProjectGrid;

    $customerNoteGrid = $form.find('#customerNoteBrowse');
    FwBrowse.search($customerNoteGrid);

    $customerDocumentGrid = $form.find('#customerDocumentBrowse');
    FwBrowse.search($customerDocumentGrid);

    $customerContactGrid = $form.find('#customerContactBrowse');
    FwBrowse.search($customerContactGrid);

    $customerProjectGrid = $form.find('#customerProjectBrowse');
    FwBrowse.search($customerProjectGrid);
};
//----------------------------------------------------------------------------------------------
