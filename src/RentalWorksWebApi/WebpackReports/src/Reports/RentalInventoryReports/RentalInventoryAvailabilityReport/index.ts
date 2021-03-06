﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryAvailabilityReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/RentalInventoryAvailabilityReport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data, response);
                    data.Report = 'Rental Inventory Availability Report';
                    data.FromDate = moment(parameters.FromDate).locale(parameters.Locale).format('L');
                    data.ToDate = moment(parameters.ToDate).locale(parameters.Locale).format('L');

                    // Determine Summary or Detail View
                    if (parameters.IsDetail === 'true') {
                        data.IsDetail = true;
                    } else {
                        data.IsDetail = false;
                    }
                    // to prevent repeating headers for these rows
                    for (let i = 0; i < data.length; i++) {
                        if (data[i].RowType === 'reservation') {
                            if (data[i - 1].RowType !== 'reservation') {
                                data[i].RenderHeader = true;
                            }
                        }
                    }

                    const headerNames = [];
                    for (let i = 0; i < data.length; i++) {
                        const el = data[i];
                        if (el) {
                            if (el.RowType === 'detail') {
                                for (let key in el) {
                                    if (key.startsWith('AvailabilityDate')) {
                                        if (el[key] !== '' && el[key] != null) {
                                            headerNames.push(el[key]);
                                        } else {
                                            headerNames.push('');
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }

                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    if (parameters.isCustomReport) {
                        document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
                    }
                    const staticDetailHeader = '<th class="nowrap">I-Code</th><th colspan="2">Description</th><th class="number">Owned</th><th class="number">In Repair</th><th class="number">Sub-Rent</th><th class="number">Late</th>';
                    const staticSummaryHeader = '<th class="nowrap">I-Code</th><th>Description</th><th class="number">Owned</th><th class="number">In Repair</th><th class="number">Sub-Rent</th><th class="number">Late</th>';
                    let mappedHeader = headerNames.map(el => `<th class="number">${el}</th>`).join('');
                    if (parameters.IsDetail === 'true') {
                        mappedHeader = staticDetailHeader + mappedHeader;
                    } else {
                        mappedHeader = staticSummaryHeader + mappedHeader;
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

(<any>window).report = new RentalInventoryAvailabilityReport();