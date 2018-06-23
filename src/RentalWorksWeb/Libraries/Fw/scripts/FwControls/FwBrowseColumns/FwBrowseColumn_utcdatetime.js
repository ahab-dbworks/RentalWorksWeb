var FwBrowseColumn_utcdatetimeClass = (function () {
    function FwBrowseColumn_utcdatetimeClass() {
    }
    FwBrowseColumn_utcdatetimeClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleString());
    };
    FwBrowseColumn_utcdatetimeClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_utcdatetimeClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_utcdatetimeClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_utcdatetimeClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_utcdatetimeClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
    };
    return FwBrowseColumn_utcdatetimeClass;
}());
var FwBrowseColumn_utcdatetime = new FwBrowseColumn_utcdatetimeClass();
//# sourceMappingURL=FwBrowseColumn_utcdatetime.js.map