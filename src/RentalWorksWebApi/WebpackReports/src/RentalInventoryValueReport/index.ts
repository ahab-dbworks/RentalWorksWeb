import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryValueReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventoryvaluereport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Rental Inventory Value Report';
                    data.System = 'RENTALWORKS';
                    data.Company = '4WALL ENTERTAINMENT';
                    // Consigned or Owned Items header text
                    if (parameters.IncludeConsigned === true && parameters.IncludeOwned === false) {
                        data.ConsignedOwnedHeader = 'Includes Consigned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === false) {
                        data.ConsignedOwnedHeader = 'Includes Owned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === true) {
                        data.ConsignedOwnedHeader = 'Includes Owned and Consigned Items';
                    } else {
                        data.ConsignedOwnedHeader = '';
                    }
                    // Determine Summary or Detail View
                    if (parameters.Summary === 'true') {
                        data.ViewSetting = 'SummaryView';
                    } else {
                        data.ViewSetting = 'DetailView';
                    }
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

(<any>window).report = new RentalInventoryValueReport();