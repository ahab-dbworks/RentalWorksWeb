var FwBrowseColumn_phoneClass = (function () {
    function FwBrowseColumn_phoneClass() {
    }
    FwBrowseColumn_phoneClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_phoneClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_phoneClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_phoneClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    ;
    FwBrowseColumn_phoneClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_phoneClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html = html.join('');
        $field.html(html);
        if ($field.attr('data-formreadonly') === 'false') {
            $field.find('input.value').inputmask('(999) 999-9999');
        }
        $field.find('input.value').val(originalvalue);
    };
    return FwBrowseColumn_phoneClass;
}());
var FwBrowseColumn_phone = new FwBrowseColumn_phoneClass();
//# sourceMappingURL=FwBrowseColumn_phone.js.map