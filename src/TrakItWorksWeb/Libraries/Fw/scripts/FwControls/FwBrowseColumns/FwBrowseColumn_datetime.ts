class FwBrowseColumn_datetimeClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')));
    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        field.value = $field.html();
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw `FwBrowseColumn_datetime.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        this.setFieldValue($browse, $tr, $field, originalvalue);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
    
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_datetime = new FwBrowseColumn_datetimeClass();