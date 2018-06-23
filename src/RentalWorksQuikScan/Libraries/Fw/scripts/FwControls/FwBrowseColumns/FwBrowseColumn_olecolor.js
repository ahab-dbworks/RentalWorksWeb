var FwBrowseColumn_olecolorClass = (function () {
    function FwBrowseColumn_olecolorClass() {
        this.setFieldEditMode = function ($browse, $field, $tr, html) {
        };
    }
    FwBrowseColumn_olecolorClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_olecolorClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_olecolorClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_olecolorClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_olecolorClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var color;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
            $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
        }
    };
    return FwBrowseColumn_olecolorClass;
}());
var FwBrowseColumn_olecolor = new FwBrowseColumn_olecolorClass();
//# sourceMappingURL=FwBrowseColumn_olecolor.js.map