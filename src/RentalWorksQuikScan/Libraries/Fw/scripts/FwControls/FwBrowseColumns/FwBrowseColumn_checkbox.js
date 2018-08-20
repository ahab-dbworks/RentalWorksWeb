class FwBrowseColumn_checkboxClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            field.value = $field.find('input').is(':checked');
        }
    }
    getFieldValue2($browse, $tr, $field, originalvalue) {
        let isChecked = false;
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            isChecked = $field.find('input').is(':checked');
        }
        else if ($tr.hasClass('viewmode')) {
            isChecked = $field.attr('data-originalvalue').toUpperCase() === 'T' ||
                $field.attr('data-originalvalue').toUpperCase() === 'Y' ||
                $field.attr('data-originalvalue').toUpperCase() === 'true';
        }
        return isChecked;
    }
    setFieldValue($browse, $tr, $field, data) {
        let checked = false;
        if (typeof data.value === 'string') {
            if (data.value.toUpperCase() === 'T' || data.value.toUpperCase() === 'Y' || data.value.toUpperCase() === 'true') {
                checked = true;
            }
        }
        else if (typeof data.value === 'boolean') {
            checked = data.value;
        }
        $field.find('input').prop('checked', checked);
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        var controller = FwBrowse.getController($browse);
        let originalValue = $field.attr('data-originalvalue');
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            let currentValue = $field.find('input').is(':checked').toString();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        var me = this;
        $field.data('checkthebox', false);
        var originalCheckedValue = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            originalCheckedValue = true;
        }
        let html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" />');
        html.push('  <label></label>');
        html.push('</div>');
        let $checkboxwrapper = jQuery(html.join(''));
        $checkboxwrapper.on('click', 'label', function (e) {
            try {
                e.stopPropagation();
                $field.data('checkthebox', !originalCheckedValue);
                FwBrowse.setRowEditMode($browse, $tr);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $field.empty().append($checkboxwrapper);
        this.setFieldValue($browse, $tr, $field, { value: originalCheckedValue });
    }
    setFieldEditMode($browse, $tr, $field) {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <input id="' + cbuniqueId + '" class="value" type="checkbox"');
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('  <label for="' + cbuniqueId + '"></label>');
        html.push('</div>');
        let htmlString = html.join('');
        $field.html(htmlString);
        if ($field.data('checkthebox') === true) {
            $field.data('checkthebox', false);
            $field.find('input.value').prop('checked', true);
            this.setFieldValue($browse, $tr, $field, { value: true });
        }
        else {
            this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        }
    }
}
var FwBrowseColumn_checkbox = new FwBrowseColumn_checkboxClass();
//# sourceMappingURL=FwBrowseColumn_checkbox.js.map