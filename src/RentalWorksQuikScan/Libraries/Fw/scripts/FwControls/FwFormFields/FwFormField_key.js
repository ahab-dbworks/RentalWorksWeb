var FwFormField_keyClass = (function () {
    function FwFormField_keyClass() {
    }
    FwFormField_keyClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_keyClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" ');
        if ($control.attr('data-originalvalue') != '') {
            html.push('value="' + $control.attr('data-originalvalue') + '"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_keyClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_keyClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_keyClass.prototype.disable = function ($control) {
    };
    FwFormField_keyClass.prototype.enable = function ($control) {
    };
    FwFormField_keyClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_keyClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_keyClass;
}());
var FwFormField_key = new FwFormField_keyClass();
//# sourceMappingURL=FwFormField_key.js.map