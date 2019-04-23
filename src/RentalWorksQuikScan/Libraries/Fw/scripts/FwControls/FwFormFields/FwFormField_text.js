var FwFormField_textClass = (function () {
    function FwFormField_textClass() {
    }
    FwFormField_textClass.prototype.renderDesignerHtml = function ($control, html) {
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
    };
    FwFormField_textClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text"');
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
    };
    FwFormField_textClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_textClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_textClass.prototype.disable = function ($control) {
    };
    FwFormField_textClass.prototype.enable = function ($control) {
    };
    FwFormField_textClass.prototype.getValue2 = function ($fwformfield) {
        var value;
        if (applicationConfig.allCaps && $fwformfield.attr('data-allcaps') !== 'false') {
            value = $fwformfield.find('.fwformfield-value').val().toUpperCase();
        }
        else {
            value = $fwformfield.find('.fwformfield-value').val();
        }
        return value;
    };
    FwFormField_textClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_textClass;
}());
var FwFormField_text = new FwFormField_textClass();
//# sourceMappingURL=FwFormField_text.js.map