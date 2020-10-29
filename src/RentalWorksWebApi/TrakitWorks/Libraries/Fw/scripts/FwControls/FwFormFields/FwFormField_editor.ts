class FwFormField_editorClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
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
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        const name = 'editor-' + program.uniqueId(8);
        html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
        html.push('<div class="fwformfield-control">');
        html.push(`<textarea name="${name}" id="${name}" class="fwformfield-value"`);
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));

        const editor = $control.find('.fwformfield-value').ckeditor().editor;
        $control.data('editor', editor);

        editor.on('change', function (evt) {
            try {
                const origVal = $control.attr('data-originalvalue');
                let value = CKEDITOR.instances[name].getData();
                if (origVal != value) {
                    $control.find('.fwformfield-value').change();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //these already default to false
        //editor.on('instanceReady', function () {
        //    try {
        //        this.dataProcessor.writer.setRules('p', {
        //            indent: false,
        //            breakBeforeOpen: false,
        //            breakAfterOpen: false,
        //            breakBeforeClose: false,
        //            breakAfterClose: false
        //        });
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);

        const name = $fwformfield.data('editor').name;

        $fwformfield.data('editor').on('instanceReady', function () {
            try {
                CKEDITOR.instances[name].setData(value);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        const value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
    onRemove($fwformfield) {
        const editor = $fwformfield.data('editor');
        if (editor !== undefined) {
            editor.destroy();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_editor = new FwFormField_editorClass();