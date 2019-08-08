class FwConfirmation {
    //----------------------------------------------------------------------------------------------
    static renderConfirmation(title: string, message: string) {
        var html, $control, maxZIndex, $body, $more;

        html = [];
        html.push('<div class="responsive fwconfirmation advisory">');
        html.push('<div class="fwconfirmationbox">');
        html.push('<div class="title">' + title + '</div>');
        html.push('<div class="body">');
        if ((typeof message === 'string') && (message.length > 0)) {
            html.push('<div class="message">' + message + '</div>');
        }
        html.push('</div>');
        html.push('<div class="more" style="display:none;">Scroll for more...</div>');
        html.push('<div class="fwconfirmation-buttonbar"></div>');
        html.push('</div>');
        html.push('</div>');
        $control = jQuery(html.join(''));

        maxZIndex = FwFunc.getMaxZ('*');
        $control.css('z-index', maxZIndex);
        jQuery('#application').append($control);

        // show "Scroll for more..." if the body is scrollable
        $body = $control.find('.body');
        $more = $control.find('.more');
        $more.toggle($body[0].scrollHeight > $body[0].clientHeight);

        return $control;
    }
    //----------------------------------------------------------------------------------------------
    static addButton($confirmation: JQuery, buttontext: string, hasDefaultEvent?: boolean) {
        var $button = jQuery('<div>')
            .addClass('fwconfirmation-button')
            .attr('role', 'button')
            .attr('tabindex', '0')
            .html(buttontext)
            .appendTo($confirmation.find('.fwconfirmation-buttonbar'))
            .on('keypress', function (e) {
                if (e.which === 13) {
                    $button.click();
                    e.preventDefault();
                }
            });

        if (hasDefaultEvent !== false) {
            $button.on('click', function () {
                FwConfirmation.destroyConfirmation($confirmation);
            });
        }

        return $button;
    }
    //----------------------------------------------------------------------------------------------
    static destroyConfirmation($confirmation) {
        $confirmation.remove();
    }
    //----------------------------------------------------------------------------------------------
    static addControls($confirmation: JQuery, controlshtml: string) {
        $confirmation.find('.body').append('<div class="controls fwform">' + controlshtml + '</div>');
        FwControl.renderRuntimeControls($confirmation.find('.fwcontrol'));
        $confirmation.data('fields', $confirmation.find('.fwformfield'));
    }
    //----------------------------------------------------------------------------------------------
    static addJqueryControl($confirmation: JQuery, $control: JQuery) {
        let $fwform = $confirmation.find('.body .fwform');
        if ($fwform.length === 0) {
            $fwform = jQuery('<div class="controls fwform"></div>');
            $confirmation.find('.body').append($fwform);
        }
        $fwform.append($control);
        FwControl.renderRuntimeControls($confirmation.find('.fwcontrol'));
        $confirmation.data('fields', $confirmation.find('.fwformfield'));
    }
    //----------------------------------------------------------------------------------------------
    static showMessage(title: string, message: string, hascancelbutton: boolean, autoclose: boolean, buttontext: string, onbuttonclick: Function) {
        var $confirmation, $btn, $btncancel;
        $confirmation = FwConfirmation.renderConfirmation(title, message);
        $btn = FwConfirmation.addButton($confirmation, buttontext, autoclose);
        $btn.on('click', onbuttonclick);
        $btn.focus();
        if (hascancelbutton) {
            $btncancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
        }
        setTimeout(function() {
            $btn.focus();
        }, 100);
        return $confirmation;
    }
    //----------------------------------------------------------------------------------------------
    static yesNo(title: string, message: string, onyes?: Function, onno?: Function) {
        var $confirmation, $btnyes, $btnno;
        $confirmation = FwConfirmation.renderConfirmation(title, message);
        $btnyes = FwConfirmation.addButton($confirmation, 'Yes', true);
        if (typeof onyes === 'function') {
            $btnyes.on('click', onyes);
        }
        $btnyes.focus();
        $btnno = FwConfirmation.addButton($confirmation, 'No', true);
        if (typeof onno === 'function') {
            $btnno.on('click', onno);
        }
    }
    //----------------------------------------------------------------------------------------------
}
