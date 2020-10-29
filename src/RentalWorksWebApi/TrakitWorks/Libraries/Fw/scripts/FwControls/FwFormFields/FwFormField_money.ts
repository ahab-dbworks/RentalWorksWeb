class FwFormField_moneyClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        var defaultCurrencySymbol;
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));

        if (sessionStorage.getItem('location') != null) {
            defaultCurrencySymbol = JSON.parse(sessionStorage.getItem('location')).defaultcurrencysymbol;
        } else {
            defaultCurrencySymbol = '$';
        }

        $control.find('.fwformfield-value').inputmask("currency", {
            prefix: defaultCurrencySymbol + ' ',
            placeholder: "0.00",
            min: ((typeof $control.attr('data-minvalue') !== 'undefined') ? $control.attr('data-minvalue') : undefined),
            max: ((typeof $control.attr('data-maxvalue') !== 'undefined') ? $control.attr('data-maxvalue') : undefined),
            digits: ((typeof $control.attr('data-digits') !== 'undefined') ? $control.attr('data-digits') : 2),
            radixPoint: '.',
            groupSeparator: ','
        });
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        var currencySymbol;
        if (typeof $fwformfield.attr('data-currencysymbol') !== 'undefined' && typeof model[$fwformfield.attr('data-currencysymbol')] !== 'undefined') {
            currencySymbol = model[$fwformfield.attr('data-currencysymbol')]
        }

        if (typeof currencySymbol === 'undefined' || currencySymbol === '') {
            currencySymbol = '$';
        }

        $fwformfield.attr('data-currencysymboldisplay', currencySymbol);

        value = ((value === '') ? '0.00' : value);
        $fwformfield
            .attr('data-originalvalue', parseFloat(value).toFixed(2))
            .find('.fwformfield-value')
            .val(value)
            .inputmask("currency", {
                prefix: currencySymbol + ' ',
                placeholder: "0.00",
                min: ((typeof $fwformfield.attr('data-minvalue') !== 'undefined') ? $fwformfield.attr('data-minvalue') : undefined),
                max: ((typeof $fwformfield.attr('data-maxvalue') !== 'undefined') ? $fwformfield.attr('data-maxvalue') : undefined),
                digits: ((typeof $fwformfield.attr('data-digits') !== 'undefined') ? $fwformfield.attr('data-digits') : 2),
                radixPoint: '.',
                groupSeparator: ','
            });
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = valuecontainer !== null ? valuecontainer : ''; //Fix for jquery.inputmask('unmaskedvalue') change to return null on empty value instead of ''

        if ($fwformfield.attr('data-dontsavedecimal') === 'true') { // remove '.00' from value for int type API fields yet retain '$' on FE
            value = value.substring(0, value.length - 3);
        }
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue
            .val(value)
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_money = new FwFormField_moneyClass();