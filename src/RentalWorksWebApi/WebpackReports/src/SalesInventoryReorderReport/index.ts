import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class SalesInventoryReorderReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventoryreorderreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const salesInventoryReorder: any = DataTable.toObjectList(response);
                    salesInventoryReorder.PrintDate = moment().format('MM/DD/YYYY');
                    salesInventoryReorder.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    salesInventoryReorder.FromDate = parameters.FromDate;
                    salesInventoryReorder.ToDate = parameters.ToDate;
                    salesInventoryReorder.Report = 'Sales Inventory Reorder Report';
                    salesInventoryReorder.System = 'RENTALWORKS';
                    salesInventoryReorder.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(salesInventoryReorder);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(salesInventoryReorder);
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

(<any>window).report = new SalesInventoryReorderReport();