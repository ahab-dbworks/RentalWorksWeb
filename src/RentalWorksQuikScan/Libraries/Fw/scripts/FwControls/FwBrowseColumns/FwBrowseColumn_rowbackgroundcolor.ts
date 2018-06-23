class FwBrowseColumn_rowbackgroundcolorClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {
        if ((typeof $field.attr('data-browserowbackgroundcolorfield') !== 'undefined') && ($field.attr('data-browserowbackgroundcolorfield').length > 0)) {
            var color, colorColumnIndex;
            if (typeof dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')] !== 'number') {
                throw 'FwBrowse.databindcallback: browserowbackgroundcolorfield: "column ' + $field.attr('data-browserowbackgroundcolorfield') + '" was not returned by the web service.';
            }
            color = dtRow[dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')]];
            $tr.css({ backgroundColor: color });
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {

    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, value: string) {
        throw 'Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $field, $tr, html) {
        $tr
            .addClass('rowbackgroundcolor')
            .css({
                'background-color': $field.attr('data-originalvalue')
            })
            ;
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode = function ($browse, $field, $tr, html) {

    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_rowbackgroundcolor = new FwBrowseColumn_rowbackgroundcolorClass();