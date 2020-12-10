export class Ajax {
    static logError(message: string, err: string) {
        (<any>window).loggedError = true;
        try {
            // if err is a JSON object and StatusCode is 500
            const obj: any = JSON.parse(err);
            if (obj.StatusCode && obj.StatusCode === 500) {
                let html: string | string[] = [];
                html.push(`<div style="margin:24px;">`);
                html.push(`  <div style="margin:10px 0 0 0;font-size:16px;">${obj.Message}</div>`);
                html.push(`  <div style="margin:10px 0 0 0;font-size:11px;border:1px solid #777777;background-color:#efefef;padding:10px;">`);
                html.push(`    <div style="font-size:14px">${message}</div>`);
                if (typeof obj.StackTrace === 'string' && obj.StackTrace.length > 0) {
                    let stackTrace = <string>obj.StackTrace.replace('\r\n', '\n');
                    let stackTraceLines = stackTrace.split('\n');
                    for (let i = 0; i < stackTraceLines.length; i++) {
                        html.push(`    <div style="margin:2px 0 0 2.5em;text-indent:-2em;">${stackTraceLines[i]}</div>`)
                    }
                }
                html.push(`  </div>`);
                html.push(`</div>`);
                html = html.join('\n');
                document.write(html);
                return;
            }
        }
        catch { }

        // default error rendering
        document.write(`<div stye="margin:24px;">`);
        document.write(`  <div>${message}</div>`);
        document.write(`  <div>${err}</div>`);
        document.write(`</div>`);
    }

    static get<T>(url: string, authorizationHeader: string): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            Ajax.showLoader();
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.onload = function () {
                if (req.status == 200) {
                    Ajax.hideLoader();
                    resolve(JSON.parse(req.response));
                }
                else {
                    Ajax.hideLoader();
                    Ajax.logError(`${req.status} ${req.statusText} - GET: ${url}`, req.responseText);
                    reject(Error(req.statusText));
                }
            };
            req.onerror = function () {
                Ajax.hideLoader();
                Ajax.logError(`${req.status} ${req.statusText} - GET: ${url}`, req.responseText);
                reject(Error("Network Error"));
            };
            req.send();
        });
    }

    static post<T>(url: string, authorizationHeader: string, data: any): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            Ajax.showLoader();
            var req = new XMLHttpRequest();
            req.open('POST', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.setRequestHeader('Content-Type', 'application/json');
            req.onload = function () {
                if (req.status == 200) {
                    Ajax.hideLoader();
                    resolve(JSON.parse(req.response));
                }
                else {
                    Ajax.hideLoader();
                    Ajax.logError(`${req.status} ${req.statusText} - POST: ${url}`, req.responseText);
                    reject(Error(req.statusText));
                }
            };
            req.onerror = function () {
                Ajax.hideLoader();
                Ajax.logError(`${req.status} ${req.statusText} - POST: ${url}`, req.responseText);
                reject(Error("Network Error"));
            };
            req.send(JSON.stringify(data));
        });
    }

    static showLoader() {
        var style = document.createElement('style');
        style.id = "ajaxStyles";
        style.innerHTML = `
            html, body {
                min-height:100% !important;
            }
            .loader-container {
                position: absolute;
                top: 0;
                right: 0;
                left: 0;
                bottom: 0;
                display: flex;
                align-items: center;
                justify-content: center;
            }

            .loader {
                border: 16px solid #f3f3f3; /* Light grey */
                border-top: 16px solid #3498db; /* Blue */
                border-radius: 50%;
                width: 120px;
                height: 120px;
                animation: spin 2s linear infinite;
            }

            @keyframes spin {
                0% {
                    transform: rotate(0deg);
                }

                100% {
                    transform: rotate(360deg);
                }
            }`;
        document.body.appendChild(style); // append in body
        //document.head.appendChild(style); // append in head

        let loaderContainer = document.createElement('div');
        loaderContainer.classList.add('loader-container');
        var loader = document.createElement('div');
        loader.classList.add('loader');
        loaderContainer.appendChild(loader);
        var bodys = document.getElementsByTagName('body');
        bodys[0].appendChild(loaderContainer);
    }

    static hideLoader() {
        let loaders = document.getElementsByClassName('loader-container');
        for (let i = 0; i < loaders.length; i++) {
            loaders[i].remove();
        }
        document.getElementById('ajaxStyles').remove();
    }
}
