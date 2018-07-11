var FwBrowseColumn_legendClass = (function () {
    function FwBrowseColumn_legendClass() {
    }
    FwBrowseColumn_legendClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    ;
    FwBrowseColumn_legendClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    ;
    FwBrowseColumn_legendClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_legend.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_legendClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    ;
    FwBrowseColumn_legendClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            var color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
            $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
        }
    };
    ;
    FwBrowseColumn_legendClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    ;
    return FwBrowseColumn_legendClass;
}());
var FwBrowseColumn_legend = new FwBrowseColumn_legendClass();
//# sourceMappingURL=FwBrowseColumn_legend.js.map