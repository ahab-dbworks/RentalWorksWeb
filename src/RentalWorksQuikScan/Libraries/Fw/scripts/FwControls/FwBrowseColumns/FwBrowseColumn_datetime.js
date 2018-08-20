class FwBrowseColumn_datetimeClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')));
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        field.value = $field.html();
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_datetime.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        this.setFieldValue($browse, $tr, $field, originalvalue);
    }
    setFieldEditMode($browse, $tr, $field) {
    }
}
var FwBrowseColumn_datetime = new FwBrowseColumn_datetimeClass();
//# sourceMappingURL=FwBrowseColumn_datetime.js.map