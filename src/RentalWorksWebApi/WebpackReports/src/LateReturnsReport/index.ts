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

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const lateReturnDueBack:any = DataTable.toObjectList(response); 
                    for (let i = 0; i < lateReturnDueBack.length; i++) {
                        if (lateReturnDueBack[i].RowType === 'OrderNumberheader') {
                            lateReturnDueBack[i].OrderDate = lateReturnDueBack[i + 1].OrderDate;
                            lateReturnDueBack[i].OrderDescription = lateReturnDueBack[i + 1].OrderDescription;
                            lateReturnDueBack[i].Agent = lateReturnDueBack[i + 1].Agent;
                            lateReturnDueBack[i].OrderedByName = lateReturnDueBack[i + 1].OrderedByName;
                            lateReturnDueBack[i].BillDateRange = lateReturnDueBack[i + 1].BillDateRange;
                            lateReturnDueBack[i].OrderUnitValue = lateReturnDueBack[i + 1].OrderUnitValue;
                            lateReturnDueBack[i].OrderReplacementCost = lateReturnDueBack[i + 1].OrderReplacementCost;
                            lateReturnDueBack[i].OrderFromDate = lateReturnDueBack[i + 1].OrderFromDate;
                            lateReturnDueBack[i].OrderToDate = lateReturnDueBack[i + 1].OrderToDate;
                            lateReturnDueBack[i].OrderPastDue = lateReturnDueBack[i + 1].OrderPastDue;
                        }
                    }
                    const globals:any = {};
                    globals.data = lateReturnDueBack;
                    globals.Type = parameters.Type;
                    globals.headerText = parameters.headerText;
                    globals.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    if (parameters.ShowUnit) { globals.ShowUnit = 'true' };
                    if (parameters.ShowReplacement) { globals.ShowReplacement = 'true' };
                    if (parameters.ShowBarCode) { globals.ShowBarCode = 'true' };
                    if (parameters.ShowSerial) { globals.ShowSerial = 'true' };

                    this.renderFooterHtml(globals.data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(globals);
                    const headerNode: HTMLDivElement = document.createElement('div');
                    headerNode.innerHTML = globals.headerText;
                    headerNode.style.cssText = 'text-align:center;font-weight:bold;margin:0 auto;font-size:12px;';
                    document.getElementsByClassName('Header')[0].appendChild(headerNode);

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

class globals {
    ShowUnit = '';
    ShowReplacement = '';
    ShowBarCode = '';
    ShowSerial = '';
    Type = '';
    PrintTime = '';
    data: DataTable;
}
