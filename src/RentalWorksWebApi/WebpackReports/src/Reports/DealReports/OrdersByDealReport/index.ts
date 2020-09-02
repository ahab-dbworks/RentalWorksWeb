import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class OrdersByDealReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/ordersbydealreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data);
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Orders By Deal Report';
                    if (parameters.FilterDatesOrderCreate) {
                        data.FilterDatesOrderCreate = true;
                        data.OrderCreateFromDate = parameters.OrderCreateFromDate;
                        data.OrderCreateToDate = parameters.OrderCreateToDate;
                    }
                    if (parameters.FilterDatesOrderStart) {
                        data.FilterDatesOrderStart = true;
                        data.OrderStartFromDate = parameters.OrderStartFromDate;
                        data.OrderStartToDate = parameters.OrderStartToDate;
                    }
                    if (parameters.FilterDatesDealCredit) {
                        data.FilterDatesDealCredit = true;
                        data.DealCreditFromDate = parameters.DealCreditFromDate;
                        data.DealCreditToDate = parameters.DealCreditToDate;
                    }
                    if (parameters.FilterDatesDealInsurance) {
                        data.FilterDatesDealInsurance = true;
                        data.DealInsuranceFromDate = parameters.DealInsuranceFromDate;
                        data.DealInsuranceToDate = parameters.DealInsuranceToDate;
                    }
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

(<any>window).report = new OrdersByDealReport();