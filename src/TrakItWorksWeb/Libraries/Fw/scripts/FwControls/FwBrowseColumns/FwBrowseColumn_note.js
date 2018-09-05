class FwBrowseColumn_noteClass {
    databindfield($browse, $field, dt, dtRow, $tr) {
    }
    ;
    getFieldValue($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('textarea.value').val();
        }
    }
    ;
    setFieldValue($browse, $tr, $field, data) {
        $field.find('textarea').val(data.value);
    }
    isModified($browse, $tr, $field) {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('textarea.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    ;
    setFieldViewMode($browse, $tr, $field) {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 0) {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#0D47A1;">insert_drive_file</i>');
        }
        else {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#4CAF50;">note_add</i>');
        }
        $noteImage.on('click', function (e) {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            }
            else {
                var $thisNoteImage = jQuery(this);
                var $confirmation, $close;
                if (typeof $thisNoteImage.data('$tooltip') !== 'undefined') {
                    $thisNoteImage.data('$tooltip').remove();
                }
                $confirmation = FwConfirmation.renderConfirmation('Notes', '');
                $close = FwConfirmation.addButton($confirmation, 'Close', true);
                FwConfirmation.addControls($confirmation, '<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="" data-enabled="false" data-datafield=""></div>');
                FwFormField.setValue($confirmation, '.note', $noteTextArea.val());
                $confirmation.find('.note textarea').css('width', '400px').css('max-width', '570px').css('height', '300px').css('resize', 'both');
            }
        });
        $noteTextArea = jQuery('<textarea class="value" style="display:none;"></textarea>');
        $noteTextArea.val(originalvalue);
        if ($field.attr('data-formreadonly') === 'true' && originalvalue.length === 0) {
            $field.empty().append($noteTextArea);
        }
        else {
            $field.empty().append([$noteImage, $noteTextArea]);
        }
    }
    ;
    setFieldEditMode($browse, $tr, $field) {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        if (originalvalue !== '') {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#0D47A1;">insert_drive_file</i>');
        }
        else {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#4CAF50;">note_add</i>');
        }
        $noteTextArea = jQuery('<textarea class="value" style="display:none;"></textarea>');
        $noteTextArea.val(originalvalue);
        $field.empty().append([$noteImage, $noteTextArea]);
        $noteImage.on('click', function (e) {
            var $confirmation, $ok, $cancel, $predefinednotes, controlhtml;
            $confirmation = FwConfirmation.renderConfirmation('Notes', '');
            $ok = FwConfirmation.addButton($confirmation, 'Ok', true);
            $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
            controlhtml = [];
            if (typeof $field.attr('data-predefinednotevalidation') === 'string') {
                controlhtml.push('<div data-control="FwFormField" data-type="combobox" data-validate="false" class="fwcontrol fwformfield predefinednotes" data-caption="Predefined Notes" data-datafield="" data-validationname="' + $field.attr('data-predefinednotevalidation') + '" boundfields="predefinednote.rowtype"></div>');
            }
            controlhtml.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Notes" data-enabled=""' + ((formmaxlength !== '0') ? 'data-maxlength="' + formmaxlength : '') + '" data-datafield=""></div>');
            FwConfirmation.addControls($confirmation, controlhtml.join('\n'));
            FwFormField.setValue($confirmation, '.note', $noteTextArea.val());
            $confirmation.find('.note textarea')
                .css({
                'width': '400px',
                'max-width': '570px',
                'height': '300px',
                'resize': 'both'
            })
                .select();
            $ok.on('click', function () {
                $noteTextArea.val($confirmation.find('.note textarea').val());
                if ($noteTextArea.val().length > 0) {
                    $noteImage.text('insert_drive_file');
                    $noteImage.css('color', '#0D47A1');
                }
                else {
                    $noteImage.text('note_add');
                    $noteImage.css('color', '#4CAF50');
                }
            });
            $confirmation.on('change', '.predefinednotes .fwformfield-value', function () {
                $confirmation.find('.note .fwformfield-value').val($confirmation.find('.predefinednotes .fwformfield-value').val());
                $confirmation.find('.predefinednotes .fwformfield-value').val('');
                $confirmation.find('.predefinednotes .fwformfield-text').val('');
            });
        });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $noteImage.click();
        }
    }
    ;
}
var FwBrowseColumn_note = new FwBrowseColumn_noteClass();
//# sourceMappingURL=FwBrowseColumn_note.js.map