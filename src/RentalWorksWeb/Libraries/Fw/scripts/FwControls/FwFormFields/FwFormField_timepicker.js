FwFormField_timepicker = {};
//---------------------------------------------------------------------------------
FwFormField_timepicker.renderDesignerHtml = function($control, html) {
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
FwFormField_timepicker.renderRuntimeHtml = function($control, html) {
    var timepickerTimeFormat, inputmaskTimeFormat;
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" id="timepicker" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons btntime">schedule</i>');
    html.push('</div>');
    $control.html(html.join(''));
    if ($control.attr('data-timeformat') === '24') {
        $control.find('#timepicker').clockpicker({ autoclose: true, donetext: 'Done' });
    } else {
        $control.find('#timepicker').clockpicker({autoclose: true, twelvehour: true, donetext: 'Done'});
    }
    $control.find('.btntime').click(function (e) {
        e.stopPropagation();
        $control.find('#timepicker').clockpicker('show').clockpicker('toggleView', 'hours');
    });
};
//---------------------------------------------------------------------------------
FwFormField_timepicker.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_timepicker.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_timepicker.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_timepicker.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_timepicker.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    $fwformfield.closest('.fwform').find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
    $fwformfield.closest('.fwform').attr('data-modified', 'true');
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_timepicker.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
