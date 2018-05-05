﻿FwFormField_time = {};
//---------------------------------------------------------------------------------
FwFormField_time.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
    html.push('<input class="fwformfield-value" type="text"');
    if ($control.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_time.renderRuntimeHtml = function($control, html) {
    var timepickerTimeFormat, inputmaskTimeFormat;
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
    if ($control.attr('data-timeformat') === '24') {
        timepickerTimeFormat = 'H:i:s';
        inputmaskTimeFormat  = 'hh:mm';
    } else {
        timepickerTimeFormat = 'h:i A';
        inputmaskTimeFormat  = 'hh:mm t';
    }
    $control.find('.fwformfield-value').inputmask(inputmaskTimeFormat);
};
//---------------------------------------------------------------------------------
FwFormField_time.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_time.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_time.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_time.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_time.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_time.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
