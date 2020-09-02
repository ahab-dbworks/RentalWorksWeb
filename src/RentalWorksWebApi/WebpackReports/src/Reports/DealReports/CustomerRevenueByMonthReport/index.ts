import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import './index.scss';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class CustomerRevenueByMonthReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
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
                    this.setReportMetadata(parameters, data);
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Customer Revenue By Month Report';
                    data.RevenueTypes = types;
                    // Determine Summary or Detail View
                    if (parameters.IsSummary === 'true') {
                        data.IsSummary = true;
                    } else {
                        data.IsSummary = false;
                    }

                    //peek at the first detail row and gather all of the month names for the column heaader row
                    const headerNames = [];
                    for (let i = 0; i < data.length; i++) {
                        const row = data[i];
                        if (row) {
                            if (row.RowType === 'detail') {
                                for (let fieldname in row) {
                                    if (fieldname.endsWith('Name')) {
                                        if (row[fieldname] !== '') {
                                            headerNames.push(row[fieldname]);
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

                    if (parameters.isCustomReport) {
                        document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
                    }

                    // blank out all of the column headers
                    for (let i = 0; i < 12; i++) {          // all month columns
                        let monthIndex: number = i + 1;
                        let elementId: string = "Month" + ("0" + monthIndex).slice(-2) + "Header";  // Month01Header, Month02Header, .. Month12Header
                        document.getElementById(elementId).innerHTML = "";
                    }
                    // inject the column headers gathered above
                    for (let i = 0; i < headerNames.length; i++) {   // only the months within the report date range
                        let monthIndex: number = i + 1;
                        let elementId: string = "Month" + ("0" + monthIndex).slice(-2) + "Header";  // Month01Header, Month02Header, .. Month12Header
                        document.getElementById(elementId).innerHTML = headerNames[i];
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

(<any>window).report = new CustomerRevenueByMonthReport();