class FwPopup {
    //----------------------------------------------------------------------------------------------
    static attach($control) {
        var $divOverlay, $divCloseButton, $divPopup, baseId, baseIdStart, $appendTo;

        baseIdStart = 1;
        baseId      = '';
        while(baseId === '') {
            var tempBaseid;
            tempBaseid = 'FwPopup' + baseIdStart.toString() + '-divOverlay';
            if (jQuery('#' + tempBaseid).length === 0) {
                baseId = 'FwPopup' + baseIdStart.toString();
            }
            baseIdStart++;
        }
        $divOverlay = jQuery('<div class="FwPopup-divOverlay">')
            .attr('id', baseId + '-divOverlay')
            .hide()
        ;
        $divPopup = jQuery('<div class="FwPopup-divPopup">')
            .attr('id', baseId + '-divPopup')
            .attr('data-baseid', baseId)
            .append($control)
        ;
        jQuery('.application').append($divOverlay);
        $divOverlay.append($divPopup);

        return $divPopup;
    };
    //----------------------------------------------------------------------------------------------
    static show($divPopup) {
        var $divOverlay, maxZIndex;

        maxZIndex = FwFunc.getMaxZ('*');
        $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay');
        $divOverlay.css({
            'z-index': maxZIndex
        });
        if (window['Modernizr'].flexbox) {
            $divOverlay.css('display', 'flex');
        } else {
            $divOverlay.css('display', 'block');
        }
    };
    //----------------------------------------------------------------------------------------------
    static hide($divPopup) {
        jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').hide();
    };
    //----------------------------------------------------------------------------------------------
    static destroy($divPopup) {
        var $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').remove();
    };
    //----------------------------------------------------------------------------------------------
    static renderPopup($content, options, title?) {
        var html, $popup, ismodal=true;
        html = [];
        html.push('<div class="fwpopup">');
            html.push('<div class="fwpopupbox">');
                if (title !== undefined) {
                    html.push('<div class="title">' + title + '</div>')
                }
            html.push('</div>');
        html.push('</div>');
        $popup = jQuery(html.join(''));
        $popup.find('.fwpopupbox').append($content);
        if (typeof options === 'object') {
            if (typeof options.ismodal === 'boolean') {
                ismodal = options.ismodal;
            }
        }
        if (!ismodal) {
            $popup.on('click', function() {
                FwPopup.destroyPopup($popup);
            });
            $popup.on('click', '.fwpopupbox', function(e) {
                e.stopPropagation();
            });
        }
        return $popup;
    };
    //----------------------------------------------------------------------------------------------
    static destroyPopup($popup) {
        $popup.remove();
    };
    //----------------------------------------------------------------------------------------------
    static showPopup($popup) {
        var maxZIndex;

        maxZIndex = FwFunc.getMaxZ('*');
        $popup.css({
            'z-index': maxZIndex
        });
        if (window['Modernizr'].flexbox) {
            $popup.css('display', 'flex');
        } else {
            $popup.css('display', 'block');
        }
        jQuery('#application').append($popup);
    };
    //----------------------------------------------------------------------------------------------
    static detachPopup($control) {
        $control.detach();
    };
    //----------------------------------------------------------------------------------------------
}
