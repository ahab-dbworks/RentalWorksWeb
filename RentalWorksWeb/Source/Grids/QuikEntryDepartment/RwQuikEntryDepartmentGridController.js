//----------------------------------------------------------------------------------------------
var RwQuikEntryDepartmentGridController = {};
//----------------------------------------------------------------------------------------------
RwQuikEntryDepartmentGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwQuikEntryDepartmentGridController.afterDelete = function($control, $tr) {
    var $form, $quikEntryDepartmentBrowse;

    $form              = $control.closest('.fwform');
    $quikEntryDepartmentBrowse = $form.find('div[data-name="QuikEntryDepartment"][data-control="FwBrowse"]');
    FwBrowse.search($quikEntryDepartmentBrowse);
}
//----------------------------------------------------------------------------------------------
RwQuikEntryDepartmentGridController.getActions = function($control) {
    var actions = [
        {text: 'Activate', value: 'ACTIVATE'},
        {text: 'Inactivate', value: 'INACTIVATE'}
    ];
    return actions;
};
//----------------------------------------------------------------------------------------------
RwQuikEntryDepartmentGridController.onAction = function($control, $form, action, uniqueids) {
    var request = {};
    switch(action) {
        case 'ACTIVATE':
            if (confirm('Activate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'T';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderNoteBrowse;
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntryDepartment"][data-control="FwBrowse"]');
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
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntryDepartment"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderNoteBrowse);
                });
            }
            break;
    }
    request.miscfields = FwModule.getFormUniqueIds($form);
};
//----------------------------------------------------------------------------------------------
