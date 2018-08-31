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

export class ProjectManagerBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new BrowseRequest();
            request.uniqueids = {};
       
            let ProjectManagerBilling: any = {};
            request.orderby = 'ProjectManager, OfficeLocation, Department, Deal, OrderNumber';
            request.uniqueids.DateType = parameters.DateType;
            request.uniqueids.ToDate = parameters.ToDate;
            request.uniqueids.FromDate = parameters.FromDate;
            request.uniqueids.ShowVendors = parameters.ShowVendors;

            if (parameters.OfficeLocationId != '') {
                request.uniqueids.LocationId = parameters.OfficeLocationId
            }
            if (parameters.DepartmentId != '') {
                request.uniqueids.DepartmentId = parameters.DepartmentId
            }
            if (parameters.DealId != '') {
                request.uniqueids.DealId = parameters.DealId
            }
            if (parameters.UserId != '') {
                request.uniqueids.ProjectManagerId = parameters.UserId
            }
            if (parameters.CustomerId != '') {
                request.uniqueids.CustomerId = parameters.CustomerId
            }

            let projectManagerPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/projectmanagerbillingreport/browse`, authorizationHeader, request)
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
