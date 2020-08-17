class FwBrowseColumn_moneyClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        if (typeof dt.ColumnIndex[$field.attr('data-currencysymbol')] === 'number') {
            var currencySymbol = dtRow[dt.ColumnIndex[$field.attr('data-currencysymbol')]];
            if (currencySymbol === '') {
                currencySymbol = '$';
            }
            $field.attr('data-currencysymboldisplay', currencySymbol);
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            var currencySymbol = (typeof $field.attr('data-currencysymboldisplay') === 'string') ? $field.attr('data-currencysymboldisplay') : '$';
            if ($value.length > 0) {
                field.value = $field.find('input.value').inputmask('unmaskedvalue');
                if (field.value === '') {
                    field.value = originalvalue.replace(currencySymbol, '');
                } else if (field.value === '0.00') {
                    field.value = '0';
                }
            } else {
                field.value = originalvalue.replace(currencySymbol, '');
            }
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        if ($field.attr('data-formreadonly') === 'true') {
            if ((data.value.length > 0) && (!isNaN(parseFloat(data.value)))) {
                $field.find('.fieldvalue').text(parseFloat(data.value).toFixed(2));
            }
        } else {
            if ((data.value.length > 0) && (!isNaN(parseFloat(data.value)))) {
                $field.find('input.value').val(parseFloat(data.value).toFixed(2));
            } else {
                $field.find('input.value').val('$0.00');
            }
        }
    }
    //---------------------------------------------------------------------------------
    isModified($browse: JQuery, $tr: JQuery, $field: JQuery): boolean {
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
    setFieldViewMode($browse, $tr, $field): void {
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var currencySymbol = (typeof $field.attr('data-currencysymboldisplay') === 'string') ? $field.attr('data-currencysymboldisplay') : '$';
        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            $field.html(currencySymbol + (<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(2)));
            $field.html(`<div class="fieldvalue">${currencySymbol}${(<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(2))}</div>`);
        } else {
            $field.html(`<div class="fieldvalue">${currencySymbol}0.00</div>`);
        }
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var currencySymbol = (typeof $field.attr('data-currencysymboldisplay') === 'string') ? $field.attr('data-currencysymboldisplay') : '$';
        let html = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        let htmlString = html.join('');
        $field.html(htmlString);
        $field.find('input.value').inputmask("currency", {
            prefix: currencySymbol,
            placeholder: "0.00",
            min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
            max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
            digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
            radixPoint: '.',
            groupSeparator: ',',
            autoGroup: (((typeof $field.attr('data-formatnumeric') !== 'undefined') && ($field.attr('data-formatnumeric') == 'true')) ? true : false)
        });

        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_money = new FwBrowseColumn_moneyClass();