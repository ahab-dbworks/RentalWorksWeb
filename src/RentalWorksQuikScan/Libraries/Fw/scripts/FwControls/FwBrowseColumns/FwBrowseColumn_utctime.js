class FwBrowseColumn_utctimeClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleTimeString());
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_utctime.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    }
    setFieldEditMode($browse, $tr, $field) {
    }
}
var FwBrowseColumn_utctime = new FwBrowseColumn_utctimeClass();
//# sourceMappingURL=FwBrowseColumn_utctime.js.map