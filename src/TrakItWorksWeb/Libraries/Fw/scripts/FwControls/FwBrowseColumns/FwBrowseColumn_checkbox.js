var FwBrowseColumn_checkboxClass = (function () {
    function FwBrowseColumn_checkboxClass() {
        this.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
            var controller = FwBrowse.getController($browse);
            if (typeof controller.apiurl !== 'undefined') {
                if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                    field.value = $field.find('input').is(':checked');
                }
            }
            else {
                if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                    field.value = ($field.find('input').is(':checked') ? 'T' : 'F');
                }
            }
        };
    }
    FwBrowseColumn_checkboxClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        var checked = false;
        if (value === 'T' || value === 'Y' || value === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    };
    FwBrowseColumn_checkboxClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var controller = FwBrowse.getController($browse);
        var originalValue = $field.attr('data-originalvalue');
        if (typeof controller.apiurl !== 'undefined') {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                var currentValue = $field.find('input').is(':checked').toString();
                isModified = currentValue !== originalValue;
            }
        }
        else {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                var currentValue = ($field.find('input').is(':checked') ? 'T' : 'F');
                isModified = currentValue !== originalValue;
            }
        }
        return isModified;
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" />');
        html.push('  <label></label>');
        html.push('</div>');
        var htmlString = html.join('');
        $field.html(htmlString);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    };
    FwBrowseColumn_checkboxClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
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
        this.setFieldValue($browse, $tr, $field, originalvalue);
    };
    return FwBrowseColumn_checkboxClass;
}());
var FwBrowseColumn_checkbox = new FwBrowseColumn_checkboxClass();
//# sourceMappingURL=FwBrowseColumn_checkbox.js.map