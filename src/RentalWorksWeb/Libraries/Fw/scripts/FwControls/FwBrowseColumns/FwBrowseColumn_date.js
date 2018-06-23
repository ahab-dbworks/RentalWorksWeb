var FwBrowseColumn_dateClass = (function () {
    function FwBrowseColumn_dateClass() {
    }
    FwBrowseColumn_dateClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_dateClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    ;
    FwBrowseColumn_dateClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        $field.find('input.value').val(value);
    };
    FwBrowseColumn_dateClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_dateClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    };
    FwBrowseColumn_dateClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<input class="value" type="text" />');
        html.push('<div class="btndate"><i class="material-icons">&#xE8DF;</i></div>');
        html = html.join('');
        $field.html(html);
        this.setFieldValue($browse, $tr, $field, originalvalue);
        $field.find('input.value').inputmask('mm/dd/yyyy');
        $field.find('input.value').datepicker({
            autoclose: true,
            format: "m/d/yyyy",
            todayHighlight: true
        }).off('focus');
        $field.on('click', '.btndate', function () {
            $field.find('input').datepicker('show');
        });
    };
    return FwBrowseColumn_dateClass;
}());
var FwBrowseColumn_date = new FwBrowseColumn_dateClass();
//# sourceMappingURL=FwBrowseColumn_date.js.map