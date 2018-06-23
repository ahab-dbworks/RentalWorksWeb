class FwBrowseColumn_checkboxClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {

    }
    //---------------------------------------------------------------------------------
    getFieldValue = function ($browse: JQuery, $tr: JQuery, $field: JQuery, field: any, originalvalue: string) {
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
        let checked = false;
        if (value === 'T' || value === 'Y' || value === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
    isModified($browse: JQuery, $tr: JQuery, $field: JQuery) {
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
    setFieldViewMode($browse: JQuery, $field: JQuery, $tr: JQuery, html: Array<string>) {
        var checked = false;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        html.push('<div class="checkboxwrapper">');
        html.push('  <input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" />');  // click events don't bubble to parent on disabled inputs unless pointer-events:none is set
        html.push('  <label></label>');
        html.push('</div>');
        let htmlString = html.join('');
        $field.html(htmlString);
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            checked = true;
        }
        $field.find('input').prop('checked', checked);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse: JQuery, $field: JQuery, $tr: JQuery, html: Array<string>) {
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
        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, originalvalue);
    }
    //---------------------------------------------------------------------------------

}

var FwBrowseColumn_checkbox = new FwBrowseColumn_checkboxClass();