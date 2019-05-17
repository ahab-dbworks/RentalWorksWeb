import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class DealOutstandingReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/dealoutstandingitemsreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.IncludeFullImages = false;
                    if (parameters.ShowResponsiblePerson === true) {
                        data.ShowResponsiblePerson = true;
                    }
                    if (parameters.ShowBarcodes === true) {
                        data.ShowBarcodes = true;
                    }
                    if (parameters.ShowVendors === true) {
                        data.ShowVendors = true;
                    }
                    if (parameters.IncludeFullImages === true || parameters.IncludeThumbnailImages === true) {
                        data.ShowImages = true;
                    } else {
                        data.ShowImages = false;
                    }
                    if (parameters.IncludeThumbnailImages === true) {
                        data.IncludeThumbnailImages = true;
                    }
                    if (parameters.IncludeFullImages === true) {
                        data.IncludeFullImages = true;
                    }
                    if (parameters.IncludeValueCost === 'R') {
                        data.IncludeValueCost = 'R';
                        data.IncludeValue = true;
                    }
                    if (parameters.IncludeValueCost === 'U') {
                        data.IncludeValueCost = 'U';
                        data.IncludeValue = true;
                    }
                    if (parameters.IncludeValueCost === 'P') {
                        data.IncludeValueCost = 'P';
                        data.IncludeValue = true;
                    }
              
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Deal Outstanding Items Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    this.renderFooterHtml(data);
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

(<any>window).report = new DealOutstandingReport();