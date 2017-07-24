FwFormField_checkbox = {};
//---------------------------------------------------------------------------------
FwFormField_checkbox.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
    html.push('<input class="fwformfield-value" type="checkbox"');
    if ($control.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (typeof $control.attr('data-name') !== 'undefined') {
        html.push(' name="' + $control.attr('data-name') + '"');
    }
    html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_checkbox.renderRuntimeHtml = function($control, html) {
    var uniqueId = FwApplication.prototype.uniqueId(10);
    html.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox"');
    if ($control.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (typeof $control.attr('data-name') !== 'undefined') {
        html.push(' name="' + $control.attr('data-name') + '"');
    }
    html.push(' />');
    html.push('<label class="fwformfield-caption" for="' + uniqueId + '">' + $control.attr('data-caption') + '</label>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_checkbox.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_checkbox.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield.attr('data-originalvalue', ((value === 'T') ? 'T' : 'F'));
    if (value === 'T' || value) {
        $fwformfield.find('input[type="checkbox"]').prop('checked', true);
    }
};
//---------------------------------------------------------------------------------
FwFormField_checkbox.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_checkbox.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_checkbox.getValue2 = function($fwformfield) {
    var controller = window[$fwformfield.closest('#moduleMaster').attr('data-module')];
    var value;
    if (typeof controller.apiurl === 'string' && controller.apiurl.length > 0) {
        value = $fwformfield.find('input').is(':checked');
    } else {
        value = $fwformfield.find('input').is(':checked') ? 'T' : 'F';
    }
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_checkbox.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('input');
    $inputvalue.prop('checked', value);
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
