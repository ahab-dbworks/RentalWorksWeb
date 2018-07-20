var FwBrowseColumn_rowbackgroundcolorClass = (function () {
    function FwBrowseColumn_rowbackgroundcolorClass() {
    }
    FwBrowseColumn_rowbackgroundcolorClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        if ((typeof $field.attr('data-browserowbackgroundcolorfield') !== 'undefined') && ($field.attr('data-browserowbackgroundcolorfield').length > 0)) {
            var color, colorColumnIndex;
            if (typeof dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')] !== 'number') {
                throw 'FwBrowse.databindcallback: browserowbackgroundcolorfield: "column ' + $field.attr('data-browserowbackgroundcolorfield') + '" was not returned by the web service.';
            }
            color = dtRow[dt.ColumnIndex[$field.attr('data-browserowbackgroundcolorfield')]];
            $tr.css({ backgroundColor: color });
        }
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_rowbackgroundcolor.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalValue = $field.attr('data-originalvalue');
        $tr
            .addClass('rowbackgroundcolor')
            .css({
            'background-color': originalValue
        });
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_rowbackgroundcolorClass;
}());
var FwBrowseColumn_rowbackgroundcolor = new FwBrowseColumn_rowbackgroundcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowbackgroundcolor.js.map