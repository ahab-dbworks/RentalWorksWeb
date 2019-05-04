var FwFormField_zipcodeClass = (function () {
    function FwFormField_zipcodeClass() {
    }
    FwFormField_zipcodeClass.prototype.renderDesignerHtml = function ($control, html) {
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
    FwFormField_zipcodeClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') != '')) {
            html.push(' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('input').inputmask({ mask: '99999[-9999]' });
    };
    FwFormField_zipcodeClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_zipcodeClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-value').blur();
    };
    FwFormField_zipcodeClass.prototype.disable = function ($control) {
    };
    FwFormField_zipcodeClass.prototype.enable = function ($control) {
    };
    FwFormField_zipcodeClass.prototype.getValue2 = function ($fwformfield) {
        var value;
        if ((typeof $fwformfield.attr('data-savemask') !== 'undefined') && ($fwformfield.attr('data-savemask') == 'true')) {
            value = $fwformfield.find('.fwformfield-value').val();
        }
        else {
            var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
            value = valuecontainer !== null ? valuecontainer : '';
        }
        return value;
    };
    FwFormField_zipcodeClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_zipcodeClass;
}());
var FwFormField_zipcode = new FwFormField_zipcodeClass();
//# sourceMappingURL=FwFormField_zipcode.js.map