var FwBrowseColumn_datetimeClass = (function () {
    function FwBrowseColumn_datetimeClass() {
    }
    FwBrowseColumn_datetimeClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')));
    };
    FwBrowseColumn_datetimeClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_datetimeClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_datetimeClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_datetimeClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_datetimeClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
    };
    return FwBrowseColumn_datetimeClass;
}());
var FwBrowseColumn_datetime = new FwBrowseColumn_datetimeClass();
//# sourceMappingURL=FwBrowseColumn_datetime.js.map