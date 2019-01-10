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

export class ReturnonAssetPrecalculatedReportRequest {
    ReportYear: string;
    ReportPeriod: string;
    Ranks: any;
    TrackedBys: any;
    WarehouseId: string;
    InventoryTypeId: string;
    CategoryId: string;
    SubCategoryId: string;
    InventoryId: string;
}

export class ReturnonAssetPrecalculatedReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            let request = new ReturnonAssetPrecalculatedReportRequest();
            request.ReportYear = parameters.ReportYear;
            request.ReportPeriod = parameters.ReportPeriod;
            request.Ranks = parameters.Ranks;
            request.TrackedBys = parameters.TrackedBys;
            request.WarehouseId = parameters.WarehouseId;
            request.InventoryTypeId = parameters.InventoryTypeId;
            request.CategoryId = parameters.CategoryId;
            request.SubCategoryId = parameters.SubCategoryId;
            request.InventoryId = parameters.InventoryId;

            let assetPrecalculated: any = {};

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/returnonassetprecalculatedreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    assetPrecalculated.rows = DataTable.toObjectList(response);
                    assetPrecalculated.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    assetPrecalculated.Report = 'Return On Asset Precalculated Report';
                    assetPrecalculated.System = 'RENTALWORKS';
                    assetPrecalculated.Company = '4WALL ENTERTAINMENT';

                    console.log('assetPrecalc: ', assetPrecalculated)
                    this.renderFooterHtml(assetPrecalculated);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(assetPrecalculated);

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

(<any>window).report = new ReturnonAssetPrecalculatedReport();