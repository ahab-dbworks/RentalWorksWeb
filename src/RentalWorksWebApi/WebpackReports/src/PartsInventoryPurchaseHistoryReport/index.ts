import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class PartsInventoryPurchaseHistoryRequest {
    PurchasedFromDate: Date;
    PurchasedToDate: Date;
    ReceivedFromDate: Date;
    ReceivedToDate: Date;
    Ranks: any;
    TrackedBys: any;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
}

export class PartsInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new PartsInventoryPurchaseHistoryRequest();

            request.PurchasedFromDate = parameters.PurchasedFromDate;
            request.PurchasedToDate = parameters.PurchasedToDate;
            request.ReceivedFromDate = parameters.ReceivedFromDate;
            request.ReceivedToDate = parameters.ReceivedToDate;
            request.Ranks = parameters.Ranks;
            request.TrackedBys = parameters.TrackedBys;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let partsInventoryPurchaseHistory: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/partsinventorypurchasehistoryreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    partsInventoryPurchaseHistory = DataTable.toObjectList(response);
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
                    console.log('partsInventoryPurchaseHistory: ', partsInventoryPurchaseHistory)
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