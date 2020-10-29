class FwFormField_colorClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="color"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none" value="#ffffff"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="fwformfield-colorselector">');
        html.push('<div class="fwformfield-color" style="background-color:#ffffff;"></div>');
        html.push('<i class="material-icons btncolorpicker">&#xE3B7;</i>');
        html.push('</div>');
        html.push('</div>');
        $control.html(html.join(''));

        $control.find('.fwformfield-colorselector')
            .colpick({
                layout: 'hex',
                colorScheme: 'light',
                color: 'ffffff',
                showEvent: '',
                onSubmit: function (hsb, hex, rgb, el) {
                    jQuery(el).siblings('.fwformfield-value').val(`#${hex}`).change();
                    jQuery(el).find('.fwformfield-color').css('background-color', `#${hex}`);
                    jQuery(el).colpickHide();
                }
            })
            .on('click', function (e: JQuery.Event) {
                try {
                    if ($control.attr('data-enabled') === 'true') {
                        jQuery(this).colpickShow(e);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void { }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-colorselector').colpickSetColor(value);
        $fwformfield.find('.fwformfield-color').css('background-color', value);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery) { }
    //---------------------------------------------------------------------------------
    enable($control: JQuery) { }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery) {
        const value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value, text, firechangeevent) {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_color = new FwFormField_colorClass();
