FwBrowseColumn_legend = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_legend.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_legend.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_legend.setFieldViewMode = function($browse, $field, $tr, html) {
    var color;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    if (originalvalue.length > 1) {
        color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
        $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_legend.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
