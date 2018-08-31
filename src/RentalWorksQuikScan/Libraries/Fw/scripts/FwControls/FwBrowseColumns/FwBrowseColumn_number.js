var FwBrowseColumn_numberClass = (function () {
    function FwBrowseColumn_numberClass() {
    }
    FwBrowseColumn_numberClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_numberClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_numberClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        if ($field.attr('data-formreadonly') === 'true') {
            $field.find('.fieldvalue').val(data.value);
        }
        else {
            $field.find('input.value').val(data.value);
        }
    };
    FwBrowseColumn_numberClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_numberClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html("<div class=\"fieldvalue\">" + originalvalue + "</div>");
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
    };
    FwBrowseColumn_numberClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var html = [];
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
        var htmlString = html.join('');
        $field.html(htmlString);
        $field.find('input.value').inputmask("numeric", {
            min: ((typeof $browse.attr('data-minvalue') !== 'undefined') ? $browse.attr('data-minvalue') : undefined),
            max: ((typeof $browse.attr('data-maxvalue') !== 'undefined') ? $browse.attr('data-maxvalue') : undefined),
            digits: ((typeof $browse.attr('data-digits') !== 'undefined') ? $browse.attr('data-digits') : 2),
            radixPoint: '.',
            groupSeparator: ',',
            autoGroup: (((typeof $browse.attr('data-formatnumeric') !== 'undefined') && ($browse.attr('data-formatnumeric') == 'true')) ? true : false)
        });
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }
    };
    return FwBrowseColumn_numberClass;
}());
var FwBrowseColumn_number = new FwBrowseColumn_numberClass();
//# sourceMappingURL=FwBrowseColumn_number.js.map