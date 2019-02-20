import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss'; 
const hbReport = require("./hbReport.hbs"); 
const hbFooter = require("./hbFooter.hbs"); 

export class LateReturnsReportRequest {
    ReportType: string;
    Days: any;
    DueBack: Date;
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealId: string;
    InventoryTypeId: string;
    OrderedByContactId: string;
}

export class LateReturnDueBackReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            let request = new LateReturnsReportRequest();
            let globals:any = {};
       
            HandlebarsHelpers.registerHelpers();
            let lateReturnDueBack: any = {};
            let headerText: string;
            let headerNode: HTMLDivElement = document.createElement('div');

            request.DueBack = parameters.DueBackDate;
            if (parameters.LateReturns) {
                globals.Type = 'PASTDUE'
                request.ReportType = 'PAST_DUE';
                request.Days = parameters.DaysPastDue;
                headerText = parameters.DaysPastDue + ' Days Past Due'

            }
            if (parameters.DueBack) {
                globals.Type = 'DUEBACK'
                request.ReportType = 'DUE_IN';
                request.Days = parameters.DueBackFewer;
                headerText = 'Due Back in ' + parameters.DueBackFewer + ' Days'
            }
            if (parameters.DueBackOn) {
                globals.Type = 'DUEBACK'
                request.ReportType = 'DUE_DATE';
                request.DueBack = parameters.DueBackDate;
                headerText = 'Due Back on ' + parameters.DueBackDate;
            }
            request.OrderedByContactId = parameters.ContactId;
            request.OfficeLocationId = parameters.OfficeLocationId ;
            request.DepartmentId = parameters.DepartmentId ;
            request.CustomerId = parameters.CustomerId ;
            request.DealId = parameters.DealId ;
            request.InventoryTypeId = parameters.InventoryTypeId ;
            if (parameters.ShowUnit) { globals.ShowUnit = 'true' };
            if (parameters.ShowReplacement) { globals.ShowReplacement = 'true' };
            if (parameters.ShowBarCode) { globals.ShowBarCode = 'true' };
            if (parameters.ShowSerial) { globals.ShowSerial = 'true' };

            Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    lateReturnDueBack = DataTable.toObjectList(response); 
                    
                    for (var i = 0; i < lateReturnDueBack.length; i++) {
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
                    globals.data = lateReturnDueBack;

                    globals.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    globals.ContractTime = moment(globals.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(globals.data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(globals);
                    headerNode.innerHTML = headerText;
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
    ContractTime = '';
    data: DataTable;
}
