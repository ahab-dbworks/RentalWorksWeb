import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class RetiredRentalInventoryReportRequest {
    FromDate: Date;
    ToDate: Date;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    InventoryId: string;
    CustomerId: string;
    DealId: string;
    RetiredReasonId: string;
}

export class RetiredRentalInventoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let retiredRentalInventory: any = {};
            let request = new RetiredRentalInventoryReportRequest();
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.InventoryId = parameters.InventoryId;
            request.CustomerId = parameters.CustomerId;
            request.DealId = parameters.DealId;
            request.RetiredReasonId = parameters.RetiredReasonId;

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/retiredrentalinventoryreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    retiredRentalInventory.Items = DataTable.toObjectList(response);
                    this.renderFooterHtml(retiredRentalInventory);
                    retiredRentalInventory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    retiredRentalInventory.FromDate = parameters.FromDate;
                    retiredRentalInventory.ToDate = parameters.ToDate;
                    retiredRentalInventory.Report = 'Retired Rental Inventory Report';
                    retiredRentalInventory.System = 'RENTALWORKS';
                    retiredRentalInventory.Company = '4WALL ENTERTAINMENT';
                    retiredRentalInventory.ShowSellInformation = parameters.ShowSellInformation;

                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(retiredRentalInventory);

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

(<any>window).report = new RetiredRentalInventoryReport();