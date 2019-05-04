var FwFormField_editorClass = (function () {
    function FwFormField_editorClass() {
    }
    FwFormField_editorClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<textarea class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_editorClass.prototype.renderRuntimeHtml = function ($control, html) {
        var editor;
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<textarea class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
        editor = $control.find('.fwformfield-value').ckeditor().editor;
        $control.data('editor', editor);
        editor.on('change', function () {
            try {
                $control.find('.fwformfield-value').change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        editor.on('instanceReady', function () {
            try {
                this.dataProcessor.writer.setRules('p', {
                    indent: false,
                    breakBeforeOpen: false,
                    breakAfterOpen: false,
                    breakBeforeClose: false,
                    breakAfterClose: false
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    FwFormField_editorClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_editorClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        $fwformfield.data('editor').on('instanceReady', function () {
            try {
                CKEDITOR.instances[$fwformfield.data('editor').name].setData(value);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    FwFormField_editorClass.prototype.disable = function ($control) {
    };
    FwFormField_editorClass.prototype.enable = function ($control) {
    };
    FwFormField_editorClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_editorClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    FwFormField_editorClass.prototype.onRemove = function ($fwformfield) {
        var editor = $fwformfield.data('editor');
        editor.destroy();
    };
    return FwFormField_editorClass;
}());
var FwFormField_editor = new FwFormField_editorClass();
//# sourceMappingURL=FwFormField_editor.js.map