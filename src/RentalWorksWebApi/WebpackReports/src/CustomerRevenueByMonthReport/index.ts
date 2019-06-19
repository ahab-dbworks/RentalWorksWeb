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
                        const el = parameters.RevenueTypes[i].value;
                        if (el === 'R') {
                            types.push('Rental');
                        }
                        if (el === 'S') {
                            types.push('Sales');
                        }
                        if (el === 'P') {
                            types.push('Parts');
                        }
                        if (el === 'M') {
                            types.push('Misc');
                        }
                        if (el === 'L') {
                            types.push('Labor');
                        }
                        if (el === 'F') {
                            types.push('L&D');
                        }
                        if (el === 'RS') {
                            types.push('Rental Sale');
                        }
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
                    const headerNames = [];
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
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }

                    console.log('rpt: ', data);
                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }

                    document.getElementById('pageBody').innerHTML = hbReport(data);
                    let mappedHeader = headerNames.map(el => `<th class="number">${el}</th>`).join('');
                    if (data.IsSummary) {
                        mappedHeader = `<th>Deal</th>` + mappedHeader;
                    } else {
                        mappedHeader = `<th>Category</th>` + mappedHeader;
                    }
                    if (headerCount < 12) {
                        const intialColspan = (12 - headerCount);
                        mappedHeader = mappedHeader + `<th colspan="${intialColspan}"></th><th class="number">All Months</th>`;
                    } else if (headerCount === 12) {
                        mappedHeader = mappedHeader + `<th class="number">All Months</th>`;
                    }
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

(<any>window).report = new CustomerRevenueByMonthReport();