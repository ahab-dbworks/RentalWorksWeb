var FwBrowseColumn_rowbackgroundcolorClass = (function () {
    function FwBrowseColumn_rowbackgroundcolorClass() {
        this.setFieldEditMode = function ($browse, $field, $tr, html) {
        };
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
    FwBrowseColumn_rowbackgroundcolorClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_rowbackgroundcolorClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        $tr
            .addClass('rowbackgroundcolor')
            .css({
            'background-color': $field.attr('data-originalvalue')
        });
    };
    return FwBrowseColumn_rowbackgroundcolorClass;
}());
var FwBrowseColumn_rowbackgroundcolor = new FwBrowseColumn_rowbackgroundcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowbackgroundcolor.js.map