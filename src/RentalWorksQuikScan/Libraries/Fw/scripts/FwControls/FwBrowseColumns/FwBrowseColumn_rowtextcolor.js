var FwBrowseColumn_rowtextcolorClass = (function () {
    function FwBrowseColumn_rowtextcolorClass() {
    }
    FwBrowseColumn_rowtextcolorClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_rowtextcolorClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_rowtextcolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        $tr
            .addClass('rowtextcolor')
            .css({
            'color': $field.attr('data-originalvalue')
        });
    };
    FwBrowseColumn_rowtextcolorClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
    };
    return FwBrowseColumn_rowtextcolorClass;
}());
var FwBrowseColumn_rowtextcolor = new FwBrowseColumn_rowtextcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowtextcolor.js.map