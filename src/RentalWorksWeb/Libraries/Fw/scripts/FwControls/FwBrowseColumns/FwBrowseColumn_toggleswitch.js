var FwBrowseColumn_toggleswitchClass = (function () {
    function FwBrowseColumn_toggleswitchClass() {
    }
    FwBrowseColumn_toggleswitchClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    FwBrowseColumn_toggleswitchClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
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
    FwBrowseColumn_toggleswitchClass.prototype.setFieldValue = function ($browse, $tr, $field, value) {
        throw 'Not Implemented!';
    };
    FwBrowseColumn_toggleswitchClass.prototype.isModified = function ($browse, $tr, $field) {
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
    FwBrowseColumn_toggleswitchClass.prototype.setFieldViewMode = function ($browse, $field, $tr, html) {
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" disabled />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>');
        html.push('</div>');
        html = html.join('');
        $field.html(html);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    };
    FwBrowseColumn_toggleswitchClass.prototype.setFieldEditMode = function ($browse, $field, $tr, html) {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>');
        html.push('</div>');
        html = html.join('');
        $field.html(html);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    };
    return FwBrowseColumn_toggleswitchClass;
}());
var FwBrowseColumn_toggleswitch = new FwBrowseColumn_toggleswitchClass();
//# sourceMappingURL=FwBrowseColumn_toggleswitch.js.map