class FwFormField_radioClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + ': ' + $control.attr('id') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="radio"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ($control.attr('data-name') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(` name="${$control.attr('data-name')}"`);
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        var $children, name, uniqueId, child;
        $children = $control.children();
        name = FwApplication.prototype.uniqueId(10);
        $control.attr('data-name', name);
        $control.empty();
        for (var i = 0; i < $children.length; i++) {
            uniqueId = FwApplication.prototype.uniqueId(10);
            child = [];
            child.push(`<input id="${uniqueId}" class="fwformfield-value" type="radio"`);
            if ($children.eq(i).attr('data-value') != '') {
                child.push(` value="${$children.eq(i).attr('data-value')}"`);
            }
            child.push(' name="' + name + '"'); //MY 5/22/2014: Radio groups cannot have the same name across forms.
            if ($control.attr('data-enabled') === 'false') {
                child.push(' disabled');
            }
            if (i == 0) {
                child.push(' checked');
            }
            child.push(' />');
            child.push(`<label for="${uniqueId}">${$children.eq(i).attr('data-caption')}</label>`);
            $children.eq(i).html(child.join(''));
        }
        if ($control.attr('data-caption')) {  //justin 11/02/2018 don't generate this Div if the caption is blank
            html.push(`<div class="fwformfield-caption">${$control.attr('data-caption')}</div>`);
            html.push('<div class="fwformfield-control"></div>');
        }
        else {
            html.push('<div class="fwformfield-control"></div>');
        }
        $control.html(html.join(''));
        $control.find('.fwformfield-control').append($children);
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield.attr('data-originalvalue', value);
        $fwformfield.find('input[value="' + value + '"]')
            .prop('checked', true);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {
        $control.find('input[name="' + $control.attr('data-name') + '"]').prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {
        $control.find('input[name="' + $control.attr('data-name') + '"]').prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        var value = $fwformfield.find('input[name="' + $fwformfield.attr('data-name') + '"]:checked').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('input[name="' + $fwformfield.attr('data-name') + '"][value="' + value + '"]');
        $inputvalue.prop('checked', true);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_radio = new FwFormField_radioClass();