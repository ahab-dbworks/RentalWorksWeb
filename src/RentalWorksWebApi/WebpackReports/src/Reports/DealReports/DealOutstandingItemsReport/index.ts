import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class DealOutstandingReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/dealoutstandingitemsreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.IncludeFullImages = false;
                    //if (parameters.ShowResponsiblePerson === true) {
                    //    data.ShowResponsiblePerson = true;
                    //}
                    //if (parameters.ShowBarcodes === true) {
                    //    data.ShowBarcodes = true;
                    //}
                    //if (parameters.ShowVendors === true) {
                    //    data.ShowVendors = true;
                    //    data.FooterColspan = 5;
                    //} else {
                    //    data.FooterColspan = 4;
                    //}
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
                    //if (parameters.IncludeValueCost === 'R') {
                    //    data.IncludeValueCost = 'R';
                    //    data.IncludeValue = true;
                    //    data.UnitValueHeading = "Replacement Cost";
                    //}
                    //if (parameters.IncludeValueCost === 'U') {
                    //    data.IncludeValueCost = 'U';
                    //    data.IncludeValue = true;
                    //    data.UnitValueHeading = "Unit Value";
                    //}
                    //if (parameters.IncludeValueCost === 'P') {
                    //    data.IncludeValueCost = 'P';
                    //    data.IncludeValue = true;
                    //    data.UnitValueHeading = "Purchase Amount";
                    //}
              
                    this.setReportMetadata(parameters, data);
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Deal Outstanding Items Report';

                    console.log('rpt', data)
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

(<any>window).report = new DealOutstandingReport();