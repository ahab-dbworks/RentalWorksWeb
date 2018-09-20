class FwBrowseColumn_timeClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                field.value = $field.find('input.value').val();
            }
            else {
                field.value = originalvalue;
            }
        }
    }
    setFieldValue($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.data('autoselect', false);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    setFieldEditMode($browse, $tr, $field) {
        var timepickerTimeFormat, inputmaskTimeFormat;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
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
        let htmlString = html.join('');
        $field.html(htmlString);
        $field.find('input.value').inputmask(inputmaskTimeFormat);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    }
}
var FwBrowseColumn_time = new FwBrowseColumn_timeClass();
//# sourceMappingURL=FwBrowseColumn_time.js.map