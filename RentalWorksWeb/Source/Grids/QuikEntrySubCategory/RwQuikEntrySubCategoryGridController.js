//----------------------------------------------------------------------------------------------
var RwQuikEntrySubCategoryGridController = {};
//----------------------------------------------------------------------------------------------
RwQuikEntrySubCategoryGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwQuikEntrySubCategoryGridController.afterDelete = function($control, $tr) {
    var $form, $quikEntrySubCategoryBrowse;

    $form              = $control.closest('.fwform');
    $quikEntrySubCategoryBrowse = $form.find('div[data-name="QuikEntrySubCategory"][data-control="FwBrowse"]');
    FwBrowse.search($quikEntrySubCategoryBrowse);
}
//----------------------------------------------------------------------------------------------
RwQuikEntrySubCategoryGridController.getActions = function($control) {
    var actions = [
        {text: 'Activate', value: 'ACTIVATE'},
        {text: 'Inactivate', value: 'INACTIVATE'}
    ];
    return actions;
};
//----------------------------------------------------------------------------------------------
RwQuikEntrySubCategoryGridController.onAction = function($control, $form, action, uniqueids) {
    var request = {};
    switch(action) {
        case 'ACTIVATE':
            if (confirm('Activate record?')) {
                request.compcontactid = uniqueids.compcontactid;
                request.active        = 'T';
                FwServices.setcompanycontactstatus(request, $control, function(response) {
                    var $orderNoteBrowse;
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntrySubCategory"][data-control="FwBrowse"]');
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
                    $orderNoteBrowse = $form.find('div[data-name="QuikEntrySubCategory"][data-control="FwBrowse"]');
                    FwBrowse.search($control);
                    FwBrowse.search($orderNoteBrowse);
                });
            }
            break;
    }
    request.miscfields = FwModule.getFormUniqueIds($form);
};
//----------------------------------------------------------------------------------------------
