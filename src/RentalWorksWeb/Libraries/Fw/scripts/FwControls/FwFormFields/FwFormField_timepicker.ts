class FwFormField_timepickerClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
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
                $control.find('input').focus();
            }
        }).off(); //Suppresses the time picker from opening on focus.

        $control.find('.fwformfield-value').change(e => {
            const $this = jQuery(e.currentTarget);
            let val = $this.val();
            val = val.toString().toUpperCase().trim();
            let meridiem;
            if (val.endsWith('PM') || val.endsWith('AM')) {
                meridiem = val.substring(val.length - 2);
                val = val.substring(0, val.length - 2); // wghat if more than pm or am, or amm or pmm
            }
            val = val.replace(/\D/g, ''); // lose all extra char : letters, symbols
            if (val.length > 4 || val.length < 3) {
                val = '00:00'
                return $this.val('00:00'); // or now
            }
            let start, end;
            if (val.length === 3) {
                start = val.substring(0, 1);
                end = val.substring(1);
            } else {
                start = val.substring(0, 2);
                end = val.substring(2);
            }

            // check if start or end is meaningful
            val = `${start}:${end}${meridiem ? meridiem : ''}`;
            $this.val(val);
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
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void {
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