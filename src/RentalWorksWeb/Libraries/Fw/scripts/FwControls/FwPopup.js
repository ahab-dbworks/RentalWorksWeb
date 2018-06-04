var FwPopup = (function () {
    function FwPopup() {
    }
    FwPopup.attach = function ($control) {
        var $divOverlay, $divCloseButton, $divPopup, baseId, baseIdStart, $appendTo;
        baseIdStart = 1;
        baseId = '';
        while (baseId === '') {
            var tempBaseid;
            tempBaseid = 'FwPopup' + baseIdStart.toString() + '-divOverlay';
            if (jQuery('#' + tempBaseid).length === 0) {
                baseId = 'FwPopup' + baseIdStart.toString();
            }
            baseIdStart++;
        }
        $divOverlay = jQuery('<div class="FwPopup-divOverlay">')
            .attr('id', baseId + '-divOverlay')
            .hide();
        $divPopup = jQuery('<div class="FwPopup-divPopup">')
            .attr('id', baseId + '-divPopup')
            .attr('data-baseid', baseId)
            .append($control);
        jQuery('.application').append($divOverlay);
        $divOverlay.append($divPopup);
        return $divPopup;
    };
    ;
    FwPopup.show = function ($divPopup) {
        var $divOverlay, maxZIndex;
        maxZIndex = FwFunc.getMaxZ('*');
        $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay');
        $divOverlay.css({
            'z-index': maxZIndex
        });
        if (window['Modernizr'].flexbox) {
            $divOverlay.css('display', 'flex');
        }
        else {
            $divOverlay.css('display', 'block');
        }
    };
    ;
    FwPopup.hide = function ($divPopup) {
        jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').hide();
    };
    ;
    FwPopup.destroy = function ($divPopup) {
        var $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').remove();
    };
    ;
    FwPopup.renderPopup = function ($content, options, title) {
        var html, $popup, ismodal = true, isNewValidation = false;
        if ($content.data('afterSaveNewValidation') !== 'undefined' && typeof $content.data('afterSaveNewValidation') === 'function') {
            isNewValidation = true;
        }
        html = [];
        html.push('<div class="fwpopup">');
        html.push('<div class="fwpopupbox" style="position:relative;">');
        if (title !== undefined) {
<<<<<<< develop
            html.push('<div class="popuptitle">' + title + '</div>');
            html.push('<div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
=======
            html.push('<div class="title">' + title + '</div>');
>>>>>>> Updates Fw
        }
        html.push('</div>');
        html.push('</div>');
        $popup = jQuery(html.join(''));
        if (isNewValidation) {
            $popup.addClass('new-validation');
        }
        $popup.find('.close-modal').one('click', function (e) {
            if (isNewValidation) {
                $content.data('afterSaveNewValidation')();
            }
            FwPopup.destroyPopup(jQuery(this).closest('.fwpopup'));
            jQuery(this).closest('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
        $popup.find('.fwpopupbox').append($content);
        if (typeof options === 'object') {
            if (typeof options.ismodal === 'boolean') {
                ismodal = options.ismodal;
            }
        }
        if (!ismodal) {
            $popup.on('click', function () {
                FwPopup.destroyPopup($popup);
            });
            $popup.on('click', '.fwpopupbox', function (e) {
                e.stopPropagation();
            });
        }
        return $popup;
    };
    ;
    FwPopup.destroyPopup = function ($popup) {
        $popup.remove();
    };
    ;
    FwPopup.showPopup = function ($popup) {
        var maxZIndex;
        maxZIndex = FwFunc.getMaxZ('*');
        $popup.css({
            'z-index': maxZIndex
        });
        if (window['Modernizr'].flexbox) {
            $popup.css('display', 'flex');
        }
        else {
            $popup.css('display', 'block');
        }
        jQuery('#application').append($popup);
    };
    ;
    FwPopup.detachPopup = function ($control) {
        $control.detach();
    };
    ;
    return FwPopup;
}());
//# sourceMappingURL=FwPopup.js.map