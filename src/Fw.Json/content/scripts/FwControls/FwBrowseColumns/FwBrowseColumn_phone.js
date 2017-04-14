FwBrowseColumn_phone = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_phone.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_phone.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_phone.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_phone.setFieldEditMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html = html.join('');
    $field.html(html);
    if ($field.attr('data-formreadonly') === 'false') {
        $field.find('input.value').inputmask('(999) 999-9999');
    }
    $field.find('input.value').val(originalvalue);
};
//---------------------------------------------------------------------------------
