export class Ajax {
    static logError(message, err) {
        console.log(message, err.message);
    }
    static get(url, authorizationHeader) {
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
    }
    static post(url, authorizationHeader, data) {
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
    }
}
//# sourceMappingURL=Ajax.js.map