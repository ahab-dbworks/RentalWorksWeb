FwBrowseColumn_key = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_key.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_key.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.html();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_key.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_key.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
