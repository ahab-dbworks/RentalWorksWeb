class FwBrowseColumn_moneyClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {
    
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                field.value = $field.find('input.value').inputmask('unmaskedvalue');
                if (field.value === '') {
                    field.value = originalvalue.replace('$', '');
                } else if (field.value === '0.00') {
                    field.value = '0';
                }
            } else {
                field.value = originalvalue.replace('$', '');
            }
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, value: string) {
        throw 'Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse: JQuery, $tr: JQuery, $field: JQuery) {
        var isModified = false;
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let originalValue = parseFloat($field.attr('data-originalvalue')).toFixed(2);
            var $value = $field.find('input.value');
            let currentValue = (<any>$field.find('input.value')).inputmask('unmaskedvalue');
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            $field.html('$' + (<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(2)));
            $field.html(`<div class="fieldvalue">$${(<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(2))}</div>`);
        } else {
            $field.html('<div class="fieldvalue">$0.00</div>');
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $field, $tr, html) {
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
        } else {
            $field.find('input.value').val('$0.00');
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_money = new FwBrowseColumn_moneyClass();