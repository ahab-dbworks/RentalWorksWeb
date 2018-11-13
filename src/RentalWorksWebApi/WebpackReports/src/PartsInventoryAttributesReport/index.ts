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

export class PartsInventoryAttributesReportRequest {
    AttributeId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
    SortBy: Array<any>;
}

export class PartsInventoryAttributesReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        console.log('parameters: ', parameters)
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new PartsInventoryAttributesReportRequest();
            request.SortBy = parameters.SortBy;
            request.AttributeId = parameters.AttributeId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let partsInventoryAttributes: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/partsinventoryattributesreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    partsInventoryAttributes = DataTable.toObjectList(response);
                    partsInventoryAttributes.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    partsInventoryAttributes.Report = 'Parts Inventory Attributes Report';
                    partsInventoryAttributes.System = 'RENTALWORKS';
                    partsInventoryAttributes.Company = '4WALL ENTERTAINMENT';
                    partsInventoryAttributes.Today = moment().format('LL');

                    console.log('partsInventoryAttributes:', partsInventoryAttributes)
                    this.renderFooterHtml(partsInventoryAttributes);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(partsInventoryAttributes);
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

(<any>window).report = new PartsInventoryAttributesReport();