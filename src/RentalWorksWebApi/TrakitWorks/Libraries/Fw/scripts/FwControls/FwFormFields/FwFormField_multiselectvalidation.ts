﻿class FwFormField_multiselectvalidationClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery, html: string[]): void {
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
        html.push('<div class="clearall"><i class="material-icons">clear</i></div>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery, html: string[]): void {
        var validationName, $valuefield, $searchfield, $btnvalidate;

        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<div contenteditable="true" class="multiselectitems"><span class="addItem" tabindex="-1"');
        if (applicationConfig.allCaps && $control.attr('data-allcaps') !== 'false') {
            html.push(' style="text-transform:uppercase"');
        }
        html.push('></span></div>');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('style="display:none" />');
        html.push('<div class="btnvalidate"><i class="material-icons">search</i></div>');
        html.push('<div class="clearall"><i class="material-icons">clear</i></div>');
        html.push('</div>');
        $control.html(html.join(''));
        validationName = $control.attr('data-validationname');
        $valuefield = $control.find('> .fwformfield-control > .fwformfield-value');
        $searchfield = $control.find('> .fwformfield-control .addItem');
        $btnvalidate = $control.find('> .fwformfield-control > .btnvalidate');
        FwMultiSelectValidation.init($control, validationName, $valuefield, $searchfield, $btnvalidate);
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);

        const $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        } else {
            $browse.removeData('selectedrows');
        }

        if (value !== '') {
            const multiselectfield = $fwformfield.find('.multiselectitems');
            //clear values
            multiselectfield.find('.multiitem').remove();

            let valueArr = value.split(',');
            valueArr = valueArr.map(s => s.trim());
            let textArr;
            const multiSeparator = jQuery($browse.find(`thead [data-validationdisplayfield="true"]`).get(0)).attr('data-multiwordseparator') || ',';
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
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery): void {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery): void {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery): any {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    getText2($fwformfield: JQuery): string {
        var text;
        if (applicationConfig.allCaps && $fwformfield.attr('data-allcaps') !== 'false') {
            text = (<string>$fwformfield.find('.fwformfield-text').val()).toUpperCase();
        } else {
            text = <string>$fwformfield.find('.fwformfield-text').val();
        }
        return text;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery, value: any, text: string, firechangeevent: boolean): void {
        let valueArr, textArr;

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

        const $multiselectfield = $fwformfield.find('.multiselectitems');
        $multiselectfield.find('.multiitem').remove();
        if (value !== '') {
            const multiSeparator = jQuery($browse.find(`thead [data-validationdisplayfield="true"]`).get(0)).attr('data-multiwordseparator') || ',';
            if (typeof text !== 'undefined') {
                textArr = text.split(multiSeparator);
            }

            if ($fwformfield.hasClass('email')) {
                textArr = value.split(',');
                valueArr = value.split(',');
            } else {
                valueArr = value.split(',');
            }

            for (let i = 0; i < valueArr.length; i++) {
                jQuery(`<div contenteditable="false" class="multiitem" data-multivalue="${valueArr[i]}">
                        <span>${textArr[i]}</span>
                        <i class="material-icons">clear</i>
                    </div>`).insertBefore($multiselectfield.find('.addItem'));
            }
            //$multiselectfield.find('.addItem').focus();
        }
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_multiselectvalidation = new FwFormField_multiselectvalidationClass();