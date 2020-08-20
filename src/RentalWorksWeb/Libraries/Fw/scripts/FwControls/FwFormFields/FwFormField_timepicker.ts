class FwFormField_timepickerClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
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
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons btntime">schedule</i>');
        html.push('</div>');
        $control.html(html.join(''));

        $control.find('.fwformfield-value').clockpicker({
            autoclose: true,
            twelvehour: ($control.attr('data-timeformat') !== '24') ? true : false,
            donetext: 'Done',
            afterDone: function () {
                $control.find('input').change().focus();
            }
        }).off(); //Suppresses the time picker from opening on focus.

        // time formatting for 24HR only
        $control.find('.fwformfield-value').change(e => {
            const $this = jQuery(e.currentTarget);
            const field = $this.closest('div[data-type="timepicker"]');
            field.removeClass('error');
            const val = $this.val().toString().replace(/\D/g, ''); // lose all extra char : letters, symbols, spaces
            if (val != '') {
                if (val.length !== 4) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }

                const start = val.substring(0, 2);
                const startDecimal = new Decimal(start);
                if (startDecimal.greaterThan(23)) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }
                const end = val.substring(2);
                const endDecimal = new Decimal(end);
                if (endDecimal.greaterThan(59)) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }

                $this.val(`${start}:${end}`);
            }
        })

        $control.on('click', '.btntime', function (e) {
            if ($control.attr('data-enabled') === 'true') {
                e.stopPropagation();
                $control.find('.fwformfield-value').clockpicker('show').clockpicker('toggleView', 'hours');
            }
        });
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        const value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_timepicker = new FwFormField_timepickerClass();