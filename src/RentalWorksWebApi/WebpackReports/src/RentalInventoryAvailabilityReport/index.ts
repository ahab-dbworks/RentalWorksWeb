import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryAvailabilityReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            Ajax.post<DataTable>(`${apiUrl}/api/v1/RentalInventoryAvailabilityReport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.Report = 'Rental Inventory Availability Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;

                    // Determine Summary or Detail View
                    if (parameters.IsSummary === 'true') {
                        data.IsSummary = true;
                    } else {
                        data.IsSummary = false;
                    }
                    const headerNames = [];
                    let headerCount = 0;

                    for (let i = 0; i < data.length; i++) {
                        const el = data[i];
                        if (el) {
                            if (el.RowType === 'detail') {
                                for (let key in el) {
                                    if (key.startsWith('Date')) {
                                        if (el[key] !== '') {
                                            headerNames.push(el[key].substring(0, 5));
                                            headerCount++
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }

                    console.log('rpt: ', data)
                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(data);
                    const staticHeader = '<th class="nowrap">I-Code</th><th>Description</th><th class="number">Owned</th><th class="number">On Truck</th><th class="number">Trans Out</th><th class="number">In Repair</th><th class="number">Sub-Rent</th><th class="number">Late</th>';
                    let mappedHeader = headerNames.map(el => `<th class="number">${el}</th>`).join('');
                    //let mappedHeader = '';
                    mappedHeader = staticHeader + mappedHeader;
                    //if (headerCount < 12) {
                    //    const intialColspan = (12 - headerCount);
                    //    mappedHeader = mappedHeader + `<th colspan="${intialColspan}"></th><th class="number">All Months</th>`;
                    //} else if (headerCount === 12) {
                    //    mappedHeader = mappedHeader + `<th class="number">All Months</th>`;
                    //}
                    document.getElementById('HeaderRow').innerHTML = mappedHeader;
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

(<any>window).report = new RentalInventoryAvailabilityReport();