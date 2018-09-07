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

export class CrewSignInReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new BrowseRequest();
            request.uniqueids = {};

            let crewSignIn: any = {};

            request.orderby = 'Location, Department, RentFromDate';
            request.uniqueids.ToDate = parameters.ToDate;
            request.uniqueids.FromDate = parameters.FromDate;
            if (parameters.OfficeLocationId != '') {
                request.uniqueids.LocationId = parameters.OfficeLocationId
            }
            if (parameters.DepartmentId != '') {
                request.uniqueids.DepartmentId = parameters.DepartmentId
            }
            if (parameters.CustomerId != '') {
                request.uniqueids.CustomerId = parameters.CustomerId
            }
            if (parameters.DealId != '') {
                request.uniqueids.DealId = parameters.DealId
            }
            if (parameters.OrderId != '') {
                request.uniqueids.OrderId = parameters.OrderId
            }

            let crewSignInPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/crewsigninreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    crewSignIn.Rows = DataTable.toObjectList(response); 
                    crewSignIn.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    crewSignIn.FromDate = parameters.FromDate;
                    crewSignIn.ToDate = parameters.ToDate;
                    crewSignIn.Report = 'Crew Sign-In Report';
                    crewSignIn.System = 'RENTALWORKS';
                    crewSignIn.Company = '4WALL ENTERTAINMENT';
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

    renderFooterHtml(model: DataTable) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CrewSignInReport();