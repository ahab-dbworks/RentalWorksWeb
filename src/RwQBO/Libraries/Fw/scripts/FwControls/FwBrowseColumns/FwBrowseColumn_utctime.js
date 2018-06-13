FwBrowseColumn_utctime = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_utctime.databindfield = function($browse, $field, dt, dtRow, $tr) {
    $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleTimeString());
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utctime.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utctime.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utctime.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
