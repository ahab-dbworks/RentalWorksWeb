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

export class RentalInventoryPurchaseHistoryRequest {
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

export class RentalInventoryPurchaseHistoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new RentalInventoryPurchaseHistoryRequest();

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

            let rentalInventoryPurchaseHistory: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventorypurchasehistoryreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    rentalInventoryPurchaseHistory = DataTable.toObjectList(response);
                    rentalInventoryPurchaseHistory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    rentalInventoryPurchaseHistory.PurchasedFromDate = parameters.PurchasedFromDate;
                    rentalInventoryPurchaseHistory.PurchasedToDate = parameters.PurchasedToDate;
                    rentalInventoryPurchaseHistory.ReceivedFromDate = parameters.ReceivedFromDate;
                    rentalInventoryPurchaseHistory.ReceivedToDate = parameters.ReceivedToDate;
                    rentalInventoryPurchaseHistory.Report = 'Rental Inventory Purchase History Report';
                    rentalInventoryPurchaseHistory.System = 'RENTALWORKS';
                    rentalInventoryPurchaseHistory.Company = '4WALL ENTERTAINMENT';

                    this.renderFooterHtml(rentalInventoryPurchaseHistory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(rentalInventoryPurchaseHistory);
                    console.log('rentalInventoryPurchaseHistory: ', rentalInventoryPurchaseHistory)
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