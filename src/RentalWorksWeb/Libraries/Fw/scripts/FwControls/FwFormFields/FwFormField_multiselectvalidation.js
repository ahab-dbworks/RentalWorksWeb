class FwFormField_multiselectvalidationClass {
    renderDesignerHtml($control, html) {
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
    renderRuntimeHtml($control, html) {
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
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        const $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        }
        else {
            $browse.removeData('selectedrows');
        }
        if (value !== '') {
            const multiselectfield = $fwformfield.find('.multiselectitems');
            const valueArr = value.split(',');
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
    disable($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    enable($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    getValue2($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    getText2($fwformfield) {
        var text = $fwformfield.find('.fwformfield-text').val();
        return text;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        const $inputvalue = $fwformfield.find('.fwformfield-value');
        const $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);
        const $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        }
        else {
            $browse.removeData('selectedrows');
        }
        const multiselectfield = $fwformfield.find('.multiselectitems');
        multiselectfield.find('.multiitem').remove();
        if (value !== '') {
            let valueArr;
            $fwformfield.hasClass('email') ? valueArr = value.split(';') : valueArr = value.split(',');
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
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_multiselectvalidation = new FwFormField_multiselectvalidationClass();
//# sourceMappingURL=FwFormField_multiselectvalidation.js.map