import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class ArAgingReportRequest {
    AsOfDate: Date;
    OfficeLocationId: string;
    CustomerId: string;
    DealCsrId: string;
    DealTypeId: string;
    DealId: string;
}

export class ArAgingReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let data: any = {};
            let request = new ArAgingReportRequest();
            request.AsOfDate = parameters.ToDate;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DealCsrId = parameters.CsrId;
            request.CustomerId = parameters.CustomerId;
            request.DealId = parameters.DealId;
            request.DealTypeId = parameters.DealTypeId;

            let promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/aragingreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    data.Rows = DataTable.toObjectList(response);

                    this.renderFooterHtml(data);

                    data.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    data.ToDate = parameters.ToDate;
                    data.Report = 'A/R Aging Report';
                    data.System = 'RENTALWORKS';
                    data.Company = '4WALL ENTERTAINMENT';
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

(<any>window).report = new ArAgingReport();