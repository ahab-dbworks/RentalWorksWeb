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

export class ReturnOnAssetPrecalculatedReportRequest {
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

export class ReturnOnAssetPrecalculatedReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let request = new ReturnOnAssetPrecalculatedReportRequest();
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
                    assetPrecalculated.ReportYear = parameters.ReportYear;
                    assetPrecalculated.Company = '4WALL ENTERTAINMENT';
                    
                    if (parameters.ReportPeriod === "FY") {
                        assetPrecalculated.ReportPeriod = "Full Year";
                    }
                    if (parameters.ReportPeriod === "M1") {
                        assetPrecalculated.ReportPeriod = "January";
                    }
                    if (parameters.ReportPeriod === "M2") {
                        assetPrecalculated.ReportPeriod = "February";
                    }
                    if (parameters.ReportPeriod === "M3") {
                        assetPrecalculated.ReportPeriod = "March";
                    }
                    if (parameters.ReportPeriod === "M4") {
                        assetPrecalculated.ReportPeriod = "April";
                    }
                    if (parameters.ReportPeriod === "M5") {
                        assetPrecalculated.ReportPeriod = "May";
                    }
                    if (parameters.ReportPeriod === "M6") {
                        assetPrecalculated.ReportPeriod = "June";
                    }
                    if (parameters.ReportPeriod === "M7") {
                        assetPrecalculated.ReportPeriod = "July";
                    }
                    if (parameters.ReportPeriod === "M8") {
                        assetPrecalculated.ReportPeriod = "August";
                    }
                    if (parameters.ReportPeriod === "M9") {
                        assetPrecalculated.ReportPeriod = "September";
                    }
                    if (parameters.ReportPeriod === "M10") {
                        assetPrecalculated.ReportPeriod = "October";
                    }
                    if (parameters.ReportPeriod === "M11") {
                        assetPrecalculated.ReportPeriod = "November";
                    }
                    if (parameters.ReportPeriod === "M12") {
                        assetPrecalculated.ReportPeriod = "December";
                    }
                    if (parameters.ReportPeriod === "Q1") {
                        assetPrecalculated.ReportPeriod = "First Quarter";
                    }
                    if (parameters.ReportPeriod === "Q2") {
                        assetPrecalculated.ReportPeriod = "Second Quarter";
                    }
                    if (parameters.ReportPeriod === "Q3") {
                        assetPrecalculated.ReportPeriod = "Third Quarter";
                    }
                    if (parameters.ReportPeriod === "Q4") {
                        assetPrecalculated.ReportPeriod = "Fourth Quarter";
                    }
                    if (parameters.ReportPeriod === "S1") {
                        assetPrecalculated.ReportPeriod = "First Semester";
                    }
                    if (parameters.ReportPeriod === "S2") {
                        assetPrecalculated.ReportPeriod = "Second Semester";
                    }


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

(<any>window).report = new ReturnOnAssetPrecalculatedReport();