var FwFormField_ssnClass = (function () {
    function FwFormField_ssnClass() {
    }
    FwFormField_ssnClass.prototype.renderDesignerHtml = function ($control, html) {
    };
    FwFormField_ssnClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('input').inputmask({ mask: '999-99-9999' });
    };
    FwFormField_ssnClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_ssnClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_ssnClass.prototype.disable = function ($control) {
    };
    FwFormField_ssnClass.prototype.enable = function ($control) {
    };
    FwFormField_ssnClass.prototype.getValue2 = function ($fwformfield) {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = valuecontainer !== null ? valuecontainer : '';
        return value;
    };
    FwFormField_ssnClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_ssnClass;
}());
var FwFormField_ssn = new FwFormField_ssnClass();
//# sourceMappingURL=FwFormField_ssn.js.map