class FwBrowseColumn_dateClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                field.value = $field.find('input.value').val();
            } else {
                field.value = originalvalue;
            }
        }
    };
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        if ($field.attr('data-formreadonly') === 'true') {
            $field.find('.fieldvalue').text(data.value);
        } else {
            $field.find('input.value').val(data.value);
        }
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
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
            todayHighlight: true,
            weekStart: FwFunc.getWeekStartInt(),
        }).off('focus');
        $field.on('click', '.btndate', function () {
            $field.find('input').datepicker('show');
        });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.btndate').click();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_date = new FwBrowseColumn_dateClass();