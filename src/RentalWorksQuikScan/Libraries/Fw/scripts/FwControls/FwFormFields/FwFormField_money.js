var FwFormField_moneyClass = (function () {
    function FwFormField_moneyClass() {
    }
    FwFormField_moneyClass.prototype.renderDesignerHtml = function ($control, html) {
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
    FwFormField_moneyClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('.fwformfield-value').inputmask("currency");
    };
    FwFormField_moneyClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_moneyClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        value = ((value === '') ? '0.00' : value);
        $fwformfield
            .attr('data-originalvalue', parseFloat(value).toFixed(2))
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_moneyClass.prototype.disable = function ($control) {
    };
    FwFormField_moneyClass.prototype.enable = function ($control) {
    };
    FwFormField_moneyClass.prototype.getValue2 = function ($fwformfield) {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = valuecontainer !== null ? valuecontainer : '';
        return value;
    };
    FwFormField_moneyClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_moneyClass;
}());
var FwFormField_money = new FwFormField_moneyClass();
//# sourceMappingURL=FwFormField_money.js.map