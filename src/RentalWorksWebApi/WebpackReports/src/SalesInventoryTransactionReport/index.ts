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

export class SalesInventoryTransactionReportRequest {
    FromDate: Date;
    ToDate: Date;
    TransactionTypes: Array<any> = [];
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
}

export class SalesInventoryTransactionReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new SalesInventoryTransactionReportRequest();
            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            request.TransactionTypes = parameters.TransactionTypes;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let salesInventoryTransaction: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventorytransactionreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    salesInventoryTransaction = DataTable.toObjectList(response);
                    salesInventoryTransaction.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    salesInventoryTransaction.FromDate = parameters.FromDate;
                    salesInventoryTransaction.ToDate = parameters.ToDate;
                    salesInventoryTransaction.Report = 'Sales Inventory Transaction Report';
                    salesInventoryTransaction.System = 'RENTALWORKS';
                    salesInventoryTransaction.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(salesInventoryTransaction);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(salesInventoryTransaction);
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

(<any>window).report = new SalesInventoryTransactionReport();