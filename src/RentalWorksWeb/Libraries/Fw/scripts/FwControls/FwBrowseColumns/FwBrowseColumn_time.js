var FwBrowseColumn_timeClass = (function () {
    function FwBrowseColumn_timeClass() {
    }
    FwBrowseColumn_timeClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_timeClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                field.value = $field.find('input.value').val();
            }
            else {
                field.value = originalvalue;
            }
        }
    };
    FwBrowseColumn_timeClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_timeClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_timeClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_timeClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var timepickerTimeFormat, inputmaskTimeFormat;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        if ($field.attr('data-timeformat') === '24') {
            timepickerTimeFormat = 'H:i:s';
            inputmaskTimeFormat = 'hh:mm';
        }
        else {
            timepickerTimeFormat = 'h:i A';
            inputmaskTimeFormat = 'hh:mm t';
        }
        html = html.join('');
        $field.html(html);
        $field.find('input.value').inputmask(inputmaskTimeFormat);
        $field.find('input.value').val(originalvalue);
    };
    return FwBrowseColumn_timeClass;
}());
var FwBrowseColumn_time = new FwBrowseColumn_timeClass();
//# sourceMappingURL=FwBrowseColumn_time.js.map