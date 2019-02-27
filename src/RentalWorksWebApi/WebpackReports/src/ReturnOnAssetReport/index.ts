import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ReturnOnAssetReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/returnonassetreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const returnAsset: any = {};
                    returnAsset.rows = DataTable.toObjectList(response);
                    returnAsset.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    returnAsset.Report = 'Return On Asset Report';
                    returnAsset.System = 'RENTALWORKS';
                    returnAsset.ReportYear = parameters.ReportYear;
                    returnAsset.Company = '4WALL ENTERTAINMENT';
                    
                    if (parameters.ReportPeriod === "FY") {
                        returnAsset.ReportPeriod = "Full Year";
                    }
                    if (parameters.ReportPeriod === "M1") {
                        returnAsset.ReportPeriod = "January";
                    }
                    if (parameters.ReportPeriod === "M2") {
                        returnAsset.ReportPeriod = "February";
                    }
                    if (parameters.ReportPeriod === "M3") {
                        returnAsset.ReportPeriod = "March";
                    }
                    if (parameters.ReportPeriod === "M4") {
                        returnAsset.ReportPeriod = "April";
                    }
                    if (parameters.ReportPeriod === "M5") {
                        returnAsset.ReportPeriod = "May";
                    }
                    if (parameters.ReportPeriod === "M6") {
                        returnAsset.ReportPeriod = "June";
                    }
                    if (parameters.ReportPeriod === "M7") {
                        returnAsset.ReportPeriod = "July";
                    }
                    if (parameters.ReportPeriod === "M8") {
                        returnAsset.ReportPeriod = "August";
                    }
                    if (parameters.ReportPeriod === "M9") {
                        returnAsset.ReportPeriod = "September";
                    }
                    if (parameters.ReportPeriod === "M10") {
                        returnAsset.ReportPeriod = "October";
                    }
                    if (parameters.ReportPeriod === "M11") {
                        returnAsset.ReportPeriod = "November";
                    }
                    if (parameters.ReportPeriod === "M12") {
                        returnAsset.ReportPeriod = "December";
                    }
                    if (parameters.ReportPeriod === "Q1") {
                        returnAsset.ReportPeriod = "First Quarter";
                    }
                    if (parameters.ReportPeriod === "Q2") {
                        returnAsset.ReportPeriod = "Second Quarter";
                    }
                    if (parameters.ReportPeriod === "Q3") {
                        returnAsset.ReportPeriod = "Third Quarter";
                    }
                    if (parameters.ReportPeriod === "Q4") {
                        returnAsset.ReportPeriod = "Fourth Quarter";
                    }
                    if (parameters.ReportPeriod === "S1") {
                        returnAsset.ReportPeriod = "First Semester";
                    }
                    if (parameters.ReportPeriod === "S2") {
                        returnAsset.ReportPeriod = "Second Semester";
                    }

                    this.renderFooterHtml(returnAsset);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(returnAsset);

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

(<any>window).report = new ReturnOnAssetReport();