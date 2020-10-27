class FwFormField_dateClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
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
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
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
            html.push('<i class="material-icons btndate">&#xE878;</i>'); //event
        } else if (ismobile) {
            html.push('<i class="material-icons md-dark btndate">&#xE878;</i>'); //event
        }
        if ($control.attr('data-datenav') == 'true') {
            html.push('<div class="datenav tomorrow"><i class="material-icons">play_arrow</i></div>');
        }
        html.push('</div>');
        $control.html(html.join(''));

        $control.data({
            'OriginalValue': '',
            'ISOFormat': '',
            'LocalizedValue': ''
        });

        $control.find('.fwformfield-value').datepicker({
            endDate:        (($control.attr('data-nofuture') == 'true') ? '+0d' : Infinity),
            autoclose:      true,
            format:         FwLocale.getDateFormat().toLowerCase(),
            todayHighlight: true,
            todayBtn:       'linked',
            weekStart:      FwFunc.getWeekStartInt(),
        }).off('focus');

        $control.find('.fwformfield-value').inputmask(FwLocale.getDateFormat().toLowerCase());

        $control
            .on('change', '.fwformfield-value', function () {
                try {
                    var today;
                    let datevalue: string = <string>jQuery(this).val();
                    today = new Date();

                    if (datevalue.match(/yyyy$/)) {
                        datevalue = datevalue.replace('yyyy', today.getFullYear());
                        $control.find('.fwformfield-value').val(datevalue);
                    }
                    let datevalue2 = new Date(datevalue);

                    if ($control.attr('data-nofuture') === 'true') {
                        if (datevalue2 > today) {
                            jQuery(this).val('');
                            $control.addClass('error');
                            FwNotification.renderNotification('ERROR', 'Cannot select future dates.');
                        } else {
                            $control.removeClass('error');
                            jQuery(this).datepicker('update');

                            $control.data({
                                'ISOFormat': FwLocale.formatLocaleDateToIso(datevalue),
                                'LocalizedValue': datevalue
                            });
                        }
                    } else {
                        jQuery(this).datepicker('update');

                        $control.data({
                            'ISOFormat':      FwLocale.formatLocaleDateToIso(datevalue),
                            'LocalizedValue': datevalue
                        });
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btndate', function () {
                try {
                    if ($control.attr('data-enabled') === 'true') {
                        $control.find('.fwformfield-value').datepicker('show');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.yesterday', function () {
                try {
                    if ($control.attr('data-enabled') === 'true') {
                        var value = $control.data('ISOFormat');
                        var date = FwLocale.getDate(value, true, { Quantity: -1, ObjectModified: 'days' });
                        $control.find('.fwformfield-value').val(date).change();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.tomorrow', function () {
                try {
                    if ($control.attr('data-enabled') === 'true') {
                        var value = $control.data('ISOFormat');
                        var date = FwLocale.getDate(value, true, { Quantity: 1, ObjectModified: 'days' });
                        $control.find('.fwformfield-value').val(date).change();
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string, model: any): void {
        $fwformfield
            .attr('data-originalvalue', FwLocale.formatDateToLocale(value))
            .find('.fwformfield-value')
            .val(FwLocale.formatDateToLocale(value));
        $fwformfield.find('.fwformfield-value').datepicker('update');

        $fwformfield.data({
            'OriginalValue':  value,
            'ISOFormat':      value,
            'LocalizedValue': FwLocale.formatDateToLocale(value)
        });
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {

    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        var value = FwLocale.formatLocaleDateToIso($fwformfield.find('.fwformfield-value').val());
        return value;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        $inputvalue.val(FwLocale.formatDateToLocale(value));
        $inputvalue.datepicker('update');
        if (firechangeevent) $inputvalue.change();

        $fwformfield.data({
            'ISOFormat':      value,
            'LocalizedValue': FwLocale.formatDateToLocale(value)
        });
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_date = new FwFormField_dateClass();