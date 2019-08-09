var FwFormField_selectClass = (function () {
    function FwFormField_selectClass() {
    }
    FwFormField_selectClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<select class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></select>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_selectClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<select class="fwformfield-value"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('></select>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_selectClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
        var html, previousoptgroup, optgroup, selected;
        html = [];
        if (!hideEmptyItem) {
            html.push('<option value=""></option>');
        }
        previousoptgroup = null;
        optgroup = '';
        if ((typeof items !== 'undefined') && (items !== null)) {
            for (var i = 0; i < items.length; i++) {
                if ((typeof items[i].optgroup !== 'undefined') && (items[i].optgroup !== previousoptgroup)) {
                    previousoptgroup = items[i].optgroup;
                    html.push('<optgroup label="' + items[i].optgroup + '">');
                }
                selected = '';
                if ((typeof items[i].selected !== 'undefined') && (items[i].selected === true)) {
                    selected = ' selected="true"';
                    $control.attr('data-originalvalue', items[i].value);
                }
                html.push('<option value="' + items[i].value + '"' + selected);
                for (var key in items[i]) {
                    if (key != 'value' && key != 'text') {
                        html.push(' data-' + key + '="' + items[i][key] + '"');
                    }
                }
                html.push('>' + items[i].text + '</option>');
                if ((typeof items[i].optgroup !== 'undefined') && ((i + 1) < items.length) && (items[i].optgroup !== items[i + 1].optgroup)) {
                    html.push('</optgroup>');
                }
            }
        }
        $control.find('.fwformfield-control > select.fwformfield-value').html(html.join('\n'));
    };
    FwFormField_selectClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_selectClass.prototype.disable = function ($control) {
    };
    FwFormField_selectClass.prototype.enable = function ($control) {
    };
    FwFormField_selectClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_selectClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_selectClass;
}());
var FwFormField_select = new FwFormField_selectClass();
//# sourceMappingURL=FwFormField_select.js.map