var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class GetManyModel {
}
class FwAjaxRequest {
    constructor() {
        this.httpMethod = 'GET';
        this.url = '';
        this.timeout = 15000;
        this.$elementToBlock = null;
        this.addAuthorizationHeader = true;
        this.requestHeaders = {};
        this.data = null;
        this.requestId = FwAjax.generateUID();
        this.request = new XMLHttpRequest();
        this.cancelable = false;
    }
}
class FwAjaxRejectReason {
    constructor() {
        this.reason = 'Exception';
        this.statusCode = -1;
        this.statusText = '';
        this.message = '';
        this.stackTrace = '';
        this.exception = null;
    }
}
class FwAjaxClass {
    constructor() {
        this.requests = {};
    }
    callWebApi(options) {
        return __awaiter(this, void 0, void 0, function* () {
            return new Promise((resolve, reject) => {
                try {
                    this.showLoader(options);
                    if (options.timeout === null) {
                        options.timeout = 15000;
                    }
                    options.request.timeout = options.timeout;
                    options.request.open(options.httpMethod, options.url);
                    if (options.httpMethod === 'POST' || options.httpMethod === 'PUT') {
                        options.request.setRequestHeader('Content-Type', 'application/json');
                    }
                    if (options.addAuthorizationHeader) {
                        options.request.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('apiToken'));
                    }
                    for (let key in options.requestHeaders) {
                        options.request.setRequestHeader(key, options.requestHeaders[key]);
                    }
                    options.request.onload = () => {
                        if (typeof FwAjax.requests[options.requestId] !== 'undefined') {
                            if (options.request.status == 200) {
                                this.hideLoader(options);
                                resolve(JSON.parse(options.request.response));
                            }
                            else {
                                this.hideLoader(options);
                                let rejectReason = new FwAjaxRejectReason();
                                rejectReason.reason = 'HttpStatusCode';
                                rejectReason.statusCode = options.request.status;
                                rejectReason.statusText = options.request.statusText;
                                rejectReason.message = `${options.request.status} ${options.request.statusText}\nGET: ${options.url}\n\n${options.request.responseText}`;
                                reject(rejectReason);
                            }
                        }
                    };
                    options.request.ontimeout = () => {
                        if (options.$elementToBlock !== null) {
                            this.hideLoader(options);
                        }
                        reject(`Request timeout expired\nGET: ${options.url}\n\n${options.request.responseText}`);
                        let rejectReason = new FwAjaxRejectReason();
                        rejectReason.reason = 'Timeout';
                        rejectReason.message = `Request timeout expired\n${options.httpMethod}: ${options.url}\n\n${options.request.responseText}`;
                        reject(rejectReason);
                    };
                    options.request.onabort = () => {
                        this.hideLoader(options);
                        let rejectReason = new FwAjaxRejectReason();
                        rejectReason.reason = 'Abort';
                        rejectReason.message = `Request was aborted\nGET: ${options.url}`;
                        reject(rejectReason);
                    };
                    options.request.onerror = () => {
                        this.hideLoader(options);
                        reject(`${options.request.status} ${options.request.statusText}\n${options.httpMethod}: ${options.url}\n\n${options.request.responseText}`);
                        let rejectReason = new FwAjaxRejectReason();
                        rejectReason.reason = 'Exception';
                        rejectReason.message = `${options.request.status} ${options.request.statusText}\n${options.httpMethod}: ${options.url}\n\n${options.request.responseText}`;
                        reject(rejectReason);
                    };
                    if (options.httpMethod === 'GET') {
                        options.request.send();
                    }
                    else if (options.httpMethod === 'POST') {
                        if (options.data != null) {
                            options.request.send(JSON.stringify(options.data));
                        }
                        else {
                            options.request.send();
                        }
                    }
                }
                catch (ex) {
                    let rejectReason = new FwAjaxRejectReason();
                    rejectReason.reason = 'Exception';
                    rejectReason.exception = ex;
                    reject(rejectReason);
                }
            });
        });
    }
    abortRequest(requestid) {
        if (typeof FwAjax.requests[requestid] !== 'undefined') {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    }
    abortAllRequests() {
        for (var requestid in FwAjax.requests) {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    }
    showLoader(options) {
        FwAjax.requests[options.requestId] = options;
        if (options.$elementToBlock !== null) {
            var isdesktop = jQuery('html').hasClass('desktop');
            var ismobile = jQuery('html').hasClass('mobile');
            if (isdesktop || (ismobile && (options.$elementToBlock !== null))) {
                if ((typeof options.$elementToBlock === 'object') && (options.$elementToBlock !== null)) {
                    if (options.$elementToBlock.hasClass('fwformfield') && options.$elementToBlock.attr('data-type') !== undefined && options.$elementToBlock.attr('data-type') === 'validation') {
                        options.$elementToBlock.find('.btnvalidate').hide();
                        options.$elementToBlock.find('.validation-loader').show();
                    }
                    else {
                        options.$elementToBlock.data('ajaxoverlay', this.showPleaseWaitOverlay(options));
                    }
                }
            }
            else if (ismobile) {
                var maxZIndex;
                jQuery('#index-loadingInner').hide();
                maxZIndex = FwFunc.getMaxZ('*');
                jQuery('#index-loading').css('z-index', maxZIndex).show();
                options.$elementToBlock.data('ajaxloadingTimeout', setTimeout(function () {
                    options.$elementToBlock.data('ajaxloadingTimeout', null);
                    jQuery('#index-loadingInner').stop().fadeIn(50);
                }, 0));
            }
        }
    }
    hideLoader(options) {
        delete FwAjax.requests[options.requestId];
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        if (isdesktop || (ismobile && (options.$elementToBlock !== null))) {
            if ((typeof options.$elementToBlock === 'object') && (options.$elementToBlock !== null)) {
                if (options.$elementToBlock.hasClass('fwformfield') && options.$elementToBlock.attr('data-type') !== undefined && options.$elementToBlock.attr('data-type') === 'validation') {
                    options.$elementToBlock.find('.validation-loader').hide();
                    options.$elementToBlock.find('.btnvalidate').show();
                }
                else {
                    this.hideOverlay(options);
                }
            }
        }
        else if (ismobile) {
            if (options.$elementToBlock.data('ajaxloadingTimeout')) {
                clearTimeout(options.$elementToBlock.data('ajaxloadingTimeout'));
            }
            jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                jQuery('#index-loading').css('z-index', 0).hide();
            });
        }
    }
    generateUID() {
        var firstPart = (Math.random() * 46656) | 0;
        var secondPart = (Math.random() * 46656) | 0;
        firstPart = ("000" + firstPart.toString(36)).slice(-3);
        secondPart = ("000" + secondPart.toString(36)).slice(-3);
        return firstPart + secondPart;
    }
    showPleaseWaitOverlay(options) {
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
    hideOverlay(options) {
        let $overlay = options.$elementToBlock.data('ajaxoverlay');
        $overlay.remove();
    }
}
var FwAjax = new FwAjaxClass();
//# sourceMappingURL=FwAjax.js.map