class FwBrowseColumn_textClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                if (applicationConfig.allCaps && $field.attr('data-allcaps') !== 'false' && $field.find('input.value').val()) {
                    field.value = $field.find('input.value').val().toUpperCase();
                } else {
                    field.value = $field.find('input.value').val();
                }
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
        // this only works if there is no spaces or other illegal css characters in the originalvalue
        if (typeof $field.attr('data-rowclassmapping') !== 'undefined') {
            var rowclassmapping = JSON.parse($field.attr('data-rowclassmapping'));
            if (originalvalue in rowclassmapping === true) {
                $tr.addClass(rowclassmapping[originalvalue]);
            }
        }
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        let html = [];
        html.push('<input class="value" type="text"');
        if (applicationConfig.allCaps && $field.attr('data-allcaps') !== 'false') {
            html.push(' style="text-transform:uppercase"');
        }
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (formmaxlength != '') {
            html.push(' maxlength="' + formmaxlength + '"');
        }
        html.push(' />');
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_text = new FwBrowseColumn_textClass();