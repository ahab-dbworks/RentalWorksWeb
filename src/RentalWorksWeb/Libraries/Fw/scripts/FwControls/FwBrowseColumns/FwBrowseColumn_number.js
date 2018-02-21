FwBrowseColumn_number = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_number.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_number.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_number.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(`<div class="fieldvalue">${originalvalue}</div>`);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_number.setFieldEditMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (typeof $browse.attr('data-minvalue') !== 'undefined') {
        html.push(' min="'+ $browse.attr('data-minvalue') + '"');
    }
    if (typeof $browse.attr('data-maxvalue') !== 'undefined') {
        html.push(' max="'+ $browse.attr('data-maxvalue') + '"');
    }
    html.push(' />');
    html = html.join('');
    $field.html(html);
    $field.find('input.value').val(originalvalue);
    $field.find('input.value').inputmask("numeric", {
        //placeholder: '0',
        min:            ((typeof $browse.attr('data-minvalue') !== 'undefined') ? $browse.attr('data-minvalue') : undefined),
        max:            ((typeof $browse.attr('data-maxvalue') !== 'undefined') ? $browse.attr('data-maxvalue') : undefined),
        digits:         ((typeof $browse.attr('data-digits') !== 'undefined') ? $browse.attr('data-digits') : 2),
        radixPoint:     '.',
        groupSeparator: ',',
        autoGroup:      (((typeof $browse.attr('data-formatnumeric') !== 'undefined') && ($browse.attr('data-formatnumeric') == 'true')) ? true : false)
    });
};
//---------------------------------------------------------------------------------
