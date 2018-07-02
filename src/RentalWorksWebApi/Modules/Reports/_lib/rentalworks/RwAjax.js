var RwAjaxClass = (function () {
    function RwAjaxClass() {
    }
    RwAjaxClass.prototype.logError = function (message, err) {
        console.log(message, err.message);
    };
    RwAjaxClass.prototype.apiGet = function (url, authorizationHeader) {
        return new Promise(function (resolve, reject) {
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.onload = function () {
                if (req.status == 200) {
                    resolve(JSON.parse(req.response));
                }
                else {
                    reject(Error(req.statusText));
                    console.log(req.responseText);
                }
            };
            req.onerror = function () {
                reject(Error("Network Error"));
            };
            req.send();
        });
    };
    RwAjaxClass.prototype.apiPost = function (url, authorizationHeader, data) {
        return new Promise(function (resolve, reject) {
            var req = new XMLHttpRequest();
            req.open('POST', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.setRequestHeader('Content-Type', 'application/json');
            req.onload = function () {
                if (req.status == 200) {
                    resolve(JSON.parse(req.response));
                }
                else {
                    reject(Error(req.statusText));
                    console.log(req.responseText);
                }
            };
            req.onerror = function () {
                reject(Error("Network Error"));
            };
            req.send(JSON.stringify(data));
        });
    };
    return RwAjaxClass;
}());
var RwAjax = new RwAjaxClass();
//# sourceMappingURL=RwAjax.js.map