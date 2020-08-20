class FwFormField_togglebuttonsClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}: ${$control.attr('id')}</div>`);
        html.push('<div class="fwformfield-control"></div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        const name = FwApplication.prototype.uniqueId(10);
        $control.attr('data-name', name);
        //if ($control.attr('data-caption')) {
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control"></div>');
        //} else {
        //    html.push('<div class="fwformfield-control"></div>');
        //}
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any): void {
        if ((typeof items !== 'undefined') && (items !== null)) {
            const name = $control.attr('data-name');
            for (let i = 0; i < items.length; i++) {
                const $item = jQuery(`<label class="togglebutton-item">
                                        <input type="radio" name="${name}" value="${items[i].value}" class="fwformfield-value" ${items[i].checked ? 'checked' : ''} ${$control.attr('data-enabled') === 'false' ? ' disabled="disabled"' : ''}>
                                        <span class="togglebutton-button">${items[i].caption}</span>
                                      </label>`);
                $control.find('.fwformfield-control').append($item);
            }
        }
    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield.attr('data-originalvalue', value);
        $fwformfield.find(`input[value="${value}"]`).prop('checked', true);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {
        $control.find(`input[name="${$control.attr('data-name')}"]`).prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {
        $control.find(`input[name="${$control.attr('data-name')}"]`).prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        const value = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"]:checked`).val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        let $inputvalue;
        if (value !== '') {
            $inputvalue = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"][value="${value}"]`);
            $inputvalue.prop('checked', true);
        } else {
            const previousVal = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"]:checked`).val();
            $inputvalue = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"][value="${previousVal}"]`);
            $inputvalue.prop('checked', false);
        }
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_togglebuttons = new FwFormField_togglebuttonsClass();