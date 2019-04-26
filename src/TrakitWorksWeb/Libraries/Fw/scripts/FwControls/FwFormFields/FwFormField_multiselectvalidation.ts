﻿class FwFormField_multiselectvalidationClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push(`<div contenteditable="true" class="multiselectitems"><span class="addItem" tabindex="-1"></span></div>`);
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-value" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('style="display:none" />');
        html.push('<div class="btnvalidate"><i class="material-icons">search</i></div>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        var validationName, $valuefield, $searchfield, $btnvalidate;

        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push(`<div contenteditable="true" class="multiselectitems"><span class="addItem" tabindex="-1"></span></div>`);
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('style="display:none" />');
        html.push('<div class="btnvalidate"><i class="material-icons">search</i></div>');
        html.push('</div>');
        $control.html(html.join(''));
        validationName = $control.attr('data-validationname');
        $valuefield = $control.find('> .fwformfield-control > .fwformfield-value');
        $searchfield = $control.find('> .fwformfield-control .addItem');
        $btnvalidate = $control.find('> .fwformfield-control > .btnvalidate');
        FwMultiSelectValidation.init($control, validationName, $valuefield, $searchfield, $btnvalidate);
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadDisplayFields($control: JQuery<HTMLElement>) {
        const $multiSelectDisplay = $control.find('.multiSelectDisplay');
        const $displayFields = $control.find('.fieldnames td[data-visible="true"]');
        let html = [];
        if ($displayFields.length !== 0) {
            for (let i = 0; i < $displayFields.length; i++) {
                let $field = $displayFields[i];
                let caption = jQuery($field).find('div.field').attr('data-caption');
                let datafield = jQuery($field).find('div.field').attr('data-browsedatafield');
                html.push(`<option data-datafield="${datafield}" value="${caption}">${caption}</option>`);
            }
        }
        $multiSelectDisplay.find('select').append(html.join(''));
        $multiSelectDisplay.css('display', 'inline-block');
    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-text')
            .val(text);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    getText2($fwformfield: JQuery<HTMLElement>): string {
        var text = <string>$fwformfield.find('.fwformfield-text').val();
        return text;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        // this is really only useful for clearing the value, otherwise it will be out of sync with the selected rows
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        const $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);

        //clears previous selected row values
        const $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        } else {
            $browse.removeData('selectedrows');
        }
        const multiselectfield = $fwformfield.find('.multiselectitems');
        multiselectfield.find('.multiitem').remove();
        if (value !== '') {
            let valueArr;
            $fwformfield.hasClass('email') ? valueArr = value.split(';') : valueArr = value.split(',');
            let textArr;
            const fieldToDisplay = $browse.find('.multiSelectDisplay select option:selected').attr('data-datafield');
            const multiSeparator = jQuery($browse.find(`thead [data-browsedatafield="${fieldToDisplay}"]`).get(0)).attr('data-multiwordseparator') || ',';
            if (typeof text !== 'undefined') {
                textArr = text.split(multiSeparator);
            }

            for (let i = 0; i < valueArr.length; i++) {
                multiselectfield.prepend(`
                    <div contenteditable="false" class="multiitem" data-multivalue="${valueArr[i]}">
                        <span>${textArr[i]}</span>
                        <i class="material-icons">clear</i>
                    </div>`);
            }
        }
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_multiselectvalidation = new FwFormField_multiselectvalidationClass();