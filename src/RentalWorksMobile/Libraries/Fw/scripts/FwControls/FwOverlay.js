var FwOverlay = (function () {
    function FwOverlay() {
    }
    FwOverlay.showPleaseWaitOverlay = function ($appendToElement, requestid) {
        var html, $moduleoverlay, maxZIndex;
        html = [];
        html.push('<div class="fwoverlay-center pleasewait">');
        html.push('<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/loading.001.gif" />');
        html.push('<div class="message">Please Wait</div>');
        html.push('</div>');
        html = html.join('');
        $moduleoverlay = jQuery('<div class="fwoverlay">');
        $moduleoverlay.css('z-index', maxZIndex);
        maxZIndex = FwFunc.getMaxZ('*');
        $moduleoverlay.html(html);
        $moduleoverlay.on('click', function () {
            $moduleoverlay.addClass('clicked');
        });
        $appendToElement.css('position', 'relative').append($moduleoverlay);
        return $moduleoverlay;
    };
    FwOverlay.showErrorOverlay = function ($appendToElement) {
        var html, overlayid, overlaycount;
        overlayid = FwControl.generateControlId('overlay');
        overlaycount = $appendToElement.data('overlayoutcount');
        if (typeof overlaycount === 'undefined') {
            $appendToElement.data('overlayoutcount', 1);
            $appendToElement.attr('data-positionbeforeoverlay', $appendToElement.css('position'));
        }
        else {
            $appendToElement.data('overlayoutcount', overlaycount + 1);
        }
        html = [];
        html.push('<div class="fwoverlay-center error">');
        html.push('<div style="height:128px;"></div>');
        html.push('<div class="message">Error</div>');
        html.push('</div>');
        html = html.join('');
        var $moduleoverlay = $appendToElement.find('.fwoverlay');
        if ($moduleoverlay.length === 0) {
            $moduleoverlay = jQuery('<div id="' + overlayid + '" class="fwoverlay">');
            $moduleoverlay.html(html);
            $appendToElement.css('position', 'relative').append($moduleoverlay);
        }
        else {
            $moduleoverlay.html(html);
            $appendToElement.css('position', 'relative').append(html);
        }
        return overlayid;
    };
    FwOverlay.hideOverlay = function ($overlay) {
        var overlayoutcount;
        $overlay.remove();
    };
    return FwOverlay;
}());
//# sourceMappingURL=FwOverlay.js.map