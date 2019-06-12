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
                    for (let i = 0; i < parameters.RevenueTypes.length; i++) {
                        types.push(parameters.RevenueTypes[i].value)
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

                    let headerNames = []
                    let headerCount = 0;
                    let rowNames = [];




                    for (let i = 0; i < data.length; i++) {
                        const el = data[i];
                        if (el.RowType === 'detail') {
                            for (let key in el) {
                                if (key.endsWith('Name')) {
                                    console.log('keyVal', el[key])
                                    if (el[key] !== '') {
                                        headerCount++
                                        headerNames.push(el[key])
                                    }
                                }
                            }
                            for (let key in el) {
                                if (!key.endsWith('Name')) {
                                    console.log('vals', el[key])
                                    if (key.startsWith('Month')) { // move this up into one loop
                                        if (el[key] !== '') {
                                            while (headerCount > 0) {
                                                rowNames.push(key)
                                                headerCount--
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    console.log('headerNames', headerNames)
                    console.log('rowNames', rowNames)
                  
                    console.log('rpt: ', data);
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

(<any>window).report = new CustomerRevenueByMonthReport();