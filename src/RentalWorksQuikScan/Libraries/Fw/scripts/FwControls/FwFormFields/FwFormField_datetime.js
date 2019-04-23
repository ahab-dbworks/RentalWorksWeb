var FwFormField_datetimeClass = (function () {
    function FwFormField_datetimeClass() {
    }
    FwFormField_datetimeClass.prototype.renderDesignerHtml = function ($control, html) {
    };
    FwFormField_datetimeClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_datetimeClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_datetimeClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_datetimeClass.prototype.disable = function ($control) {
    };
    FwFormField_datetimeClass.prototype.enable = function ($control) {
    };
    FwFormField_datetimeClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_datetimeClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_datetimeClass;
}());
var FwFormField_datetime = new FwFormField_datetimeClass();
//# sourceMappingURL=FwFormField_datetime.js.map