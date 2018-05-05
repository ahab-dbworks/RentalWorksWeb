class FwOverlay {
    //----------------------------------------------------------------------------------------------
    static showPleaseWaitOverlay($appendToElement, requestid) {
        var html, $moduleoverlay, maxZIndex;

        html = [];
        html.push('<div class="fwoverlay-center pleasewait">');
            html.push('<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/loading.001.gif" />');
            html.push('<div class="message">Please Wait</div>');
            //if (typeof requestid === 'string') {
            //    html.push('<button class="btnCancel">Cancel</button>');
            //}
        html.push('</div>');
        html = html.join('');
    
        $moduleoverlay = jQuery('<div class="fwoverlay">');
        $moduleoverlay.css('z-index', maxZIndex);
        maxZIndex = FwFunc.getMaxZ('*');
        $moduleoverlay.html(html);
        $moduleoverlay.on('click', function() {
            $moduleoverlay.addClass('clicked');
        });
        //$moduleoverlay.on('click', '.btnCancel', function(event) {
        //    event.stopPropagation();
        //    FwAppData.abortRequest(requestid);
        //});
        $appendToElement.css('position', 'relative').append($moduleoverlay);

        return $moduleoverlay;
    }
    //----------------------------------------------------------------------------------------------
    static showErrorOverlay($appendToElement) {
        var html, overlayid, overlaycount;

        overlayid = FwControl.generateControlId('overlay');
        overlaycount = $appendToElement.data('overlayoutcount');
        if (typeof overlaycount === 'undefined') {
            $appendToElement.data('overlayoutcount', 1);
            $appendToElement.attr('data-positionbeforeoverlay', $appendToElement.css('position'));
        } else {
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
        } else {
            $moduleoverlay.html(html);
            $appendToElement.css('position', 'relative').append(html);
        }

        return overlayid;
    }
    //----------------------------------------------------------------------------------------------
    static hideOverlay($overlay) {
        var overlayoutcount;
        $overlay.remove();
        //$hideFromElement.css('position', $hideFromElement.attr('data-positionbeforeoverlay'));
        //overlaycount = $hideFromElement.data('overlayoutcount');
        //if (overlaycount === 1) {
        //    $hideFromElement.removeData('overlayoutcount');
        //    $hideFromElement.removeAttr('data-positionbeforeoverlay');
        //} else {
        //    $hideFromElement.data('overlayoutcount', overlaycount - 1);
        //}
    }
    //----------------------------------------------------------------------------------------------
}