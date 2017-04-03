//----------------------------------------------------------------------------------------------
var FwContactCompanyGridController = {
    Module: 'ContactCompany'
};
//----------------------------------------------------------------------------------------------
FwContactCompanyGridController.init = function ($control) {

};
//----------------------------------------------------------------------------------------------
FwContactCompanyGridController.afterDelete = function($control, $tr) {
    var $form, $contactNoteBrowse;

    $form              = $control.closest('.fwform');
    $contactNoteBrowse = $form.find('div[data-name="ContactNote"][data-control="FwBrowse"]');
    FwBrowse.search($contactNoteBrowse);
}
//----------------------------------------------------------------------------------------------
FwContactCompanyGridController.addGridSubMenu = function($control, $menu) {
    // Activate
    FwApplicationTree.clickEvents['{1E82C664-0306-4C03-88EE-507922667AFD}'] = function(event) {
        var request, $trselected, $form, $confirmation, $yes, $no;
        $form       = $control.closest('.fwform');
        $trselected = $control.find('tr.selected');

        if ($trselected.length > 0) {
            if ($trselected.hasClass('editrow')) {
                FwNotification.renderNotification('WARNING', 'You must save the record before performing this function.');
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Activate Record?');
                $yes          = FwConfirmation.addButton($confirmation, 'Yes', true);
                $no           = FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', function() {
                    request = {
                        method:        'SetCompanyContactStatus',
                        active:        'T',
                        compcontactid: $trselected.find('div[data-browsedatafield="compcontactid"]').attr('data-originalvalue'),
                        contactid:     $trselected.find('div[data-browsedatafield="contactid"]').attr('data-originalvalue')
                    }
                    FwBrowse.getGridData($control, request, function(response) {
                        try {
                            FwBrowse.search($control);
                            FwBrowse.search($form.find('div[data-name="ContactNote"][data-control="FwBrowse"]'));
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
            }
        } else {
            FwNotification.renderNotification('WARNING', 'You must select a row before performing this function.');
        }
    };
    
    // Inactivate
    FwApplicationTree.clickEvents['{36827171-807C-45B5-8CAC-5885AADD56EB}'] = function(event) {
        var request, $trselected, $form, $confirmation, $yes, $no;
        $form       = $control.closest('.fwform');
        $trselected = $control.find('tr.selected');

        if ($trselected.length > 0) {
            if ($trselected.hasClass('editrow')) {
                FwNotification.renderNotification('WARNING', 'You must save the record before performing this function.');
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Inactivate Record?');
                $yes          = FwConfirmation.addButton($confirmation, 'Yes', true);
                $no           = FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', function() {
                    request = {
                        method:        'SetCompanyContactStatus',
                        active:        'F',
                        compcontactid: $trselected.find('div[data-browsedatafield="compcontactid"]').attr('data-originalvalue'),
                        contactid:     $trselected.find('div[data-browsedatafield="contactid"]').attr('data-originalvalue')
                    }
                    FwBrowse.getGridData($control, request, function(response) {
                        try {
                            FwBrowse.search($control);
                            FwBrowse.search($form.find('div[data-name="ContactNote"][data-control="FwBrowse"]'));
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
            }
        } else {
            FwNotification.renderNotification('WARNING', 'You must select a row before performing this function.');
        }
    };
};
//----------------------------------------------------------------------------------------------