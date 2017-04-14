//----------------------------------------------------------------------------------------------
var FwPopup = {};
//----------------------------------------------------------------------------------------------
FwPopup.attach = function($control) {
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
FwPopup.show = function($divPopup) {
    var $divOverlay, maxZIndex;

    maxZIndex = FwFunc.getMaxZ('*');
    $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay');
    $divOverlay.css({
        'z-index': maxZIndex
    });
    if (window.Modernizr.flexbox) {
        $divOverlay.css('display', 'flex');
    } else {
        $divOverlay.css('display', 'block');
    }
};
//----------------------------------------------------------------------------------------------
FwPopup.hide = function($divPopup) {
    jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').hide();
};
//----------------------------------------------------------------------------------------------
FwPopup.destroy = function($divPopup) {
    $divOverlay = jQuery('#' + $divPopup.attr('data-baseid') + '-divOverlay').remove();
};
//----------------------------------------------------------------------------------------------
FwPopup.renderPopup = function($content, options) {
    var html, $popup, ismodal=true;
    html = [];
    html.push('<div class="fwpopup">');
        html.push('<div class="fwpopupbox"></div>');
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
FwPopup.destroyPopup = function($popup) {
    $popup.remove();
};
//----------------------------------------------------------------------------------------------
FwPopup.showPopup = function($popup) {
    var maxZIndex;

    maxZIndex = FwFunc.getMaxZ('*');
    $popup.css({
        'z-index': maxZIndex
    });
    if (window.Modernizr.flexbox) {
        $popup.css('display', 'flex');
    } else {
        $popup.css('display', 'block');
    }
    jQuery('#application').append($popup);
};
//----------------------------------------------------------------------------------------------
FwPopup.detachPopup = function($control) {
    $control.detach();
};
//----------------------------------------------------------------------------------------------
