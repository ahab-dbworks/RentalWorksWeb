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
                    const data: any = DataTable.toObjectList(response); 
                    for (let i = 0; i < data.length; i++) {
                        if (data[i].RowType === 'OrderNumberheader') {
                            data[i].OrderDate = data[i + 1].OrderDate;
                            data[i].OrderDescription = data[i + 1].OrderDescription;
                            data[i].Agent = data[i + 1].Agent;
                            data[i].OrderedByName = data[i + 1].OrderedByName;
                            data[i].BillDateRange = data[i + 1].BillDateRange;
                            //data[i].OrderUnitValue = data[i + 1].OrderUnitValue; // These values were not served by the API => Undefined
                            //data[i].OrderReplacementCost = data[i + 1].OrderReplacementCost;
                            data[i].OrderFromDate = data[i + 1].OrderFromDate;
                            data[i].OrderToDate = data[i + 1].OrderToDate;
                            data[i].OrderPastDue = data[i + 1].OrderPastDue;
                        }
                    }
                    data.System = 'RENTALWORKS';
                    data.Company = '4WALL ENTERTAINMENT';
                    data.Report = 'Late Return / Due Back Report';
                    data.Type = parameters.Type;
                    data.subtitle = parameters.headerText;
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    if (parameters.ShowUnit) { data.ShowUnit = true };
                    if (parameters.ShowReplacement) { data.ShowReplacement = true };
                    if (parameters.ShowBarCode) { data.ShowBarCode = true };
                    if (parameters.ShowSerial) { data.ShowSerial = true };

                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(data);

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