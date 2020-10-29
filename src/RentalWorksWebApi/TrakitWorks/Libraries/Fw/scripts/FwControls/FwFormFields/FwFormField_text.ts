class FwFormField_textClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
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
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push(`<input class="fwformfield-value" type="text" name="${'a'+Math.random()}"`);
        if (applicationConfig.allCaps && $control.attr('data-allcaps') !== 'false') {
            html.push(' style="text-transform:uppercase"');
        }
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') !== '')) {
            html.push(' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        if ((typeof $control.attr('data-autocapitalize') !== 'undefined') && ($control.attr('data-autocapitalize') !== '')) {
            html.push(' autocapitalize="' + $control.attr('data-autocapitalize') + '"');
        }
        if ((typeof $control.attr('data-autocorrect') !== 'undefined') && ($control.attr('data-autocorrect') !== '')) {
            html.push(' autocorrect="' + $control.attr('data-autocorrect') + '"');
        }
        if ((sessionStorage.getItem('applicationsettings') !== null) && (typeof JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase !== 'undefined') && (JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase)) {
            html.push(' style="text-transform:uppercase"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
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
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        var value;
        if (applicationConfig.allCaps && $fwformfield.attr('data-allcaps') !== 'false') {
            value = (<string>$fwformfield.find('.fwformfield-value').val()).toUpperCase();
        } else {
            value = $fwformfield.find('.fwformfield-value').val();
        }
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_text = new FwFormField_textClass();