﻿class FwBrowseColumn_moneyClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        //if (typeof dt.ColumnIndex[$field.attr('data-currencysymbol')] === 'number') {
        //    var currencySymbol = dtRow[dt.ColumnIndex[$field.attr('data-currencysymbol')]];
        //    if (currencySymbol === '') {
        //        currencySymbol = '$';
        //    }
        //    $field.attr('data-currencysymboldisplay', currencySymbol);
        //}

        let currencySymbol: string = '';
        if (typeof dt.ColumnIndex[$field.attr('data-currencysymbol')] === 'number') {    // developer has explicitly assigned the data-currencysymbol attribute, defining the name of the field that has the correct currency symbol to render
            currencySymbol = dtRow[dt.ColumnIndex[$field.attr('data-currencysymbol')]];
        }
        else if (typeof dt.ColumnIndex['CurrencySymbol'] === 'number') {                 // developer has not explicitly assigned the data-currencysymbol attribute, but a field called CurrencySymbol exists
            currencySymbol = dtRow[dt.ColumnIndex['CurrencySymbol']];
        }
        if (currencySymbol === '') {
            if (sessionStorage.getItem('location') != null) {
                currencySymbol = JSON.parse(sessionStorage.getItem('location')).defaultcurrencysymbol;
            } else {
                currencySymbol = '$';
            }
        }
        $field.attr('data-currencysymboldisplay', currencySymbol);
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
                    field.value = originalvalue;
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
                $field.find('input.value').val(parseFloat(data.value));
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
        let currencySymbol = '$';
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if ((typeof $browse.attr('data-currencysymboldisplay') === 'string') && $browse.attr('data-currencysymboldisplay') !== '') {
            currencySymbol = $browse.attr('data-currencysymboldisplay');
        }

        if (typeof $field.attr('data-currencysymboldisplay') === 'string' && $field.attr('data-currencysymboldisplay') !== '') {
            currencySymbol = $field.attr('data-currencysymboldisplay');
        }

        if ((originalvalue.length > 0) && (!isNaN(parseFloat(originalvalue)))) {
            let digits = 2;
            if (typeof $field.attr('data-digits') !== 'undefined') {
                digits = $field.attr('data-digits');
            }
            //$field.html(currencySymbol +(<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(2)));
            $field.html(`<div class="fieldvalue">${currencySymbol} ${(<any>window).numberWithCommas(parseFloat(originalvalue).toFixed(digits))}</div>`);
        } else {
            $field.html(`<div class="fieldvalue">${currencySymbol} 0.00</div>`);
        }
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        let currencySymbol = '$';
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if ((typeof $browse.attr('data-currencysymboldisplay') === 'string') && $browse.attr('data-currencysymboldisplay') !== '') {
            currencySymbol = $browse.attr('data-currencysymboldisplay');
        }

        if (typeof $field.attr('data-currencysymboldisplay') === 'string' && $field.attr('data-currencysymboldisplay') !== '') {
            currencySymbol = $field.attr('data-currencysymboldisplay');
        }

        let html = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        let htmlString = html.join('');
        $field.html(htmlString);
        $field.find('input.value').inputmask("currency", {
            prefix: currencySymbol + ' ',
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