class FwBrowseColumn_hiddenClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    ;
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    ;
    setFieldValue($browse, $tr, $field, data) {
        $field.html(data.value);
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    ;
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
    }
    ;
    setFieldEditMode($browse, $tr, $field) {
    }
    ;
}
var FwBrowseColumn_hidden = new FwBrowseColumn_hiddenClass();
//# sourceMappingURL=FwBrowseColumn_hidden.js.map