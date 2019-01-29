import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class VendorInvoiceBatchReportRequest {
    BatchId: string;
    BatchNumber: string;
    OfficeLocation: string;
}

export class VendorInvoiceBatchReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let report: any = {};
            let request = new VendorInvoiceBatchReportRequest();
            request.BatchId = parameters.BatchId;

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/vendorinvoicebatchreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    report.Items = DataTable.toObjectList(response);
                    this.renderFooterHtml(report);
                    report.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    report.Date = moment().format('MM/DD/YYYY');
                    report.Report = 'Vendor Invoice Batch Report';
                    report.System = 'RENTALWORKS';
                    report.Company = '4WALL ENTERTAINMENT';
                    report.BatchNumber = report.Items[0].BatchNumber;
                    report.OfficeLocation = report.Items[0].Location;

                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
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
    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new VendorInvoiceBatchReport();