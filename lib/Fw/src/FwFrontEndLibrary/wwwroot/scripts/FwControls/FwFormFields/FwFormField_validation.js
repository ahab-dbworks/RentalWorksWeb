FwFormField_validation = {};
//---------------------------------------------------------------------------------
FwFormField_validation.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<img class="btnvalidate" src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/browsesearch.001.png" />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_validation.renderRuntimeHtml = function ($control, html) {
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
    html.push('<input class="fwformfield-value" type="hidden" />');
    html.push('<input class="fwformfield-text" type="text" autocapitalize="none"');
    if ($control.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (typeof $control.attr('data-placeholder') !== 'undefined') {
        html.push(' placeholder="' + $control.attr('data-placeholder') + '"');
    }
    if ((sessionStorage.getItem('applicationsettings') != null) && (typeof JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase != 'undefined') && (JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase)) {
        html.push(' style="text-transform:uppercase"');
    }
    html.push(' />');
    html.push('<i class="material-icons btnvalidate">search</i>');

    //if ($control.attr('data-validationpeek') === 'true') {
    //    html.push('<div class="btnpeek"><i class="material-icons">more_horiz</i></div>')
    //}

    //ow 02/01/2018 reversed logic back
    //jh 01/28/2018 fixed reversed logic
    //html.push('<div class="btnpeek"')
    //if ($control.attr('data-validationpeek') === 'false') {
    //    html.push(' style="display:none"')
    //}
    //html.push('><i class="material-icons">more_horiz</i></div>')

    //jh 02/02/2018 uses applicationConfig.defaultPeek to determine whether or not to show the "..." by default.  Dev can override per control
    showPeek = false;
    if (applicationConfig.defaultPeek) {
        showPeek = (!($control.attr('data-validationpeek') === 'false'));
    }
    else {
        showPeek = ($control.attr('data-validationpeek') === 'true');
    }
    if (showPeek) {
        html.push('<i class="material-icons btnpeek">more_horiz</i>')
    } 

    html.push('</div>');
    $control.html(html.join(''));
    FwValidation.init($control);
};
//---------------------------------------------------------------------------------
FwFormField_validation.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_validation.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('input.fwformfield-value')
            .val(value);
    $fwformfield.find('.fwformfield-text')
        .val(text);
};
//---------------------------------------------------------------------------------
FwFormField_validation.disable = function($control) {
    $control.find('.btnvalidate').attr('data-enabled', 'false');
    $control.find('.fwformfield-text').prop('disabled', true);
};
//---------------------------------------------------------------------------------
FwFormField_validation.enable = function($control) {
    $control.find('.btnvalidate').attr('data-enabled', 'true');
    $control.find('.fwformfield-text').prop('disabled', false);
};
//---------------------------------------------------------------------------------
FwFormField_validation.getValue2 = function($fwformfield) {
    var value = $fwformfield.find('.fwformfield-value').val();
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_validation.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    var $inputtext  = $fwformfield.find('.fwformfield-text');
    $inputtext.val(text);
    $inputvalue.val(value);
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
