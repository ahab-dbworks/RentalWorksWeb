import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class UnretiredRentalInventoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            console.log(parameters)
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/UnretiredRentalInventoryReport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const unretiredRentalInventory: any = DataTable.toObjectList(response);
                    unretiredRentalInventory.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    unretiredRentalInventory.FromDate = parameters.FromDate;
                    unretiredRentalInventory.ToDate = parameters.ToDate;
                    unretiredRentalInventory.Report = 'Unretired Rental Inventory Report';
                    unretiredRentalInventory.System = 'RENTALWORKS';
                    unretiredRentalInventory.Company = '4WALL ENTERTAINMENT';

                    this.renderFooterHtml(unretiredRentalInventory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(unretiredRentalInventory);
                    console.log('unretiredRentalInventory: ', unretiredRentalInventory)
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

(<any>window).report = new UnretiredRentalInventoryReport();