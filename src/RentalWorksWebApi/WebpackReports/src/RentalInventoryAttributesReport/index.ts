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

export class RentalInventoryAttributesReportRequest {
    AttributeId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    SortBy: Array<any>;
}

export class RentalInventoryAttributesReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new RentalInventoryAttributesReportRequest();
            request.SortBy = parameters.SortBy;
            request.AttributeId = parameters.AttributeId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let rentalInventoryAttributes: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventoryattributesreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    rentalInventoryAttributes = DataTable.toObjectList(response);
                    rentalInventoryAttributes.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    rentalInventoryAttributes.Report = 'Rental Inventory Attributes Report';
                    rentalInventoryAttributes.System = 'RENTALWORKS';
                    rentalInventoryAttributes.Company = '4WALL ENTERTAINMENT';
                    rentalInventoryAttributes.Today = moment().format('LL');

                    console.log('rentalInventoryAttributes:', rentalInventoryAttributes)
                    this.renderFooterHtml(rentalInventoryAttributes);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(rentalInventoryAttributes);
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

(<any>window).report = new RentalInventoryAttributesReport();