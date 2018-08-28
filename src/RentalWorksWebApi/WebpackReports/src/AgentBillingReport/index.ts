import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../theme/webpackReports.scss'
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class AgentBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let agentBilling: any = {};
            let today = new Date();
            console.log('parameters: ', parameters);
            request.orderby = 'Agent, OfficeLocation, Department, Deal, OrderNumber';
            //request.uniqueids = {
            //    FromDate: parameters.FromDate,
            //    ToDate: parameters.ToDate,
            //    LocationId: parameters.OfficeLocationId,
            //    DepartmentId: parameters.DepartmentId,
            //    DateType: parameters.DateType,
            //    DealId: parameters.DealId,
            //    ShowVendors: parameters.ShowVendors,
            //    AgentId: parameters.UserId
            //}
            console.log('request: ', request)
            let agentBillingPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/agentbillingreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    agentBilling = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('agentBilling: ', agentBilling); 

                    agentBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    agentBilling.FromDate = parameters.FromDate;
                    agentBilling.ToDate = parameters.ToDate;
                    agentBilling.Report = 'Agent Billing Report';
                    agentBilling.System = 'RENTALWORKS';
                    agentBilling.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(agentBilling);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(agentBilling);

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

(<any>window).report = new AgentBillingReport();