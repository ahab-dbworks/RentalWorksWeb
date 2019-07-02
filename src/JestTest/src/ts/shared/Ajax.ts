export class AjaxOptions {
    url: string;
    headers?: any = {};
    data?: any = {};
    request: XMLHttpRequest = new XMLHttpRequest();
}

export class Ajax {
    static logError(message: string, err: string) {
        console.error(message, err);
    }

    static get<T>(options: AjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            options.request.open('GET', options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    Ajax.logError(`GET: ${options.url}`, options.request.status.toString() + ' ' + options.request.statusText + ' ' + options.request.responseText);
                    reject(Error('Network Error'));
                    //reject(Error(JSON.stringify(options.request.response)));
                }
            };
            options.request.onerror = function () {
                reject(Error('Network Error'));
            };
            options.request.send();
        });
    }

    static postJson<T>(options: AjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            //console.log('postJson:', options);
            options.request.open('POST', options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    Ajax.logError(`Network Error: POST ${options.url}`, options.request.responseText);
                    reject(`Network Error: POST ${options.url} - ${options.request.status} ${options.request.statusText} ${options.request.responseText}`);
                }
            };
            options.request.onerror = function () {
                Ajax.logError(`POST ${options.request.status}: ${options.url}`, options.request.responseText);
                reject(`Network Error: POST ${options.request.status}: ${options.url}\n${options.request.responseText}`);
            };
            options.request.send(JSON.stringify(options.data));
        });
    }

    static putJson<T>(options: AjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            options.request.open('PUT', options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    //Ajax.logError(`POST: ${options.url}`, req.responseText);
                    reject(Error(options.request.statusText));
                }
            };
            options.request.onerror = function () {
                Ajax.logError(`PUT: ${options.url}`, options.request.responseText);
                reject(Error('Network Error'));
            };
            options.request.send(JSON.stringify(options.data));
        });
    }

    static delete<T>(options: AjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            options.request.open('DELETE', options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    //Ajax.logError(`POST: ${options.url}`, req.responseText);
                    reject(Error(options.request.statusText));
                }
            };
            options.request.onerror = function () {
                Ajax.logError(`DELETE: ${options.url}`, options.request.responseText);
                reject(Error('Network Error'));
            };
            options.request.send(JSON.stringify(options.data));
        });
    }
}
