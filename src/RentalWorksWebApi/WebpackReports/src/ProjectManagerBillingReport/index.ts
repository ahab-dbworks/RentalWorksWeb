import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs"); 

export class ProjectManagerBillingReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    IncludeNoCharge: boolean;
    OfficeLocationId: string;
    DepartmentId: string;
    ProjectManagerId: string;
    CustomerId: string;
    DealId: string;
}

export class ProjectManagerBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let ProjectManagerBilling: any = {};

            let request = new ProjectManagerBillingReportRequest();
            request.DateType = parameters.DateType;
            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            request.IncludeNoCharge = parameters.IncludeNoCharge;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.DealId = parameters.DealId;
            request.ProjectManagerId = parameters.UserId;
            request.CustomerId = parameters.CustomerId;

            let projectManagerPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/projectmanagerbillingreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    ProjectManagerBilling = DataTable.toObjectList(response);
                    ProjectManagerBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    ProjectManagerBilling.FromDate = parameters.FromDate;
                    ProjectManagerBilling.ToDate = parameters.ToDate;
                    ProjectManagerBilling.Report = 'Project Manager Billing Report';
                    ProjectManagerBilling.System = 'RENTALWORKS';
                    ProjectManagerBilling.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(ProjectManagerBilling);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(ProjectManagerBilling);

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

(<any>window).report = new ProjectManagerBillingReport();
