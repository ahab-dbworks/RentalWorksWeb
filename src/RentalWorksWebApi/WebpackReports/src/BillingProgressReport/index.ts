import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class BillingProgressReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let data: any = {};
            let request = new BrowseRequest();
            request.uniqueids = {
                AsOfDate: parameters.ToDate,
            }
            if (parameters.OfficeLocationId) {
                request.uniqueids.OfficeLocationId = parameters.OfficeLocationId;
            }
            if (parameters.AgentId) {
                request.uniqueids.AgentId = parameters.AgentId;
            }
            if (parameters.CreditInvoices) {
                request.uniqueids.IncludeCreditInvoices = parameters.CreditInvoices;
            }
            if (parameters.CsrId) {
                request.uniqueids.DealCsrId = parameters.CsrId;
            }
            if (parameters.CustomerId) {
                request.uniqueids.CustomerId = parameters.CustomerId;
            }
            if (parameters.DealId) {
                request.uniqueids.DealId = parameters.DealId;
            }
            if (parameters.DealTypeId) {
                request.uniqueids.DealTypeId = parameters.DealTypeId;
            }
            if (parameters.DepartmentId) {
                request.uniqueids.DepartmentId = parameters.DepartmentId;
            }
            if (parameters.ExcludeOrders) {
                request.uniqueids.ExcludeOrdersBilled100 = parameters.ExcludeOrders;
            }
            if (parameters.statuslist) {
                request.uniqueids.statuslist = parameters.statuslist;
            }

            console.log(request);

            let glDistributionPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/billingprogressreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    data.Rows = DataTable.toObjectList(response);

                    this.renderFooterHtml(data);

                    data.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    data.ToDate = parameters.ToDate;
                    console.log(data);
                    
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
    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new BillingProgressReport();