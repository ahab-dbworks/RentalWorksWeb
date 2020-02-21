class FwBrowseColumn_numericupdownClass implements IFwBrowseColumn {
    //---------------------------------------------------------------------------------
    databindfield($browse, $field, dt, dtRow, $tr): void {

    }
    //---------------------------------------------------------------------------------
    getFieldValue($browse, $tr, $field, field, originalvalue): void {
        if (($tr.hasClass('editmode')) || ($tr.hasClass('newmode'))) {
            var $value = $field.find('input.value');
            if ($value.length > 0) {
                if (applicationConfig.allCaps && $field.attr('data-allcaps') !== 'false' && $field.find('input.value').val()) {
                    field.value = $field.find('input.value').val().toUpperCase();
                } else {
                    field.value = $field.find('input.value').val();
                }
            } else {
                field.value = originalvalue;
            }
        }
    }
    //---------------------------------------------------------------------------------
    setFieldValue($browse: JQuery, $tr: JQuery, $field: JQuery, data: FwBrowse_SetFieldValueData): void {
        $field.find('input.value').val(data.value);
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
        let html = [];
        $field.data('autoselect', false);
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';

        html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
        html.push('<div style="position:relative">');
        html.push(`     <input class="value" type="number" style="height:1.5em; width:40px; text-align:center;" value="${originalvalue}">`);
        html.push('</div>');
        html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
        $field.empty().append(jQuery(html.join('')));

        $field.data({
            interval: {},
            increment: function () {
                const $value = $field.find('.value');
                let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                $value.val(++oldval);
            },
            decrement: function () {
                const $value = $field.find('.value');
                let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                if (oldval > 0) {
                    $value.val(--oldval);
                }
            }
        });

        if (jQuery('html').hasClass('desktop')) {
            $field
                .on('click', '.incrementQuantity', () => {
                    $field.data('increment')();
                    $field.find('.value').change();
                })
                .on('click', '.decrementQuantity', () => {
                    $field.data('decrement')();
                    $field.find('.value').change();
                });
        };

        //$field.find('.value').on('keydown', e => {
        //    e.stopPropagation();
        //});

        const allowNegative = $field.attr('data-allownegative');
        if (allowNegative == 'false') {
            $field.find('.value').on('keydown', (e) => {
                if (!((e.keyCode > 95 && e.keyCode < 106)
                    || (e.keyCode > 47 && e.keyCode < 58)
                    || e.keyCode == 8 || e.keyCode == 9
                    || (e.keyCode > 36 && e.keyCode < 41))) {
                    return false;
                }
            });
        }
    }
    //---------------------------------------------------------------------------------
    setFieldEditMode($browse, $tr, $field): void {
        var originalvalue = (typeof $field.attr('data-originalvalue') === 'string') ? $field.attr('data-originalvalue') : '';
        let html = [];

        html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
        html.push('<div style="position:relative">');
        html.push(`     <input class="value" type="number" style="height:1.5em; width:40px; text-align:center;" value="${originalvalue}">`);
        html.push('</div>');
        html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');

        let htmlString = html.join('');
        $field.html(htmlString);
        this.setFieldValue($browse, $tr, $field, { value: originalvalue });
        if ($field.data('autoselect') === true) {
            $field.data('autoselect', false);
            $field.find('.value').select();
        }

        const allowNegative = $field.attr('data-allownegative');
        if (allowNegative == 'false') {
            $field.find('.value').on('keydown', (e) => {
                if (!((e.keyCode > 95 && e.keyCode < 106)
                    || (e.keyCode > 47 && e.keyCode < 58)
                    || e.keyCode == 8 || e.keyCode == 9
                    || (e.keyCode > 36 && e.keyCode < 41))) {
                    return false;
                }
            });
        }
    }
    //---------------------------------------------------------------------------------
}

var FwBrowseColumn_numericupdown = new FwBrowseColumn_numericupdownClass();