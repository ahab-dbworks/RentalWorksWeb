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
        return isModified;
    }
    //---------------------------------------------------------------------------------
    setFieldViewMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        if (originalvalue.length > 1) {
            let color = ((originalvalue.charAt(0) == '#') ? originalvalue : ('#' + originalvalue));
            $field.html('<div class="legendbox" style="background-color:' + originalvalue + ';width:14px;height:20px;border:1px solid #777777;"></div>');
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        /*
         
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none" value="#ffffff"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<div class="fwformfield-colorselector">');
        html.push('<div class="fwformfield-color" style="background-color:#ffffff;"></div>');
        html.push('<i class="material-icons btncolorpicker">&#xE3B7;</i>');
        html.push('</div>');
        html.push('</div>');
        $control.html(html.join(''));

        $control.find('.fwformfield-colorselector')
            .colpick({
                layout: 'hex',
                colorScheme: 'light',
                color: 'ffffff',
                showEvent: '',
                onSubmit: function (hsb, hex, rgb, el) {
                    jQuery(el).siblings('.fwformfield-value').val('#' + hex).change();
                    jQuery(el).find('.fwformfield-color').css('background-color', '#' + hex);
                    jQuery(el).colpickHide();
                }
            })
            .on('click', function (e: JQuery.Event) {
                try {
                    if ($control.attr('data-enabled') === 'true') {
                        jQuery(this).colpickShow(e);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;



         */
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_olecolor = new FwBrowseColumn_olecolorClass();