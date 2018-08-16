var FwBrowseColumn_rowtextcolorClass = (function () {
    function FwBrowseColumn_rowtextcolorClass() {
    }
    FwBrowseColumn_rowtextcolorClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_rowtextcolorClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_rowtextcolor.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_rowtextcolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalValue = $field.attr('data-originalvalue');
        $tr
            .addClass('rowtextcolor')
            .css({
            'color': originalValue
        });
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_rowtextcolorClass;
}());
var FwBrowseColumn_rowtextcolor = new FwBrowseColumn_rowtextcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowtextcolor.js.map