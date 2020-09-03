class GetManyModel<T> {
    PageNo: number;
    PageSize: number;
    TotalRows: number;
    Items: Array<T>;
}

class FwAjaxRequest<T> {
    httpMethod: 'GET' | 'POST' | 'PUT' | 'DELETE' = 'GET';
    url: string = '';
    timeout?: number = 15000;
    $elementToBlock?: JQuery = null;
    addAuthorizationHeader?: boolean = true;
    requestHeaders?: any = {};
    data?: T = null;
    requestId?: string = FwAjax.generateUID();
    xmlHttpRequest?: XMLHttpRequest = new XMLHttpRequest();
    cancelable?: boolean = false;
    setWebApiUrl?(relativeUrl: string) {
        let baseUrl = <string>applicationConfig.apiurl;
        while (baseUrl.lastIndexOf('/') === baseUrl.length - 1) {
            baseUrl = baseUrl.substring(0, baseUrl.length - 1);
        }
        while (relativeUrl.indexOf('/') === 0) {
            relativeUrl = relativeUrl.substring(1, relativeUrl.length);
        }
        this.url = baseUrl + '/' + relativeUrl;
    }
    logoutOnAuthFailure?: boolean = true;
    forceJsonParseResponse?= false;
}

interface IRequest {
    [key: string]: FwAjaxRequest<any>;
}

class FwAjaxRejectReason {
    reason: 'HttpStatusCode' | 'Exception' | 'Abort' | 'Timeout' = 'Exception';
    statusCode: number = -1;
    statusText: string = '';
    message: string = '';
    stackTrace: string = '';
    exception: any = null;
}

class FwAjaxClass {
    requests: IRequest = {};
    //----------------------------------------------------------------------------------------------
    async callWebApi<TRequest, TResponse>(options: FwAjaxRequest<TRequest>): Promise<TResponse> {
        return new Promise<TResponse>(async (resolve, reject) => {
            try {
                if (typeof options.timeout === 'undefined' || options.timeout === null) {
                    options.timeout = 15000;
                }
                if (typeof options.$elementToBlock === 'undefined') {
                    options.$elementToBlock = null;
                }
                if (typeof options.addAuthorizationHeader === 'undefined') {
                    options.addAuthorizationHeader = true;
                }
                if (typeof options.requestHeaders === 'undefined') {
                    options.requestHeaders = {};
                }
                if (typeof options.requestId === 'undefined') {
                    options.requestId = FwAjax.generateUID();
                }
                if (typeof options.xmlHttpRequest === 'undefined') {
                    options.xmlHttpRequest = new XMLHttpRequest();
                }
                if (typeof options.cancelable === 'undefined') {
                    options.cancelable = false;
                }
                this.showLoader(options);
                options.xmlHttpRequest.timeout = options.timeout;
                options.xmlHttpRequest.open(options.httpMethod, options.url);
                if (options.httpMethod === 'POST' || options.httpMethod === 'PUT') {
                    options.xmlHttpRequest.setRequestHeader('Content-Type', 'application/json');
                }
                if (typeof options.addAuthorizationHeader === 'undefined' || options.addAuthorizationHeader === true) {
                    options.xmlHttpRequest.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('apiToken'));
                }
                for (let key in options.requestHeaders) {
                    options.xmlHttpRequest.setRequestHeader(key, options.requestHeaders[key]);
                }
                options.xmlHttpRequest.onload = () => {
                    if (typeof FwAjax.requests[options.requestId] !== 'undefined') {
                        this.hideLoader(options);
                        if (options.xmlHttpRequest.status === 500) {
                            return this.rejectFwApiException<TRequest>(options, reject);
                        }
                        if ((options.xmlHttpRequest.status === 401 || options.xmlHttpRequest.status === 403) &&
                            (options.logoutOnAuthFailure === undefined || options.logoutOnAuthFailure === true)) {
                            sessionStorage.clear();
                            window.location.reload(true);
                        }
                        if (options.forceJsonParseResponse ||
                            (options.xmlHttpRequest.getResponseHeader('content-type') !== null && options.xmlHttpRequest.getResponseHeader('content-type').indexOf('application/json') !== -1)) {
                            return resolve(JSON.parse(options.xmlHttpRequest.response));
                        }
                        return resolve(options.xmlHttpRequest.response);
                    }
                };
                options.xmlHttpRequest.ontimeout = () => {
                    this.hideLoader(options);
                    let rejectReason = new FwAjaxRejectReason();
                    rejectReason.reason = 'Timeout';
                    rejectReason.message = `Request timeout expired\n${options.httpMethod}: ${options.url}\n\n${options.xmlHttpRequest.responseText}`;
                    return reject(rejectReason);
                };
                options.xmlHttpRequest.onabort = () => {
                    this.hideLoader(options);
                    let rejectReason = new FwAjaxRejectReason();
                    rejectReason.reason = 'Abort';
                    rejectReason.message = `Request was aborted\nGET: ${options.url}`;
                    reject(rejectReason);
                }
                options.xmlHttpRequest.onerror = () => {
                    this.hideLoader(options);
                    return this.rejectFwApiException<TRequest>(options, reject);
                };
                if (options.httpMethod === 'GET') {
                    options.xmlHttpRequest.send();
                } else {
                    if (options.data != null) {
                        options.xmlHttpRequest.send(JSON.stringify(options.data));
                    } else {
                        options.xmlHttpRequest.send();
                    }
                }
            } catch (ex) {
                this.hideLoader(options);
                let rejectReason = new FwAjaxRejectReason();
                rejectReason.reason = 'Exception';
                rejectReason.exception = ex;
                return reject(rejectReason);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    private rejectFwApiException<TRequest>(options: FwAjaxRequest<TRequest>, reject: (reason: any) => void) {
        let rejectReason = new FwAjaxRejectReason();
        rejectReason.reason = 'HttpStatusCode';
        rejectReason.statusCode = options.xmlHttpRequest.status;
        rejectReason.statusText = options.xmlHttpRequest.statusText;
        if (options.xmlHttpRequest.responseText.indexOf('{') === 0) {
            var apiException = JSON.parse(options.xmlHttpRequest.responseText);
            rejectReason.message = apiException.Message;
            console.error(`${options.xmlHttpRequest.status} ${options.xmlHttpRequest.statusText}\nGET: ${options.url}\n\n${apiException.StackTrace}`);
        } else {
            rejectReason.message = `${options.xmlHttpRequest.status} ${options.xmlHttpRequest.statusText}\nGET: ${options.url}\n\n${options.xmlHttpRequest.responseText}`;
        }
        return reject(rejectReason);
    }
    //----------------------------------------------------------------------------------------------
    abortRequest(requestid) {
        if (typeof FwAjax.requests[requestid] !== 'undefined') {
            FwAjax.requests[requestid].xmlHttpRequest.abort();
            delete FwAjax.requests[requestid];
        }
    }
    //----------------------------------------------------------------------------------------------
    abortAllRequests() {
        for (var requestid in FwAjax.requests) {
            FwAjax.requests[requestid].xmlHttpRequest.abort();
            delete FwAjax.requests[requestid];
        }
    }
    //----------------------------------------------------------------------------------------------
    showLoader(options: FwAjaxRequest<any>) {
        FwAjax.requests[options.requestId] = options;
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        if (isdesktop || (ismobile && (options.$elementToBlock !== null))) {
            if ((typeof options.$elementToBlock === 'object') && (options.$elementToBlock !== null)) {
                if (options.$elementToBlock.hasClass('fwformfield') && options.$elementToBlock.attr('data-type') !== undefined && options.$elementToBlock.attr('data-type') === 'validation') {
                    // hide validation search button and show spinner
                    options.$elementToBlock.find('.btnvalidate').hide();
                    options.$elementToBlock.find('.validation-loader').show();
                } else {
                    options.$elementToBlock.data('ajaxoverlay', this.showPleaseWaitOverlay(options));
                }
            }
        } else if (ismobile) {
            var maxZIndex;
            jQuery('#index-loadingInner').hide();
            maxZIndex = FwFunc.getMaxZ('*');
            jQuery('#index-loading').css('z-index', maxZIndex).show();
            const $elementToBlock = (options.$elementToBlock !== null) ? options.$elementToBlock : jQuery('body');
            $elementToBlock.data('ajaxloadingTimeout', setTimeout(function () {
                $elementToBlock.data('ajaxloadingTimeout', null);
                jQuery('#index-loadingInner').stop().fadeIn(50);
            }, 0));
        }
    }
    //----------------------------------------------------------------------------------------------
    hideLoader(options: FwAjaxRequest<any>) {
        delete FwAjax.requests[options.requestId];
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        if (isdesktop || (ismobile && (options.$elementToBlock !== null))) {
            if ((typeof options.$elementToBlock === 'object') && (options.$elementToBlock !== null)) {
                if (options.$elementToBlock.hasClass('fwformfield') && options.$elementToBlock.attr('data-type') !== undefined && options.$elementToBlock.attr('data-type') === 'validation') {
                    // replace spinner with search again
                    options.$elementToBlock.find('.validation-loader').hide();
                    options.$elementToBlock.find('.btnvalidate').show();
                } else {
                    this.hideOverlay(options);
                }
            }
        } else if (ismobile) {
            if ((typeof options.$elementToBlock === 'object') && (options.$elementToBlock !== null)) {
                if (options.$elementToBlock.data('ajaxloadingTimeout')) {
                    clearTimeout(options.$elementToBlock.data('ajaxloadingTimeout'));
                }
            }
            jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                jQuery('#index-loading').css('z-index', 0).hide();
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    generateUID() {
        var firstPart: any = (Math.random() * 46656) | 0;
        var secondPart: any = (Math.random() * 46656) | 0;
        firstPart = ("000" + firstPart.toString(36)).slice(-3);
        secondPart = ("000" + secondPart.toString(36)).slice(-3);
        return firstPart + secondPart;
    }
    //----------------------------------------------------------------------------------------------
    showPleaseWaitOverlay(options: FwAjaxRequest<any>) {
        var html, $moduleoverlay, maxZIndex;

        html = [];
        html.push('<div class="fwoverlay-center pleasewait">');
        html.push('<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'theme/fwimages/icons/128/loading.001.gif" />');
        html.push('<div class="message">Please Wait</div>');
        if (typeof options.cancelable && options.requestId === 'string') {
            html.push('<button class="btnCancel">Cancel</button>');
        }
        html.push('</div>');
        html = html.join('');

        $moduleoverlay = jQuery('<div class="fwoverlay">');
        $moduleoverlay.css('z-index', maxZIndex);
        maxZIndex = FwFunc.getMaxZ('*');
        $moduleoverlay.html(html);
        $moduleoverlay.on('click', function () {
            $moduleoverlay.addClass('clicked');
        });
        $moduleoverlay.on('click', '.btnCancel', (event) => {
            event.stopPropagation();
            this.hideOverlay($moduleoverlay);
            this.abortRequest(options.requestId);
        });
        options.$elementToBlock.css('position', 'relative').append($moduleoverlay);

        return $moduleoverlay;
    }
    //----------------------------------------------------------------------------------------------
    hideOverlay(options: FwAjaxRequest<any>) {
        let $overlay = options.$elementToBlock.data('ajaxoverlay');
        if (typeof $overlay !== 'undefined') {
            $overlay.remove();
        }
    }
    //----------------------------------------------------------------------------------------------
}

var FwAjax = new FwAjaxClass();
