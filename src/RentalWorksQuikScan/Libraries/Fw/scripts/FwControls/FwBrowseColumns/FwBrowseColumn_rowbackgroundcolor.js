class FwBrowseColumn_rowbackgroundcolorClass {
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
    getFieldValue($browse, $tr, $field, field, originalvalue) {
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_rowbackgroundcolor.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        let originalValue = $field.attr('data-originalvalue');
        $tr
            .addClass('rowbackgroundcolor')
            .css({
            'background-color': originalValue
        });
    }
    setFieldEditMode($browse, $tr, $field) {
    }
}
var FwBrowseColumn_rowbackgroundcolor = new FwBrowseColumn_rowbackgroundcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowbackgroundcolor.js.map