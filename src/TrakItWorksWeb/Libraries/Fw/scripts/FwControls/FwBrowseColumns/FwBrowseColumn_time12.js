FwBrowseColumn_time12 = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_time12.databindfield = function($browse, $field, dt, dtRow, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time12.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    $field.html(originalvalue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time12.setFieldViewMode = function($browse, $field, $tr, html) {
    var time;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    time = originalvalue.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
    if (time.length > 1) {
        time = time.slice(1);
        time[5] = +time[0] < 12 ? ' AM' : ' PM';
        time[0] = +time[0] % 12 || 12;
    }
    time = time.join('');
    $field.html(time);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_time12.setFieldEditMode = function($browse, $field, $tr, html) {
    
};
//---------------------------------------------------------------------------------
