var FwBrowseColumn_noteClass = (function () {
    function FwBrowseColumn_noteClass() {
    }
    FwBrowseColumn_noteClass.prototype.databindfield = function ($browse, $field, dt, dtRow, $tr) {
    };
    ;
    FwBrowseColumn_noteClass.prototype.getFieldValue = function ($browse, $tr, $field, field, originalvalue) {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('textarea.value').val();
        }
    };
    ;
    FwBrowseColumn_noteClass.prototype.setFieldValue = function ($browse, $tr, $field, data) {
        $field.find('textarea').val(data.value);
    };
    FwBrowseColumn_noteClass.prototype.isModified = function ($browse, $tr, $field) {
        var isModified = false;
        var originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var currentValue = $field.find('textarea.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    ;
    FwBrowseColumn_noteClass.prototype.setFieldViewMode = function ($browse, $tr, $field) {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue !== '') {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer">insert_drive_file</i>');
            $noteImage.on('click', function (e) {
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
            });
            $noteTextArea = jQuery('<textarea class="value" style="display:none;"></textarea>');
            $noteTextArea.val(originalvalue);
            $field.empty().append([$noteImage, $noteTextArea]);
            $noteImage
                .hover(function onhover() {
                var $thisNoteImage = jQuery(this);
                var $tooltip = jQuery('<p class="tooltip" style="position:absolute;border:1px solid #333;background-color:#161616;border-radius:5px;padding:10px;color:#fff;font-size:12px;font-family:Arial;max-width:300px;word-wrap:break-word;"></p>').text(originalvalue).appendTo('body').fadeIn('slow');
                $thisNoteImage.data('$tooltip', $tooltip);
            }, function endhover() {
                var $thisNoteImage = jQuery(this);
                if (typeof $thisNoteImage.data('$tooltip') !== 'undefined') {
                    $thisNoteImage.data('$tooltip').remove();
                }
                $thisNoteImage.removeData('$tooltip');
            })
                .mousemove(function () {
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
                $noteImage.data('$tooltip').css({ top: mousey, left: mousex });
            });
        }
        else {
            $field.empty();
        }
    };
    ;
    FwBrowseColumn_noteClass.prototype.setFieldEditMode = function ($browse, $tr, $field) {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        $noteImage = jQuery('<i class="material-icons" style="cursor:pointer">insert_drive_file</i>');
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
            $confirmation.find('.note textarea').css('width', '400px').css('max-width', '570px').css('height', '300px').css('resize', 'both');
            $ok.on('click', function () {
                $noteTextArea.val($confirmation.find('.note textarea').val());
            });
            $confirmation.on('change', '.predefinednotes .fwformfield-value', function () {
                $confirmation.find('.note .fwformfield-value').val($confirmation.find('.predefinednotes .fwformfield-value').val());
                $confirmation.find('.predefinednotes .fwformfield-value').val('');
                $confirmation.find('.predefinednotes .fwformfield-text').val('');
            });
        });
        $noteTextArea.select();
    };
    ;
    return FwBrowseColumn_noteClass;
}());
var FwBrowseColumn_note = new FwBrowseColumn_noteClass();
//# sourceMappingURL=FwBrowseColumn_note.js.map