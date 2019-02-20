import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RetiredRentalInventoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/retiredrentalinventoryreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const retiredRentalInventory: any = {};
                    retiredRentalInventory.Items = DataTable.toObjectList(response);
                    retiredRentalInventory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    retiredRentalInventory.FromDate = parameters.FromDate;
                    retiredRentalInventory.ToDate = parameters.ToDate;
                    retiredRentalInventory.Report = 'Retired Rental Inventory Report';
                    retiredRentalInventory.System = 'RENTALWORKS';
                    retiredRentalInventory.Company = '4WALL ENTERTAINMENT';
                    retiredRentalInventory.ShowSellInformation = parameters.ShowSellInformation;
                    retiredRentalInventory.IncludeUnretired = parameters.IncludeUnretired;
                    this.renderFooterHtml(retiredRentalInventory);

                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(retiredRentalInventory);

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

(<any>window).report = new RetiredRentalInventoryReport();