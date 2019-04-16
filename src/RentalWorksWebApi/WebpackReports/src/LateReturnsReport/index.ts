import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss'; 
const hbReport = require("./hbReport.hbs"); 
const hbFooter = require("./hbFooter.hbs"); 

export class LateReturnDueBackReport extends WebpackReport {
    // DO NOT USE THIS REPORT AS A TEMPLATE
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const report:any = DataTable.toObjectList(response); 
                    for (let i = 0; i < report.length; i++) {
                        if (report[i].RowType === 'OrderNumberheader') {
                            report[i].OrderDate = report[i + 1].OrderDate;
                            report[i].OrderDescription = report[i + 1].OrderDescription;
                            report[i].Agent = report[i + 1].Agent;
                            report[i].OrderedByName = report[i + 1].OrderedByName;
                            report[i].BillDateRange = report[i + 1].BillDateRange;
                            report[i].OrderUnitValue = report[i + 1].OrderUnitValue;
                            report[i].OrderReplacementCost = report[i + 1].OrderReplacementCost;
                            report[i].OrderFromDate = report[i + 1].OrderFromDate;
                            report[i].OrderToDate = report[i + 1].OrderToDate;
                            report[i].OrderPastDue = report[i + 1].OrderPastDue;
                        }
                    }
                    report.System = 'RENTALWORKS';
                    report.Company = '4WALL ENTERTAINMENT';
                    report.Report = 'Late Return / Due Back Report';
                    report.Type = parameters.Type;
                    report.subtitle = parameters.headerText;
                    report.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    if (parameters.ShowUnit) { report.ShowUnit = true };
                    if (parameters.ShowReplacement) { report.ShowReplacement = true };
                    if (parameters.ShowBarCode) { report.ShowBarCode = true };
                    if (parameters.ShowSerial) { report.ShowSerial = true };

                    this.renderFooterHtml(report);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    console.log('report', report)
                    document.getElementById('pageBody').innerHTML = hbReport(report);

                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: DataTable) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }

}

(<any>window).report = new LateReturnDueBackReport();