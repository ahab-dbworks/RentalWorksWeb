class FwFormField_phoneinternationalClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="content">');
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="tel"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('</div>');
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="tel" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') != '')) {
            html.push(` maxlength="${$control.attr('data-maxlength')}"`);
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        const $input = $control.find('input');

        $input.intlTelInput(
            {
                nationalMode: true,
                separateDialCode: true,
                utilsScript: "https://localhost/rentalworksweb/libraries/fw/scripts/jquery/internationalphone/build/js/utils.js",
                autoPlaceholder: "off",
            }
        );
        // https://github.com/jackocnr/intl-tel-input intlTelInput Documentation
        $input.on("countrychange", e => {
            const $this = jQuery(e.currentTarget);
            const countryCode = $this.intlTelInput('getSelectedCountryData').dialCode;
            if (countryCode === '1') {
                $this.inputmask('(999) 999-9999');
                $input.attr('maxlength', '14');
            } else {
                $this.inputmask('remove');
                $input.attr('maxlength', '10')
            }
            $this.change();
        });
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void {
        const $input = $fwformfield.find('input');
        const countryCode = $input.intlTelInput('getSelectedCountryData').dialCode;
        if (countryCode === '1') {
            $input.inputmask('(999) 999-9999');
            $input.attr('maxlength', '14');
        } else {
            $input.inputmask('remove');
            $input.attr('maxlength', '10');
        }
        if (value) {
            $fwformfield
                .attr('data-originalvalue', value)
                .find('input').intlTelInput('setNumber', value);
        } else {
            console.error(`Value is undefined.`);
        }
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        const $input = $fwformfield.find('input');
        let value;
        const countryCode = $input.intlTelInput('getSelectedCountryData').dialCode;
        if (countryCode === '1') {
            value = $input.val();
        } else {
            value = $input.intlTelInput('getNumber');
        }
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        if (value) {
            const $inputvalue = $fwformfield.find('input').intlTelInput('setNumber', value);
            if (firechangeevent) $inputvalue.change();
        } else {
            console.error(`Value is undefined.`);
        }
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_phoneinternational = new FwFormField_phoneinternationalClass();