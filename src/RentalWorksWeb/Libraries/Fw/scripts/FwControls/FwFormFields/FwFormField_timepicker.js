class FwFormField_timepickerClass {
    renderDesignerHtml($control, html) {
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
    renderRuntimeHtml($control, html) {
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
        }).off();
        $control.find('.fwformfield-value').change(e => {
            const $this = jQuery(e.currentTarget);
            let val = $this.val();
            val = val.toString().toUpperCase().trim();
            let meridiem;
            if (val.endsWith('PM') || val.endsWith('AM')) {
                meridiem = val.substring(val.length - 2);
                val = val.substring(0, val.length - 2);
            }
            val = val.replace(/\D/g, '');
            if (val.length > 4 || val.length < 3) {
                val = '00:00';
                return $this.val('00:00');
            }
            let start, end;
            if (val.length === 3) {
                start = val.substring(0, 1);
                end = val.substring(1);
            }
            else {
                start = val.substring(0, 2);
                end = val.substring(2);
            }
            val = `${start}:${end}${meridiem ? meridiem : ''}`;
            $this.val(val);
        });
        $control.on('click', '.btntime', function (e) {
            if ($control.attr('data-enabled') === 'true') {
                e.stopPropagation();
                $control.find('.fwformfield-value').clockpicker('show').clockpicker('toggleView', 'hours');
            }
        });
    }
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    }
    disable($control) {
    }
    enable($control) {
    }
    getValue2($fwformfield) {
        const value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_timepicker = new FwFormField_timepickerClass();
//# sourceMappingURL=FwFormField_timepicker.js.map