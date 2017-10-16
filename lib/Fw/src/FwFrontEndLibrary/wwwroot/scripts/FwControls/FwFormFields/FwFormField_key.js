FwFormField_key = {};
//---------------------------------------------------------------------------------
FwFormField_key.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_key.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" ');
        if ($control.attr('data-originalvalue') != '') {
            html.push('value="' + $control.attr('data-originalvalue') + '"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_key.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_key.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_key.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_key.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_key.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_key.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
