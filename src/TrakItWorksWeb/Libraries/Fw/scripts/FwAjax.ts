class GetManyModel<T> {
    PageNo: number;
    PageSize: number;
    TotalRows: number;
    Items: Array<T>;
}

class FwAjaxRequest {
    httpMethod: 'GET' | 'POST' | 'PUT' | 'DELETE' = 'GET';
    url: string = '';
    timeout: number = 15000;
    $elementToBlock: JQuery = null;
    addAuthorizationHeader: boolean = true;
    requestHeaders: any = {};
    data: any = null;
    requestId: string = FwAjax.generateUID();
    request: XMLHttpRequest = new XMLHttpRequest();
    cancelable: boolean = false;
}

interface IRequest {
   [key: string]: FwAjaxRequest;
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
    async callWebApi<T>(options: FwAjaxRequest): Promise<T> {
        return new Promise<T>((resolve, reject) => {
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
                }
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
                } else if (options.httpMethod === 'POST') {
                    if (options.data != null) {
                        options.request.send(JSON.stringify(options.data));
                    } else {
                        options.request.send();
                    }
                }
            } catch (ex) {
                let rejectReason = new FwAjaxRejectReason();
                rejectReason.reason = 'Exception';
                rejectReason.exception = ex;
                reject(rejectReason);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    abortRequest(requestid) {
        if (typeof FwAjax.requests[requestid] !== 'undefined') {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    }
    //----------------------------------------------------------------------------------------------
    abortAllRequests() {
        for (var requestid in FwAjax.requests) {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    }
    //----------------------------------------------------------------------------------------------
    showLoader(options: FwAjaxRequest) {
        FwAjax.requests[options.requestId] = options;
        if (options.$elementToBlock !== null) {
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
                options.$elementToBlock.data('ajaxloadingTimeout', setTimeout(function () {
                    options.$elementToBlock.data('ajaxloadingTimeout', null);
                    jQuery('#index-loadingInner').stop().fadeIn(50);
                }, 0));
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    hideLoader(options: FwAjaxRequest) {
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
            if (options.$elementToBlock.data('ajaxloadingTimeout')) {
                clearTimeout(options.$elementToBlock.data('ajaxloadingTimeout'));
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
    showPleaseWaitOverlay(options: FwAjaxRequest) {
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
    hideOverlay(options: FwAjaxRequest) {
        let $overlay = options.$elementToBlock.data('ajaxoverlay');
        $overlay.remove();
    }
    //----------------------------------------------------------------------------------------------
//    showLoader() {
//        var style = document.createElement('style');
//        style.id = "ajaxStyles";
//        style.innerHTML = `
//html, body {
//    min-height:100% !important;
//}
//.loader-container {
//    position: absolute;
//    top: 0;
//    right: 0;
//    left: 0;
//    bottom: 0;
//    display: flex;
//    align-items: center;
//    justify-content: center;
//}

//.loader {
//    border: 16px solid #f3f3f3; /* Light grey */
//    border-top: 16px solid #3498db; /* Blue */
//    border-radius: 50%;
//    width: 120px;
//    height: 120px;
//    animation: spin 2s linear infinite;
//}

//@keyframes spin {
//    0% {
//        transform: rotate(0deg);
//    }

//    100% {
//        transform: rotate(360deg);
//    }
//}
//        `;
//        document.body.appendChild(style); // append in body
//        //document.head.appendChild(style); // append in head

//        let loaderContainer = document.createElement('div');
//        loaderContainer.classList.add('loader-container');
//        var loader = document.createElement('div');
//        loader.classList.add('loader');
//        loaderContainer.appendChild(loader);
//        var bodys = document.getElementsByTagName('body');
//        bodys[0].appendChild(loaderContainer);
//    }

//    hideLoader() {
//        let loaders = document.getElementsByClassName('loader-container');
//        for (let i = 0; i < loaders.length; i++) {
//            loaders[i].remove();
//        }
//        document.getElementById('ajaxStyles').remove();
//    }
}

var FwAjax = new FwAjaxClass();
