//----------------------------------------------------------------------------------------------
var DocumentGridController = {
    Module: 'DocumentGrid'
};
//----------------------------------------------------------------------------------------------
DocumentGridController.init = function($control) {
};
//----------------------------------------------------------------------------------------------
DocumentGridController.addGridSubMenu = function($control, $menu) {
    //Toggle Active / Inactive
    FwApplicationTree.clickEvents['{CFF9AAC6-20B0-4030-AC36-DAE119F0E18F}'] = function(event) {
        var request, $trselected, $form;
        $form       = $control.closest('.fwform');
        $trselected = $control.find('tr.selected');

        if ($trselected.length > 0) {
            if ($trselected.hasClass('editrow')) {
                FwNotification.renderNotification('WARNING', 'You must save the record before performing this function.');
            } else {
                request = {
                    method:         'ToggleAppdocumentInactive',
                    appdocumentid:  $trselected.find('div[data-browsedatafield="AppDocumentId"]').attr('data-originalvalue')
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
    };
};
//----------------------------------------------------------------------------------------------