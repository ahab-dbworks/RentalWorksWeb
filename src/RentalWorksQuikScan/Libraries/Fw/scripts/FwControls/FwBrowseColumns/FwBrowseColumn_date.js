class FwBrowseColumn_dateClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    ;
    setFieldValue($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
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
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    }
    setFieldEditMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<input class="value" type="text" />');
        html.push('<div class="btndate"><i class="material-icons">&#xE8DF;</i></div>');
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        $field.find('input.value').inputmask('mm/dd/yyyy');
        $field.find('input.value').datepicker({
            autoclose: true,
            format: "m/d/yyyy",
            todayHighlight: true
        }).off('focus');
        $field.on('click', '.btndate', function () {
            $field.find('input').datepicker('show');
        });
    }
}
var FwBrowseColumn_date = new FwBrowseColumn_dateClass();
//# sourceMappingURL=FwBrowseColumn_date.js.map