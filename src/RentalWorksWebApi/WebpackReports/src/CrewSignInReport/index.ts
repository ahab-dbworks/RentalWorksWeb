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

export class CrewSignInReportRequest {
    FromDate: Date;
    ToDate: Date;
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealId: string;
    OrderId: string;
}

export class CrewSignInReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let crewSignIn: any = {};
            let request = new CrewSignInReportRequest();
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.CustomerId = parameters.CustomerId;
            request.DealId = parameters.DealId;
            request.OrderId = parameters.OrderId;
            let crewSignInPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/crewsigninreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    crewSignIn = DataTable.toObjectList(response);
                    crewSignIn.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    crewSignIn.FromDate = parameters.FromDate;
                    crewSignIn.ToDate = parameters.ToDate;
                    crewSignIn.Report = 'Crew Sign-In Report';
                    crewSignIn.System = 'RENTALWORKS';
                    crewSignIn.Company = '4WALL ENTERTAINMENT';
                    console.log('crewSignIn:', crewSignIn);

                    this.renderFooterHtml(crewSignIn);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(crewSignIn);

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

(<any>window).report = new CrewSignInReport();