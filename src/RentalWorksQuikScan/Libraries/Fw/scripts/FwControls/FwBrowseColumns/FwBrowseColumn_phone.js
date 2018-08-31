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
    FwBrowseColumn_phoneClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
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
    FwBrowseColumn_phoneClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    };
    FwBrowseColumn_phoneClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var html = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        var htmlString = html.join('');
        $field.html(htmlString);
        if ($field.attr('data-formreadonly') === 'false') {
            $field.find('input.value').inputmask('(999) 999-9999');
        }
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    };
    return FwBrowseColumn_phoneClass;
}());
var FwBrowseColumn_phone = new FwBrowseColumn_phoneClass();
//# sourceMappingURL=FwBrowseColumn_phone.js.map