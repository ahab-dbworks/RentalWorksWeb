import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class SalesInventoryTransactionReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventorytransactionreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const salesInventoryTransaction: any = DataTable.toObjectList(response);
                    salesInventoryTransaction.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    salesInventoryTransaction.FromDate = parameters.FromDate;
                    salesInventoryTransaction.ToDate = parameters.ToDate;
                    salesInventoryTransaction.Report = 'Sales Inventory Transaction Report';
                    salesInventoryTransaction.System = 'RENTALWORKS';
                    salesInventoryTransaction.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(salesInventoryTransaction);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(salesInventoryTransaction);
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

(<any>window).report = new SalesInventoryTransactionReport();