import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class RentalInventoryChangeReportRequest {
    FromDate: string;
    ToDate: string;
    Ranks: any;
    TrackedBys: any;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
}

export class RentalInventoryChangeReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new RentalInventoryChangeReportRequest();
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.Ranks = parameters.Ranks;
            request.TrackedBys = parameters.TrackedBys;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let rentalInventoryChange: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/RentalInventoryChangeReport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    rentalInventoryChange.rows = DataTable.toObjectList(response);
                    rentalInventoryChange.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    rentalInventoryChange.Report = 'Rental Inventory Change Report';
                    rentalInventoryChange.System = 'RENTALWORKS';
                    rentalInventoryChange.Company = '4WALL ENTERTAINMENT';

                    console.log('report: ', rentalInventoryChange)
                    this.renderFooterHtml(rentalInventoryChange);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(rentalInventoryChange);

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

(<any>window).report = new RentalInventoryChangeReport();