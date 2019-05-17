﻿import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryQCRequiredReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventoryqcrequiredreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = {};
                    data.rows = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.Report = 'Rental Inventory QC Required Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    data.Today = moment().format('LL');

                    let detailRowCount = 0;
                    const rows = data.rows;
                    for (let i = 0; i < rows.length; i++) {          
                        if (rows[i].RowType === 'detail') {
                            detailRowCount++
                            if (rows[i + 1].RowType !== 'detail') {
                                rows.splice(i + 1, 0, { "RowType": "RowCount", "Count": detailRowCount });
                                detailRowCount = 0;
                            }
                        }
                    }
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

(<any>window).report = new RentalInventoryQCRequiredReport();