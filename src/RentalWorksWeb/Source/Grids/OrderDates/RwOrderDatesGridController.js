//----------------------------------------------------------------------------------------------
var RwOrderDatesGridController = {};
//----------------------------------------------------------------------------------------------
RwOrderDatesGridController.init = function ($control) {
    $control
        .on('blur', 'div[data-browsedatafield=""] input', function() {
            
        })
    ;
};
//----------------------------------------------------------------------------------------------
RwOrderDatesGridController.afterDelete = function($control, $tr) {
    var $form, $orderDatesBrowse;

    $form              = $control.closest('.fwform');
    $orderDatesBrowse = $form.find('div[data-name="OrderDates"][data-control="FwBrowse"]');
    FwBrowse.search($orderDatesBrowse);
}
//----------------------------------------------------------------------------------------------
