var FwFormField_multiselectvalidationClass = (function () {
    function FwFormField_multiselectvalidationClass() {
    }
    FwFormField_multiselectvalidationClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push("<div contenteditable=\"true\" class=\"multiselectitems\"><span class=\"addItem\" tabindex=\"-1\"></span></div>");
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-value" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('style="display:none" />');
        html.push('<div class="btnvalidate"><i class="material-icons">search</i></div>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_multiselectvalidationClass.prototype.renderRuntimeHtml = function ($control, html) {
        var validationName, $valuefield, $searchfield, $btnvalidate;
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push("<div contenteditable=\"true\" class=\"multiselectitems\"><span class=\"addItem\" tabindex=\"-1\"></span></div>");
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
    };
    FwFormField_multiselectvalidationClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_multiselectvalidationClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        var $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        }
        else {
            $browse.removeData('selectedrows');
        }
        if (value !== '') {
            var multiselectfield = $fwformfield.find('.multiselectitems');
            multiselectfield.find('.multiitem').remove();
            var valueArr = value.split(',');
            valueArr = valueArr.map(function (s) { return s.trim(); });
            var textArr = void 0;
            var multiSeparator = jQuery($browse.find("thead [data-validationdisplayfield=\"true\"]").get(0)).attr('data-multiwordseparator') || ',';
            if (typeof text !== 'undefined') {
                textArr = text.split(multiSeparator);
            }
            for (var i = 0; i < valueArr.length; i++) {
                multiselectfield.prepend("\n                    <div contenteditable=\"false\" class=\"multiitem\" data-multivalue=\"" + valueArr[i] + "\">\n                        <span>" + textArr[i] + "</span>\n                        <i class=\"material-icons\">clear</i>\n                    </div>");
            }
        }
    };
    FwFormField_multiselectvalidationClass.prototype.disable = function ($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    };
    FwFormField_multiselectvalidationClass.prototype.enable = function ($control) {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    };
    FwFormField_multiselectvalidationClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_multiselectvalidationClass.prototype.getText2 = function ($fwformfield) {
        var text = $fwformfield.find('.fwformfield-text').val();
        return text;
    };
    FwFormField_multiselectvalidationClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        var $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);
        var $browse = $fwformfield.data('browse');
        if (typeof $browse.data('selectedrows') === 'undefined') {
            $browse.data('selectedrows', {});
        }
        else {
            $browse.removeData('selectedrows');
        }
        var multiselectfield = $fwformfield.find('.multiselectitems');
        multiselectfield.find('.multiitem').remove();
        if (value !== '') {
            var valueArr = void 0;
            $fwformfield.hasClass('email') ? valueArr = value.split(';') : valueArr = value.split(',');
            var textArr = void 0;
            var multiSeparator = jQuery($browse.find("thead [data-validationdisplayfield=\"true\"]").get(0)).attr('data-multiwordseparator') || ',';
            if (typeof text !== 'undefined') {
                textArr = text.split(multiSeparator);
            }
            for (var i = 0; i < valueArr.length; i++) {
                multiselectfield.prepend("\n                    <div contenteditable=\"false\" class=\"multiitem\" data-multivalue=\"" + valueArr[i] + "\">\n                        <span>" + textArr[i] + "</span>\n                        <i class=\"material-icons\">clear</i>\n                    </div>");
            }
        }
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_multiselectvalidationClass;
}());
var FwFormField_multiselectvalidation = new FwFormField_multiselectvalidationClass();
//# sourceMappingURL=FwFormField_multiselectvalidation.js.map