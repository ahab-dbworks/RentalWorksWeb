//----------------------------------------------------------------------------------------------
var RwQuoteController = {
    Module: 'Quote'
};
//----------------------------------------------------------------------------------------------
RwQuoteController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwQuoteController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwQuoteController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Quote', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwQuoteController.openBrowse = function() {
    var $browse;

    $browse = jQuery(jQuery('#tmpl-modules-QuoteBrowse').html());
    $browse = FwModule.openBrowse($browse);
    FwBrowse.init($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwQuoteController.addBrowseMenuItems = function($menuObject) {
    var $all, $rental, $sales, $signup, viewSubitems, $view;

    $all    = FwMenu.generateDropDownViewBtn('All Quotes', true);
    $rental = FwMenu.generateDropDownViewBtn('Rental Quotes', false);
    $sales  = FwMenu.generateDropDownViewBtn('Sales Quotes', false);
    //ag 02/12/2015 - signup is N/A for TW WEB
    //$signup   = FwMenu.generateDropDownViewBtn('View Sign Up', false);

    $all.on('click', function() {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        TwContactController.ActiveView = 'ALL';
        FwBrowse.databind($browse);
    });
    $rental.on('click', function() {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        TwContactController.ActiveView = 'RENTAL';
        FwBrowse.databind($browse);
    });
    $sales.on('click', function() {
        var $browse;
        $browse = jQuery(this).closest('.fwbrowse');
        TwContactController.ActiveView = 'SALES';
        FwBrowse.databind($browse);
    });
    //$signup.on('click', function() {
    //    var $browse;
    //    $browse = jQuery(this).closest('.fwbrowse');
    //    TwContactController.ActiveView = 'SIGNUP';
    //    FwBrowse.databind($browse);
    //});

    FwMenu.addVerticleSeparator($menuObject);

    viewSubitems = [];
    viewSubitems.push($all);
    viewSubitems.push($rental);
    viewSubitems.push($sales);
    //viewSubitems.push($signup);
    $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

    return $menuObject;
};
//----------------------------------------------------------------------------------------------
RwQuoteController.openForm = function(mode) {
    var $form, $ratetype, $outgoingType, $incomingType;

    $form = jQuery(jQuery('#tmpl-modules-QuoteForm').html());
    $form = FwModule.openForm($form, mode);

    $ratetype = $form.find('.txtRateType');
    FwFormField.loadItems($ratetype, [
        {value:'DAILY',     text:'Daily', selected:true},
        {value:'W',     text:'Weekly'},
        {value:'M',     text:'Monthly'}
    ]);

    $outgoingType = $form.find('.txtOutgoingType');
    FwFormField.loadItems($outgoingType, [
        {value:'D',     text:'Deliver', selected:true},
        {value:'W',     text:'Ship'},
        {value:'M',     text:'Customer Pick Up'}
    ]);
    
    $incomingType = $form.find('.txtIncomingType');
    FwFormField.loadItems($incomingType, [
        {value:'D',     text:'Customer Deliver', selected:true},
        {value:'W',     text:'Customer Ship'},
        {value:'M',     text:'Pick Up'}
    ]);
    return $form;
};
//----------------------------------------------------------------------------------------------
RwQuoteController.loadForm = function(uniqueids) {
    var $form;

    $form = RwQuoteController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="dealorder.orderid"] input').val(uniqueids.orderid);
    FwModule.loadForm(RwQuoteController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwQuoteController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwQuoteController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwQuoteController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="orderview.orderid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
RwQuoteController.renderGrids = function($form) {
    var $masterItemGrid, $masterItemGridControl, $masterItemGridSales, $masterItemGridSalesControl, $masterItemGridFacilities, $masterItemGridFacilitiesControl, 
        $masterItemGridLabor, $masterItemGridLaborControl, $masterItemGridMisc, $masterItemGridMiscControl, $contactGrid, $contactGridControl, $orderNoteGrid, $orderNoteGridControl,
        $orderContractNoteGrid, $orderContractNoteGridControl, $orderActivityDatesGrid, $orderActivityDatesGridControl, 
        $quikEntryItemsGrid, $quikEntryItemsGridControl, $quikEntryDepartmentGrid, $quikEntryDepartmentGridControl, $quikEntryCategoryGrid, $quikEntryCategoryGridControl,
        $quikEntrySubCategoryGrid, $quikEntrySubCategoryGridControl, $quikEntryAccessoriesOptionsGrid, $quikEntryAccessoriesOptionsGridControl, $orderDatesGrid, $orderDatesControl;

    
    // load Rental Item Grid
    $masterItemGrid = $form.find('.masterItemGrid');
    $masterItemGridControl = jQuery(jQuery('#tmpl-grids-MasterItemBrowse').html());
    $masterItemGrid.empty().append($masterItemGridControl);
    $masterItemGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($masterItemGridControl);
    FwBrowse.renderRuntimeHtml($masterItemGridControl);

    // load Sales Item Grid
    $masterItemGridSales = $form.find('.masterItemGridSales');
    $masterItemGridSalesControl = jQuery(jQuery('#tmpl-grids-MasterItemBrowse').html());
    $masterItemGridSales.empty().append($masterItemGridSalesControl);
    $masterItemGridSalesControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($masterItemGridSalesControl);
    FwBrowse.renderRuntimeHtml($masterItemGridSalesControl);

    // load Facilities Item Grid
    $masterItemGridFacilities = $form.find('.masterItemGridFacilities');
    $masterItemGridFacilitiesControl = jQuery(jQuery('#tmpl-grids-MasterItemBrowse').html());
    $masterItemGridFacilities.empty().append($masterItemGridFacilitiesControl);
    $masterItemGridFacilitiesControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($masterItemGridFacilitiesControl);
    FwBrowse.renderRuntimeHtml($masterItemGridFacilitiesControl);

    // load Laobr Item Grid
    $masterItemGridLabor = $form.find('.masterItemGridLabor');
    $masterItemGridLaborControl = jQuery(jQuery('#tmpl-grids-MasterItemBrowse').html());
    $masterItemGridLabor.empty().append($masterItemGridLaborControl);
    $masterItemGridLaborControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($masterItemGridLaborControl);
    FwBrowse.renderRuntimeHtml($masterItemGridLaborControl);

    // load Misc Item Grid
    $masterItemGridMisc = $form.find('.masterItemGridMisc');
    $masterItemGridMiscControl = jQuery(jQuery('#tmpl-grids-MasterItemBrowse').html());
    $masterItemGridMisc.empty().append($masterItemGridMiscControl);
    $masterItemGridMiscControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($masterItemGridMiscControl);
    FwBrowse.renderRuntimeHtml($masterItemGridMiscControl);

    // load Contact Grid
    $contactGrid = $form.find('.contactGrid');
    $contactGridControl = jQuery(jQuery('#tmpl-grids-ContactBrowse').html());
    $contactGrid.empty().append($contactGridControl);
    $contactGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($contactGridControl);
    FwBrowse.renderRuntimeHtml($contactGridControl);

    // load Note Grid
    $orderNoteGrid = $form.find('.orderNoteGrid');
    $orderNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderNoteBrowse').html());
    $orderNoteGrid.empty().append($orderNoteGridControl);
    $orderNoteGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($orderNoteGridControl);
    FwBrowse.renderRuntimeHtml($orderNoteGridControl);

    // load Contract Note Grid
    $orderContractNoteGrid = $form.find('.orderContractNoteGrid');
    $orderContractNoteGridControl = jQuery(jQuery('#tmpl-grids-OrderContractNoteBrowse').html());
    $orderContractNoteGrid.empty().append($orderContractNoteGridControl);
    $orderContractNoteGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($orderContractNoteGridControl);
    FwBrowse.renderRuntimeHtml($orderContractNoteGridControl);

    // load Activity Dates Grid
    $orderActivityDatesGrid = $form.find('.orderActivityDatesGrid');
    $orderActivityDatesGridControl = jQuery(jQuery('#tmpl-grids-OrderActivityDatesBrowse').html());
    $orderActivityDatesGrid.empty().append($orderActivityDatesGridControl);
    $orderActivityDatesGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($orderActivityDatesGridControl);
    FwBrowse.renderRuntimeHtml($orderActivityDatesGridControl);

    // load QuikEntry Items Grid
    $quikEntryItemsGrid = $form.find('.quikEntryItemsGrid');
    $quikEntryItemsGridControl = jQuery(jQuery('#tmpl-grids-QuikEntryItemsBrowse').html());
    $quikEntryItemsGrid.empty().append($quikEntryItemsGridControl);
    $quikEntryItemsGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($quikEntryItemsGridControl);
    FwBrowse.renderRuntimeHtml($quikEntryItemsGridControl);
    FwBrowse.addLegend($quikEntryItemsGridControl, 'Complete', '#ffff00');
    FwBrowse.addLegend($quikEntryItemsGridControl, 'Kit', '#ffff00');
    FwBrowse.addLegend($quikEntryItemsGridControl, 'QC Required', '#ffa500');

    // load QuikEntry Accessories/Items Grid
    $quikEntryAccessoriesOptionsGrid = $form.find('.quikEntryAccessoriesOptionsGrid');
    $quikEntryAccessoriesOptionsGridControl = jQuery(jQuery('#tmpl-grids-QuikEntryAccessoriesOptionsBrowse').html());
    $quikEntryAccessoriesOptionsGrid.empty().append($quikEntryAccessoriesOptionsGridControl);
    $quikEntryAccessoriesOptionsGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($quikEntryAccessoriesOptionsGridControl);
    FwBrowse.renderRuntimeHtml($quikEntryAccessoriesOptionsGridControl);
    FwBrowse.addLegend($quikEntryAccessoriesOptionsGridControl, 'Req. Options', '#ff0000');
    FwBrowse.addLegend($quikEntryAccessoriesOptionsGridControl, 'Options', '#ffff00');
    FwBrowse.addLegend($quikEntryAccessoriesOptionsGridControl, 'Percent', '#ffff00');

    // load QuikEntry Department Grid
    $quikEntryDepartmentGrid = $form.find('.quikEntryDepartmentGrid');
    $quikEntryDepartmentGridControl = jQuery(jQuery('#tmpl-grids-QuikEntryDepartmentBrowse').html());
    $quikEntryDepartmentGrid.empty().append($quikEntryDepartmentGridControl);
    $quikEntryDepartmentGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($quikEntryDepartmentGridControl);
    FwBrowse.renderRuntimeHtml($quikEntryDepartmentGridControl);

    // load QuikEntry Category Grid
    $quikEntryCategoryGrid = $form.find('.quikEntryCategoryGrid');
    $quikEntryCategoryGridControl = jQuery(jQuery('#tmpl-grids-QuikEntryCategoryBrowse').html());
    $quikEntryCategoryGrid.empty().append($quikEntryCategoryGridControl);
    $quikEntryCategoryGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($quikEntryCategoryGridControl);
    FwBrowse.renderRuntimeHtml($quikEntryCategoryGridControl);

    // load QuikEntry Sub Category Grid
    $quikEntrySubCategoryGrid = $form.find('.quikEntrySubCategoryGrid');
    $quikEntrySubCategoryGridControl = jQuery(jQuery('#tmpl-grids-QuikEntrySubCategoryBrowse').html());
    $quikEntrySubCategoryGrid.empty().append($quikEntrySubCategoryGridControl);
    $quikEntrySubCategoryGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($quikEntrySubCategoryGridControl);
    FwBrowse.renderRuntimeHtml($quikEntrySubCategoryGridControl);

    // load Order Dates Grid (Pick, Est. Start, Est. Stop)
    $orderDatesGrid = $form.find('.orderDatesGrid');
    $orderDatesGridControl = jQuery(jQuery('#tmpl-grids-OrderDatesBrowse').html());
    $orderDatesGrid.empty().append($orderDatesGridControl);
    $orderDatesGridControl.data('ondatabind', function(request) {
        request.uniqueids = {
            orderid: $form.find('div.fwformfield[data-datafield=""] input').val()
        };
    });
    FwBrowse.init($orderDatesGridControl);
    FwBrowse.renderRuntimeHtml($orderDatesGridControl);

};
//----------------------------------------------------------------------------------------------
RwQuoteController.afterLoad = function($form) {
    var $masterItemGrid, $masterItemGridSales, $masterItemGridFacilities, $masterItemGridLabor, $masterItemGridMisc, $contactGrid, $orderNoteGrid, 
        $orderContractNoteGrid, $orderActivityDatesGrid, $quikEntryItemsGrid, $quikEntryDepartmentGrid, $quikEntryCategoryGrid, $quikEntrySubCategoryGrid, $quikEntryAccessoriesOptionsGrid, $orderDatesGrid;

    //$masterItemGrid = $form.find('#masterItemBrowse');
    //FwBrowse.search($masterItemGrid);

    //$masterItemGridSales= $form.find('#masterItemBrowse');
    //FwBrowse.search($masterItemGridSales);

    //$masterItemGridFacilities = $form.find('#masterItemBrowse');
    //FwBrowse.search($masterItemGridFacilities);

    //$masterItemGridLabor = $form.find('#masterItemBrowse');
    //FwBrowse.search($masterItemGridLabor);

    //$masterItemGridMisc = $form.find('#masterItemBrowse');
    //FwBrowse.search($masterItemGridMisc);

    //$contactGrid = $form.find('#contactBrowse');
    //FwBrowse.search($contactGrid);

    //$orderNoteGrid = $form.find('#orderNoteBrowse');
    //FwBrowse.search($orderNoteGrid);

    //$orderContractNoteGrid = $form.find('#orderContractNoteBrowse');
    //FwBrowse.search($orderContractNoteGrid);

    //$orderActivityDatesGrid = $form.find('#orderActivityDatesBrowse');
    //FwBrowse.search($orderActivityDatesGrid);

    //$quikEntryItemsGrid = $form.find('#quikEntryItemsBrowse');
    //FwBrowse.search($quikEntryItemsGrid);

    //$quikEntryDepartmentGrid = $form.find('#quikEntryDepartmentBrowse');
    //FwBrowse.search($quikEntryDepartmentGrid);

    //$quikEntryCategoryGrid = $form.find('#quikEntryCategoryBrowse');
    //FwBrowse.search($quikEntryCategoryGrid);

    //$quikEntrySubCategoryGrid = $form.find('#quikEntrySubCategoryBrowse');
    //FwBrowse.search($quikEntrySubCategoryGrid);

    //$quikEntryAccessoriesOptionsGrid = $form.find('#quikEntryAccessoriesOptionsBrowse');
    //FwBrowse.search($quikEntryAccessoriesOptionsGrid);

    //$orderDatesGrid = $form.find('#orderDatesBrowse');
    //FwBrowse.search($orderDatesGrid);
};
//----------------------------------------------------------------------------------------------
