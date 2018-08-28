import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class GLDistributionReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let glDistribution: any = {};
            let request = new BrowseRequest();
            request.uniqueids = {
                FromDate: parameters.FromDate,
                ToDate: parameters.ToDate,
            }
            request.orderby = 'Location,GroupHeadingOrder,AccountNumber,AccountDescription';
            if (parameters.OfficeLocationId) {
                request.uniqueids.OfficeLocationId = parameters.OfficeLocationId;
            }
            if (parameters.GlAccountId) {
                request.uniqueids.GlAccountId = parameters.GlAccountId;
            }

            let glDistributionPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/gldistributionreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    glDistribution.GLItems = DataTable.toObjectList(response);
                    this.renderFooterHtml(glDistribution);
                    glDistribution.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    glDistribution.FromDate = parameters.FromDate;
                    glDistribution.ToDate = parameters.ToDate;
                    glDistribution.Location = parameters.Location;
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(glDistribution);

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

(<any>window).report = new GLDistributionReport();