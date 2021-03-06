class FwBrowseColumn_numberClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($browse: JQuery, $tr: JQuery, $field: JQuery) {
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(`<div class="fieldvalue">${originalvalue}</div>`);
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {
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
        if ($field.attr('data-formreadonly') === 'true') {
            $field.find('.fieldvalue').text(data.value);
        } else {
            $field.find('input.value').val(data.value);
        }
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        let isModified = false;
        const originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            const currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(`<div class="fieldvalue">${originalvalue}</div>`);
        $field.data('autoselect', false);
        $field.find('.fieldvalue').inputmask("numeric", {
            min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
            max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
            digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
            digitsOptional: ((typeof $field.attr('data-digits') !== 'undefined') ? false : true),
            radixPoint: '.',
            groupSeparator: ',',
            autoGroup: (((typeof $field.attr('data-formatnumeric') !== 'undefined') && ($field.attr('data-formatnumeric') == 'true')) ? true : false)
        });
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
        //var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        //originalvalue = (<any>window).numberWithCommas(parseInt(originalvalue));
        //$field.html(`<div class="fieldvalue">${originalvalue}</div>`);

        //$field.data('autoselect', false);
        ////$field.find('.fieldvalue').inputmask("numeric", {
        ////    min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
        ////    max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
        ////    digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
        ////    digitsOptional: ((typeof $field.attr('data-digits') !== 'undefined') ? false : true),
        ////    radixPoint: '.',
        ////    groupSeparator: ',',
        ////    autoGroup: (((typeof $field.attr('data-formatnumeric') !== 'undefined') && ($field.attr('data-formatnumeric') == 'true')) ? true : false)
        ////});

        //$field.on('click', function() {
        //    if ($field.attr('data-formreadonly') !== 'true') {
        //        $field.data('autoselect', true);
        //    }
        //});
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        const html: Array<string> = [];
        html.push('<input class="value" type="text"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $browse.attr('data-minvalue') !== 'undefined') {
            html.push(' min="' + $browse.attr('data-minvalue') + '"');
        }
        if (typeof $browse.attr('data-maxvalue') !== 'undefined') {
            html.push(' max="' + $browse.attr('data-maxvalue') + '"');
        }
        html.push(' />');
        $field.html(html.join(''));
        $field.find('input.value').inputmask("numeric", {
            min: ((typeof $field.attr('data-minvalue') !== 'undefined') ? $field.attr('data-minvalue') : undefined),
            max: ((typeof $field.attr('data-maxvalue') !== 'undefined') ? $field.attr('data-maxvalue') : undefined),
            digits: ((typeof $field.attr('data-digits') !== 'undefined') ? $field.attr('data-digits') : 2),
            radixPoint: '.',
            groupSeparator: ',',
            autoGroup: (((typeof $field.attr('data-formatnumeric') !== 'undefined') && ($field.attr('data-formatnumeric') == 'true')) ? true : false)
        });
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_number = new FwBrowseColumn_numberClass();