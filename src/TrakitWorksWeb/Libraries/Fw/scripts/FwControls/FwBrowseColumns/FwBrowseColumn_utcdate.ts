class FwBrowseColumn_utcdateClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        if ($field.attr('data-originalvalue') !== '') {
            $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')).toLocaleDateString());
        }
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {

    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw `FwBrowseColumn_utcdate.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {

    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_utcdate = new FwBrowseColumn_utcdateClass();