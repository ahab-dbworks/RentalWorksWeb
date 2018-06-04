FwBrowseColumn_time = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.databindfield = function ($browse, $field, dt, dtRow, $tr) {

};
//---------------------------------------------------------------------------------
<<<<<<< develop
FwBrowseColumn_time.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
=======
FwBrowseColumn_time.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        var $value = $field.find('input.value');
        if ($value.length > 0) {
            field.value = $field.find('input.value').val();
        } else {
            field.value = originalvalue;
        }
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.isModified = function ($browse, $tr, $field) {
    var isModified = false;
    let originalValue = $field.attr('data-originalvalue');
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        let currentValue = $field.find('input.value').val();
        isModified = currentValue !== originalValue;
    }
    return isModified;
>>>>>>> Updates Fw
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.setFieldViewMode = function ($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.setFieldEditMode = function ($browse, $field, $tr, html) {
    var timepickerTimeFormat, inputmaskTimeFormat;
    var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    if ($field.attr('data-timeformat') === '24') {
        timepickerTimeFormat = 'H:i:s';
        inputmaskTimeFormat = 'hh:mm';
    } else {
        timepickerTimeFormat = 'h:i A';
        inputmaskTimeFormat = 'hh:mm t';
    }
    html = html.join('');
    $field.html(html);
    $field.find('input.value').inputmask(inputmaskTimeFormat);
    $field.find('input.value').val(originalvalue);

};
//---------------------------------------------------------------------------------
