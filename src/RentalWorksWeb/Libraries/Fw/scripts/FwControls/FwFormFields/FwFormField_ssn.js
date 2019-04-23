class FwFormField_ssnClass {
    renderDesignerHtml($control, html) {
    }
    renderRuntimeHtml($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('input').inputmask({ mask: '999-99-9999' });
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
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = valuecontainer !== null ? valuecontainer : '';
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_ssn = new FwFormField_ssnClass();
//# sourceMappingURL=FwFormField_ssn.js.map