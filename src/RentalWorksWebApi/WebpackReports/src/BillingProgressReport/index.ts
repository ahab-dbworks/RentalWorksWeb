import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");


export class BillingProgressReportRequest {
    AsOfDate: Date;
    Statuses: any;
    IncludeCredits: boolean;
    ExcludeBilled100: boolean;
    OfficeLocationId: string;
    DepartmentId: string;
    DealCsrId: string;
    CustomerId: string;
    DealTypeId: string;
    DealId: string;
    AgentId: string;
}

export class BillingProgressReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let data: any = {};
            let request = new BillingProgressReportRequest();
            request.AsOfDate = parameters.ToDate;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.AgentId = parameters.AgentId;
            request.IncludeCredits = parameters.CreditInvoices;
            request.DealCsrId = parameters.CsrId;
            request.CustomerId = parameters.CustomerId;
            request.DealId = parameters.DealId;
            request.DealTypeId = parameters.DealTypeId;
            request.ExcludeBilled100 = parameters.ExcludeOrders;
            //request.Statuses = parameters.statuslist;
            request.Statuses = parameters.Statuses;

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/billingprogressreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    data.Rows = DataTable.toObjectList(response);

                    this.renderFooterHtml(data);

                    data.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Billing Progress Report';
                    data.System = 'RENTALWORKS';
                    data.Company = '4WALL ENTERTAINMENT';
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(data);

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

(<any>window).report = new BillingProgressReport();