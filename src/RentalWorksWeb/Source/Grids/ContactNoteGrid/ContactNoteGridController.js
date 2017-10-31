//----------------------------------------------------------------------------------------------
var ContactNoteGridController = {
    Module: 'ContactNoteGrid'
};
//----------------------------------------------------------------------------------------------
ContactNoteGridController.beforeValidateContactCompany = function($browse, $grid, request) {
    var $form;
    $form = $grid.closest('.fwform');
    if (typeof request.miscfields !== 'object') {
        request.miscfields = {};
    }
    request.miscfields['contact.contactid'] = {
        value: $form.find('div.fwformfield[data-datafield="ContactId"] input').val()
    };
};
//----------------------------------------------------------------------------------------------