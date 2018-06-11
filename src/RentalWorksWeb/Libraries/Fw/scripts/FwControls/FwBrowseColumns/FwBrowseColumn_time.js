FwBrowseColumn_time = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.databindfield = function ($browse, $field, dt, dtRow, $tr) {

};
//---------------------------------------------------------------------------------
FwBrowseColumn_time.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {

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
    html.push('<div class="fwformfield-control">');
    html.push('<input class="value" type="text" autocapitalize="none"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html.push('</div>');
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
