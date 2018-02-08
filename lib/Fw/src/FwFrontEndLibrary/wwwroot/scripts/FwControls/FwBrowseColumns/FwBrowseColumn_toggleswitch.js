FwBrowseColumn_toggleswitch = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_toggleswitch.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_toggleswitch.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
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
FwBrowseColumn_toggleswitch.setFieldViewMode = function($browse, $field, $tr, html) {
    var checked = false;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<div class="checkboxwrapper">');
    html.push('  <label class="switch">');
    html.push('    <input type="checkbox" disabled />');
    html.push('    <span class="slider"></span>');
    html.push('  </label>')
    html.push('</div>');
    html = html.join('');
    $field.html(html);
    if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
        checked = true;
    }
    $field.find('input').prop('checked', checked);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_toggleswitch.setFieldEditMode = function($browse, $field, $tr, html) {
    var checked = false;
    var cbuniqueId    = FwApplication.prototype.uniqueId(10); 
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    html.push('<div class="checkboxwrapper">');
    html.push('  <label class="switch">');
    html.push('    <input type="checkbox" />');
    html.push('    <span class="slider"></span>');
    html.push('  </label>')
    html.push('</div>');
    html = html.join('');
    $field.html(html);
    if (originalvalue === 'T' || originalvalue === 'Y' || originalvalue === 'true') {
        checked = true;
    }
    $field.find('input').prop('checked', checked);
};
//---------------------------------------------------------------------------------
