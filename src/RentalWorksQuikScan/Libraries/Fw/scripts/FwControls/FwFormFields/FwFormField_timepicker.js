var FwFormField_timepickerClass = (function () {
    function FwFormField_timepickerClass() {
    }
    FwFormField_timepickerClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push("<div class=\"fwformfield-caption\">" + $control.attr('data-caption') + "</div>");
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_timepickerClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push("<div class=\"fwformfield-caption\">" + $control.attr('data-caption') + "</div>");
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
        }).off();
        $control.find('.fwformfield-value').change(function (e) {
            var $this = jQuery(e.currentTarget);
            var field = $this.closest('div[data-type="timepicker"]');
            field.removeClass('error');
            var val = $this.val().toString().replace(/\D/g, '');
            if (val != '') {
                if (val.length !== 4) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }
                var start = val.substring(0, 2);
                var startDecimal = new Decimal(start);
                if (startDecimal.greaterThan(23)) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }
                var end = val.substring(2);
                var endDecimal = new Decimal(end);
                if (endDecimal.greaterThan(59)) {
                    field.addClass('error');
                    FwNotification.renderNotification('WARNING', "You've entered a non-standard time format.");
                    return;
                }
                $this.val(start + ":" + end);
            }
        });
        $control.on('click', '.btntime', function (e) {
            if ($control.attr('data-enabled') === 'true') {
                e.stopPropagation();
                $control.find('.fwformfield-value').clockpicker('show').clockpicker('toggleView', 'hours');
            }
        });
    };
    FwFormField_timepickerClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_timepickerClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_timepickerClass.prototype.disable = function ($control) {
    };
    FwFormField_timepickerClass.prototype.enable = function ($control) {
    };
    FwFormField_timepickerClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_timepickerClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_timepickerClass;
}());
var FwFormField_timepicker = new FwFormField_timepickerClass();
//# sourceMappingURL=FwFormField_timepicker.js.map