FwBrowseColumn_money = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_money.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_money.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').inputmask('unmaskedvalue');
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_money.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
        $field.html('$' + numberWithCommas(parseFloat(originalvalue).toFixed(2)));
    } else {
        $field.html('$0.00');
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_money.setFieldEditMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html = html.join('');
    $field.html(html);
    $field.find('input.value').inputmask("currency");
    if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
        $field.find('input.value').val(parseFloat(originalvalue).toFixed(2));
    } else {
        $field.find('input.value').val('$0.00');
    }
};
//---------------------------------------------------------------------------------
