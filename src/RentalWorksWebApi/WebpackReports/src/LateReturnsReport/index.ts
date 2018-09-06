import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse'; // added Browse Request obj
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss'; 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class LateReturnDueBackReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
            let globals:any = {};
       
            HandlebarsHelpers.registerHelpers();
            let lateReturnDueBack: any = {};
            console.log('parameters: ', parameters);            
            let headerText: string;
            let headerNode: HTMLDivElement = document.createElement('div');

            request.uniqueids.IsSummary = false;
            request.uniqueids.DueBack = parameters.DueBackDate;
            if (parameters.LateReturns) {
                globals.Type = 'PASTDUE'
                request.uniqueids.ReportType = 'PAST_DUE';
                request.uniqueids.Days = parameters.DaysPastDue;
                headerText = parameters.DaysPastDue + ' Days Past Due'

            }
            if (parameters.DueBack) {
                globals.Type = 'DUEBACK'
                request.uniqueids.ReportType = 'DUE_IN';
                request.uniqueids.Days = parameters.DueBackFewer;
                headerText = 'Due Back in ' + parameters.DueBackFewer + ' Days'
            }
            if (parameters.DueBackOn) {
                globals.Type = 'DUEBACK'
                request.uniqueids.ReportType = 'DUE_DATE';
                request.uniqueids.DueBack = parameters.DueBackDate;
                headerText = 'Due Back on ' + parameters.DueBackDate;
            }
            if (parameters.ContactId !== '') { request.uniqueids.ContactId = parameters.ContactId };
            if (parameters.OfficeLocationId !== '') { request.uniqueids.OfficeLocationId = parameters.OfficeLocationId };
            if (parameters.DepartmentId !== '') { request.uniqueids.DepartmentId = parameters.DepartmentId };
            if (parameters.CustomerId !== '') { request.uniqueids.CustomerId = parameters.CustomerId };
            if (parameters.DealId !== '') { request.uniqueids.DealId = parameters.DealId };
            if (parameters.InventoryTypeId !== '') { request.uniqueids.InventoryTypeId = parameters.InventoryTypeId };
            if (parameters.ShowUnit) { globals.ShowUnit = 'true' };
            if (parameters.ShowReplacement) { globals.ShowReplacement = 'true' };
            if (parameters.ShowBarCode) { globals.ShowBarCode = 'true' };
            if (parameters.ShowSerial) { globals.ShowSerial = 'true' };
            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    lateReturnDueBack = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('lateReturnDueBack: ', lateReturnDueBack); // will help in building the handlebars
                    
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
                    console.log('globals: ', globals);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(globals);
                    headerNode.innerHTML = headerText;
                    headerNode.style.cssText = 'text-align:center;font-weight:bold;margin:0 auto;font-size:13px;';
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
