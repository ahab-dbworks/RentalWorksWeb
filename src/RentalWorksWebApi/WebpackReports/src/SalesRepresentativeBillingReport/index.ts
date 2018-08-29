import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../theme/webpackReports.scss'
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs"); 

export class SalesRepresentativeBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new BrowseRequest();
            request.uniqueids = {};
       
            let SalesRepresentativeBilling: any = {};
            request.orderby = 'SalesRepresentative, OfficeLocation, Department, Deal, OrderNumber';
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
                request.uniqueids.SalesRepresentativeId = parameters.UserId
            }
            if (parameters.CustomerId != '') {
                request.uniqueids.CustomerId = parameters.CustomerId
            }

            let salesRepPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/salesrepresentativebillingreport/browse`, authorizationHeader, request)
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
