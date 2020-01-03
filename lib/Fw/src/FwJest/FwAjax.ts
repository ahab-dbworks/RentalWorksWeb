export class FwAjaxOptions {
    url: string;
    headers?: any = {};
    data?: any = {};
    request: XMLHttpRequest = new XMLHttpRequest();
}

export class FwAjax {
    private static getFormattedResponseText(responseText: string): string {
        let result = responseText;
        if (responseText.startsWith('{')) {
            try {
                const responseObj = JSON.parse(responseText);
                //responseText = JSON.stringify(responseObj.Message, null, 4);
                result = responseObj.Message + '\n' + responseObj.StackTrace;
            }
            catch {

            }
        }
        return result;
    }

    static get<T>(options: FwAjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            const METHOD = 'GET';
            options.request.open(METHOD, options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 400) {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
                else if (options.request.status == 500) {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
                else if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
            };
            options.request.onerror = function () {
                const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
            };
            options.request.send();
        });
    }

    static postJson<T>(options: FwAjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            const METHOD = 'POST';
            //console.log('postJson:', options);
            options.request.open(METHOD, options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 400) {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
                else if (options.request.status == 500) {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
                else if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else if (options.request.status == 400) {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
                else if (options.request.status == 500) {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
                else {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
            };
            options.request.onerror = function () {
                const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
            };
            options.request.send(JSON.stringify(options.data));
        });
    }

    static putJson<T>(options: FwAjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            const METHOD = 'PUT';
            options.request.open(METHOD, options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 400) {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
                else if (options.request.status == 500) {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
                else if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
            };
            options.request.onerror = function () {
                const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
            };
            options.request.send(JSON.stringify(options.data));
        });
    }

    static delete<T>(options: FwAjaxOptions): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            const METHOD = 'DELETE';
            options.request.open(METHOD, options.url);
            if (typeof options.headers !== 'undefined') {
                for (let key in options.headers) {
                    options.request.setRequestHeader(key, options.headers[key]);
                }
            }
            options.request.onload = function () {
                if (options.request.status == 400) {
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${options.request.responseText}`);
                }
                else if (options.request.status == 500) {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
                else if (options.request.status == 200) {
                    resolve(JSON.parse(options.request.response));
                }
                else {
                    const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                    reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
                }
            };
            options.request.onerror = function () {
                const responseText = FwAjax.getFormattedResponseText(options.request.responseText);
                reject(`${options.request.status} ${options.request.statusText}: ${METHOD} ${options.url}\n${responseText}`);
            };
            options.request.send(JSON.stringify(options.data));
        });
    }
}

export class FwGetResponse<T> {
    Items: T[]
    PageNo: number
    PageSize: number
    TotalRows: number
}
