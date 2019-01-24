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

export class UnretiredRentalInventoryReportRequest {
    FromDate: Date;
    ToDate: Date;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    UnretiredReasonId: string;
}

export class UnretiredRentalInventoryReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            console.log(parameters)
            HandlebarsHelpers.registerHelpers();
            let request = new UnretiredRentalInventoryReportRequest();

            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;
            request.UnretiredReasonId = parameters.UnretiredReasonId;


            let unretiredRentalInventory: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/UnretiredRentalInventoryReport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    unretiredRentalInventory = DataTable.toObjectList(response);
                    unretiredRentalInventory.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    unretiredRentalInventory.FromDate = parameters.FromDate;
                    unretiredRentalInventory.ToDate = parameters.ToDate;
                    unretiredRentalInventory.Report = 'Unretired Rental Inventory Report';
                    unretiredRentalInventory.System = 'RENTALWORKS';
                    unretiredRentalInventory.Company = '4WALL ENTERTAINMENT';

                    this.renderFooterHtml(unretiredRentalInventory);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(unretiredRentalInventory);
                    console.log('unretiredRentalInventory: ', unretiredRentalInventory)
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

(<any>window).report = new UnretiredRentalInventoryReport();