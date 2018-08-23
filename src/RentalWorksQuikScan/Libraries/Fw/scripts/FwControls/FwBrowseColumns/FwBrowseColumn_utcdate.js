class FwBrowseColumn_utcdateClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleDateString());
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_utcdate.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
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
var FwBrowseColumn_utcdate = new FwBrowseColumn_utcdateClass();
//# sourceMappingURL=FwBrowseColumn_utcdate.js.map