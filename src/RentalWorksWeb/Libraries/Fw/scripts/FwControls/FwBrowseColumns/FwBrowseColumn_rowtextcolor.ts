class FwBrowseColumn_rowtextcolorClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {

    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw `FwBrowseColumn_rowtextcolor.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        let originalValue = $field.attr('data-originalvalue');
        $tr
            .addClass('rowtextcolor')
            .css({
                'color': originalValue
            });
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {

    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_rowtextcolor = new FwBrowseColumn_rowtextcolorClass();