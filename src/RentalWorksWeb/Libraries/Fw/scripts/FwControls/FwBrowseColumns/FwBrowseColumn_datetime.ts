﻿class FwBrowseColumn_datetimeClass {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr) {
        $field.attr('data-originalvalue', new Date($field.attr('data-originalvalue')));
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
        var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
        $field.html(originalvalue);
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $field, $tr, html) {
    
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_datetime = new FwBrowseColumn_datetimeClass();