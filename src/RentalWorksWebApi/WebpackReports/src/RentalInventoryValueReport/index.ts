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

export class RentalInventoryValueRequest {
    FromDate: Date;
    ToDate: Date;
    IncludeOwned: boolean;
    IncludeConsigned: boolean;
    IncludeZeroQuantity: boolean;
    GroupByICode: boolean;
    SerializedValueBasedOn: string;
    Ranks: any;
    TrackedBys: any;
    Summary: boolean;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
}

export class RentalInventoryValueReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new RentalInventoryValueRequest();

            request.ToDate = parameters.ToDate;
            request.FromDate = parameters.FromDate;
            request.IncludeOwned = parameters.IncludeOwned;
            request.IncludeConsigned = parameters.IncludeConsigned;
            request.GroupByICode = parameters.GroupByICode;
            request.SerializedValueBasedOn = parameters.SerializedValueBasedOn;
            request.Ranks = parameters.Ranks;
            request.TrackedBys = parameters.TrackedBys;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.RentalInventoryId;
            request.IncludeZeroQuantity = parameters.IncludeZeroQuantity;

            if (parameters.Summary === 'T') {
                request.Summary = true;
            } else {
                request.Summary = false;
            }

            let rentalInventoryValue: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventoryvaluereport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    rentalInventoryValue = DataTable.toObjectList(response);
                    rentalInventoryValue.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    rentalInventoryValue.FromDate = parameters.FromDate;
                    rentalInventoryValue.ToDate = parameters.ToDate;
                    rentalInventoryValue.Report = 'Rental Inventory Value Report';
                    rentalInventoryValue.System = 'RENTALWORKS';
                    rentalInventoryValue.Company = '4WALL ENTERTAINMENT';
                    if (parameters.IncludeConsigned === true) {
                        rentalInventoryValue.IncludeConsigned = 'Includes Consigned Items';
                    } else {
                        rentalInventoryValue.IncludeConsigned = '';
                    }
                    this.renderFooterHtml(rentalInventoryValue);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(rentalInventoryValue);
                    console.log('rentalInventoryValue: ', rentalInventoryValue)
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

(<any>window).report = new RentalInventoryValueReport();