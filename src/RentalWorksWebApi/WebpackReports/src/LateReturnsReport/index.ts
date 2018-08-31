﻿import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable'; // added Browse Request obj
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class LateReturnDueBackReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let lateReturnDueBack: any = {};
            console.log('parameters: ', parameters);            

            request.uniqueids.IsSummary = false;
            if (parameters.LateReturns) {
                request.uniqueids.ReportType = 'PAST_DUE';
                request.uniqueids.Days = parameters.DaysPastDue;
            }
            if (parameters.DueBack) {
                request.uniqueids.ReportType = 'DUE_IN';
                request.uniqueids.Days = parameters.DueBackFewer;
            }
            if (parameters.DueBackOn) {
                request.uniqueids.ReportType = 'DUE_DATE';
                request.uniqueids.DueBack = parameters.DueBackDate;
            }
            request.uniqueids.DueBack = '08/01/2018';
            request.uniqueids.ContactId = parameters.ContactId

            console.log(request)
            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    lateReturnDueBack = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('lateReturnDueBack: ', lateReturnDueBack); // will help in building the handlebars

                    lateReturnDueBack.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    lateReturnDueBack.ContractTime = moment(lateReturnDueBack.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(lateReturnDueBack);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(lateReturnDueBack);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: DataTable) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new LateReturnDueBackReport();
