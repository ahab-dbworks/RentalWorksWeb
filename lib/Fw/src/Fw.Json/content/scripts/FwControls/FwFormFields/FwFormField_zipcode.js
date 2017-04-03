FwFormField_zipcode = {};
//---------------------------------------------------------------------------------
FwFormField_zipcode.renderDesignerHtml = function($control, html) {
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
FwFormField_zipcode.renderRuntimeHtml = function($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if ((typeof $control.attr('data-maxlength') !== 'undefined') && ($control.attr('data-maxlength') != '')) {
            html.push (' maxlength="' + $control.attr('data-maxlength') + '"');
        }
        html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
    $control.find('input').inputmask({mask: '99999[-9999]'});
};
//---------------------------------------------------------------------------------
FwFormField_zipcode.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_zipcode.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
    $fwformfield.find('.fwformfield-value').blur(); //MY 9/19/2014: Fix for masking display on form load.
};
//---------------------------------------------------------------------------------
FwFormField_zipcode.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_zipcode.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_zipcode.getValue2 = function($fwformfield) {
    var value;
    if ((typeof $fwformfield.attr('data-savemask') !== 'undefined') && ($fwformfield.attr('data-savemask') == 'true')) {
        value = $fwformfield.find('.fwformfield-value').val();
    } else {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        value = valuecontainer !== null ? valuecontainer : ''; //Fix for jquery.inputmask('unmaskedvalue') change to return null on empty value instead of ''
    }
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_zipcode.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
