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

export class SalesInventoryPurchaseHistoryRequest {
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

export class SalesInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new SalesInventoryPurchaseHistoryRequest();

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

            let salesInventoryPurchaseHistory: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventorypurchasehistoryreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    salesInventoryPurchaseHistory = DataTable.toObjectList(response);
                    salesInventoryPurchaseHistory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
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