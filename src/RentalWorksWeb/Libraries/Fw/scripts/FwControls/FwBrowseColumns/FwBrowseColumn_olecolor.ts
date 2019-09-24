class FwBrowseColumn_olecolorClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {

    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        throw `FwBrowseColumn_olecolor.setFieldValue: setFieldValue is not supported on column: ${$field.attr('data-datafield')}`;
    }
    //---------------------------------------------------------------------------------
    isModified($browse, $tr, $field): boolean {
        var isModified = false;
        let originalValue = $field.attr('data-originalvalue');
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            let currentValue = $field.find('input.value').val();
            isModified = currentValue !== originalValue;
        }
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            const color = ((originalvalue.charAt(0) == '#') ? originalvalue : (`#${originalvalue}`));
            $field.html(`<div class="legendbox" style="background-color:${originalvalue};width:14px;height:20px;border:1px solid #777777;border-radius:2px;"></div>`);
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        const originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            let editable = false;
            let paletteColor = '#616161';
            if ($field.attr('data-formreadonly') === 'false') {
                editable = true;
                paletteColor = '#2196f3'
            }
            $field.html(`<div class="color-wrapper" style="padding: 0 0 0 4px;"><div class="legendbox" style="border-radius:2px;float:left;cursor:pointer;background-color:${originalvalue};width:14px;height:20px;border:1px solid #777777;"></div><i style="float:right;color:${paletteColor};padding: 0;font-size: 1.5em;margin: auto 5px;box-sizing:border-box;cursor:pointer;" class="material-icons btncolorpicker">&#xE3B7;</i><div>`);
            const $wrapper = $field.find('div.color-wrapper');
            $wrapper
                //.colpick({
                //    layout: 'hex',
                //    colorScheme: 'light',
                //    color: 'ffffff',
                //    showEvent: '',
                //    onSubmit: function (hsb, hex, rgb, el) {
                //        $field.attr('data-originalvalue', `#${hex}`).change();
                //        $field.find('div.legendbox').css('background-color', `#${hex}`);
                //        jQuery(el).colpickHide();
                //    },
                //    onHide: function () {
                //        jQuery('body').find('.colpick').remove();
                //    }
                //})
                .on('click', function (e: JQuery.Event) {
                    try {
                        if (editable) {
                            jQuery('body').find('.colpick').remove();
                            $wrapper
                                .colpick({
                                    layout: 'hex',
                                    colorScheme: 'light',
                                    color: 'ffffff',
                                    onSubmit: function (hsb, hex, rgb, el) {
                                        $field.attr('data-originalvalue', `#${hex}`).change();
                                        $field.find('div.legendbox').css('background-color', `#${hex}`);
                                        jQuery(el).colpickHide();
                                    }
                                })
                            jQuery(this).colpickShow(e);
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_olecolor = new FwBrowseColumn_olecolorClass();