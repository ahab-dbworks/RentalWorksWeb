class FwBrowseColumn_rowtextcolorClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    getFieldValue($browse, $tr, $field, field, originalvalue) {
    }
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_rowtextcolor.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    setFieldViewMode($browse, $tr, $field) {
        let originalValue = $field.attr('data-originalvalue');
        $tr
            .addClass('rowtextcolor')
            .css({
            'color': originalValue
        });
    }
    setFieldEditMode($browse, $tr, $field) {
    }
}
var FwBrowseColumn_rowtextcolor = new FwBrowseColumn_rowtextcolorClass();
//# sourceMappingURL=FwBrowseColumn_rowtextcolor.js.map