class FwPopupClass {
    //----------------------------------------------------------------------------------------------
    attach($control: JQuery): JQuery {
        let baseIdStart = 1;
        let baseId = '';
        while (baseId === '') {
            const tempBaseid = 'FwPopup' + baseIdStart.toString() + '-divOverlay';
            if (jQuery('#' + tempBaseid).length === 0) {
                baseId = 'FwPopup' + baseIdStart.toString();
            }
            baseIdStart++;
        }
        const $divOverlay = jQuery('<div class="FwPopup-divOverlay">')
            .attr('id', baseId + '-divOverlay')
            .hide();
        const $divPopup = jQuery('<div class="FwPopup-divPopup">')
            .attr('id', baseId + '-divPopup')
            .attr('data-baseid', baseId)
            .append($control);
        jQuery('.application').append($divOverlay);
        $divOverlay.append($divPopup);

        return $divPopup;
    };
    //----------------------------------------------------------------------------------------------
    show($divPopup: JQuery): void {
        const maxZIndex = FwFunc.getMaxZ('*');
        const $divOverlay = jQuery(`#${$divPopup.attr('data-baseid')}-divOverlay`);
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
    hide($divPopup: JQuery): void {
        jQuery(`#${$divPopup.attr('data-baseid')}-divOverlay`).hide();
    };
    //----------------------------------------------------------------------------------------------
    destroy($divPopup: JQuery): void {
        jQuery(`#${$divPopup.attr('data-baseid')}-divOverlay`).remove();
    };
    //----------------------------------------------------------------------------------------------
    renderPopup($content: JQuery, options: {ismodal?: boolean}, title?: string, popoutModuleId?: string): JQuery {
        let isNewValidation = false;
        if ($content.data('afterSaveNewValidation') !== 'undefined' && typeof $content.data('afterSaveNewValidation') === 'function') {
            isNewValidation = true;
        }
        const html: Array<string> = [];
        html.push('<div class="fwpopup">');
        html.push('<div class="fwpopupbox">');
        if (title !== undefined) {
            html.push(`<div class="popuptitle">${title}</div>`);
            html.push('<div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        }
        if (popoutModuleId !== undefined) {
            html.push('<div class="popout-modal" style="display:flex; position:absolute; top:10px; right:100px; cursor:pointer;"><i class="material-icons">open_in_new</i><div class="btn-text">Pop-Out</div></div>');
        }
        html.push('</div>');
        html.push('</div>');
        const $popup = jQuery(html.join(''));

        if (isNewValidation) {
            $popup.addClass('new-validation');
        }

        $popup.find('.close-modal').one('click', e => {
            if (isNewValidation) {
                $content.data('afterSaveNewValidation')();
            }
            this.destroyPopup(jQuery(e.currentTarget).closest('.fwpopup'));
            jQuery(e.currentTarget).closest('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        $popup.find('.popout-modal').one('click', e => {
            if (isNewValidation) {
                $content.data('afterSaveNewValidation')();
            }
            const popoutUniqueDatafield = $content.data('uniqueids').data('datafield');
            const popoutKeys: any = {};
            const $this = jQuery(e.currentTarget);
            popoutKeys[popoutUniqueDatafield] = popoutModuleId;
            const popupWait = FwOverlay.showPleaseWaitOverlay($content, null);
            setTimeout(() => {
                const $popoutForm = (<any>window[$content.data('controller')]).loadForm(popoutKeys);
                FwModule.openModuleTab($popoutForm, "", true, 'FORM', true);
                FwOverlay.hideOverlay(popupWait);
                this.destroyPopup($this.closest('.fwpopup'));
            });

            $this.closest('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
        let ismodal = true;
        $popup.find('.fwpopupbox').append($content);
        if (typeof options === 'object') {
            if (typeof options.ismodal === 'boolean') {
                ismodal = options.ismodal;
            }
        }
        if (!ismodal) {
            $popup.on('click', e => {
                FwPopup.destroyPopup($popup);
            });
            $popup.on('click', '.fwpopupbox', e => {
                e.stopPropagation();
            });
        }
        return $popup;
    };
    //----------------------------------------------------------------------------------------------
    destroyPopup($popup: JQuery): void {
        $popup.remove();
    };
    //----------------------------------------------------------------------------------------------
    showPopup($popup: JQuery): void {
        const maxZIndex = FwFunc.getMaxZ('*');
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
    detachPopup($control: JQuery): void {
        $control.detach();
    };
    //----------------------------------------------------------------------------------------------
}

var FwPopup = new FwPopupClass();
