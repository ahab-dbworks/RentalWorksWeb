import { Ajax } from './Ajax';

export abstract class WebpackReport {
    renderReportCompleted: boolean = false;
    renderReportFailed: boolean = false;
    headerHtml: string = '';
    footerHtml: string = '';
    action: ActionType; 

    // experimental
    renderStatus: string = 'Stopped';
    renderProgress: number = 0;

    constructor() {
        window.addEventListener('unload', (ev: Event) => {
            ev.stopImmediatePropagation();
            if (window.opener != null) {
                window.opener.postMessage('ReportUnload', '*'); //sending message back to requesting page to notify new window is closed
            }
        });
        window.addEventListener('load', (ev: Event) => {
            const reportURL: any = ev.srcElement.baseURI;
            if (window.opener != null) {
                window.opener.postMessage(reportURL, '*'); //sending message back to requesting page to notify new window is loaded
            }
        }); 

        window.addEventListener('message', (ev: MessageEvent) => {
            if (typeof ev.data.action !== 'undefined') {
                let message = <ReportPageMessage>ev.data;
                this.processMessage(message);
                //if (message.action === 'Preview') {
                //    sessionStorage.setItem('message', JSON.stringify(message));
                //}
            }
        });
    }

    processMessage(message: ReportPageMessage) {
        this.action = message.action;
        switch (message.action) {
            case 'Preview':
                (<WebpackReport>(<any>window).report).renderReport(message.apiUrl, message.authorizationHeader, message.request.parameters);
                break;
            case 'PrintHtml':
                (<WebpackReport>(<any>window).report).renderReport(message.apiUrl, message.authorizationHeader, message.request.parameters);
                break;
        }
    }
    
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        let htmlElements = document.getElementsByTagName('html');
        if (this.action === 'Preview' || this.action === 'PrintHtml') {
            htmlElements[0].classList.add('preview');
        } else {
            htmlElements[0].classList.add('pdf');
        }
    }

    onRenderReportCompleted() {
        if (!this.renderReportCompleted) {
            this.renderReportCompleted = true;
            if (this.action === 'PrintHtml') {
                window.print();
            }

            // experimental
            this.renderStatus = 'Completed';
            this.renderProgress = 100;
        }
    }

    onRenderReportFailed(err: any) {
        if (!this.renderReportCompleted) {
            this.renderReportCompleted = true;
            this.renderReportFailed = true;

            // experimental
            this.renderStatus = 'Failed';
            this.renderProgress = 100;

            Ajax.logError('An error occured while rendering the report.', err);
        }
    }
}

export type RenderMode = 'Html' | 'Pdf' | 'Email';
export type ActionType =  'None' | 'Preview' | 'PrintHtml';

export class ReportPageMessage {
    action: ActionType = 'None';
    apiUrl: string = '';
    authorizationHeader: string = '';
    request: RenderRequest = new RenderRequest();
}

export class RenderRequest {
    renderMode: RenderMode = 'Html';
    parameters: any = {};
    email = new EmailInfo();
    downloadPdfAsAttachment: boolean = false;
}

export class RenderResponse {
    renderMode: RenderMode;
    htmlReportUrl: string = '';
    pdfReportUrl: string = '';
}

export class EmailInfo {
    from: string = '';
    to: string = '';
    cc: string = '';
    subject: string = '';
    body: string = '';
}