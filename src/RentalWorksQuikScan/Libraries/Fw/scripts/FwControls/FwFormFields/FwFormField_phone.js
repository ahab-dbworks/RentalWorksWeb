var FwFormField_phoneClass = (function () {
    function FwFormField_phoneClass() {
    }
    FwFormField_phoneClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="content">');
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="tel"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('</div>');
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_phoneClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') != '')) {
            html.push(' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('input').inputmask('(999) 999-9999');
    };
    FwFormField_phoneClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_phoneClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_phoneClass.prototype.disable = function ($control) {
    };
    FwFormField_phoneClass.prototype.enable = function ($control) {
    };
    FwFormField_phoneClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_phoneClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_phoneClass;
}());
var FwFormField_phone = new FwFormField_phoneClass();
//# sourceMappingURL=FwFormField_phone.js.map