var FwFormField_encryptClass = (function () {
    function FwFormField_encryptClass() {
    }
    FwFormField_encryptClass.prototype.renderDesignerHtml = function ($control, html) {
    };
    FwFormField_encryptClass.prototype.renderRuntimeHtml = function ($control, html) {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') != '')) {
            html.push(' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        if ((sessionStorage.getItem('applicationsettings') != null) && (typeof JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase != 'undefined') && (JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase)) {
            html.push(' style="text-transform:uppercase"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_encryptClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_encryptClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    };
    FwFormField_encryptClass.prototype.disable = function ($control) {
    };
    FwFormField_encryptClass.prototype.enable = function ($control) {
    };
    FwFormField_encryptClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_encryptClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_encryptClass;
}());
var FwFormField_encrypt = new FwFormField_encryptClass();
//# sourceMappingURL=FwFormField_encrypt.js.map