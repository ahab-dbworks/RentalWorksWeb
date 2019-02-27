import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventorypurchasehistoryreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const rentalInventoryPurchaseHistory: any = DataTable.toObjectList(response);
                    rentalInventoryPurchaseHistory.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    rentalInventoryPurchaseHistory.PurchasedFromDate = parameters.PurchasedFromDate;
                    rentalInventoryPurchaseHistory.PurchasedToDate = parameters.PurchasedToDate;
                    rentalInventoryPurchaseHistory.ReceivedFromDate = parameters.ReceivedFromDate;
                    rentalInventoryPurchaseHistory.ReceivedToDate = parameters.ReceivedToDate;
                    rentalInventoryPurchaseHistory.Report = 'Rental Inventory Purchase History Report';
                    rentalInventoryPurchaseHistory.System = 'RENTALWORKS';
                    rentalInventoryPurchaseHistory.Company = '4WALL ENTERTAINMENT';

                    if (parameters.PurchasedFromDate !== '' || parameters.PurchasedToDate !== '') {
                        rentalInventoryPurchaseHistory.showPurchaseDates = true;
                        if (parameters.PurchasedFromDate === '') {
                            rentalInventoryPurchaseHistory.PurchasedFromDate = '(no date)';
                        }
                        if (parameters.PurchasedToDate === '') {
                            rentalInventoryPurchaseHistory.PurchasedToDate = '(no date)';
                        }
                    }
                    if (parameters.ReceivedFromDate !== '' || parameters.ReceivedToDate !== '') {
                        rentalInventoryPurchaseHistory.showReceiveDates = true;
                        if (parameters.ReceivedFromDate === '') {
                            rentalInventoryPurchaseHistory.ReceivedFromDate = '(no date)';
                        }
                        if (parameters.ReceivedToDate === '') {
                            rentalInventoryPurchaseHistory.ReceivedToDate = '(no date)';
                        }
                    }

                    this.renderFooterHtml(rentalInventoryPurchaseHistory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(rentalInventoryPurchaseHistory);
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

(<any>window).report = new RentalInventoryPurchaseHistoryReport();