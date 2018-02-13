FwFormField_datetime = {};
//---------------------------------------------------------------------------------
FwFormField_datetime.renderDesignerHtml = function($control, html) {
    
};
//---------------------------------------------------------------------------------
FwFormField_datetime.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_datetime.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_datetime.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_datetime.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_datetime.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_datetime.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_datetime.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
