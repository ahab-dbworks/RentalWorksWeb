class FwBrowseColumn_validationClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
        var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
        $field.attr('data-originaltext', displayFieldValue);
    }
    getFieldUniqueId($browse, $tr, $field, uniqueid, originalvalue) {
        if ($tr.hasClass('editmode')) {
            uniqueid.value = $field.find('input.value').val();
        }
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    setFieldValue($browse, $tr, $field, data) {
        $field.find('.value').val(data.value);
        $field.find('.text').val(data.text);
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
        $field.data('autoselect', false);
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var showPeek = false;
        let html = [];
        if (applicationConfig.defaultPeek === true) {
            showPeek = (!($field.attr('data-validationpeek') === 'false'));
        }
        else {
            showPeek = ($field.attr('data-validationpeek') === 'true');
        }
        if (showPeek) {
            html.push('<div class="btnpeek"><i class="material-icons">more_horiz</i></div>');
        }
        let htmlString = html.join('');
        $field.html(originaltext + htmlString);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    setFieldEditMode($browse, $tr, $field) {
        var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var showPeek = false;
        let html = [];
        html.push('<input class="value" type="hidden" />');
        html.push('<input class="text" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="btnvalidate"><i class="material-icons">&#xE8B6;</i></div>');
        if (applicationConfig.defaultPeek === true) {
            showPeek = (!($field.attr('data-validationpeek') === 'false'));
        }
        else {
            showPeek = ($field.attr('data-validationpeek') === 'true');
        }
        if (showPeek) {
            html.push('<div class="btnpeek"><i class="material-icons">more_horiz</i></div>');
        }
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue, text: originaltext });
        FwValidation.init($field);
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.text').select();
        }
    }
    ;
}
var FwBrowseColumn_validation = new FwBrowseColumn_validationClass();
//# sourceMappingURL=FwBrowseColumn_validation.js.map