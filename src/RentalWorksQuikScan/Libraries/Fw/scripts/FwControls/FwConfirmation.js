class FwConfirmation {
    static renderConfirmation(title, message) {
        var html, $control, maxZIndex, $body, $more;
        html = [];
        html.push('<div class="responsive fwconfirmation">');
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
        $body = $control.find('.body');
        $more = $control.find('.more');
        $more.toggle($body[0].scrollHeight > $body[0].clientHeight);
        $control.on('click', '.fwconfirmation-button.default', function () {
            FwConfirmation.destroyConfirmation($control);
        });
        return $control;
    }
    static addButton($control, buttontext, hasDefaultEvent) {
        var html, $button, cssclass;
        cssclass = ((typeof hasDefaultEvent === 'boolean') && (!hasDefaultEvent)) ? '' : ' default';
        html = [];
        html.push('<div class="fwconfirmation-button' + cssclass + '" tabindex="0" role="button">' + buttontext + '</div>');
        $button = jQuery(html.join(''));
        $control.find('.fwconfirmation-buttonbar').append($button);
        $button.on('keypress', function (e) {
            if (e.which === 13) {
                $button.click();
                e.preventDefault();
            }
        });
        return $button;
    }
    static destroyConfirmation($control) {
        $control.remove();
    }
    static addControls($control, controlshtml) {
        $control.find('.body').append('<div class="controls fwform">' + controlshtml + '</div>');
        FwControl.renderRuntimeControls($control.find('.fwcontrol'));
        $control.data('fields', $control.find('.fwformfield'));
    }
    static showMessage(title, message, hascancelbutton, autoclose, buttontext, onbuttonclick) {
        var $confirmation, $btn, $btncancel;
        $confirmation = FwConfirmation.renderConfirmation(title, message);
        $btn = FwConfirmation.addButton($confirmation, buttontext, autoclose);
        $btn.on('click', onbuttonclick);
        $btn.focus();
        if (hascancelbutton) {
            $btncancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
        }
        setTimeout(function () {
            $btn.focus();
        }, 100);
        return $confirmation;
    }
    static yesNo(title, message, onyes, onno) {
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
}
//# sourceMappingURL=FwConfirmation.js.map