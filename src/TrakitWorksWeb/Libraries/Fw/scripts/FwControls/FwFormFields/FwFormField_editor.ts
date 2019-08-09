class FwFormField_editorClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
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
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<textarea name= editor1 class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
        //var editor = $control.find('.fwformfield-value').ckeditor().editor;
        //$control.data('editor', editor);

        //editor.on('change', function () {
        //    try {
        //        $control.find('.fwformfield-value').change();
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
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
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void {
        var editor = $fwformfield.find('.fwformfield-value').ckeditor();
        $fwformfield.data('editor', editor);

        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);

        editor.ckeditorGet().on('change', function () {
            try {
                $fwformfield.find('.fwformfield-value').change();
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //$fwformfield.data('editor').on('instanceReady', function () {
        //    try {
        //        CKEDITOR.instances[$fwformfield.data('editor').name].setData(value);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
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
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
    onRemove($fwformfield) {
        //var editor = $fwformfield.find('.fwformfield-value').ckeditor();
        $fwformfield.find('.fwformfield-value').remove();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_editor = new FwFormField_editorClass();