﻿FwBrowseColumn_hidden = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_hidden.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_hidden.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_hidden.isModified = function ($browse, $tr, $field) {
    var isModified = false;
    let originalValue = $field.attr('data-originalvalue');
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        let currentValue = $field.find('input.value').val();
        isModified = currentValue !== originalValue;
    }
    return isModified;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_hidden.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_hidden.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
