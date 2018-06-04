FwBrowseColumn_rowtextcolor = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowtextcolor.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowtextcolor.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowtextcolor.isModified = function ($browse, $tr, $field) {
    var isModified = false;
    return isModified;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowtextcolor.setFieldViewMode = function($browse, $field, $tr, html) {
    $tr
        .addClass('rowtextcolor')
        .css({
            'color': $field.attr('data-originalvalue')
        })
    ;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowtextcolor.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
