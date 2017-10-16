FwBrowseColumn_note = {};
//---------------------------------------------------------------------------------
FwBrowseColumn_note.databindfield = function($browse, $field, dt, dtRow, field, $tr) {
    
};
//---------------------------------------------------------------------------------
FwBrowseColumn_note.getFieldValue = function($browse, $tr, $field, field, originalvalue) {
    if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
        field.value = $field.find('textarea.value').val();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_note.setFieldViewMode = function($browse, $field, $tr, html) {
    var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
    var originalvalue  = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    if (originalvalue !== '') {
        $noteImage = jQuery('<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/16/fileextension-document.png" style="cursor:pointer;" />');
        $noteImage.on('click', function(e) {
            var $confirmation, $close;
            $confirmation = FwConfirmation.renderConfirmation('Notes', '');
            $close        = FwConfirmation.addButton($confirmation, 'Close', true);
            FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="" data-enabled="false" data-datafield=""></div>');
            FwFormField.setValue($confirmation, '.note', $noteTextArea.val());
            $confirmation.find('.note textarea').css('width', '400px').css('max-width', '570px').css('height', '300px').css('resize', 'both');
        });
        $noteTextArea  = jQuery('<textarea class="value" style="display:none;"></textarea>');
        $noteTextArea.val(originalvalue);
        $field.empty().append([$noteImage, $noteTextArea]);

        $noteImage
            .hover(function() {
                jQuery('<p class="tooltip" style="position:absolute;border:1px solid #333;background-color:#161616;border-radius:5px;padding:10px;color:#fff;font-size:12px;font-family:Arial;max-width:300px;word-wrap:break-word;"></p>').text(originalvalue).appendTo('body').fadeIn('slow');
            }, function() {
                jQuery('.tooltip').remove();
            })
            .mousemove(function() {
                var $this, mousex, mousey;
                $this = jQuery(this);
                mousex = $this.offset().left + 10;
                if (mousex + 300 > jQuery(document).width()) {
                    mousex = jQuery(document).width() - 330;
                }
                mousey = $this.offset().top + 10;
                if (mousey + 80 > jQuery(document).height()) {
                    mousey = jQuery(document).height() - 80;
                }
            
                jQuery('.tooltip').css({ top: mousey, left: mousex });
            })
        ;
    } else {
        $field.empty();
    }
};
//---------------------------------------------------------------------------------
FwBrowseColumn_note.setFieldEditMode = function($browse, $field, $tr, html) {
    var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
    var originalvalue = (typeof $field.attr('data-originalvalue')  === 'string') ? $field.attr('data-originalvalue') : '';
    var formmaxlength = (typeof $field.attr('data-formmaxlength')  === 'string') ? $field.attr('data-formmaxlength') : '';
    $noteImage = jQuery('<img src="' + applicationConfig.appbaseurl + 'theme/fwimages/icons/16/fileextension-document.png" style="cursor:pointer;" />');
    $noteTextArea  = jQuery('<textarea class="value" style="display:none;"></textarea>');
    $noteTextArea.val(originalvalue);
    $field.empty().append([$noteImage, $noteTextArea]);

    $noteImage.on('click', function(e) {
        var $confirmation, $ok, $cancel, $predefinednotes, controlhtml;
        $confirmation = FwConfirmation.renderConfirmation('Notes', '');
        $ok           = FwConfirmation.addButton($confirmation, 'Ok', true);
        $cancel       = FwConfirmation.addButton($confirmation, 'Close', true);
        controlhtml = [];
        if (typeof $field.attr('data-predefinednotevalidation') === 'string') {
             controlhtml.push('<div data-control="FwFormField" data-type="combobox" data-validate="false" class="fwcontrol fwformfield predefinednotes" data-caption="Predefined Notes" data-datafield="" data-validationname="' + $field.attr('data-predefinednotevalidation') + '" boundfields="predefinednote.rowtype"></div>');
        }
        controlhtml.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Notes" data-enabled=""' + ((formmaxlength !== '0') ? 'data-maxlength="' + formmaxlength : '') + '" data-datafield=""></div>');
        FwConfirmation.addControls($confirmation, controlhtml.join('\n'));
        FwFormField.setValue($confirmation, '.note', $noteTextArea.val());
        $confirmation.find('.note textarea').css('width', '400px').css('max-width', '570px').css('height', '300px').css('resize', 'both');
        $ok.on('click', function() {
            $noteTextArea.val($confirmation.find('.note textarea').val());
        });
        $confirmation.on('change', '.predefinednotes .fwformfield-value', function() {
            $confirmation.find('.note .fwformfield-value').val($confirmation.find('.predefinednotes .fwformfield-value').val());
            $confirmation.find('.predefinednotes .fwformfield-value').val('');
            $confirmation.find('.predefinednotes .fwformfield-text').val('');
        });
    });
};
//---------------------------------------------------------------------------------
