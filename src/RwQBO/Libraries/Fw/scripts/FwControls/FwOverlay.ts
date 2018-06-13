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
        $moduleoverlay.on('click', function () {
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
    static showProgressBarOverlay($appendToElement, progressBarSessionId) {
        let html, $moduleoverlay, maxZIndex, progressCompleted, caption, percentage, handle, currentStep, totalSteps, fullurl;
        currentStep = 0;
        totalSteps = 100;

        let request: any = {};
        let url = `api/v1/progressmeter/${progressBarSessionId}`;

        fullurl = applicationConfig.apiurl + url;
        progressCompleted = false;
        html = [];

        let ajaxOptions: JQuery.AjaxSettings<any> = {
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

        html.push(`<progress max="" value="0"><span class="progress_span">0</span></progress>`);
        html.push(`<div class="progress_bar_text"></div>`);
        html.push(`<div class="progress_bar_caption"></div>`);

        $moduleoverlay = jQuery(`<div class="progress_bar">`);
        $moduleoverlay.html(html.join(''));
        $appendToElement.css('position', 'relative').append($moduleoverlay);

        $moduleoverlay.hide();

        handle = setInterval(() => {
            jQuery.ajax(ajaxOptions)
                .done(response => {
                    if (currentStep >= 1) {
                        $moduleoverlay.show();
                    }

                    try {
                        if (isNaN(response.CurrentStep) || undefined) {
                            caption = 'Processing...';
                            currentStep += 5;
                            percentage = Math.floor((currentStep / totalSteps) * 100);
                            $moduleoverlay.find('progress').val(currentStep);
                            $moduleoverlay.find('progress').attr('max', totalSteps);
                            $moduleoverlay.find('.progress_bar_text').text(`${percentage}%`);
                            $moduleoverlay.find('.progress_span').text(`${percentage}%`);
                            $moduleoverlay.find('.progress_bar_caption').text(caption);

                        } else {
                            caption = response.Caption;
                            currentStep = parseInt(response.CurrentStep);
                            totalSteps = parseInt(response.TotalSteps);
                            percentage = Math.floor((currentStep / totalSteps) * 100);

                            $moduleoverlay.find('progress').val(currentStep);
                            $moduleoverlay.find('progress').attr('max', totalSteps);
                            $moduleoverlay.find('.progress_bar_text').text(`${percentage}%`);
                            $moduleoverlay.find('.progress_span').text(`${percentage}%`);
                            $moduleoverlay.find('.progress_bar_caption').text(caption);
                        }
                    }
                    catch (ex) {
                        console.log('showProgressBarOverlay error: ', ex)
                    }

                    if (currentStep === totalSteps) {
                        progressCompleted = true;
                        if (progressCompleted) {
                            clearInterval(handle);
                            handle = 0
                        }
                    }
                });
        }, 500);

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