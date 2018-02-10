FwBrowseColumn_utcdatetime = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdatetime.databindfield = function($browse, $field, dt, dtRow, $tr) {
    $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleString());
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdatetime.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdatetime.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdatetime.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
