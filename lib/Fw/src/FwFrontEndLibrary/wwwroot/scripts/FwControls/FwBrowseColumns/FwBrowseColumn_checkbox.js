FwBrowseColumn_checkbox = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_checkbox.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_checkbox.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
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
};
//---------------------------------------------------------------------------------
FwBrowseColumn_checkbox.setFieldViewMode = function($browse, $field, $tr, html) {
    var checked = false;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="checkbox" disabled="disabled" style="box-sizing:border-box;" />');
    html = html.join('');
    $field.html(html);
    if ((originalvalue == 'T') || (originalvalue == 'Y')) {
        checked = true;
    }
    $field.find('input').prop('checked', checked);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_checkbox.setFieldEditMode = function($browse, $field, $tr, html) {
    var checked = false;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<input class="value" type="checkbox"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html = html.join('');
    $field.html(html);
    if ((originalvalue == 'T') || (originalvalue == 'Y')) {
        checked = true;
    }
    $field.find('input').prop('checked', checked);
};
//---------------------------------------------------------------------------------
