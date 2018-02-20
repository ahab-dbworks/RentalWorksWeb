FwBrowseColumn_validation = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_validation.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    var displayFieldValue = dtRow[dt.ColumnIndex[$field.attr('data-browsedisplayfield')]];
    $field.attr('data-originaltext', displayFieldValue);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_validation.getFieldUniqueId = function ($browse, $tr, $field, uniqueid, originalvalue) {
    if ($tr.hasClass('editmode')) {
        uniqueid.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_validation.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('input.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_validation.setFieldViewMode = function ($browse, $field, $tr, html) {
    var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
    $field.html(originaltext);
};
//---------------------------------------------------------------------------------
FwBrowseColumn_validation.setFieldEditMode = function ($browse, $field, $tr, html) {
    var validationName, validationFor, $valuefield, $textfield, $btnvalidate;
    var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
    var originaltext = (typeof $field.attr('data-originaltext') === 'string') ? $field.attr('data-originaltext') : '';
    var showPeek = false;
    html.push('<input class="value" type="hidden" />');
    html.push('<input class="text" type="text"');
    if ($browse.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html.push('<div class="btnvalidate"><i class="material-icons">&#xE8B6;</i></div>');
    if (applicationConfig.defaultPeek === true) {
        showPeek = (!($field.attr('data-validationpeek') === 'false'));
    }
    else {
        showPeek = ($field.attr('data-validationpeek') === 'true');
    }
    if (showPeek) {
        html.push('<div class="btnpeek"><i class="material-icons">more_horiz</i></div>');
    }
    html = html.join('');
    $field.html(html);
    $field.find('.value').val(originalvalue);
    $field.find('.text').val(originaltext);
    FwValidation.init($field);
};
//---------------------------------------------------------------------------------
