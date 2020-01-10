class FwPopupClass {
    //----------------------------------------------------------------------------------------------
    attach($control) {
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
    show($divPopup) {
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
    hide($divPopup) {
        jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').hide();
    };
    //----------------------------------------------------------------------------------------------
    destroy($divPopup) {
        var $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').remove();
    };
    //----------------------------------------------------------------------------------------------
    renderPopup($content, options, title?, popoutModuleId?) {
        var me = this;
        var html, $popup, ismodal = true, isNewValidation = false;
        if ($content.data('afterSaveNewValidation') !== 'undefined' && typeof $content.data('afterSaveNewValidation') === 'function') {
            isNewValidation = true;
        }
        html = [];
        html.push('<div class="fwpopup">');
        html.push('<div class="fwpopupbox" style="position:relative;">');
        if (title !== undefined) {
            html.push('<div class="popuptitle">' + title + '</div>');
            html.push('<div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        }
        if (popoutModuleId !== undefined) {
            html.push('<div class="popout-modal" style="display:flex; position:absolute; top:10px; right:100px; cursor:pointer;"><i class="material-icons">open_in_new</i><div class="btn-text">Pop-Out</div></div>');
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
            me.destroyPopup(jQuery(this).closest('.fwpopup'));
            jQuery(this).closest('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        $popup.find('.popout-modal').one('click', function (e) {
            if (isNewValidation) {
                $content.data('afterSaveNewValidation')();
            }
            let popoutUniqueDatafield = $content.data('uniqueids').data('datafield');
            let popoutKeys = {};
            let $this = jQuery(this);
            popoutKeys[popoutUniqueDatafield] = popoutModuleId;
            let popupWait = FwOverlay.showPleaseWaitOverlay($content, null);
            setTimeout(function () {
                var $popoutForm = (<any>window)[$content.data('controller')].loadForm(popoutKeys);
                FwModule.openModuleTab($popoutForm, "", true, 'FORM', true);
                FwOverlay.hideOverlay(popupWait);
                me.destroyPopup($this.closest('.fwpopup'));
            });

            $this.closest('.fwpopup').off('click');
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
    //----------------------------------------------------------------------------------------------
    destroyPopup($popup) {
        $popup.remove();
    };
    //----------------------------------------------------------------------------------------------
    showPopup($popup) {
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
    detachPopup($control) {
        $control.detach();
    };
    //----------------------------------------------------------------------------------------------
}

var FwPopup = new FwPopupClass();
