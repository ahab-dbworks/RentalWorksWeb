FwFormField_number = {};
//---------------------------------------------------------------------------------
FwFormField_number.renderDesignerHtml = function($control, html) {
    html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
    html.push('<input class="fwformfield-value" type="number"');
    if ($control.attr('data-enabled') === 'false') {
        html.push(' disabled="disabled"');
    }
    if (typeof $control.attr('data-minvalue') !== 'undefined') {
        html.push(' min="'+ $control.attr('data-minvalue') + '"');
    }
    if (typeof $control.attr('data-maxvalue') !== 'undefined') {
        html.push(' max="'+ $control.attr('data-maxvalue') + '"');
    }
    html.push(' />');
    html.push('</div>');
    $control.html(html.join(''));
};
//---------------------------------------------------------------------------------
FwFormField_number.renderRuntimeHtml = function($control, html) {
    var min, max, digits, digitsoptional, autogroup, rightalign;
    var isdesktop = jQuery('html').hasClass('desktop');
    var ismobile  = jQuery('html').hasClass('mobile');
    html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
    html.push('<div class="fwformfield-control">');
    if (ismobile) {
        html.push('<div class="qtybtn"><i class="material-icons md-dark subtract">&#xE15D;</i></div>'); //remove_circle_outline
        html.push('<input class="fwformfield-value" type="number" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    } else if (isdesktop) {
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
    }
    if (isdesktop) {
        html.push('<div class="add-on">');
            html.push('<div class="icon-n"></div>');
            html.push('<div class="icon-s"></div>');
        html.push('</div>');
    } else if (ismobile) {
        html.push('<div class="qtybtn"><i class="material-icons md-dark add">&#xE148;</i></div>'); //add_circle_outline
    }
    html.push('</div>');
    $control.html(html.join(''));

    if (typeof $control.attr('data-minvalue') == 'undefined') {
        if (typeof $control.attr('data-maxlength') !== 'undefined') {
            min = '-' + '9'.repeat($control.attr('data-maxlength'));
            $control.attr('data-minvalue', min);
        } else {
            min = 'undefined';
        }
    } else {
        min = $control.attr('data-minvalue');
    }
    if (typeof $control.attr('data-maxvalue') == 'undefined') {
        if (typeof $control.attr('data-maxlength') !== 'undefined') {
            max = '9'.repeat($control.attr('data-maxlength'));
            $control.attr('data-maxvalue', max);
        } else {
            max = 'undefined';
        }
    } else {
        max = $control.attr('data-maxvalue');
    }
    digits         = ((typeof $control.attr('data-digits') !== 'undefined') ? $control.attr('data-digits') : 2);
    digitsoptional = (((typeof $control.attr('data-digitsoptional') !== 'undefined') && ($control.attr('data-digitsoptional') == 'false')) ? false : true)
    autogroup      = (((typeof $control.attr('data-formatnumeric') !== 'undefined') && ($control.attr('data-formatnumeric') == 'true')) ? true : false);
    rightalign     = jQuery('html').hasClass('desktop');
    if (isdesktop) {
        $control.find('.fwformfield-value').inputmask("numeric", {
            min:            min,
            max:            max,
            digits:         digits,
            digitsOptional: digitsoptional,
            radixPoint:     '.',
            groupSeparator: ',',
            autoGroup:      autogroup,
            rightAlign:     rightalign
        });
    }

    $control
        .data({
            interval: {},
            increment: function() {
                var $value = $control.find('input.fwformfield-value');
                var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                if ((typeof $control.attr('data-maxvalue') !== 'undefined') && ($control.attr('data-maxvalue') <= oldval)) {

                } else {
                    $value.val(++oldval).change();
                }
            },
            decrement: function() {
                var $value = $control.find('input.fwformfield-value');
                var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                if ((typeof $control.attr('data-minvalue') !== 'undefined') && ($control.attr('data-minvalue') >= oldval)) {

                } else {
                    $value.val(--oldval).change();
                }
            }
        })
        .on('change', '.fwformfield-value', function() {
            if (parseFloat(this.value) > parseFloat($control.attr('data-maxvalue'))) {
                jQuery(this).val($control.attr('data-maxvalue'));
            } else if (parseFloat(this.value) < parseFloat($control.attr('data-minvalue'))) {
                jQuery(this).val($control.attr('data-minvalue'));
            }
        })
        .on('focus', '.fwformfield-value', function() {
            var $this;
            $this = jQuery(this);
            if (parseInt($this.val()) === 0) {
                $this.val('');
            }
        })
        .on('blur', '.fwformfield-value', function() {
            var $this;
            $this = jQuery(this);
            if (($this.val() == '') && ($control.attr('data-originalvalue') != '')) {
                $this.val($control.attr('data-originalvalue'));
            }
        })
    ;
    if (jQuery('html').hasClass('desktop')) {
        $control
            .on('mousedown', '.icon-n', function() {
                if ($control.attr('data-enabled') === 'true') {
                    $control.data('increment')();
                    $control.data('interval', setInterval(function() { $control.data('increment')(); }, 200));
                }
            })
            .on('mouseup mouseleave', '.icon-n', function() {
                clearInterval($control.data('interval'));
            })
            .on('mousedown', '.icon-s', function() {
                if ($control.attr('data-enabled') === 'true') {
                    $control.data('decrement')();
                    $control.data('interval', setInterval(function() { $control.data('decrement')(); }, 200));
                }
            })
            .on('mouseup mouseleave', '.icon-s', function() {
                clearInterval($control.data('interval'));
            })
        ;
    }
    else if (jQuery('html').hasClass('mobile')) {
        $control
            .on('click', '.add', function() {
                if ($control.attr('data-enabled') === 'true') {
                    $control.data('increment')();
                }
            })
            .on('click', '.subtract', function() {
                if ($control.attr('data-enabled') === 'true') {
                    $control.data('decrement')();
                }
            })
        ;
    }
};
//---------------------------------------------------------------------------------
FwFormField_number.loadItems = function($control, items, hideEmptyItem) {

};
//---------------------------------------------------------------------------------
FwFormField_number.loadForm = function($fwformfield, table, field, value, text) {
    $fwformfield
        .attr('data-originalvalue', value)
        .find('.fwformfield-value')
            .val(value);
};
//---------------------------------------------------------------------------------
FwFormField_number.disable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_number.enable = function($control) {

};
//---------------------------------------------------------------------------------
FwFormField_number.getValue2 = function($fwformfield) {
    var valuecontainer = $fwformfield.find('.fwformfield-value').inputmask('unmaskedvalue');
    var value = (typeof valuecontainer === 'string' && valuecontainer.length > 0) ? valuecontainer : '0'; //Fix for jquery.inputmask('unmaskedvalue') which returns null on empty values
    return value;
};
//---------------------------------------------------------------------------------
FwFormField_number.setValue = function($fwformfield, value, text, firechangeevent) {
    var $inputvalue = $fwformfield.find('.fwformfield-value');
    $inputvalue.val(value); 
    if (firechangeevent) $inputvalue.change();
};
//---------------------------------------------------------------------------------
