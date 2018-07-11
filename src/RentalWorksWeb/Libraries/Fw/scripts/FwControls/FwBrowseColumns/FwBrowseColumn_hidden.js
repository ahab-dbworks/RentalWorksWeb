var FwBrowseColumn_hiddenClass = (function () {
    function FwBrowseColumn_hiddenClass() {
    }
    FwBrowseColumn_hiddenClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    ;
    FwBrowseColumn_hiddenClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    ;
    FwBrowseColumn_hiddenClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.html(data.value);
    };
    FwBrowseColumn_hiddenClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    ;
    FwBrowseColumn_hiddenClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
    };
    ;
    FwBrowseColumn_hiddenClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
    };
    ;
    return FwBrowseColumn_hiddenClass;
}());
var FwBrowseColumn_hidden = new FwBrowseColumn_hiddenClass();
//# sourceMappingURL=FwBrowseColumn_hidden.js.map