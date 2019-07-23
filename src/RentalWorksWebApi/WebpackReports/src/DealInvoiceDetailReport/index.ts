import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class DealInvoiceDetailReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/dealinvoicedetailreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Sub-Rental Billing Analysis Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    console.log('rpt', data)
                    parameters.DeductVendorItemCost === 'true' ? data.DeductVendorItemCost = true : data.DeductVendorItemCost = false;
                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(data);
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

(<any>window).report = new DealInvoiceDetailReport();