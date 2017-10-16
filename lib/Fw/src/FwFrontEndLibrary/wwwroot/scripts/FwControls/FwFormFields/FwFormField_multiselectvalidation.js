FwFormField_multiselectvalidation = {};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-value" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<img class="btnvalidate" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/browsesearch.001.png" />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.renderRuntimeHtml = function($control, html) {
    var validationName, $valuefield, $searchfield, $btnvalidate;

    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text" readonly="true"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="add-on"><div class="icon-validation"></div></div>');
    html.push('</div>');
    $control.html(html.join(''));
    validationName = $control.attr('data-validationname');
    $valuefield = $control.find('> .fwformfield-control > .fwformfield-value');
    $searchfield = $control.find('> .fwformfield-control > .fwformfield-text');
    $btnvalidate = $control.find('> .fwformfield-control > .add-on');
    FwMultiSelectValidation.init($control, validationName, $valuefield, $searchfield, $btnvalidate);
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('input.fwformfield-value')
            .val(value);
    $fwformfield.find('.fwformfield-text')
        .val(text);
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.disable = function($control) {
    $control.find('.btnvalidate').attr('data-enabled', 'false');
    $control.find('.fwformfield-text').prop('disabled', true);
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.enable = function($control) {
    $control.find('.btnvalidate').attr('data-enabled', 'true');
    $control.find('.fwformfield-text').prop('disabled', false);
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_multiselectvalidation.setValue = function($fwformfield, value, text, firechangeevent) {
    // this is really only useful for clearing the value, otherwise it will be out of sync with the selected rows
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    var $inputtext  = $fwformfield.find('.fwformfield-text');
    $inputtext.val(text);
    $inputvalue.val(value);
    $fwformfield.data('browse').removeData('selectedrows');
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
