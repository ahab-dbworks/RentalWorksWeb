import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class PickListReportRequest {
    PickListId: string;
}

export class PickListReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new PickListReportRequest();
            let pickList: any = {};

            request.PickListId = parameters.PickListId;

            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/picklistreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    pickList = DataTable.toObjectList(response);
                    pickList.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    pickList.Report = 'Pick List Report';
                    pickList.System = 'RENTALWORKS';
                    pickList.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(pickList);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(pickList);
                    console.log('pickList: ', pickList )
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

(<any>window).report = new PickListReport();