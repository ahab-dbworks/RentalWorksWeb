class FwBrowseColumn_time12Class {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_time12.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var time;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        time = originalvalue.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
        if (time.length > 1) {
            time = time.slice(1);
            time[5] = +time[0] < 12 ? ' AM' : ' PM';
            time[0] = +time[0] % 12 || 12;
        }
        time = time.join('');
        $field.html(time);
    }
    setFieldEditMode($browse, $tr, $field) {
    }
}
var FwBrowseColumn_time12 = new FwBrowseColumn_time12Class();
//# sourceMappingURL=FwBrowseColumn_time12.js.map