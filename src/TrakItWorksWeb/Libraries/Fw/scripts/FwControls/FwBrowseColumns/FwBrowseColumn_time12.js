var FwBrowseColumn_time12Class = (function () {
    function FwBrowseColumn_time12Class() {
    }
    FwBrowseColumn_time12Class.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_time12Class.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_time12Class.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        throw "FwBrowseColumn_time12.setFieldValue: setFieldValue is not supported on column: " + $field.attr('data-datafield');
    };
    FwBrowseColumn_time12Class.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    };
    FwBrowseColumn_time12Class.prototype.setFieldViewMode = function ($browse, $tr, $field) {
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
    };
    FwBrowseColumn_time12Class.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    return FwBrowseColumn_time12Class;
}());
var FwBrowseColumn_time12 = new FwBrowseColumn_time12Class();
//# sourceMappingURL=FwBrowseColumn_time12.js.map