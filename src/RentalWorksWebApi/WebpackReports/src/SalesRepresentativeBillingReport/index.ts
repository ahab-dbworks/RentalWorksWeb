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

export class SalesRepresentativeBillingReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    IncludeNoCharge: boolean;
    OfficeLocationId: string;
    DepartmentId: string;
    SalesRepresentativeId: string;
    CustomerId: string;
    DealId: string;
}

export class SalesRepresentativeBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            let SalesRepresentativeBilling: any = {};
            HandlebarsHelpers.registerHelpers();

            let request = new SalesRepresentativeBillingReportRequest();
            request.DateType = parameters.DateType;
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.IncludeNoCharge = parameters.IncludeNoCharge;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.DealId = parameters.DealId;
            request.SalesRepresentativeId = parameters.UserId;
            request.CustomerId = parameters.CustomerId;

            let salesRepPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/salesrepresentativebillingreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    SalesRepresentativeBilling = DataTable.toObjectList(response);
                    SalesRepresentativeBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    SalesRepresentativeBilling.FromDate = parameters.FromDate;
                    SalesRepresentativeBilling.ToDate = parameters.ToDate;
                    SalesRepresentativeBilling.Report = 'Sales Representative Billing Report';
                    SalesRepresentativeBilling.System = 'RENTALWORKS';
                    SalesRepresentativeBilling.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(SalesRepresentativeBilling);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(SalesRepresentativeBilling);
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

(<any>window).report = new SalesRepresentativeBillingReport();
