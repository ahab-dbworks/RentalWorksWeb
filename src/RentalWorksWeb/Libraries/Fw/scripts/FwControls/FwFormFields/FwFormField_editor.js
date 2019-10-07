class FwFormField_editorClass {
    renderDesignerHtml($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push('<textarea class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    renderRuntimeHtml($control, html) {
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push('<textarea name= editor1 class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
        const editorElement = $control.find('.fwformfield-value');
        const editor = editorElement.ckeditor();
        editorElement.data('editor', editor);
    }
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        var editor = $fwformfield.find('.fwformfield-value').ckeditor();
        $fwformfield.data('editor', editor);
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        editor.ckeditorGet().on('change', function () {
            try {
                $fwformfield.find('.fwformfield-value').change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    disable($control) {
    }
    enable($control) {
    }
    getValue2($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    }
    onRemove($fwformfield) {
        $fwformfield.find('.fwformfield-value').remove();
    }
}
var FwFormField_editor = new FwFormField_editorClass();
//# sourceMappingURL=FwFormField_editor.js.map