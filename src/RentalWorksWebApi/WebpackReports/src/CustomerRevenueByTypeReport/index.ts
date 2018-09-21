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


export class CustomerRevenueByTypeReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealTypeId: string;
    DealId: string;
    OrderTypeId: string;
}

export class CustomerRevenueByTypeReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            let request = new CustomerRevenueByTypeReportRequest();
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate
            request.DateType = parameters.DateType;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.CustomerId = parameters.CustomerId;
            request.DealTypeId = parameters.DealTypeId;
            request.DealId = parameters.DealId;
            request.OrderTypeId = parameters.OrderTypeId;

            HandlebarsHelpers.registerHelpers();
            let customerRevenueByType: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/customerrevenuebytypereport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    customerRevenueByType = DataTable.toObjectList(response);
                    customerRevenueByType.FromDate = parameters.FromDate;
                    customerRevenueByType.ToDate = parameters.ToDate;
                    customerRevenueByType.Report = 'Customer Revenue By Type Report';
                    customerRevenueByType.System = 'RENTALWORKS';
                    customerRevenueByType.Company = '4WALL ENTERTAINMENT';
                    customerRevenueByType.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    customerRevenueByType.ContractTime = moment(customerRevenueByType.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(customerRevenueByType);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(customerRevenueByType);
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

(<any>window).report = new CustomerRevenueByTypeReport();