class FwOverlay {
    //----------------------------------------------------------------------------------------------
    static showPleaseWaitOverlay($appendToElement, requestid) {
        const html: Array<string> = [];
        html.push('<div class="fwoverlay-center pleasewait">');
        html.push(`<img src="${applicationConfig.appbaseurl}${applicationConfig.appvirtualdirectory}theme/fwimages/icons/128/loading.001.gif" />`);
        html.push('<div class="message">Please Wait</div>');
        //if (typeof requestid === 'string') {
        //    html.push('<button class="btnCancel">Cancel</button>');
        //}
        html.push('</div>');

        const $moduleoverlay = jQuery('<div class="fwoverlay">');
        const maxZIndex = FwFunc.getMaxZ('*');
        $moduleoverlay.html(html.join(''));
        $moduleoverlay.css('z-index', maxZIndex);
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
        let currentStep: number = 0;
        let totalSteps: number = 100;
        let caption: string;
        let percentage: any;

        const fullurl = `${applicationConfig.apiurl}api/v1/progressmeter/${progressBarSessionId}`;
        let progressCompleted: boolean = false;

        const ajaxOptions: JQuery.AjaxSettings<any> = {
            method: 'GET',
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                Authorization: `Bearer ${sessionStorage.getItem('apiToken')}`
            },
            context: {
                requestid: FwAppData.generateUUID()
            },
        };
        const html: Array<string> = [];
        html.push(`<progress max="100" value="100"><span class="progress_span">0</span></progress>`);
        html.push(`<div class="progress_bar_text"></div>`);
        html.push(`<div class="progress_bar_caption">Initiating your request...</div>`);

        const $moduleoverlay = jQuery(`<div class="progress_bar">`);
        $moduleoverlay.html(html.join(''));
        $appendToElement.css('position', 'relative').append($moduleoverlay);

        let handle = setInterval(() => {
            jQuery.ajax(ajaxOptions)
                .done(response => {
                    try {
                        if ((isNaN(response.CurrentStep)) || (response.CurrentStep === 'undefined') || (response.CurrentStep === undefined)) {
                            caption = 'Processing...';
                            currentStep += 2.5;
                            $moduleoverlay.find('progress').val(100);
                            $moduleoverlay.find('progress').attr('max', 100);
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
                        FwFunc.showError(ex);
                    }

                    if (currentStep >= totalSteps) {
                        progressCompleted = true;
                        if (progressCompleted) {
                            clearInterval(handle);
                            handle = 0;
                        }
                    }
                });
        }, 500);

        return $moduleoverlay;
    }
    //----------------------------------------------------------------------------------------------
    static showErrorOverlay($appendToElement) {
        const overlayid = FwControl.generateControlId('overlay');
        const overlaycount = $appendToElement.data('overlayoutcount');
        if (typeof overlaycount === 'undefined') {
            $appendToElement.data('overlayoutcount', 1);
            $appendToElement.attr('data-positionbeforeoverlay', $appendToElement.css('position'));
        } else {
            $appendToElement.data('overlayoutcount', overlaycount + 1);
        }

        const html: Array<string> = [];
        html.push('<div class="fwoverlay-center error">');
        html.push('<div style="height:128px;"></div>');
        html.push('<div class="message">Error</div>');
        html.push('</div>');

        let $moduleoverlay = $appendToElement.find('.fwoverlay');
        if ($moduleoverlay.length === 0) {
            $moduleoverlay = jQuery(`<div id="${overlayid}" class="fwoverlay">`);
            $moduleoverlay.html(html.join(''));
            $appendToElement.css('position', 'relative').append($moduleoverlay);
        } else {
            $moduleoverlay.html(html.join(''));
            $appendToElement.css('position', 'relative').append(html);
        }

        return overlayid;
    }
    //----------------------------------------------------------------------------------------------
    static hideOverlay($overlay): void {
        $overlay.remove();
    }
    //----------------------------------------------------------------------------------------------
}