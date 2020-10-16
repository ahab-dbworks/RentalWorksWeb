class FwFormField_numberClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="number"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-minvalue') !== 'undefined') {
            html.push(' min="' + $control.attr('data-minvalue') + '"');
        }
        if (typeof $control.attr('data-maxvalue') !== 'undefined') {
            html.push(' max="' + $control.attr('data-maxvalue') + '"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        var min, max, digits, digitsoptional, autogroup, rightalign;
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        if (ismobile) {
            html.push('<div class="qtybtn"><i class="material-icons md-dark subtract">&#xE15D;</i></div>'); //remove_circle_outline
            html.push('<input class="fwformfield-value" type="number" autocapitalize="none"');
            if ($control.attr('data-enabled') === 'false') {
                html.push(' disabled="disabled"');
            }
            html.push(' />');
            html.push('<div class="qtybtn"><i class="material-icons md-dark add">&#xE148;</i></div>'); //add_circle_outline
        } else if (isdesktop) {
            html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
            if ($control.attr('data-enabled') === 'false') {
                html.push(' disabled="disabled"');
            }
            html.push(' />');
        }
        html.push('</div>');
        $control.html(html.join(''));

        if (typeof $control.attr('data-minvalue') == 'undefined') {
            if (typeof $control.attr('data-maxlength') !== 'undefined') {
                min = '-';
                let maxlength = parseInt(<string>$control.attr('data-maxlength'));
                for (let i = 0; i < maxlength; i++) {
                    min += '9';
                }
                $control.attr('data-minvalue', min);
            } else {
                min = 'undefined';
            }
        } else {
            min = $control.attr('data-minvalue');
        }
        if (typeof $control.attr('data-maxvalue') == 'undefined') {
            if (typeof $control.attr('data-maxlength') !== 'undefined') {
                max = '';
                let maxlength = parseInt(<string>$control.attr('data-maxlength'));
                for (let i = 0; i < maxlength; i++) {
                    min += '9';
                }
                $control.attr('data-maxvalue', max);
            } else {
                max = 'undefined';
            }
        } else {
            max = $control.attr('data-maxvalue');
        }
        digits = ((typeof $control.attr('data-digits') !== 'undefined') ? $control.attr('data-digits') : 2);
        digitsoptional = (((typeof $control.attr('data-digitsoptional') !== 'undefined') && ($control.attr('data-digitsoptional') == 'false')) ? false : true)
        autogroup = (((typeof $control.attr('data-formatnumeric') !== 'undefined') && ($control.attr('data-formatnumeric') == 'true')) ? true : false);
        rightalign = jQuery('html').hasClass('desktop');
        if (isdesktop) {
            $control.find('.fwformfield-value').inputmask("numeric", {
                min: min,
                max: max,
                digits: digits,
                digitsOptional: digitsoptional,
                radixPoint: '.',
                groupSeparator: ',',
                autoGroup: autogroup,
                rightAlign: rightalign
            });
        }

        $control
            .on('change', '.fwformfield-value', function () {
                if (parseFloat((<HTMLInputElement>this).value) > parseFloat($control.attr('data-maxvalue'))) {
                    jQuery(this).val($control.attr('data-maxvalue'));
                } else if (parseFloat((<HTMLInputElement>this).value) < parseFloat($control.attr('data-minvalue'))) {
                    jQuery(this).val($control.attr('data-minvalue'));
                }
            })
            .on('focus', '.fwformfield-value', function () {
                var $this;
                $this = jQuery(this);
                if (parseInt($this.val()) === 0) {
                    $this.val('');
                }
            })
            .on('blur', '.fwformfield-value', function () {
                var $this;
                $this = jQuery(this);
                if (($this.val() == '') && ($control.attr('data-originalvalue') != '')) {
                    $this.val($control.attr('data-originalvalue'));
                }
            })
            ;
        if (jQuery('html').hasClass('mobile')) {
            $control
                .on('click', '.add', function () {
                    if ($control.attr('data-enabled') === 'true') {
                        var $value = $control.find('input.fwformfield-value');
                        var oldval = jQuery.isNumeric(parseFloat(<string>$value.val())) ? parseFloat(<string>$value.val()) : 0;
                        if ((typeof $control.attr('data-maxvalue') !== 'undefined') && (parseFloat($control.attr('data-maxvalue')) <= oldval)) {

                        } else {
                            $value.val(++oldval).change();
                        }
                    }
                })
                .on('click', '.subtract', function () {
                    if ($control.attr('data-enabled') === 'true') {
                        var $value = $control.find('input.fwformfield-value');
                        var oldval = jQuery.isNumeric(parseFloat(<string>$value.val())) ? parseFloat(<string>$value.val()) : 0;
                        if ((typeof $control.attr('data-minvalue') !== 'undefined') && (parseFloat($control.attr('data-minvalue')) >= oldval)) {

                        } else {
                            $value.val(--oldval).change();
                        }
                    }
                })
                ;
        }
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
        var value = (typeof valuecontainer === 'string' && valuecontainer.length > 0) ? valuecontainer : '0'; //Fix for jquery.inputmask('unmaskedvalue') which returns null on empty values
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_number = new FwFormField_numberClass();