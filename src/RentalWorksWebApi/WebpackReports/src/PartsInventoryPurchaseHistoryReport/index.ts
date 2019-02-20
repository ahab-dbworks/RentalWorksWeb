import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class PartsInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/partsinventorypurchasehistoryreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const partsInventoryPurchaseHistory: any = DataTable.toObjectList(response);
                    partsInventoryPurchaseHistory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    partsInventoryPurchaseHistory.PurchasedFromDate = parameters.PurchasedFromDate;
                    partsInventoryPurchaseHistory.PurchasedToDate = parameters.PurchasedToDate;
                    partsInventoryPurchaseHistory.ReceivedFromDate = parameters.ReceivedFromDate;
                    partsInventoryPurchaseHistory.ReceivedToDate = parameters.ReceivedToDate;
                    partsInventoryPurchaseHistory.Report = 'Parts Inventory Purchase History Report';
                    partsInventoryPurchaseHistory.System = 'RENTALWORKS';
                    partsInventoryPurchaseHistory.Company = '4WALL ENTERTAINMENT';

                    if (parameters.PurchasedFromDate !== '' || parameters.PurchasedToDate !== '') {
                        partsInventoryPurchaseHistory.showPurchaseDates = true;
                        if (parameters.PurchasedFromDate === '') {
                            partsInventoryPurchaseHistory.PurchasedFromDate = '(no date)';
                        }
                        if (parameters.PurchasedToDate === '') {
                            partsInventoryPurchaseHistory.PurchasedToDate = '(no date)';
                        }
                    }
                    if (parameters.ReceivedFromDate !== '' || parameters.ReceivedToDate !== '') {
                        partsInventoryPurchaseHistory.showReceiveDates = true;
                        if (parameters.ReceivedFromDate === '') {
                            partsInventoryPurchaseHistory.ReceivedFromDate = '(no date)';
                        }
                        if (parameters.ReceivedToDate === '') {
                            partsInventoryPurchaseHistory.ReceivedToDate = '(no date)';
                        }
                    }

                    this.renderFooterHtml(partsInventoryPurchaseHistory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(partsInventoryPurchaseHistory);
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

(<any>window).report = new PartsInventoryPurchaseHistoryReport();