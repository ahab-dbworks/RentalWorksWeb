import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class AgentBillingReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    IncludeNoCharge: boolean;
    OfficeLocationId: string;
    DepartmentId: string;
    AgentId: string;
    CustomerId: string;
    DealId: string;
}

export class AgentBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            let request = new AgentBillingReportRequest();
            request.DateType = parameters.DateType;
            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            request.IncludeNoCharge = parameters.IncludeNoCharge;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.DealId = parameters.DealId;
            request.AgentId = parameters.UserId;
            request.CustomerId = parameters.CustomerId;

            let agentBilling: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/agentbillingreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    agentBilling = DataTable.toObjectList(response);
                    agentBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    agentBilling.FromDate = parameters.FromDate;
                    agentBilling.ToDate = parameters.ToDate;
                    agentBilling.Report = 'Agent Billing Report';
                    agentBilling.System = 'RENTALWORKS';
                    agentBilling.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(agentBilling);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(agentBilling);

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

(<any>window).report = new AgentBillingReport();