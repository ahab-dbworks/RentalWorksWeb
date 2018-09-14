var FwBrowseColumn_validationClass = (function () {
    function FwBrowseColumn_validationClass() {
    }
    FwBrowseColumn_validationClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
        var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
        $field.attr('data-originaltext', displayFieldValue);
    };
    FwBrowseColumn_validationClass.prototype.getFieldUniqueId = function ($browse, $tr, $field, uniqueid, originalvalue) {
        if ($tr.hasClass('editmode')) {
            uniqueid.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_validationClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('input.value').val();
        }
    };
    FwBrowseColumn_validationClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('.value').val(data.value);
        $field.find('.text').val(data.text);
    };
    FwBrowseColumn_validationClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_validationClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        $field.data('autoselect', false);
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var showPeek = false;
        var html = [];
        if (applicationConfig.defaultPeek === true) {
            showPeek = (!($field.attr('data-validationpeek') === 'false'));
        }
        else {
            showPeek = ($field.attr('data-validationpeek') === 'true');
        }
        if (showPeek) {
            html.push('<div class="btnpeek"><i class="material-icons">more_horiz</i></div>');
        }
        var htmlString = html.join('');
        $field.html(originaltext + htmlString);
        $field.on('click', function () {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
        });
    };
    FwBrowseColumn_validationClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
        var showPeek = false;
        var html = [];
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
        var htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue, text: originaltext });
        FwValidation.init($field);
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.text').select();
        }
    };
    ;
    return FwBrowseColumn_validationClass;
}());
var FwBrowseColumn_validation = new FwBrowseColumn_validationClass();
//# sourceMappingURL=FwBrowseColumn_validation.js.map