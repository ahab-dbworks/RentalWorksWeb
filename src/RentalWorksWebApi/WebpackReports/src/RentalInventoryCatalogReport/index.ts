import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");


export class RentalInventoryCatalogReportRequest {
    Classifications: any;
    TrackedBys: any;
    Ranks: any;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    WarehouseCatalogId: string;
    IncludeZeroQuantity: boolean;
}

export class RentalInventoryCatalogReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let data: any = {};
            let request = new RentalInventoryCatalogReportRequest();

            request.Classifications = parameters.classificationlist;
            request.TrackedBys = parameters.trackedbylist;
            request.Ranks = parameters.ranklist;
            request.WarehouseId = parameters.WarehouseId,
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.RentalInventoryId;
            request.WarehouseCatalogId = parameters.WarehouseCatalogId;
            request.IncludeZeroQuantity = parameters.IncludeZeroQuantity;

            let promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventorycatalogreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    data.Rows = DataTable.toObjectList(response);
                    console.log(data);
                    this.renderFooterHtml(data);

                    data.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(data);

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

(<any>window).report = new RentalInventoryCatalogReport();