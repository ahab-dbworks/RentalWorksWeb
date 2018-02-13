﻿FwBrowseColumn_text = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_text.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_text.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_text.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_text.setFieldEditMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    var formmaxlength = (typeof $field.attr('data-formmaxlength')  === 'string') ? $field.attr('data-formmaxlength') : '';
    html.push('<input class="value" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (formmaxlength != '') {
        html.push(' maxlength="' + formmaxlength + '"');
    }
    html.push(' />');
    html = html.join('');
    $field.html(html);
    $field.find('input.value').val(originalvalue);
};
//---------------------------------------------------------------------------------
