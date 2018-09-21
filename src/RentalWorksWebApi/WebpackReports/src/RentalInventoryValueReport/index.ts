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

            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate;
            request.IncludeOwned = parameters.IncludeOwned;
            request.IncludeConsigned = parameters.IncludeConsigned;
            request.IncludeZeroQuantity = parameters.IncludeZeroQuantity;
            request.GroupByICode = parameters.GroupByICode;
            request.SerializedValueBasedOn = parameters.SerializedValueBasedOn;
            request.Ranks = parameters.Ranks;
            request.TrackedBys = parameters.TrackedBys;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            if (parameters.Summary === 'true') {
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
                    // Consigned or Owned Items header text
                    if (parameters.IncludeConsigned === true && parameters.IncludeOwned === false) {
                        rentalInventoryValue.ConsignedOwnedHeader = 'Includes Consigned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === false) {
                        rentalInventoryValue.ConsignedOwnedHeader = 'Includes Owned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === true) {
                        rentalInventoryValue.ConsignedOwnedHeader = 'Includes Owned and Consigned Items';
                    } else {
                        rentalInventoryValue.ConsignedOwnedHeader = '';
                    }
                    // Determine Summary or Detail View
                    if (parameters.Summary === 'true') {
                        rentalInventoryValue.ViewSetting = 'SummaryView';
                    } else {
                        rentalInventoryValue.ViewSetting = 'DetailView';
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