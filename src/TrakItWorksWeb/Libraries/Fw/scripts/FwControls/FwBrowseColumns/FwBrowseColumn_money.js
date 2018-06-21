var FwBrowseColumn_moneyClass = (function () {
    function FwBrowseColumn_moneyClass() {
    }
    FwBrowseColumn_moneyClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_moneyClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
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
    };
    FwBrowseColumn_moneyClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var originalValue = parseFloat($field.attr('data-originalvalue')).toFixed(2);
            var $value = $field.find('input.value');
            var currentValue = $field.find('input.value').inputmask('unmaskedvalue');
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_moneyClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            $field.html('$' + window.numberWithCommas(parseFloat(originalvalue).toFixed(2)));
            $field.html("<div class=\"fieldvalue\">$" + window.numberWithCommas(parseFloat(originalvalue).toFixed(2)) + "</div>");
        }
        else {
            $field.html('<div class="fieldvalue">$0.00</div>');
        }
    };
    FwBrowseColumn_moneyClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html = html.join('');
        $field.html(html);
        $field.find('input.value').inputmask("currency");
        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            $field.find('input.value').val(parseFloat(originalvalue).toFixed(2));
        }
        else {
            $field.find('input.value').val('$0.00');
        }
    };
    return FwBrowseColumn_moneyClass;
}());
var FwBrowseColumn_money = new FwBrowseColumn_moneyClass();
//# sourceMappingURL=FwBrowseColumn_money.js.map