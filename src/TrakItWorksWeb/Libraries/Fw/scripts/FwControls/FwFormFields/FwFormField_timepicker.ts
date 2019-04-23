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
        } else {
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
        var value = $fwformfield.find('.fwformfield-value').val();
        $fwformfield.closest('.fwform').find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        $fwformfield.closest('.fwform').attr('data-modified', 'true');
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_timepicker = new FwFormField_timepickerClass();