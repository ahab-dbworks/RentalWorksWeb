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

            HandlebarsHelpers.registerHelpers();
            let request = new BrowseRequest();
            request.uniqueids = {};
       
            let agentBilling: any = {};
            request.orderby = 'Agent, OfficeLocation, Department, Deal, OrderNumber';
            request.uniqueids.DateType = parameters.DateType;
            request.uniqueids.ToDate = parameters.ToDate;
            request.uniqueids.FromDate = parameters.FromDate;
            request.uniqueids.ShowVendors = parameters.ShowVendors;
            if (parameters.OfficeLocationId != '') {
                request.uniqueids.LocationId = parameters.OfficeLocationId
            }
            if (parameters.DepartmentId != '') {
                request.uniqueids.DepartmentId = parameters.DepartmentId
            }
            if (parameters.DealId != '') {
                request.uniqueids.DealId = parameters.DealId
            }
            if (parameters.UserId != '') {
                request.uniqueids.AgentId = parameters.UserId
            }
            if (parameters.CustomerId != '') {
                request.uniqueids.CustomerId = parameters.CustomerId
            }
            
            let agentBillingPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/agentbillingreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {

                    agentBilling = DataTable.toObjectList(response); // converts res to javascript obj
                    agentBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    agentBilling.FromDate = parameters.FromDate;
                    agentBilling.ToDate = parameters.ToDate;
                    agentBilling.Report = 'Agent Billing Report';
                    agentBilling.System = 'RENTALWORKS';
                    agentBilling.Company = '4WALL ENTERTAINMENT'; // needs data from response
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