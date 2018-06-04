FwBrowseColumn_olecolor = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_olecolor.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_olecolor.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_olecolor.isModified = function ($browse, $tr, $field) {
    var isModified = false;
    return isModified;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_olecolor.setFieldViewMode = function($browse, $field, $tr, html) {
    var color;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    if (originalvalue.length > 1) {
        color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
        $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_olecolor.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
