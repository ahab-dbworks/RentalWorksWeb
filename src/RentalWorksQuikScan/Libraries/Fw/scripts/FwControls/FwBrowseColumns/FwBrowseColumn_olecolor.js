var FwBrowseColumn_olecolorClass = (function () {
    function FwBrowseColumn_olecolorClass() {
    }
    FwBrowseColumn_olecolorClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_olecolorClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_olecolorClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_olecolor.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_olecolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_olecolorClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            var color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
            $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
        }
    };
    FwBrowseColumn_olecolorClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_olecolorClass;
}());
var FwBrowseColumn_olecolor = new FwBrowseColumn_olecolorClass();
//# sourceMappingURL=FwBrowseColumn_olecolor.js.map