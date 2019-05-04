var FwFormField_dateClass = (function () {
    function FwFormField_dateClass() {
    }
    FwFormField_dateClass.prototype.renderDesignerHtml = function ($control, html) {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('</div>');
        $control.html(html.join(''));
    };
    FwFormField_dateClass.prototype.renderRuntimeHtml = function ($control, html) {
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        if ($control.attr('data-datenav') == 'true') {
            html.push('<div class="datenav yesterday"><i class="material-icons">play_arrow</i></div>');
        }
        html.push('<input class="fwformfield-value" type="text" autocapitalize="none"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        if (isdesktop) {
            html.push('<i class="material-icons btndate">&#xE878;</i>');
        }
        else if (ismobile) {
            html.push('<i class="material-icons md-dark btndate">&#xE878;</i>');
        }
        if ($control.attr('data-datenav') == 'true') {
            html.push('<div class="datenav tomorrow"><i class="material-icons">play_arrow</i></div>');
        }
        html.push('</div>');
        $control.html(html.join(''));
        $control.find('.fwformfield-value').inputmask('mm/dd/yyyy');
        $control.find('.fwformfield-value').datepicker({
            endDate: (($control.attr('data-nofuture') == 'true') ? '+0d' : Infinity),
            autoclose: true,
            format: "mm/dd/yyyy",
            todayHighlight: true,
            todayBtn: 'linked'
        }).off('focus');
        $control
            .on('change', '.fwformfield-value', function () {
            try {
                var today;
                var datevalue = jQuery(this).val();
                today = new Date();
                if (datevalue.match(/yyyy$/)) {
                    datevalue = datevalue.replace('yyyy', today.getFullYear());
                    $control.find('.fwformfield-value').val(datevalue);
                }
                var datevalue2 = new Date(datevalue);
                if ($control.attr('data-nofuture') === 'true') {
                    if (datevalue2 > today) {
                        jQuery(this).val('');
                        $control.addClass('error');
                        FwNotification.renderNotification('ERROR', 'Cannot select future dates.');
                    }
                    else {
                        $control.removeClass('error');
                        jQuery(this).datepicker('update');
                    }
                }
                else {
                    jQuery(this).datepicker('update');
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.btndate', function () {
            try {
                if ($control.attr('data-enabled') === 'true') {
                    $control.find('.fwformfield-value').datepicker('show');
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.today', function () {
            var today;
            try {
                today = FwFunc.getDate();
                $control.find('.fwformfield-value').val(today).change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.yesterday', function () {
            var date, value;
            try {
                value = $control.find('.fwformfield-value').val();
                date = FwFunc.getDate(value, -1);
                $control.find('.fwformfield-value').val(date).change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.tomorrow', function () {
            var date, value;
            try {
                value = $control.find('.fwformfield-value').val();
                date = FwFunc.getDate(value, 1);
                $control.find('.fwformfield-value').val(date).change();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    FwFormField_dateClass.prototype.loadItems = function ($control, items, hideEmptyItem) {
    };
    FwFormField_dateClass.prototype.loadForm = function ($fwformfield, table, field, value, text) {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-value').datepicker('update');
    };
    FwFormField_dateClass.prototype.disable = function ($control) {
    };
    FwFormField_dateClass.prototype.enable = function ($control) {
    };
    FwFormField_dateClass.prototype.getValue2 = function ($fwformfield) {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    };
    FwFormField_dateClass.prototype.setValue = function ($fwformfield, value, text, firechangeevent) {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(value);
        $inputvalue.datepicker('update');
        if (firechangeevent)
            $inputvalue.change();
    };
    return FwFormField_dateClass;
}());
var FwFormField_date = new FwFormField_dateClass();
//# sourceMappingURL=FwFormField_date.js.map