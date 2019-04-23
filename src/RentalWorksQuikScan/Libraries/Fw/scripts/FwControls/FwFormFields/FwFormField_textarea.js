var FwFormField_textareaClass = (function () {
    function FwFormField_textareaClass() {
    }
    FwFormField_textareaClass.prototype.renderDesignerHtml = function ($control, html) {
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
    FwFormField_textareaClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<textarea class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') !== '') && ($control.attr('data-maxlength') !== '-1') && ($control.attr('data-maxlength') !== '0')) {
            html.push(' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        if ((typeof $control.attr('data-height') !== 'undefined') && ($control.attr('data-height') !== '')) {
            html.push(' style="height:' + $control.attr('data-height') + ';"');
        }
        if ((sessionStorage.getItem('applicationsettings') !== null) && (typeof JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase !== 'undefined') && (JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase)) {
            html.push(' style="text-transform:uppercase"');
        }
        html.push('></textarea>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_textareaClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_textareaClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_textareaClass.prototype.disable = function ($control) {
    };
    FwFormField_textareaClass.prototype.enable = function ($control) {
    };
    FwFormField_textareaClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_textareaClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_textareaClass;
}());
var FwFormField_textarea = new FwFormField_textareaClass();
//# sourceMappingURL=FwFormField_textarea.js.map