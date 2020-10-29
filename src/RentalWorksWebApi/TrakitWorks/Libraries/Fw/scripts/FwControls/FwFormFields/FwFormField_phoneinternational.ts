// https://github.com/jackocnr/intl-tel-input intlTelInput Documentation

class FwFormField_phoneinternationalClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
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
    renderRuntimeHtml($control: JQuery, html: string[]): void {
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
        html.push('<i class="material-icons btnCall" style="flex: 0 0 auto;padding: 0 .2em;color: #616161;cursor:pointer;">phone</i>');
        html.push('</div>');
        $control.html(html.join(''));

        $control.find('.btnCall').on('click', e => {
            const mail = document.createElement("a");
            mail.href = `tel:${this.getValue2($control)}`;
            mail.click();
        });

        const $input = $control.find('input');
        $input.inputmask('(999) 999-9999');
        $input.attr('maxlength', '14');
        $input.intlTelInput(
            {
                nationalMode: true,
                separateDialCode: true,
                //utilsScript: `${applicationConfig.apiurl}/scripts/jquery/internationalphone/build/js/utils.js"`,
                autoPlaceholder: "off",
                formatOnDisplay: false,
            }
        );

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
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        if (value) {
            $fwformfield.attr('data-originalvalue', value);
            $fwformfield.find('input').intlTelInput('setNumber', value);
        }
        const $form = $fwformfield.closest('.fwform');
        if ($form.attr('data-modified') === 'true') {        // required because if country is not US, when value is set above, it triggers countrychange event. Event registration cannot be moved because loadForm is not called when opening a new record
            const $tab = jQuery(`#${$form.parent().attr('data-tabid')}`);
            $tab.find('.modified').html('');
            $form.attr('data-modified', 'false');
            $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
            $form.find('.btn[data-type="RefreshMenuBarButton"]').removeClass('disabled');
        }
        const $input = $fwformfield.find('input');
        const countryCode = $input.intlTelInput('getSelectedCountryData').dialCode;
        if (countryCode === '1') {
            $input.inputmask('(999) 999-9999');
            $input.attr('maxlength', '14');
        } else {
            $input.inputmask('remove');
            $input.attr('maxlength', '10');
        }
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
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
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        const $input = $fwformfield.find('input');
        if (value) {
            const $inputvalue = $input.intlTelInput('setNumber', value);
            if (firechangeevent) $inputvalue.change();
        }
        const countryCode = $input.intlTelInput('getSelectedCountryData').dialCode;
        if (countryCode === '1') {
            $input.inputmask('(999) 999-9999');
            $input.attr('maxlength', '14');
        } else {
            $input.inputmask('remove');
            $input.attr('maxlength', '10');
        }
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_phoneinternational = new FwFormField_phoneinternationalClass();