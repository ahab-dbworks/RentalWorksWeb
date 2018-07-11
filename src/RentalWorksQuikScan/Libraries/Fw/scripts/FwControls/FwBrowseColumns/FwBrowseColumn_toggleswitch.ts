class FwBrowseColumn_toggleswitchClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

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
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        let checked = false;
        if (typeof data.value === 'string') {
            if (data.value.toUpperCase() === 'T' || data.value.toUpperCase() === 'Y' || data.value.toUpperCase() === 'true') {
                checked = true;
            }
        } else if (typeof data.value === 'boolean') {
            checked = data.value;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
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
    setFieldViewMode($browse, $tr, $field): void {
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" disabled />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>')
        html.push('</div>');
        let htmlString = html.join('');
        $field.html(htmlString);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var checked = false;
        var cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <label class="switch">');
        html.push('    <input type="checkbox" />');
        html.push('    <span class="slider"></span>');
        html.push('  </label>')
        html.push('</div>');
        let htmlStr = html.join('');
        $field.html(htmlStr);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        this.setFieldValue($browse, $tr, $field, { value: checked });
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_toggleswitch = new FwBrowseColumn_toggleswitchClass();