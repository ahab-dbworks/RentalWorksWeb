//----------------------------------------------------------------------------------------------
var RwOrderContractNoteGridController = {};
//----------------------------------------------------------------------------------------------
RwOrderContractNoteGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwOrderContractNoteGridController.afterDelete = function($control, $tr) {
    var $form, $orderContractNoteBrowse;

    $form              = $control.closest('.fwform');
    $orderContractNoteBrowse = $form.find('div[data-name="OrderContractNote"][data-control="FwBrowse"]');
    FwBrowse.search($orderContractNoteBrowse);
}
//----------------------------------------------------------------------------------------------
RwOrderContractNoteGridController.getActions = function($control) {
    var actions = [
        {text: 'Activate', value: 'ACTIVATE'},
        {text: 'Inactivate', value: 'INACTIVATE'}
    ];
    return actions;
};
//----------------------------------------------------------------------------------------------
RwOrderContractNoteGridController.onAction = function($control, $form, action, uniqueids) {
    var request = {};
    switch(action) {
        case 'ACTIVATE':
            if (confirm('Activate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'T';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderContractNoteBrowse;
                    $orderContractNoteBrowse = $form.find('div[data-name="OrderContractNote"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderContractNoteBrowse);
                });
            }
            break;
        case 'INACTIVATE':
            if (confirm('Inactivate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'F';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderContractNoteBrowse;
                    $orderContractNoteBrowse = $form.find('div[data-name="OrderContractNote"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderContractNoteBrowse);
                });
            }
            break;
    }
    request.miscfields = FwModule.getFormUniqueIds($form);
};
//----------------------------------------------------------------------------------------------
RwOrderContractNoteGridController.addGridSubMenu = function($control, $menu) {
    var $submenubtn, $submenucolumn, $submenucolumn2, $optiongroup, $optiongroup2, $toggleinactive;

    $submenubtn    = FwMenu.addSubMenu($menu);
    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn);
    $submenucolumn2 = FwMenu.addSubMenuColumn($submenubtn);

    $optiongroup   = FwMenu.addSubMenuGroup($submenucolumn, 'Note');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Set as Printable');
    $optiongroup   = FwMenu.addSubMenuGroup($submenucolumn, '');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Save');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Delete');
    $optiongroup2   = FwMenu.addSubMenuGroup($submenucolumn2, 'Export');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup2, 'Download Excel Workbook (*.xlsx)');

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