import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class CustomerRevenueByMonthReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            Ajax.post<DataTable>(`${apiUrl}/api/v1/CustomerRevenueByMonthReport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    let types: any = [];
                    // Revenue Types for header
                    for (let i = 0; i < parameters.RevenueTypes.length; i++) {
                        types.push(parameters.RevenueTypes[i].value);
                    }
                    types = types.join(', ')
                    const data: any = DataTable.toObjectList(response);
                    data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Customer Revenue By Month Report';
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    data.RevenueTypes = types;
                    // Determine Summary or Detail View
                    if (parameters.IsSummary === 'true') {
                        data.IsSummary = true;
                    } else {
                        data.IsSummary = false;
                    }
                    HandlebarsHelpers.registerHelpers
                    const headerNames = [];
                    //const rowNames = [];
                    let headerCount = 0;

                    for (let i = 0; i < data.length; i++) {
                        const el = data[i];
                        if (el) {
                            if (el.RowType === 'detail') {
                                for (let key in el) {
                                    if (key.endsWith('Name')) {
                                        if (el[key] !== '') {
                                            headerNames.push(el[key]);
                                            headerCount++
                                            //rowNames.push(key.substring(0, 7));
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    //JQuery(document).find('.summary-row').app
                    //console.log('rowNames', rowNames)
                    console.log('rpt: ', data);
                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }

                    document.getElementById('pageBody').innerHTML = hbReport(data);
                    let mappedHeader = headerNames.map(el => `<th>${el}</th>`).join('');
                    mappedHeader = `<th></th>` + mappedHeader;
                    if (headerCount < 12) {
                        const intialColspan = (12 - headerCount);
                        //headerNames.unshift(`<th colspan="${colspan}"></th>`);
                        mappedHeader = mappedHeader + `<th colspan="${intialColspan}"></th><th class="number">All Months</th>`;
                    } else if (headerCount === 12) {
                        mappedHeader = mappedHeader + `</th><th class="number">All Months</th>`;
                    }
                    document.getElementById('summaryHeaderRow').innerHTML = mappedHeader;
                    console.log('headerNames', mappedHeader)
                    //document.querySelector('tr[data-rowtype="detail"]').innerHTML = rowNames.map(el => `<td>{{${el}}}</td>`).join('');

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

(<any>window).report = new CustomerRevenueByMonthReport();