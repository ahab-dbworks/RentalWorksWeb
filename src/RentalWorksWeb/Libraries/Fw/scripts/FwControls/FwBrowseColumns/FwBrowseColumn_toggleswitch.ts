class FwBrowseColumn_toggleswitchClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        var controller = FwBrowse.getController($browse);
        if (typeof controller.apiurl !== 'undefined') {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                field.value = $field.find('input').is(':checked');
            }
        } else {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                field.value = ($field.find('input').is(':checked') ? 'T' : 'F');
            }
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, value: string) {
        throw 'Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field) {
        var isModified = false;
        var controller = FwBrowse.getController($browse);
        let originalValue = $field.attr('data-originalvalue');
        if (typeof controller.apiurl !== 'undefined') {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                let currentValue = $field.find('input').is(':checked').toString();
                isModified = currentValue !== originalValue;
            }
        } else {
            if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
                let currentValue = ($field.find('input').is(':checked') ? 'T' : 'F');
                isModified = currentValue !== originalValue;
            }
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $field, $tr, html) {
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" disabled />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>')
        html.push('</div>');
        html = html.join('');
        $field.html(html);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $field, $tr, html) {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>')
        html.push('</div>');
        html = html.join('');
        $field.html(html);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_toggleswitch = new FwBrowseColumn_toggleswitchClass();