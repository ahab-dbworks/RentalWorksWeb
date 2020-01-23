class FwBrowseColumn_checkboxClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, field: any, originalvalue: string): void {
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            field.value = $field.find('input').is(':checked');
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue2($browse: JQuery, $tr: JQuery, $field: JQuery, originalvalue: string): boolean {
        let isChecked = false;
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            isChecked = $field.find('input').is(':checked');
        } else if ($tr.hasClass('viewmode')) {
            isChecked = $field.attr('data-originalvalue').toUpperCase() === 'T' ||
                $field.attr('data-originalvalue').toUpperCase() === 'Y' ||
                $field.attr('data-originalvalue').toUpperCase() === 'true';
        }
        return isChecked;
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
    isModified($browse: JQuery, $tr: JQuery, $field: JQuery): boolean {
        let isModified = false;
        const controller = FwBrowse.getController($browse);
        const originalValue = $field.attr('data-originalvalue');
        if ($tr.hasClass('editmode') || $tr.hasClass('newmode')) {
            const currentValue = $field.find('input').is(':checked').toString();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse: JQuery, $tr: JQuery, $field: JQuery): void {
        let originalCheckedValue = false;
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
            originalCheckedValue = true;
        }
        $field.data('checkthebox', originalCheckedValue);
        const html = [];
        html.push('<div class="checkboxwrapper">');
        html.push('  <input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" />');  // click events don't bubble to parent on disabled inputs unless pointer-events:none is set
        html.push('  <label></label>');
        html.push('</div>');
        const $checkboxwrapper = jQuery(html.join(''));
        $checkboxwrapper.on('click', 'label', function (e: JQuery.Event) {
            try {
                e.stopPropagation();
                $field.data('checkthebox', !originalCheckedValue);
                FwBrowse.setRowEditMode($browse, $tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $field.empty().append($checkboxwrapper);
        this.setFieldValue($browse, $tr, $field, { value: originalCheckedValue });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse: JQuery, $tr: JQuery, $field: JQuery): void {
        const cbuniqueId = FwApplication.prototype.uniqueId(10);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        const html = [];
        html.push('<div class="checkboxwrapper">');
        html.push(`  <input id="${cbuniqueId}" class="value" type="checkbox"`);
        if ($browse.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push(`  <label for="${cbuniqueId}"></label>`);
        html.push('</div>');
        $field.html(html.join(''));
        this.setFieldValue($browse, $tr, $field, { value: $field.data('checkthebox') });
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_checkbox = new FwBrowseColumn_checkboxClass();