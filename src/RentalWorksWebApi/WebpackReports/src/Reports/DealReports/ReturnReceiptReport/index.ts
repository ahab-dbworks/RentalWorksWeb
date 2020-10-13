import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import './index.scss';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ReturnReceiptReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/ReturnReceiptReport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response, parameters);
                    this.setReportMetadata(parameters, data);
                    data.FromDate = this.formatDateToLocale(parameters.FromDate, parameters.Locale)
                    data.ToDate = this.formatDateToLocale(parameters.ToDate, parameters.Locale)
                    data.Report = 'Return Receipt Report';
                    // to prevent repeating headers for these rows
                    for (let i = 0; i < data.length; i++) {
                        if (data[i].RecordType === 'ASSIGNED' || data[i].RecordType === 'RETURNED_TO_INVENTORY') {
                            if (data[i + 1].RecordType === 'ASSIGNED' || data[i + 1].RecordType === 'RETURNED_TO_INVENTORY') {
                                data[i + 1].RenderHeader = false;
                            } 
                        }
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

(<any>window).report = new ReturnReceiptReport();