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

export class SalesInventoryReorderReportRequest {
    ReorderPointMode: string;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    IncludeZeroReorderPoint: boolean;
}

export class SalesInventoryReorderReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new SalesInventoryReorderReportRequest();
            request.ReorderPointMode = parameters.ReorderTypes;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;
            request.IncludeZeroReorderPoint = parameters.IncludeZeroReorderPoint;

            let salesInventoryReorder: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/salesinventoryreorderreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    salesInventoryReorder = DataTable.toObjectList(response);
                    salesInventoryReorder.PrintDate = moment().format('YYYY-MM-DD');
                    salesInventoryReorder.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    salesInventoryReorder.FromDate = parameters.FromDate;
                    salesInventoryReorder.ToDate = parameters.ToDate;
                    salesInventoryReorder.Report = 'Sales Inventory Reorder Report';
                    salesInventoryReorder.System = 'RENTALWORKS';
                    salesInventoryReorder.Company = '4WALL ENTERTAINMENT';
                    console.log(salesInventoryReorder)
                    this.renderFooterHtml(salesInventoryReorder);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(salesInventoryReorder);
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

(<any>window).report = new SalesInventoryReorderReport();