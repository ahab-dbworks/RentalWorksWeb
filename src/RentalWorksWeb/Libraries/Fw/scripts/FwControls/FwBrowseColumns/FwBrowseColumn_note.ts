class FwBrowseColumn_noteClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    };
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            field.value = $field.find('textarea.value').val();
        }
    };
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        $field.find('textarea').val(data.value);
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('textarea.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    };
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 0) {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#0D47A1;">insert_drive_file</i>');
        } else {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#4CAF50;">note_add</i>');
        }
        $noteImage.on('click', function (e) {
            if ($field.attr('data-formreadonly') !== 'true') {
                $field.data('autoselect', true);
            } else {
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
        } else {
            $field.empty().append([$noteImage, $noteTextArea]);
        }
        if ($browse.attr('data-type') == 'Grid' && $browse.attr('data-enabled') == 'false') {  //allows viewing notes on disabled grids
            $field.attr('data-formreadonly', 'true');
        }
        //if (originalvalue !== '') {
        //    $noteImage
        //        .hover(
        //            function onhover() {
        //                var $thisNoteImage = jQuery(this);
        //                var $tooltip = jQuery('<p class="tooltip" style="position:absolute;border:1px solid #333;background-color:#161616;border-radius:5px;padding:10px;color:#fff;font-size:12px;font-family:Arial;max-width:300px;word-wrap:break-word;"></p>').text(originalvalue).appendTo('body').fadeIn('slow');
        //                $thisNoteImage.data('$tooltip', $tooltip);
        //            },
        //            function endhover() {
        //                var $thisNoteImage = jQuery(this);
        //                if (typeof $thisNoteImage.data('$tooltip') !== 'undefined') {
        //                    $thisNoteImage.data('$tooltip').remove();
        //                }
        //                $thisNoteImage.removeData('$tooltip');
        //            }
        //        )
        //        .mousemove(function () {
        //            var $this, mousex, mousey;
        //            $this = jQuery(this);
        //            mousex = $this.offset().left + 10;
        //            if (mousex + 300 > jQuery(document).width()) {
        //                mousex = jQuery(document).width() - 330;
        //            }
        //            mousey = $this.offset().top + 10;
        //            if (mousey + 80 > jQuery(document).height()) {
        //                mousey = jQuery(document).height() - 80;
        //            }

        //            $noteImage.data('$tooltip').css({ top: mousey, left: mousex });
        //        })
        //    ;
        //}
    };
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var $noteImage, $noteTextArea, $notePopup, $notePopupControl, $notePopupHtml;
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        var formmaxlength = (typeof $field.attr('data-formmaxlength') === 'string') ? $field.attr('data-formmaxlength') : '';
        if (originalvalue !== '') {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#0D47A1;">insert_drive_file</i>');
        } else {
            $noteImage = jQuery('<i class="material-icons" style="cursor:pointer;color:#4CAF50;">note_add</i>');
        }
        $noteTextArea = jQuery('<textarea class="value" style="display:none;"></textarea>');
        $noteTextArea.val(originalvalue);
        $field.empty().append([$noteImage, $noteTextArea]);

        $noteImage.on('click', function (e) {
            const $confirmation = FwConfirmation.renderConfirmation('Notes', '');
            const $ok = FwConfirmation.addButton($confirmation, 'Ok', true);
            const $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
            const controlhtml: Array<string> = [];
            let addPrintNotes = false;
            if (typeof $field.attr('data-predefinednotevalidation') === 'string') {
                controlhtml.push('<div data-control="FwFormField" data-type="combobox" data-validate="false" class="fwcontrol fwformfield predefinednotes" data-caption="Predefined Notes" data-datafield="" data-validationname="' + $field.attr('data-predefinednotevalidation') + '" boundfields="predefinednote.rowtype"></div>');
            }
            if ($field.attr('data-addprintnotes') !== 'true') {
                controlhtml.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Notes" data-enabled=""' + ((formmaxlength !== '0') ? 'data-maxlength="' + formmaxlength : '') + '" data-datafield=""></div>');
            } else {
                addPrintNotes = true;
                controlhtml.push('<div class="flexrow">');
                controlhtml.push('  <div class="flexcolumn">');
                controlhtml.push('    <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Notes" data-enabled=""' + ((formmaxlength !== '0') ? 'data-maxlength="' + formmaxlength : '') + '" data-datafield=""></div>');
                controlhtml.push('  </div>');
                controlhtml.push('  <div class="flexcolumn">');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                controlhtml.push(`      <div style="float:left;width:300px;text-decoration:underline;">Print Note on these Documents</div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="PrintNoteOnQuote" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order transfer-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="PrintNoteOnOrder" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="PrintNoteOnPurchaseOrder" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow transfer-order order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pick List" data-datafield="PrintNoteOnPickList" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Out Contract" data-datafield="PrintNoteOnOutContract" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Receive Contract" data-datafield="PrintNoteOnReceiveContract" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return List" data-datafield="PrintNoteOnReturnList" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vendor Return List" data-datafield="PrintNoteOnVendorReturnList" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vendor Receive List" data-datafield="PrintNoteOnVendorReceiveList" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow transfer-order purchase-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return Contract" data-datafield="PrintNoteOnReturnContract" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order transfer-order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Contract" data-datafield="PrintNoteOnInContract" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow order" data-control="FwContainer" data-type="fieldrow" style="display:none;">');
                controlhtml.push(`      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Invoice" data-datafield="PrintNoteOnInvoice" style="float:left;width:100px;"></div>`);
                controlhtml.push('    </div>');
                controlhtml.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                controlhtml.push(`      <div data-datavalue="CheckAll" data-confirmfield="CheckAllNone" style="float:left;width:120px;text-decoration:underline;color:#4646ff;">Check All</div>`);
                controlhtml.push('    </div>');
                controlhtml.push('  </div>');
                controlhtml.push('</div>');
                $confirmation.find('.fwconfirmationbox').css({ 'width': '605px' });
            }
            FwConfirmation.addControls($confirmation, controlhtml.join('\n'));
            const formController = $browse.closest('.fwform').attr('data-controller');
            if (addPrintNotes) {
                $confirmation.find('.note textarea')
                    .css({
                        'width': '285px',
                        'max-width': '570px',
                        'height': '510px',
                        'resize': 'vertical'
                    })
                    .select();
                if (formController === 'TransferOrderController') {
                    $confirmation.find('.transfer-order').show();
                    $confirmation.find('div[data-datafield="PrintNoteOnOrder"] label').text('Transfer Order');
                    $confirmation.find('div[data-datafield="PrintNoteOnOutContract"] label').text('Manifest');
                    $confirmation.find('div[data-datafield="PrintNoteOnInContract"] label').text('Transfer Receipt');
                }
                if (formController === 'PurchaseOrderController') {
                    $confirmation.find('.purchase-order').show();
                }
                if (formController === 'OrderController' || formController === 'QuoteController') {
                    $confirmation.find('.order').show();
                }
                fillInCheckboxesFromRow($confirmation, $tr);

                $ok.on('mousedown', () => { // saving checkbox values before popup is destroyed on 'ok' click
                    const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
                    $checkboxes.each((i, e) => {
                        const dataField = jQuery(e).attr('data-datafield');
                        FwBrowse.setFieldValue($browse, $tr, dataField, { value: FwFormField.getValueByDataField($confirmation, dataField) === 'T' ? 'true' : 'false' });
                    });
                });
                // Check All / None
                $confirmation.find('div[data-confirmfield="CheckAllNone"]').on('click', e => {
                    const checkAll = $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue');
                    const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
                    if (checkAll === 'CheckAll') {
                        // select all fields
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckNone');
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check None');
                        $checkboxes.each((i, e) => {
                            const dataField = jQuery(e).attr('data-datafield');
                            FwFormField.setValueByDataField($confirmation, dataField, true);
                        });
                    } else {
                        //unselect all
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckAll');
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check All');
                        $checkboxes.each((i, e) => {
                            const dataField = jQuery(e).attr('data-datafield');
                            FwFormField.setValueByDataField($confirmation, dataField, false);
                        });
                    }
                })
                function fillInCheckboxesFromRow($confirmation, $tr) {
                    const $checkboxes = $confirmation.find('div[data-type="checkbox"]:visible');
                    let checkedCount = 0;
                    $checkboxes.each((i, e) => {
                        const dataField = jQuery(e).attr('data-datafield');
                        const val = $tr.find(`.field[data-browsedatafield="${dataField}"]`).attr('data-originalvalue') === 'true';
                        val === true ? checkedCount++ : '';
                        FwFormField.setValueByDataField($confirmation, dataField, val);
                    });

                    if (checkedCount === $checkboxes.length) {
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').attr('data-datavalue', 'CheckNone');
                        $confirmation.find('div[data-confirmfield="CheckAllNone"]').text('Check None');
                    }
                }
            }
            FwFormField.setValue($confirmation, '.note', $noteTextArea.val());
            if (!addPrintNotes) {
                $confirmation.find('.note textarea')
                    .css({
                        'width': '400px',
                        'max-width': '570px',
                        'height': '510px',
                        'resize': 'both'
                    })
                    .select();
            }
            $ok.on('click', function () {
                $noteTextArea.val($confirmation.find('.note textarea').val());
                if ($noteTextArea.val().length > 0) {
                    $noteImage.text('insert_drive_file');
                    $noteImage.css('color', '#0D47A1');
                } else {
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
    };
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_note = new FwBrowseColumn_noteClass();