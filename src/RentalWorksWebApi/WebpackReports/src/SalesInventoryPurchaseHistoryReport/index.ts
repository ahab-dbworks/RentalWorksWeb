import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class SalesInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventorypurchasehistoryreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const salesInventoryPurchaseHistory: any = DataTable.toObjectList(response);
                    salesInventoryPurchaseHistory.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    salesInventoryPurchaseHistory.PurchasedFromDate = parameters.PurchasedFromDate;
                    salesInventoryPurchaseHistory.PurchasedToDate = parameters.PurchasedToDate;
                    salesInventoryPurchaseHistory.ReceivedFromDate = parameters.ReceivedFromDate;
                    salesInventoryPurchaseHistory.ReceivedToDate = parameters.ReceivedToDate;
                    salesInventoryPurchaseHistory.Report = 'Sales Inventory Purchase History Report';
                    salesInventoryPurchaseHistory.System = 'RENTALWORKS';
                    salesInventoryPurchaseHistory.Company = '4WALL ENTERTAINMENT';

                    if (parameters.PurchasedFromDate !== '' || parameters.PurchasedToDate !== '') {
                        salesInventoryPurchaseHistory.showPurchaseDates = true;
                        if (parameters.PurchasedFromDate === '') {
                            salesInventoryPurchaseHistory.PurchasedFromDate = '(no date)';
                        }
                        if (parameters.PurchasedToDate === '') {
                            salesInventoryPurchaseHistory.PurchasedToDate = '(no date)';
                        }
                    }
                    if (parameters.ReceivedFromDate !== '' || parameters.ReceivedToDate !== '') {
                        salesInventoryPurchaseHistory.showReceiveDates = true;
                        if (parameters.ReceivedFromDate === '') {
                            salesInventoryPurchaseHistory.ReceivedFromDate = '(no date)';
                        }
                        if (parameters.ReceivedToDate === '') {
                            salesInventoryPurchaseHistory.ReceivedToDate = '(no date)';
                        }
                    }

                    this.renderFooterHtml(salesInventoryPurchaseHistory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(salesInventoryPurchaseHistory);
                    console.log('salesInventoryPurchaseHistory: ', salesInventoryPurchaseHistory)
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

(<any>window).report = new SalesInventoryPurchaseHistoryReport();