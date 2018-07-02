class RwAjaxClass {
    logError(message, err: any) {
        console.log(message, err.message);
    }

    apiGet<T>(url: string, authorizationHeader: string): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
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

    apiPost<T>(url: string, authorizationHeader: string, data: any): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
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

var RwAjax = new RwAjaxClass();