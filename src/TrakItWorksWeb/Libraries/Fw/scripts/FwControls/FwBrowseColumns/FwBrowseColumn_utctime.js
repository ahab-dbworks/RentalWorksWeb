var FwBrowseColumn_utctimeClass = (function () {
    function FwBrowseColumn_utctimeClass() {
    }
    FwBrowseColumn_utctimeClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleTimeString());
    };
    FwBrowseColumn_utctimeClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_utctimeClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_utctimeClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_utctimeClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_utctimeClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
    };
    return FwBrowseColumn_utctimeClass;
}());
var FwBrowseColumn_utctime = new FwBrowseColumn_utctimeClass();
//# sourceMappingURL=FwBrowseColumn_utctime.js.map