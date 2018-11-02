var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var GetManyModel = (function () {
    function GetManyModel() {
    }
    return GetManyModel;
}());
var FwAjaxRequest = (function () {
    function FwAjaxRequest() {
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
    return FwAjaxRequest;
}());
var FwAjaxRejectReason = (function () {
    function FwAjaxRejectReason() {
        this.reason = 'Exception';
        this.statusCode = -1;
        this.statusText = '';
        this.message = '';
        this.stackTrace = '';
        this.exception = null;
    }
    return FwAjaxRejectReason;
}());
var FwAjaxClass = (function () {
    function FwAjaxClass() {
        this.requests = {};
    }
    FwAjaxClass.prototype.callWebApi = function (options) {
        return __awaiter(this, void 0, void 0, function () {
            var _this = this;
            return __generator(this, function (_a) {
                return [2, new Promise(function (resolve, reject) {
                        try {
                            _this.showLoader(options);
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
                            for (var key in options.requestHeaders) {
                                options.request.setRequestHeader(key, options.requestHeaders[key]);
                            }
                            options.request.onload = function () {
                                if (typeof FwAjax.requests[options.requestId] !== 'undefined') {
                                    if (options.request.status == 200) {
                                        _this.hideLoader(options);
                                        resolve(JSON.parse(options.request.response));
                                    }
                                    else {
                                        _this.hideLoader(options);
                                        var rejectReason = new FwAjaxRejectReason();
                                        rejectReason.reason = 'HttpStatusCode';
                                        rejectReason.statusCode = options.request.status;
                                        rejectReason.statusText = options.request.statusText;
                                        rejectReason.message = options.request.status + " " + options.request.statusText + "\nGET: " + options.url + "\n\n" + options.request.responseText;
                                        reject(rejectReason);
                                    }
                                }
                            };
                            options.request.ontimeout = function () {
                                if (options.$elementToBlock !== null) {
                                    _this.hideLoader(options);
                                }
                                reject("Request timeout expired\nGET: " + options.url + "\n\n" + options.request.responseText);
                                var rejectReason = new FwAjaxRejectReason();
                                rejectReason.reason = 'Timeout';
                                rejectReason.message = "Request timeout expired\n" + options.httpMethod + ": " + options.url + "\n\n" + options.request.responseText;
                                reject(rejectReason);
                            };
                            options.request.onabort = function () {
                                _this.hideLoader(options);
                                var rejectReason = new FwAjaxRejectReason();
                                rejectReason.reason = 'Abort';
                                rejectReason.message = "Request was aborted\nGET: " + options.url;
                                reject(rejectReason);
                            };
                            options.request.onerror = function () {
                                _this.hideLoader(options);
                                reject(options.request.status + " " + options.request.statusText + "\n" + options.httpMethod + ": " + options.url + "\n\n" + options.request.responseText);
                                var rejectReason = new FwAjaxRejectReason();
                                rejectReason.reason = 'Exception';
                                rejectReason.message = options.request.status + " " + options.request.statusText + "\n" + options.httpMethod + ": " + options.url + "\n\n" + options.request.responseText;
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
                            var rejectReason = new FwAjaxRejectReason();
                            rejectReason.reason = 'Exception';
                            rejectReason.exception = ex;
                            reject(rejectReason);
                        }
                    })];
            });
        });
    };
    FwAjaxClass.prototype.abortRequest = function (requestid) {
        if (typeof FwAjax.requests[requestid] !== 'undefined') {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    };
    FwAjaxClass.prototype.abortAllRequests = function () {
        for (var requestid in FwAjax.requests) {
            FwAjax.requests[requestid].request.abort();
            delete FwAjax.requests[requestid];
        }
    };
    FwAjaxClass.prototype.showLoader = function (options) {
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
    };
    FwAjaxClass.prototype.hideLoader = function (options) {
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
    };
    FwAjaxClass.prototype.generateUID = function () {
        var firstPart = (Math.random() * 46656) | 0;
        var secondPart = (Math.random() * 46656) | 0;
        firstPart = ("000" + firstPart.toString(36)).slice(-3);
        secondPart = ("000" + secondPart.toString(36)).slice(-3);
        return firstPart + secondPart;
    };
    FwAjaxClass.prototype.showPleaseWaitOverlay = function (options) {
        var _this = this;
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
        $moduleoverlay.on('click', '.btnCancel', function (event) {
            event.stopPropagation();
            _this.hideOverlay($moduleoverlay);
            _this.abortRequest(options.requestId);
        });
        options.$elementToBlock.css('position', 'relative').append($moduleoverlay);
        return $moduleoverlay;
    };
    FwAjaxClass.prototype.hideOverlay = function (options) {
        var $overlay = options.$elementToBlock.data('ajaxoverlay');
        $overlay.remove();
    };
    return FwAjaxClass;
}());
var FwAjax = new FwAjaxClass();
//# sourceMappingURL=FwAjax.js.map