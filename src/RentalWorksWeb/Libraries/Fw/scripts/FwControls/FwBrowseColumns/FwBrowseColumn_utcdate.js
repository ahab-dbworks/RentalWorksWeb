var FwBrowseColumn_utcdateClass = (function () {
    function FwBrowseColumn_utcdateClass() {
    }
    FwBrowseColumn_utcdateClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleDateString());
    };
    FwBrowseColumn_utcdateClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    };
    FwBrowseColumn_utcdateClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_utcdate.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_utcdateClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_utcdateClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_utcdateClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_utcdateClass;
}());
var FwBrowseColumn_utcdate = new FwBrowseColumn_utcdateClass();
//# sourceMappingURL=FwBrowseColumn_utcdate.js.map