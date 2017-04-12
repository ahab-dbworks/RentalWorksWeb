//----------------------------------------------------------------------------------------------
var RwQuikEntryAccessoriesOptionsGridController = {};
//----------------------------------------------------------------------------------------------
RwQuikEntryAccessoriesOptionsGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwQuikEntryAccessoriesOptionsGridController.afterDelete = function($control, $tr) {
    var $form, $quikEntryAccessoriesOptionsBrowse;

    $form              = $control.closest('.fwform');
    $quikEntryAccessoriesOptionsBrowse = $form.find('div[data-name="QuikEntryAccessoriesOptions"][data-control="FwBrowse"]');
    FwBrowse.search($quikEntryAccessoriesOptionsBrowse);
}
//----------------------------------------------------------------------------------------------
RwQuikEntryAccessoriesOptionsGridController.getActions = function($control) {
    var actions = [
        {text: 'Activate', value: 'ACTIVATE'},
        {text: 'Inactivate', value: 'INACTIVATE'}
    ];
    return actions;
};
//----------------------------------------------------------------------------------------------
RwQuikEntryAccessoriesOptionsGridController.onAction = function($control, $form, action, uniqueids) {
    var request = {};
    switch(action) {
        case 'ACTIVATE':
            if (confirm('Activate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'T';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderNoteBrowse;
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntryAccessoriesOptions"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderNoteBrowse);
                });
            }
            break;
        case 'INACTIVATE':
            if (confirm('Inactivate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'F';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderNoteBrowse;
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntryAccessoriesOptions"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderNoteBrowse);
                });
            }
            break;
    }
    request.miscfields = FwModule.getFormUniqueIds($form);
};
//----------------------------------------------------------------------------------------------
RwQuikEntryAccessoriesOptionsGridController.addGridSubMenu = function($control, $menu) {
    var $submenubtn, $submenucolumn, $optiongroup, $toggleinactive;

    $submenubtn    = FwMenu.addSubMenu($menu);
    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn);

    $optiongroup   = FwMenu.addSubMenuGroup($submenucolumn, 'View');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Image');
        $toggleinactive   = FwMenu.addSubMenuBtn($optiongroup, 'Attributes');
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
//----------------------------------------------------------------------------------------------
