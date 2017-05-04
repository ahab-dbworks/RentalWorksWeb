FwFormField_percent = {};
//---------------------------------------------------------------------------------
FwFormField_percent.renderDesignerHtml = function($control, html) {
    
};
//---------------------------------------------------------------------------------
FwFormField_percent.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
    $control.find('input').inputmask({alias: 'numeric', suffix: ' %', digits: 2});
};
//---------------------------------------------------------------------------------
FwFormField_percent.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_percent.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_percent.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_percent.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_percent.getValue2 = function($fwformfield) {
    var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
    var value = valuecontainer !== null ? valuecontainer : ''; //Fix for jquery.inputmask('unmaskedvalue') change to return null on empty value instead of ''
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_percent.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
