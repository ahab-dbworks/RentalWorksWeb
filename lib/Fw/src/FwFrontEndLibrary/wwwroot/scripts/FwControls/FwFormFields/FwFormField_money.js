FwFormField_money = {};
//---------------------------------------------------------------------------------
FwFormField_money.renderDesignerHtml = function($control, html) {
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
FwFormField_money.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
    $control.find('.fwformfield-value').inputmask("currency");
};
//---------------------------------------------------------------------------------
FwFormField_money.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_money.loadForm = function($fwformfield, table, field, value, text) {
    value = ((value === '') ? '0.00' : value);
    $fwformfield
        .attr('data-originalvalue', parseFloat(value).toFixed(2))
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_money.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_money.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_money.getValue2 = function($fwformfield) {
    var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
    var value = valuecontainer !== null ? valuecontainer : ''; //Fix for jquery.inputmask('unmaskedvalue') change to return null on empty value instead of ''
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_money.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
