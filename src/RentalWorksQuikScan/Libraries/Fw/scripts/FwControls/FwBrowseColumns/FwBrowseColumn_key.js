var FwBrowseColumn_keyClass = (function () {
    function FwBrowseColumn_keyClass() {
    }
    FwBrowseColumn_keyClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_keyClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.html();
        }
    };
    FwBrowseColumn_keyClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_key.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_keyClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.html();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_keyClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_keyClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_keyClass;
}());
var FwBrowseColumn_key = new FwBrowseColumn_keyClass();
//# sourceMappingURL=FwBrowseColumn_key.js.map