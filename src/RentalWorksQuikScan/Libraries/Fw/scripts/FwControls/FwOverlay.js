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
    FwOverlay.showProgressBarOverlay = function ($appendToElement, progressBarSessionId) {
        var html, $moduleoverlay, maxZIndex, progressCompleted, caption, percentage, handle, currentStep, totalSteps, fullurl;
        currentStep = 100;
        totalSteps = 100;
        var request = {};
        var url = "api/v1/progressmeter/" + progressBarSessionId;
        fullurl = applicationConfig.apiurl + url;
        progressCompleted = false;
        html = [];
        var ajaxOptions = {
            method: 'GET',
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                Authorization: 'Bearer ' + sessionStorage.getItem('apiToken')
            },
            context: {
                requestid: FwAppData.generateUUID()
            },
        };
        html.push("<progress max=\"100\" value=\"100\"><span class=\"progress_span\">0</span></progress>");
        html.push("<div class=\"progress_bar_text\"></div>");
        html.push("<div class=\"progress_bar_caption\">Initiating your request...</div>");
        $moduleoverlay = jQuery("<div class=\"progress_bar\">");
        $moduleoverlay.html(html.join(''));
        $appendToElement.css('position', 'relative').append($moduleoverlay);
        handle = setInterval(function () {
            jQuery.ajax(ajaxOptions)
                .done(function (response) {
                try {
                    if (isNaN(response.CurrentStep) || undefined) {
                        caption = 'Processing...';
                        currentStep += 2.5;
                        $moduleoverlay.find('progress').val(100);
                        $moduleoverlay.find('progress').attr('max', 100);
                        $moduleoverlay.find('.progress_bar_caption').text(caption);
                    }
                    else {
                        caption = response.Caption;
                        currentStep = parseInt(response.CurrentStep);
                        totalSteps = parseInt(response.TotalSteps);
                        percentage = Math.floor((currentStep / totalSteps) * 100);
                        $moduleoverlay.find('progress').val(currentStep);
                        $moduleoverlay.find('progress').attr('max', totalSteps);
                        $moduleoverlay.find('.progress_bar_text').text(percentage + "%");
                        $moduleoverlay.find('.progress_span').text(percentage + "%");
                        $moduleoverlay.find('.progress_bar_caption').text(caption);
                    }
                }
                catch (ex) {
                    console.log('showProgressBarOverlay error: ', ex);
                }
                if (currentStep === totalSteps) {
                    progressCompleted = true;
                    if (progressCompleted) {
                        clearInterval(handle);
                        handle = 0;
                    }
                }
            });
        }, 500);
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