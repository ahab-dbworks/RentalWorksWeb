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

export class PartsInventoryReorderReportRequest {
    ReorderPointMode: string;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    IncludeZeroReorderPoint: boolean;
}

export class PartsInventoryReorderReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new PartsInventoryReorderReportRequest();
            request.ReorderPointMode = parameters.ReorderTypes;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;
            request.IncludeZeroReorderPoint = parameters.IncludeZeroReorderPoint;

            let partsInventoryReorder: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/partsinventoryreorderreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    partsInventoryReorder = DataTable.toObjectList(response);
                    partsInventoryReorder.PrintDate = moment().format('MM/DD/YYYY');
                    partsInventoryReorder.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    partsInventoryReorder.FromDate = parameters.FromDate;
                    partsInventoryReorder.ToDate = parameters.ToDate;
                    partsInventoryReorder.Report = 'Parts Inventory Reorder Report';
                    partsInventoryReorder.System = 'RENTALWORKS';
                    partsInventoryReorder.Company = '4WALL ENTERTAINMENT';
                    console.log(partsInventoryReorder)
                    this.renderFooterHtml(partsInventoryReorder);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(partsInventoryReorder);
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

(<any>window).report = new PartsInventoryReorderReport();