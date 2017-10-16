FwBrowseColumn_rowbackgroundcolor = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_rowbackgroundcolor.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    if ((typeof field.browserowbackgroundcolorfield !== 'undefined') && (field.browserowbackgroundcolorfield.length > 0)) {
        var color, colorColumnIndex;
        if (typeof dt.ColumnIndex[field.browserowbackgroundcolorfield] !== 'number') {
            throw 'FwBrowse.databindcallback: browserowbackgroundcolorfield: "column ' + field.browserowbackgroundcolorfield + '" was not returned by the web service.';
        }
        color = dtRow[dt.ColumnIndex[field.browserowbackgroundcolorfield]];
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
