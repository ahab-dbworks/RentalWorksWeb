﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as Handlebars from 'handlebars/dist/cjs/handlebars';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ArAgingReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            Ajax.post<DataTable>(`${apiUrl}/api/v1/aragingreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.AsOfDate = parameters.AsOfDate;
                    data.Report = 'A/R Aging Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    this.renderFooterHtml(data);

                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    
                    if (parameters.ReportTemplate != undefined) {
                        const customReport:any = Handlebars.compile(parameters.ReportTemplate);
                        document.getElementById('pageBody').innerHTML = customReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
                    }

                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }
    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new ArAgingReport();