﻿import { Ajax } from './Ajax';
import * as Handlebars from 'handlebars/dist/cjs/handlebars';
import { HandlebarsHelpers } from './HandlebarsHelpers';
import moment from 'moment';
export abstract class WebpackReport {
    renderReportCompleted: boolean = false;
    renderReportFailed: boolean = false;
    footerHtml: string = '';
    action: ActionType;
    //----------------------------------------------------------------------------------------------
    constructor() {
        window.addEventListener('unload', (ev: Event) => {
            ev.stopImmediatePropagation();
            if (window.opener != null) {
                window.opener.postMessage('ReportUnload', '*'); // postMessage to report page to notify new window (rendered report) is closed
            }
        });
        window.addEventListener('load', (ev: Event) => {
            const srcEl: any = ev.srcElement;
            const reportURL: any = srcEl.baseURI;
            if (window.opener != null) {
                window.opener.postMessage(reportURL, '*'); // postMessage to report page to notify new window (rendered report) is loaded
            }
        });
        window.addEventListener('message', (ev: MessageEvent) => {
            if (typeof ev.data.action !== 'undefined') {
                const message = <ReportPageMessage>ev.data;
                this.processMessage(message);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    setReportMetadata(parameters: any, data: any, reportResponse: any) {  // parameters included here for future expansion
        var localemoment = moment().locale(parameters.Locale);
        data.Locale = parameters.Locale;
        data.PrintTime = localemoment.format('LTS');
        data.PrintDate = localemoment.format('L');
        data.PrintDateTime = `${localemoment.format('L')} ${localemoment.format('LTS')}`;
        data.System = 'UNKNOWN SYSTEM';
        data.Company = 'UNKNOWN COMPANY';
        data.DateFields = reportResponse.DateFields;
        //if (sessionStorage.getItem('controldefaults') !== null) {
        //    const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        //    if (typeof controlDefaults !== 'undefined') {
        //        if (typeof controlDefaults.companyname === 'string') {
        //            data.Company = controlDefaults.companyname;
        //        }
        //        if (typeof controlDefaults.systemname === 'string') {
        //            data.System = controlDefaults.systemname;
        //        }
        //    }
        //}
        if (parameters.companyName) {
            data.Company = parameters.companyName;
        }
        if (parameters.systemName) {
            data.System = parameters.systemName;
        }

        console.log('report data: ', data);

        // convert all ISO dates to locale dates
        let fieldIsDate: boolean = false;
        let dataIsArray: boolean = Array.isArray(data);
        if (dataIsArray) {
            for (var rec of data) {
                for (var field in rec) {
                    if (rec[field]) {
                        fieldIsDate = false;
                        for (var dateField of data.DateFields) {
                            if (dateField.toLowerCase() === field.toLowerCase()) {
                                fieldIsDate = true;
                            }
                        }
                        if (fieldIsDate) {
                            rec[field] = moment(rec[field]).locale(parameters.Locale).format('L');
                        }
                    }
                }
            }
        }
        else {
            for (var field in data) {
                if (data[field]) {
                    fieldIsDate = false;
                    for (var dateField of data.DateFields) {
                        if (dateField.toLowerCase() === field.toLowerCase()) {
                            fieldIsDate = true;
                        }
                    }
                    if (fieldIsDate) {
                        data[field] = moment(data[field]).locale(parameters.Locale).format('L');
                    }
                }
            }
        }

        console.log('report data: ', data);
    }
    //----------------------------------------------------------------------------------------------
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        let htmlElements = document.getElementsByTagName('html');
        if (this.action === 'Preview' || this.action === 'PrintHtml') {
            htmlElements[0].classList.add('preview');
        } else {
            htmlElements[0].classList.add('pdf');
        }

        parameters.isCustomReport = parameters.ReportTemplate != undefined;
        HandlebarsHelpers.registerHelpers(parameters.isCustomReport);
        if (parameters.isCustomReport) {
            parameters.CustomReport = Handlebars.compile(parameters.ReportTemplate);
        }
    }
    //----------------------------------------------------------------------------------------------
    onRenderReportCompleted() {
        if (!this.renderReportCompleted) {
            this.renderReportCompleted = true;
            if (this.action === 'PrintHtml') {
                setTimeout(() => { window.print(); }, 50);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    onRenderReportFailed(err: any) {
        if (!this.renderReportCompleted) {
            this.renderReportCompleted = true;
            this.renderReportFailed = true;

            Ajax.logError('An error occured while rendering the report.', err);
        }
    }
    //----------------------------------------------------------------------------------------------
}

export type RenderMode = 'Html' | 'Pdf' | 'Email';
export type ActionType = 'None' | 'Preview' | 'PrintHtml';

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