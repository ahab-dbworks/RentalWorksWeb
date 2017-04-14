﻿//----------------------------------------------------------------------------------------------
var FwContactNoteGridController = {
    Module: 'ContactNote'
};
//----------------------------------------------------------------------------------------------
FwContactNoteGridController.beforeValidateContactCompany = function($browse, $grid, request) {
    var $form;
    $form = $grid.closest('.fwform');
    if (typeof request.miscfields !== 'object') {
        request.miscfields = {};
    }
    request.miscfields['contact.contactid'] = {
        value: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
    };
};
//----------------------------------------------------------------------------------------------