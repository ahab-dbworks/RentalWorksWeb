var FwFormField_checkboxClass = (function () {
    function FwFormField_checkboxClass() {
    }
    FwFormField_checkboxClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="checkbox"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(' name="' + $control.attr('data-name') + '"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_checkboxClass.prototype.renderRuntimeHtml = function ($control, html) {
        var uniqueId = FwApplication.prototype.uniqueId(10);
        html.push('<div class="fwformfield-control">');
        html.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(' name="' + $control.attr('data-name') + '"');
        }
        html.push(' />');
        html.push('<label class="checkbox-caption" for="' + uniqueId + '">' + $control.attr('data-caption') + '</label>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_checkboxClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_checkboxClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        if (typeof value === 'string') {
            $fwformfield.attr('data-originalvalue', ((value === 'T') ? 'T' : 'F'));
            if (value === 'T') {
                $fwformfield.find('input[type="checkbox"]').prop('checked', true);
            }
        }
        else if (typeof value === 'boolean') {
            $fwformfield.attr('data-originalvalue', value.toString());
            if (value === true) {
                $fwformfield.find('input[type="checkbox"]').prop('checked', true);
            }
        }
    };
    FwFormField_checkboxClass.prototype.disable = function ($control) {
    };
    FwFormField_checkboxClass.prototype.enable = function ($control) {
    };
    FwFormField_checkboxClass.prototype.getValue2 = function ($fwformfield) {
        var controller = FwFormField.getController($fwformfield);
        var value;
        if ((typeof controller != 'undefined') && (typeof controller.apiurl !== 'undefined')) {
            value = $fwformfield.find('input').is(':checked');
        }
        else {
            value = $fwformfield.find('input').is(':checked') ? 'T' : 'F';
        }
        return value;
    };
    FwFormField_checkboxClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('input');
        $inputvalue.prop('checked', value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_checkboxClass;
}());
var FwFormField_checkbox = new FwFormField_checkboxClass();
//# sourceMappingURL=FwFormField_checkbox.js.map