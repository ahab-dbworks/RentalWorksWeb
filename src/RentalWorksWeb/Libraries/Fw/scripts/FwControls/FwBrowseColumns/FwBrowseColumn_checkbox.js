var FwBrowseColumn_checkboxClass = (function () {
    function FwBrowseColumn_checkboxClass() {
        this.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                field.value = $field.find('input').is(':checked');
            }
        };
        this.getFieldValue2 = function ($browse, $tr, $field, originalvalue) {
            var isChecked = false;
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                isChecked = $field.find('input').is(':checked');
            }
            else if ($tr.hasClass('viewmode')) {
                isChecked = $field.attr('data-originalvalue').toUpperCase() === 'T' ||
                    $field.attr('data-originalvalue').toUpperCase() === 'Y' ||
                    $field.attr('data-originalvalue').toUpperCase() === 'true';
            }
            return isChecked;
        };
    }
    FwBrowseColumn_checkboxClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldValue = function ($browse, $tr, $field, isChecked) {
        var checked = false;
        if (typeof isChecked === 'string') {
            if (isChecked.toUpperCase() === 'T' || isChecked.toUpperCase() === 'Y' || isChecked.toUpperCase() === 'true') {
                checked = true;
            }
        }
        else if (typeof isChecked === 'boolean') {
            checked = isChecked;
        }
        $field.find('input').prop('checked', checked);
    };
    FwBrowseColumn_checkboxClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var controller = FwBrowse.getController($browse);
        var originalValue = $field.attr('data-originalvalue');
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            var currentValue = $field.find('input').is(':checked').toString();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var me = this;
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        var html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" />');
        html.push('  <label></label>');
        html.push('</div>');
        var $checkboxwrapper = jQuery(html.join(''));
        $checkboxwrapper.on('click', 'label', function (e) {
            try {
                e.stopPropagation();
                $field.data('data-checkthebox', !checked);
                FwBrowse.setRowEditMode($browse, $tr);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $field.empty().append($checkboxwrapper);
        $field.find('input').prop('checked', checked);
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <input id="' + cbuniqueId + '" class="value" type="checkbox"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('  <label for="' + cbuniqueId + '"></label>');
        html.push('</div>');
        var htmlString = html.join('');
        $field.html(htmlString);
        if ($field.data('data-checkthebox') === true) {
            $field.data('data-checkthebox', false);
            $field.find('input.value').prop('checked', true);
            this.setFieldValue($browse, $tr, $field, true);
        }
        else {
            this.setFieldValue($browse, $tr, $field, originalvalue);
        }
    };
    return FwBrowseColumn_checkboxClass;
}());
var FwBrowseColumn_checkbox = new FwBrowseColumn_checkboxClass();
//# sourceMappingURL=FwBrowseColumn_checkbox.js.map