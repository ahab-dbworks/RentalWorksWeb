class FwBrowseColumn_numberClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {
    
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, value: string) {
        throw 'Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field) {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(`<div class="fieldvalue">${originalvalue}</div>`);

        $field.find('.fieldvalue').inputmask("numeric", {
            min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
            max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
            digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
            digitsOptional: ((typeof $field.attr('data-digits') !== 'undefined') ? false : true),
            radixPoint: '.',
            groupSeparator: ',',
            autoGroup: (((typeof $field.attr('data-formatnumeric') !== 'undefined') && ($field.attr('data-formatnumeric') == 'true')) ? true : false)
        });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $field, $tr, html) {
        var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $browse.attr('data-minvalue') !== 'undefined') {
            html.push(' min="'+ $browse.attr('data-minvalue') + '"');
        }
        if (typeof $browse.attr('data-maxvalue') !== 'undefined') {
            html.push(' max="'+ $browse.attr('data-maxvalue') + '"');
        }
        html.push(' />');
        html = html.join('');
        $field.html(html);
        $field.find('input.value').val(originalvalue);
        $field.find('input.value').inputmask("numeric", {
            //placeholder: '0',
            min:            ((typeof $browse.attr('data-minvalue') !== 'undefined') ? $browse.attr('data-minvalue') : undefined),
            max:            ((typeof $browse.attr('data-maxvalue') !== 'undefined') ? $browse.attr('data-maxvalue') : undefined),
            digits:         ((typeof $browse.attr('data-digits') !== 'undefined') ? $browse.attr('data-digits') : 2),
            radixPoint:     '.',
            groupSeparator: ',',
            autoGroup:      (((typeof $browse.attr('data-formatnumeric') !== 'undefined') && ($browse.attr('data-formatnumeric') == 'true')) ? true : false)
        });
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_number = new FwBrowseColumn_numberClass();