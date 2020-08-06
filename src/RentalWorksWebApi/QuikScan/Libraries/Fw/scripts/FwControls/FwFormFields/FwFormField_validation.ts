class FwFormField_validationClass implements IFwFormField {
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push(FwControl.generateDesignerHandle($control.attr('data-type'), $control.attr('id')));
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text"');
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        html.push(' />');
        html.push('<i class="material-icons btnvalidate">search</i>');
        html.push('</div>');
        $control.html(html.join(''));
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void {
        html.push('<div class="fwformfield-caption">' + $control.attr('data-caption') + '</div>');
        html.push('<div class="fwformfield-control">');
        html.push('<input class="fwformfield-value" type="hidden" />');
        html.push('<input class="fwformfield-text" type="text" autocapitalize="none"');
        if (applicationConfig.allCaps && $control.attr('data-allcaps') !== 'false') {
            html.push(' style="text-transform:uppercase"');
        }
        if ($control.attr('data-enabled') === 'false') {
            html.push(' disabled="disabled"');
        }
        if (typeof $control.attr('data-placeholder') !== 'undefined') {
            html.push(' placeholder="' + $control.attr('data-placeholder') + '"');
        }
        if ((sessionStorage.getItem('applicationsettings') !== null) && (typeof JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase !== 'undefined') && (JSON.parse(sessionStorage.getItem('applicationsettings')).webtouppercase)) {
            html.push(' style="text-transform:uppercase"');
        }
        html.push(' />');
        html.push('<i class="material-icons btnvalidate">search</i>');

        // push hidden spinning loader
        html.push('<div class="sk-fading-circle validation-loader"><div class="sk-circle1 sk-circle"></div><div class="sk-circle2 sk-circle"></div><div class="sk-circle3 sk-circle"></div><div class="sk-circle4 sk-circle"></div><div class="sk-circle5 sk-circle"></div><div class="sk-circle6 sk-circle"></div><div class="sk-circle7 sk-circle"></div><div class="sk-circle8 sk-circle"></div><div class="sk-circle9 sk-circle"></div><div class="sk-circle10 sk-circle"></div><div class="sk-circle11 sk-circle"></div><div class="sk-circle12 sk-circle"></div></div>');

        const isWebAdmin = (sessionStorage.getItem('userid') !== null) ? JSON.parse(sessionStorage.getItem('userid')).webadministrator === 'true' : false;
        let showPeek = false;
        if (!($control.attr('data-validationpeek') === 'false')) {
            if (isWebAdmin === true) {
                showPeek = true;
            }
            else if (isWebAdmin === false && FwValidation.isHomeModule($control)) {
                showPeek = true;
            }
        }

        if (showPeek) {
            html.push('<i class="material-icons btnpeek">more_horiz</i>');
        }

        html.push('</div>');
        $control.html(html.join(''));
        FwValidation.init($control);
    }
    //---------------------------------------------------------------------------------
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void {

    }
    //---------------------------------------------------------------------------------
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void {
        $fwformfield
            .attr('data-originalvalue', value)
            .find('input.fwformfield-value')
            .val(value);
        $fwformfield.find('.fwformfield-text')
            .val(text);
    }
    //---------------------------------------------------------------------------------
    disable($control: JQuery<HTMLElement>): void {
        $control.find('.btnvalidate').attr('data-enabled', 'false');
        $control.find('.fwformfield-text').prop('disabled', true);
    }
    //---------------------------------------------------------------------------------
    enable($control: JQuery<HTMLElement>): void {
        $control.find('.btnvalidate').attr('data-enabled', 'true');
        $control.find('.fwformfield-text').prop('disabled', false);
    }
    //---------------------------------------------------------------------------------
    getValue2($fwformfield: JQuery<HTMLElement>): any {
        var value = $fwformfield.find('.fwformfield-value').val();
        return value;
    }
    //---------------------------------------------------------------------------------
    getText2($fwformfield: JQuery<HTMLElement>): string {
        var text;
        if (applicationConfig.allCaps && $fwformfield.attr('data-allcaps') !== 'false') {
            text = (<string>$fwformfield.find('.fwformfield-text').val()).toUpperCase();
        } else {
            text = $fwformfield.find('.fwformfield-text').val();
        }
        return text;
    }
    //---------------------------------------------------------------------------------
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void {
        var $inputvalue = $fwformfield.find('.fwformfield-value');
        var $inputtext = $fwformfield.find('.fwformfield-text');
        $inputtext.val(text);
        $inputvalue.val(value);
        if (firechangeevent) $inputvalue.change();
    }
    //---------------------------------------------------------------------------------
}

var FwFormField_validation = new FwFormField_validationClass();