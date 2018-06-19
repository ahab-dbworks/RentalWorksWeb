//----------------------------------------------------------------------------------------------
var RwMasterItemGridController = {};
//----------------------------------------------------------------------------------------------
RwMasterItemGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwMasterItemGridController.afterDelete = function($control, $tr) {
    var $form, $masterItemBrowse;

    $form              = $control.closest('.fwform');
    $masterItemBrowse = $form.find('div[data-name="MasterItem"][data-control="FwBrowse"]');
    FwBrowse.search($masterItemBrowse);
}
//----------------------------------------------------------------------------------------------
RwMasterItemGridController.getActions = function($control) {
    var actions = [
        {text: 'Activate', value: 'ACTIVATE'},
        {text: 'Inactivate', value: 'INACTIVATE'}
    ];
    return actions;
};
//----------------------------------------------------------------------------------------------
RwMasterItemGridController.onAction = function($control, $form, action, uniqueids) {
    var request = {};
    switch(action) {
        case 'ACTIVATE':
            if (confirm('Activate record?')) {
                request.orderid = uniqueids.orderid;
                request.active        = 'T';
                FwServices.setorderstatus(request, $control, function(response) {
                    var $masterItemBrowse;
                    $masterItemBrowse = $form.find('div[data-name="MasterItem"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($masterItemBrowse);
                });
            }
            break;
        case 'INACTIVATE':
            if (confirm('Inactivate record?')) {
                request.orderid = uniqueids.ORDERid;
                request.active        = 'F';
                FwServices.setorderstatus(request, $control, function(response) {
                    var $masterItemBrowse;
                    $masterItemBrowse = $form.find('div[data-name="MasterItem"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($masterItemBrowse);
                });
            }
            break;
    }
    request.miscfields = FwModule.getFormUniqueIds($form);
};
//----------------------------------------------------------------------------------------------
RwMasterItemGridController.addGridSubMenu = function($control, $menu) {
    var $submenubtn, $submenucolumn, $submenucolumn2, $optiongroup, $optiongroup2, $toggleinactive;

    $submenubtn    = FwMenu.addSubMenu($menu);
    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn);
    $submenucolumn2 = FwMenu.addSubMenuColumn($submenubtn);

    $optiongroup   = FwMenu.addSubMenuGroup($submenucolumn, '');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'QuikEntry');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Search');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Options');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Summary/Detail');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Template...');
    $optiongroup   = FwMenu.addSubMenuGroup($submenucolumn, '');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Consolidate Item');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Copy Line Item');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Insert Item into Complete/Kit');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Lock Line Item');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Lock All Items');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Unlock All Items');
    $optiongroup2   = FwMenu.addSubMenuGroup($submenucolumn2, '');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'Availability');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Compatible Items...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Image...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Inventory Item...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Quantity Adjustments...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Substitutes...');

    $optiongroup2   = FwMenu.addSubMenuGroup($submenucolumn2, '');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'Create Pick List...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'Sub Rent Items...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'View Item Status...');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'QuikLocate Items...');
    $toggleinactive.on('click', function() {
        var request, $trselected, $form;
        $form       = $control.closest('.fwform');
        $trselected = $control.find('tr.selected');

        if ($trselected.length > 0) {
            if ($trselected.hasClass('editrow')) {
                FwNotification.renderNotification('WARNING', 'You must save the record before performing this function.');
            } else {
                request = {
                    method:         'ToggleAppdocumentInactive',
                    appdocumentid:  $trselected.find('div[data-browsedatafield="appdocumentid"]').attr('data-originalvalue')
                }
                FwBrowse.getGridData($control, request, function(response) {
                    try {
                        FwBrowse.search($control);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } else {
            FwNotification.renderNotification('WARNING', 'You must select a row before performing this function.');
        }
    });
};