import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class InvoiceSummaryReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    Status: Array<string> = [];
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealId: string;
}

export class InvoiceSummaryReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new InvoiceSummaryReportRequest();

            request.DateType = parameters.DateType;
            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            //request.Status = parameters.IncludeNoCharge;
            request.OfficeLocationId = parameters.OfficeLocationId
            request.DepartmentId = parameters.DepartmentId
            request.CustomerId = parameters.CustomerId
            request.DealId = parameters.DealId

            let invoiceSummary: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/invoicesummaryreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    invoiceSummary = DataTable.toObjectList(response);
                    invoiceSummary.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    invoiceSummary.FromDate = parameters.FromDate;
                    invoiceSummary.ToDate = parameters.ToDate;
                    invoiceSummary.Report = 'Agent Billing Report';
                    invoiceSummary.System = 'RENTALWORKS';
                    invoiceSummary.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(invoiceSummary);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(invoiceSummary);
                    console.log('invoiceSummary: ', invoiceSummary )
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

(<any>window).report = new InvoiceSummaryReport();