var FwFormField_timepickerClass = (function () {
    function FwFormField_timepickerClass() {
    }
    FwFormField_timepickerClass.prototype.renderDesignerHtml = function ($control, html) {
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
    };
    FwFormField_timepickerClass.prototype.renderRuntimeHtml = function ($control, html) {
        var timepickerTimeFormat, inputmaskTimeFormat;
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" id="timepicker" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons btntime">schedule</i>');
        html.push('</div>');
        $control.html(html.join(''));
        if ($control.attr('data-timeformat') === '24') {
            $control.find('#timepicker').clockpicker({
                autoclose: true, donetext: 'Done', afterDone: function () {
                    $control.find('input').focus();
                }
            });
        }
        else {
            $control.find('#timepicker').clockpicker({
                autoclose: true, twelvehour: true, donetext: 'Done', afterDone: function () {
                    $control.find('input').focus();
                }
            });
        }
        $control.find('input').off();
        $control.find('.btntime').click(function (e) {
            e.stopPropagation();
            $control.find('#timepicker').clockpicker('show').clockpicker('toggleView', 'hours');
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
        $fwformfield.closest('.fwform').find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        $fwformfield.closest('.fwform').attr('data-modified', 'true');
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