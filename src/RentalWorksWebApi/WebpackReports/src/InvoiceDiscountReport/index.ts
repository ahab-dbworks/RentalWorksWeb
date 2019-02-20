import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class InvoiceDiscountReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/invoicediscountreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const invoiceDiscount: any = DataTable.toObjectList(response);
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