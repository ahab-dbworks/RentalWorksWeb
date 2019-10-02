class FwFormField_togglebuttonsClass {
    renderDesignerHtml($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}: ${$control.attr('id')}</div>`);
        html.push('<div class="fwformfield-control"></div>');
        $control.html(html.join(''));
    }
    renderRuntimeHtml($control, html) {
        const name = FwApplication.prototype.uniqueId(10);
        $control.attr('data-name', name);
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control"></div>');
        $control.html(html.join(''));
    }
    loadItems($control, items) {
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
    loadForm($fwformfield, table, field, value, text) {
        $fwformfield.attr('data-originalvalue', value);
        $fwformfield.find(`input[value="${value}"]`).prop('checked', true);
    }
    disable($control) {
        $control.find(`input[name="${$control.attr('data-name')}"]`).prop('disabled', true);
    }
    enable($control) {
        $control.find(`input[name="${$control.attr('data-name')}"]`).prop('disabled', false);
    }
    getValue2($fwformfield) {
        const value = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"]:checked`).val();
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        const $inputvalue = $fwformfield.find(`input[name="${$fwformfield.attr('data-name')}"][value="${value}"]`);
        $inputvalue.prop('checked', true);
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_togglebuttons = new FwFormField_togglebuttonsClass();
//# sourceMappingURL=FwFormField_togglebuttons.js.map