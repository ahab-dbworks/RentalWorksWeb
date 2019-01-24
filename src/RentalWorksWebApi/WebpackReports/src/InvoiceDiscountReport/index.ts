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

export class InvoiceDiscountReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealId: string;
    DiscountReasonId: string;
    DiscountPercent: string;
}

export class InvoiceDiscountReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            let request = new InvoiceDiscountReportRequest();
            request.DateType = parameters.DateType;
            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            request.DiscountPercent = parameters.DiscountPercent;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.DealId = parameters.DealId;
            request.CustomerId = parameters.CustomerId;
            request.DiscountReasonId = parameters.DiscountReasonId;

            let invoiceDiscount: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/invoicediscountreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    invoiceDiscount = DataTable.toObjectList(response);

                    invoiceDiscount.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    invoiceDiscount.FromDate = parameters.FromDate;
                    invoiceDiscount.ToDate = parameters.ToDate;
                    invoiceDiscount.DiscountPercent = parameters.DiscountPercent;
                    invoiceDiscount.Report = 'Invoice Discount Report';
                    invoiceDiscount.System = 'RENTALWORKS';
                    invoiceDiscount.Company = '4WALL ENTERTAINMENT';
                    
                    this.renderFooterHtml(invoiceDiscount);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(invoiceDiscount);

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

(<any>window).report = new InvoiceDiscountReport();