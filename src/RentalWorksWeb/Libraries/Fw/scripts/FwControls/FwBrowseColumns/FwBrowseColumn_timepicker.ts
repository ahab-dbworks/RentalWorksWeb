class FwBrowseColumn_timepickerClass implements IFwBrowseColumn {
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
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        $field.find('input.value').val(data.value);
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
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
        $field.data('autoselect', false);
        $field.on('click', function() {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var timepickerTimeFormat, inputmaskTimeFormat;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<input id="timepicker" class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="btntime"><i class="material-icons">schedule</i></div>');
        if ($field.attr('data-timeformat') === '24') {
            timepickerTimeFormat = 'H:i:s';
            inputmaskTimeFormat = 'hh:mm';
        } else {
            timepickerTimeFormat = 'h:i A';
            inputmaskTimeFormat = 'hh:mm t';
        }
        let htmlString = html.join('');
        $field.html(htmlString);
        if ($field.attr('data-timeformat') === '24') {
            $field.find('#timepicker').clockpicker({
                autoclose: true, donetext: 'Done', afterDone: function () {
                    $field.find('input').focus();
                }
            });
        } else {
            $field.find('#timepicker').clockpicker({
                autoclose: true, twelvehour: true, donetext: 'Done', afterDone: function () {
                    $field.find('input').focus();
                }
            });
        }
        $field.find('.btntime').click(function (e) {
            e.stopPropagation();
            $field.find('#timepicker').clockpicker('show').clockpicker('toggleView', 'hours');
        });
        $field.find('input').off();
        $field.find('input.value').inputmask(inputmaskTimeFormat);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.btntime').click();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_timepicker = new FwBrowseColumn_timepickerClass();