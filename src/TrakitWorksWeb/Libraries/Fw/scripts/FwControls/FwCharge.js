//---------------------------------------------------------------------------------
var FwCharge = {};
//---------------------------------------------------------------------------------
FwCharge.init = function () {

};
//---------------------------------------------------------------------------------
FwCharge.renderRuntimeHtml = function($control) {
    var html, data_rendermode, appchargetemplates, activeTemplate, data_datafield, maxlength;
    data_rendermode = $control.attr('data-rendermode');
    data_datafield = $control.attr('data-datafield');
    $control.attr('data-rendermode', 'runtime');
    appchargetemplates = JSON.parse(sessionStorage.getItem('appCharge'));
    activeTemplate = appchargetemplates[0];
    for (var i = 1; i < appchargetemplates.length; i++) {
        if (Date.parse(activeTemplate.date) <= Date.parse(appchargetemplates[i].date)) {
            activeTemplate = appchargetemplates[i];
        }
    }

    $control.attr('data-template', activeTemplate.id);
    $control.css({ 'display': 'flex' });
    html = [];
    for (var i = 0; i < activeTemplate.fields.length; i++) {
        if (activeTemplate.fields[i].primary == 'T') {
            maxlength = activeTemplate.fields[i].maxlength;
            html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="' + activeTemplate.fields[i].caption + '" data-datafield="' + data_datafield + (i+1) + '" data-maxlength="' + maxlength + '" style="flex:' + maxlength + ' 0 40px;"></div>');
        }
    }

    $control.html(html.join(''));

    $control
        .on('keyup', 'input.fwformfield-value', function() {
            if (this.value.length == this.maxLength) {
                jQuery(this).closest('.fwformfield').next().find('input.fwformfield-value').focus();
            }
        })
    ;

    FwControl.renderRuntimeControls($control.find('.fwcontrol'));
};
//---------------------------------------------------------------------------------
FwCharge.rerenderRuntimeHtml = function($form, tables) {
    var html, appchargetemplates, activeTemplate, data_datafield, $fwchargefields, maxlength;

    $fwchargefields = $form.find('div.fwcharge');
    appchargetemplates = JSON.parse(sessionStorage.getItem('appCharge'));

    $fwchargefields.each(function(index, element) {
        data_datafield = jQuery(element).attr('data-datafield');

        for (var i = 0; i < appchargetemplates.length; i++) {
            if (appchargetemplates[i].id == $form.find('div[data-datafield="' + jQuery(element).attr('data-boundfield') + '"] input').val()) {
                activeTemplate = appchargetemplates[i];
            }
        }

        if (typeof activeTemplate != 'undefined') {
            jQuery(element).attr('data-template', activeTemplate.id);
            jQuery(element).css({ 'display': 'flex' });
            html = [];
            for (var i = 0; i < activeTemplate.fields.length; i++) {
                if (activeTemplate.fields[i].primary == 'T') {
                    maxlength = activeTemplate.fields[i].maxlength;
                    html.push('<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="' + activeTemplate.fields[i].caption + '" data-datafield="' + data_datafield + (i+1) + '" data-maxlength="' + maxlength + '" style="flex:' + maxlength + ' 0 40px;"></div>');
                }
            }

            jQuery(element).empty().html(html.join(''));

            FwControl.renderRuntimeControls(jQuery(element).find('.fwcontrol'));

            FwFormField.loadForm(jQuery(element).find('.fwformfield'), tables);
        }
    });
};
//---------------------------------------------------------------------------------
