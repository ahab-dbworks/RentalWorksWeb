var FwBrowseColumn_timepickerClass = (function () {
    function FwBrowseColumn_timepickerClass() {
    }
    FwBrowseColumn_timepickerClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_timepickerClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
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
    FwBrowseColumn_timepickerClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
    };
    FwBrowseColumn_timepickerClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_timepickerClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_timepickerClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var timepickerTimeFormat, inputmaskTimeFormat;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var html = [];
        html.push('<input id="timepicker" class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="btntime"><i class="material-icons">schedule</i></div>');
        if ($field.attr('data-timeformat') === '24') {
            timepickerTimeFormat = 'H:i:s';
            inputmaskTimeFormat = 'hh:mm';
        }
        else {
            timepickerTimeFormat = 'h:i A';
            inputmaskTimeFormat = 'hh:mm t';
        }
        var htmlString = html.join('');
        $field.html(htmlString);
        if ($field.attr('data-timeformat') === '24') {
            $field.find('#timepicker').clockpicker({ autoclose: true, donetext: 'Done' });
        }
        else {
            $field.find('#timepicker').clockpicker({ autoclose: true, twelvehour: true, donetext: 'Done' });
        }
        $field.find('.btntime').click(function (e) {
            e.stopPropagation();
            $field.find('#timepicker').clockpicker('show').clockpicker('toggleView', 'hours');
        });
        $field.find('input.value').inputmask(inputmaskTimeFormat);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
    };
    return FwBrowseColumn_timepickerClass;
}());
var FwBrowseColumn_timepicker = new FwBrowseColumn_timepickerClass();
//# sourceMappingURL=FwBrowseColumn_timepicker.js.map