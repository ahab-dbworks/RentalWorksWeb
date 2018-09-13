class FwBrowseColumn_moneyClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                field.value = $field.find('input.value').inputmask('unmaskedvalue');
                if (field.value === '') {
                    field.value = originalvalue.replace('$', '');
                }
                else if (field.value === '0.00') {
                    field.value = '0';
                }
            }
            else {
                field.value = originalvalue.replace('$', '');
            }
        }
    }
    setFieldValue($browse, $tr, $field, data) {
        if ((data.value.length > 0) && (!isNaN(parseFloat(data.value)))) {
            $field.find('input.value').val(parseFloat(data.value).toFixed(2));
        }
        else {
            $field.find('input.value').val('$0.00');
        }
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let originalValue = parseFloat($field.attr('data-originalvalue')).toFixed(2);
            var $value = $field.find('input.value');
            let currentValue = $field.find('input.value').inputmask('unmaskedvalue');
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            $field.html('$' + window.numberWithCommas(parseFloat(originalvalue).toFixed(2)));
            $field.html(`<div class="fieldvalue">$${window.numberWithCommas(parseFloat(originalvalue).toFixed(2))}</div>`);
        }
        else {
            $field.html('<div class="fieldvalue">$0.00</div>');
        }
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    setFieldEditMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        let htmlString = html.join('');
        $field.html(htmlString);
        $field.find('input.value').inputmask("currency");
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    }
}
var FwBrowseColumn_money = new FwBrowseColumn_moneyClass();
//# sourceMappingURL=FwBrowseColumn_money.js.map