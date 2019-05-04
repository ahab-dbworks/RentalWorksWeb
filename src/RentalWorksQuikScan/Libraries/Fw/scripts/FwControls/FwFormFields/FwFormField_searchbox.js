var FwFormField_searchboxClass = (function () {
    function FwFormField_searchboxClass() {
    }
    FwFormField_searchboxClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' /><img class="imgbutton" src="' + $control.attr('data-imgurl') + '" />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_searchboxClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('  <input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push('  />');
        html.push('  <i class="material-icons btnvalidate">&#xE8B6;</i>');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_searchboxClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_searchboxClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_searchboxClass.prototype.disable = function ($control) {
    };
    FwFormField_searchboxClass.prototype.enable = function ($control) {
    };
    FwFormField_searchboxClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_searchboxClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_searchboxClass;
}());
var FwFormField_searchbox = new FwFormField_searchboxClass();
//# sourceMappingURL=FwFormField_searchbox.js.map