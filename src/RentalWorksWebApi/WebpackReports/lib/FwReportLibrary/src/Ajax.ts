export class Ajax {
    static logError(message: string, err: string) {
        //console.log(message, err.message);
        document.write(`<div>${message}</div>`);
        document.write(`<div>${err}</div>`);
    }

    static get<T>(url: string, authorizationHeader: string): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.onload = function () {
                if (req.status == 200) {
                    resolve(JSON.parse(req.response));
                }
                else {
                    Ajax.logError(`GET: ${url}`, req.responseText);
                    reject(Error(req.statusText));
                }
            };
            req.onerror = function () {
                Ajax.logError(`GET: ${url}`, req.responseText);
                reject(Error("Network Error"));
            };
            req.send();
        });
    }

    static post<T>(url: string, authorizationHeader: string, data: any): Promise<T> {
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
                    Ajax.logError(`POST: ${url}`, req.responseText);
                    reject(Error(req.statusText));
                }
            };
            req.onerror = function () {
                Ajax.logError(`POST: ${url}`, req.responseText);
                reject(Error("Network Error"));
            };
            req.send(JSON.stringify(data));
        });
    }
}
