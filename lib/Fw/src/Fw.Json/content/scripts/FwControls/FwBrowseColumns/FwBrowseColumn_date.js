FwBrowseColumn_date = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_date.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_date.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_date.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_date.setFieldEditMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<div class="control">');
        html.push('<input class="value" type="text" />');
        html.push('<div class="add-on"><div class="icon-datepicker"></div></div>');
    html.push('</div>');
    html = html.join('');
    $field.html(html);
    $field.find('input.value').val(originalvalue);
    $field.find('input.value').inputmask('mm/dd/yyyy');
    $field.find('input.value').datepicker({
        autoclose:      true,
        format:         "m/d/yyyy",
        todayHighlight: true
    }).off('focus');
    $field.on('click', '.add-on', function() {
        $field.find('input').datepicker('show');
    });
};
//---------------------------------------------------------------------------------
