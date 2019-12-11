var FwFormField_togglebuttonsClass = (function () {
    function FwFormField_togglebuttonsClass() {
    }
    FwFormField_togglebuttonsClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push("<div class=\"fwformfield-caption\">" + $control.attr('data-caption') + ": " + $control.attr('id') + "</div>");
        html.push('<div class="fwformfield-control"></div>');
        $control.html(html.join(''));
    };
    FwFormField_togglebuttonsClass.prototype.renderRuntimeHtml = function ($control, html) {
        var name = FwApplication.prototype.uniqueId(10);
        $control.attr('data-name', name);
        html.push("<div class=\"fwformfield-caption\">" + $control.attr('data-caption') + "</div>");
        html.push('<div class="fwformfield-control"></div>');
        $control.html(html.join(''));
    };
    FwFormField_togglebuttonsClass.prototype.loadItems = function ($control, items) {
        if ((typeof items !== 'undefined') && (items !== null)) {
            var name_1 = $control.attr('data-name');
            for (var i = 0; i < items.length; i++) {
                var $item = jQuery("<label class=\"togglebutton-item\">\n                                        <input type=\"radio\" name=\"" + name_1 + "\" value=\"" + items[i].value + "\" class=\"fwformfield-value\" " + (items[i].checked ? 'checked' : '') + " " + ($control.attr('data-enabled') === 'false' ? ' disabled="disabled"' : '') + ">\n                                        <span class=\"togglebutton-button\">" + items[i].caption + "</span>\n                                      </label>");
                $control.find('.fwformfield-control').append($item);
            }
        }
    };
    FwFormField_togglebuttonsClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield.attr('data-originalvalue', value);
        $fwformfield.find("input[value=\"" + value + "\"]").prop('checked', true);
    };
    FwFormField_togglebuttonsClass.prototype.disable = function ($control) {
        $control.find("input[name=\"" + $control.attr('data-name') + "\"]").prop('disabled', true);
    };
    FwFormField_togglebuttonsClass.prototype.enable = function ($control) {
        $control.find("input[name=\"" + $control.attr('data-name') + "\"]").prop('disabled', false);
    };
    FwFormField_togglebuttonsClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find("input[name=\"" + $fwformfield.attr('data-name') + "\"]:checked").val();
        return value;
    };
    FwFormField_togglebuttonsClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find("input[name=\"" + $fwformfield.attr('data-name') + "\"][value=\"" + value + "\"]");
        $inputvalue.prop('checked', true);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_togglebuttonsClass;
}());
var FwFormField_togglebuttons = new FwFormField_togglebuttonsClass();
//# sourceMappingURL=FwFormField_togglebuttons.js.map