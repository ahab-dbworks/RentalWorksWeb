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
    FwBrowseColumn_dateClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('input.value').val(data.value);
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
    FwBrowseColumn_dateClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        $field.data('clickedInViewMode', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('clickedInViewMode', true);
            }
        });
    };
    FwBrowseColumn_dateClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var html = [];
        html.push('<input class="value" type="text" />');
        html.push('<div class="btndate"><i class="material-icons">&#xE8DF;</i></div>');
        var htmlString = html.join('');
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
        if ($field.data('clickedInViewMode') === true) {
            $field.data('clickedInViewMode', false);
            $field.find('.btndate').click();
        }
    };
    return FwBrowseColumn_dateClass;
}());
var FwBrowseColumn_date = new FwBrowseColumn_dateClass();
//# sourceMappingURL=FwBrowseColumn_date.js.map