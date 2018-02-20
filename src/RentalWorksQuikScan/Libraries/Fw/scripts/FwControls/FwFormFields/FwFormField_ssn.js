FwFormField_ssn = {};
//---------------------------------------------------------------------------------
FwFormField_ssn.renderDesignerHtml = function($control, html) {
    
};
//---------------------------------------------------------------------------------
FwFormField_ssn.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
    $control.find('input').inputmask({mask: '999-99-9999'});
};
//---------------------------------------------------------------------------------
FwFormField_ssn.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_ssn.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_ssn.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_ssn.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_ssn.getValue2 = function($fwformfield) {
    var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
    var value = valuecontainer !== null ? valuecontainer : ''; //Fix for jquery.inputmask('unmaskedvalue') change to return null on empty value instead of ''
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_ssn.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
