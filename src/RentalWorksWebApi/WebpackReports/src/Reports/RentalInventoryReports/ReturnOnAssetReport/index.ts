import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ReturnOnAssetReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/returnonassetreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response, parameters);
                    this.setReportMetadata(parameters, data);
                    data.Report = 'Return On Asset Report';
                    if (parameters.UseDateRange) {
                        data.FromDate = this.formatDateToLocale(parameters.FromDate, parameters.Locale)
                        data.ToDate = this.formatDateToLocale(parameters.ToDate, parameters.Locale);
                        data.UseDateRange = parameters.UseDateRange;
                    } else {
                        data.ReportYear = parameters.ReportYear;
                        if (parameters.ReportPeriod === "FY") {
                            data.ReportPeriod = "Full Year";
                        }
                        if (parameters.ReportPeriod === "M1") {
                            data.ReportPeriod = "January";
                        }
                        if (parameters.ReportPeriod === "M2") {
                            data.ReportPeriod = "February";
                        }
                        if (parameters.ReportPeriod === "M3") {
                            data.ReportPeriod = "March";
                        }
                        if (parameters.ReportPeriod === "M4") {
                            data.ReportPeriod = "April";
                        }
                        if (parameters.ReportPeriod === "M5") {
                            data.ReportPeriod = "May";
                        }
                        if (parameters.ReportPeriod === "M6") {
                            data.ReportPeriod = "June";
                        }
                        if (parameters.ReportPeriod === "M7") {
                            data.ReportPeriod = "July";
                        }
                        if (parameters.ReportPeriod === "M8") {
                            data.ReportPeriod = "August";
                        }
                        if (parameters.ReportPeriod === "M9") {
                            data.ReportPeriod = "September";
                        }
                        if (parameters.ReportPeriod === "M10") {
                            data.ReportPeriod = "October";
                        }
                        if (parameters.ReportPeriod === "M11") {
                            data.ReportPeriod = "November";
                        }
                        if (parameters.ReportPeriod === "M12") {
                            data.ReportPeriod = "December";
                        }
                        if (parameters.ReportPeriod === "Q1") {
                            data.ReportPeriod = "First Quarter";
                        }
                        if (parameters.ReportPeriod === "Q2") {
                            data.ReportPeriod = "Second Quarter";
                        }
                        if (parameters.ReportPeriod === "Q3") {
                            data.ReportPeriod = "Third Quarter";
                        }
                        if (parameters.ReportPeriod === "Q4") {
                            data.ReportPeriod = "Fourth Quarter";
                        }
                        if (parameters.ReportPeriod === "S1") {
                            data.ReportPeriod = "First Semester";
                        }
                        if (parameters.ReportPeriod === "S2") {
                            data.ReportPeriod = "Second Semester";
                        }
                    }
                    console.log(parameters, data)

                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    if (parameters.isCustomReport) {
                        document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
                    }

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