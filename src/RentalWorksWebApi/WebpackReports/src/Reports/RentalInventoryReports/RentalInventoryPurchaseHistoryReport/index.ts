import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventorypurchasehistoryreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data);
                    data.PurchasedFromDate = parameters.PurchasedFromDate;
                    data.PurchasedToDate = parameters.PurchasedToDate;
                    data.ReceivedFromDate = parameters.ReceivedFromDate;
                    data.ReceivedToDate = parameters.ReceivedToDate;
                    data.Report = 'Rental Inventory Purchase History Report';

                    if (parameters.PurchasedFromDate !== '' || parameters.PurchasedToDate !== '') {
                        data.showPurchaseDates = true;
                        if (parameters.PurchasedFromDate === '') {
                            data.PurchasedFromDate = '(no date)';
                        }
                        if (parameters.PurchasedToDate === '') {
                            data.PurchasedToDate = '(no date)';
                        }
                    }
                    if (parameters.ReceivedFromDate !== '' || parameters.ReceivedToDate !== '') {
                        data.showReceiveDates = true;
                        if (parameters.ReceivedFromDate === '') {
                            data.ReceivedFromDate = '(no date)';
                        }
                        if (parameters.ReceivedToDate === '') {
                            data.ReceivedToDate = '(no date)';
                        }
                    }

                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    if (parameters.isCustomReport) {
                        document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
                    }
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