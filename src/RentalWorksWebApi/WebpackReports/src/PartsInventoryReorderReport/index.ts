﻿import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class PartsInventoryReorderReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();

            Ajax.post<DataTable>(`${apiUrl}/api/v1/partsinventoryreorderreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const partsInventoryReorder: any = DataTable.toObjectList(response);
                    partsInventoryReorder.PrintDate = moment().format('MM/DD/YYYY');
                    partsInventoryReorder.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    partsInventoryReorder.FromDate = parameters.FromDate;
                    partsInventoryReorder.ToDate = parameters.ToDate;
                    partsInventoryReorder.Report = 'Parts Inventory Reorder Report';
                    partsInventoryReorder.System = 'RENTALWORKS';
                    partsInventoryReorder.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(partsInventoryReorder);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(partsInventoryReorder);
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

(<any>window).report = new PartsInventoryReorderReport();