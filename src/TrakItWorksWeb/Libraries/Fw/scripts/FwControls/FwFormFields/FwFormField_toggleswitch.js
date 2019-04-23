class FwFormField_toggleswitchClass {
    renderDesignerHtml($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<label class="switch">');
        html.push('<input class="fwformfield-value" type="checkbox"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(' name="' + $control.attr('data-name') + '"');
        }
        html.push(' />');
        html.push('<span class="slider"></span>');
        html.push('</label>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    renderRuntimeHtml($control, html) {
        var uniqueId = FwApplication.prototype.uniqueId(10);
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<label class="switch">');
        html.push('<input id="' + uniqueId + '" class="fwformfield-value" type="checkbox"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(' name="' + $control.attr('data-name') + '"');
        }
        html.push(' />');
        html.push('<span class="slider"></span>');
        html.push('</label>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        if (typeof value === 'string') {
            $fwformfield.attr('data-originalvalue', ((value === 'T') ? 'T' : 'F'));
            if (value === 'T') {
                $fwformfield.find('input').prop('checked', true);
            }
        }
        else if (typeof value === 'boolean') {
            $fwformfield.attr('data-originalvalue', value.toString());
            if (value === true) {
                $fwformfield.find('input').prop('checked', true);
            }
        }
    }
    disable($control) {
    }
    enable($control) {
    }
    getValue2($fwformfield) {
        var controller = FwFormField.getController($fwformfield);
        var value;
        if ((typeof controller != 'undefined') && (typeof controller.apiurl !== 'undefined')) {
            value = $fwformfield.find('input').is(':checked');
        }
        else {
            value = $fwformfield.find('input').is(':checked') ? 'T' : 'F';
        }
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('input');
        $inputvalue.prop('checked', value);
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_toggleswitch = new FwFormField_toggleswitchClass();
//# sourceMappingURL=FwFormField_toggleswitch.js.map