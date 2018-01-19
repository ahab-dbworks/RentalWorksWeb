FwBrowseColumn_utcdate = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdate.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')));
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdate.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdate.setFieldViewMode = function($browse, $field, $tr, html) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_utcdate.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
