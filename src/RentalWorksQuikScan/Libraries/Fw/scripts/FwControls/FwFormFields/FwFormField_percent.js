var FwFormField_percentClass = (function () {
    function FwFormField_percentClass() {
    }
    FwFormField_percentClass.prototype.renderDesignerHtml = function ($control, html) {
    };
    FwFormField_percentClass.prototype.renderRuntimeHtml = function ($control, html) {
        var digits;
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        digits = ((typeof $control.attr('data-digits') !== 'undefined') ? $control.attr('data-digits') : 2);
        $control.find('input').inputmask({ alias: 'numeric', suffix: ' %', digits: digits });
    };
    FwFormField_percentClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_percentClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_percentClass.prototype.disable = function ($control) {
    };
    FwFormField_percentClass.prototype.enable = function ($control) {
    };
    FwFormField_percentClass.prototype.getValue2 = function ($fwformfield) {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = valuecontainer !== null ? valuecontainer : '';
        return value;
    };
    FwFormField_percentClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_percentClass;
}());
var FwFormField_percent = new FwFormField_percentClass();
//# sourceMappingURL=FwFormField_percent.js.map