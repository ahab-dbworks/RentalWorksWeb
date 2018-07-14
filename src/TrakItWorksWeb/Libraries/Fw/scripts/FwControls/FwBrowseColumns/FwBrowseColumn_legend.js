class FwBrowseColumn_legendClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    ;
    getFieldValue($browse, $tr, $field, field, originalvalue) {
    }
    ;
    setFieldValue($browse, $tr, $field, data) {
        throw `FwBrowseColumn_legend.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    ;
    setFieldViewMode($browse, $tr, $field) {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            let color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
            $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
        }
    }
    ;
    setFieldEditMode($browse, $tr, $field) {
    }
    ;
}
var FwBrowseColumn_legend = new FwBrowseColumn_legendClass();
//# sourceMappingURL=FwBrowseColumn_legend.js.map