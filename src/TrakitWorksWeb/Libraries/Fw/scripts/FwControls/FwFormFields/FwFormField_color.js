class FwFormField_colorClass {
    renderDesignerHtml($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="color"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    renderRuntimeHtml($control, html) {
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
            .on('click', function (e) {
            try {
                if ($control.attr('data-enabled') === 'true') {
                    jQuery(this).colpickShow(e);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    loadItems($control, items, hideEmptyItem) {
    }
    loadForm($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-colorselector').colpickSetColor(value);
        $fwformfield.find('.fwformfield-color').css('background-color', value);
    }
    disable($control) {
    }
    enable($control) {
    }
    getValue2($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    setValue($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent)
            $inputvalue.change();
    }
}
var FwFormField_color = new FwFormField_colorClass();
//# sourceMappingURL=FwFormField_color.js.map