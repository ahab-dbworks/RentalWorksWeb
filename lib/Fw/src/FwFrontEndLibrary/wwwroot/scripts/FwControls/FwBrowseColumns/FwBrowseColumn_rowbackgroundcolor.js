FwBrowseColumn_rowbackgroundcolor = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowbackgroundcolor.databindfield = function($browse, $field, dt, dtRow, $tr) {
    if ((typeof $field.attr('data-browserowbackgroundcolorfield') !== 'undefined') && ($field.attr('data-browserowbackgroundcolorfield').length > 0)) {
        var color, colorColumnIndex;
        if (typeof dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')] !== 'number') {
            throw 'FwBrowse.databindcallback: browserowbackgroundcolorfield: "column ' + $field.attr('data-browserowbackgroundcolorfield') + '" was not returned by the web service.';
        }
        color = dtRow[dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')]];
        $tr.css({backgroundColor: color});
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowbackgroundcolor.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowbackgroundcolor.setFieldViewMode = function($browse, $field, $tr, html) {
    $tr
        .addClass('rowbackgroundcolor')
        .css({
            'background-color': $field.attr('data-originalvalue')
        })
    ;
};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowbackgroundcolor.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
