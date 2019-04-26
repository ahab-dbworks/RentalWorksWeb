class FwBrowseColumn_validationClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
        $field.attr('data-originaltext', displayFieldValue);
    }
    //---------------------------------------------------------------------------------
    getFieldUniqueId($browse, $tr, $field, uniqueid, originalvalue): void {
        if ($tr.hasClass('editmode')) {
            uniqueid.value = $field.find('input.value').val();
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            if (applicationConfig.allCaps && $field.attr('data-allcaps') !== 'false' && $field.find('input.value').val()) {
                field.value = $field.find('input.value').val().toUpperCase();
            } else {
                field.value = $field.find('input.value').val();
            }
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        $field.find('.value').val(data.value);
        $field.find('.text').val(data.text);
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
        // push hidden spinner
        html.push('<div class="sk-fading-circle validation-loader"><div class="sk-circle1 sk-circle"></div><div class="sk-circle2 sk-circle"></div><div class="sk-circle3 sk-circle"></div><div class="sk-circle4 sk-circle"></div><div class="sk-circle5 sk-circle"></div><div class="sk-circle6 sk-circle"></div><div class="sk-circle7 sk-circle"></div><div class="sk-circle8 sk-circle"></div><div class="sk-circle9 sk-circle"></div><div class="sk-circle10 sk-circle"></div><div class="sk-circle11 sk-circle"></div><div class="sk-circle12 sk-circle"></div></div>');

        let htmlString = html.join('');
        $field.html(originaltext + htmlString);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var showPeek = false;
        let html = [];
        html.push('<input class="value" type="hidden" />');
        html.push('<input class="text" type="text"');
        if (applicationConfig.allCaps) {
            html.push(' style="text-transform:uppercase"');
        }
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="btnvalidate"><i class="material-icons">&#xE8B6;</i></div>');
        // push hidden spinner
        html.push('<div class="sk-fading-circle validation-loader"><div class="sk-circle1 sk-circle"></div><div class="sk-circle2 sk-circle"></div><div class="sk-circle3 sk-circle"></div><div class="sk-circle4 sk-circle"></div><div class="sk-circle5 sk-circle"></div><div class="sk-circle6 sk-circle"></div><div class="sk-circle7 sk-circle"></div><div class="sk-circle8 sk-circle"></div><div class="sk-circle9 sk-circle"></div><div class="sk-circle10 sk-circle"></div><div class="sk-circle11 sk-circle"></div><div class="sk-circle12 sk-circle"></div></div>');

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
            //$field.find('.btnvalidate').click();
        }
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_validation = new FwBrowseColumn_validationClass();