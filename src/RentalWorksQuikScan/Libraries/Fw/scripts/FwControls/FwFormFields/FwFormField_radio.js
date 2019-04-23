var FwFormField_radioClass = (function () {
    function FwFormField_radioClass() {
    }
    FwFormField_radioClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + ': ' + $control.attr('id') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="radio"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ($control.attr('data-name') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-name') !== 'undefined') {
            html.push(" name=\"" + $control.attr('data-name') + "\"");
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_radioClass.prototype.renderRuntimeHtml = function ($control, html) {
        var $children, name, uniqueId, child;
        $children = $control.children();
        name = FwApplication.prototype.uniqueId(10);
        $control.attr('data-name', name);
        $control.empty();
        for (var i = 0; i < $children.length; i++) {
            uniqueId = FwApplication.prototype.uniqueId(10);
            child = [];
            child.push("<input id=\"" + uniqueId + "\" class=\"fwformfield-value\" type=\"radio\"");
            if ($children.eq(i).attr('data-value') != '') {
                child.push(" value=\"" + $children.eq(i).attr('data-value') + "\"");
            }
            child.push(' name="' + name + '"');
            if ($control.attr('data-enabled') === 'false') {
                child.push(' disabled');
            }
            if (i == 0) {
                child.push(' checked');
            }
            child.push(' />');
            child.push("<label for=\"" + uniqueId + "\">" + $children.eq(i).attr('data-caption') + "</label>");
            $children.eq(i).html(child.join(''));
        }
        if ($control.attr('data-caption')) {
            html.push("<div class=\"fwformfield-caption\">" + $control.attr('data-caption') + "</div>");
            html.push('<div class="fwformfield-control"></div>');
        }
        else {
            html.push('<div class="fwformfield-control"></div>');
        }
        $control.html(html.join(''));
        $control.find('.fwformfield-control').append($children);
    };
    FwFormField_radioClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_radioClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield.attr('data-originalvalue', value);
        $fwformfield.find('input[value="' + value + '"]')
            .prop('checked', true);
    };
    FwFormField_radioClass.prototype.disable = function ($control) {
        $control.find('input[name="' + $control.attr('data-name') + '"]').prop('disabled', true);
    };
    FwFormField_radioClass.prototype.enable = function ($control) {
        $control.find('input[name="' + $control.attr('data-name') + '"]').prop('disabled', false);
    };
    FwFormField_radioClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('input[name="' + $fwformfield.attr('data-name') + '"]:checked').val();
        return value;
    };
    FwFormField_radioClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('input[name="' + $fwformfield.attr('data-name') + '"][value="' + value + '"]');
        $inputvalue.prop('checked', true);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_radioClass;
}());
var FwFormField_radio = new FwFormField_radioClass();
//# sourceMappingURL=FwFormField_radio.js.map