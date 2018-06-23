class FwBrowseColumn_rowtextcolorClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue) {

    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, value: string) {
        throw 'Not Implemented!';
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field) {
        var isModified = false;
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $field, $tr, html) {
        $tr
            .addClass('rowtextcolor')
            .css({
                'color': $field.attr('data-originalvalue')
            })
            ;
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $field, $tr, html) {

    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_rowtextcolor = new FwBrowseColumn_rowtextcolorClass();