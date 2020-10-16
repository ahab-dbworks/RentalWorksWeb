class FwFormField_selectClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<select class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></select>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<select class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></select>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: { text: string, value: string, selected?: boolean, optgroup?: string }[], hideEmptyItem: boolean): void {
        var optgroup, selected;

        const html = [];
        if (!hideEmptyItem) {
            html.push('<option value=""></option>');
        }
        let previousoptgroup = null;
        optgroup = '';
        if ((typeof items !== 'undefined') && (items !== null)) {
            for (var i = 0; i < items.length; i++) {
                if ((typeof items[i].optgroup !== 'undefined') && (items[i].optgroup !== previousoptgroup)) {
                    previousoptgroup = items[i].optgroup;
                    html.push('<optgroup label="' + items[i].optgroup + '">');
                }
                selected = '';
                if ((typeof items[i].selected !== 'undefined') && (items[i].selected === true)) {
                    selected = ' selected="true"';
                    $control.attr('data-originalvalue', items[i].value);
                }
                html.push('<option value="' + items[i].value + '"' + selected);
                for (var key in items[i]) {
                    if (key != 'value' && key != 'text') {
                        html.push(' data-' + key + '="' + items[i][key] + '"');
                    }
                }
                html.push('>' + items[i].text + '</option>');
                if ((typeof items[i].optgroup !== 'undefined') && ((i + 1) < items.length) && (items[i].optgroup !== items[i + 1].optgroup)) {
                    html.push('</optgroup>');
                }
            }
        }
        $control.find('.fwformfield-control > select.fwformfield-value').html(html.join('\n'));
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
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text?: string, firechangeevent?: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_select = new FwFormField_selectClass();