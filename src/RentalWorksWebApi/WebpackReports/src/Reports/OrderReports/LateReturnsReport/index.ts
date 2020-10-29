﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class LateReturnsReport extends WebpackReport {
    // DO NOT USE THIS REPORT AS A TEMPLATE
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {

            if (parameters.LateReturns) {
                parameters.Type = 'PASTDUE'
                parameters.ReportType = 'PAST_DUE';
                parameters.Days = parameters.DaysPastDue;
                parameters.headerText = `${parameters.DaysPastDue} Days Past Due`;
            }
            if (parameters.DueBack) {
                parameters.Type = 'DUEBACK'
                parameters.ReportType = 'DUE_IN';
                parameters.Days = parameters.DueBackFewer;
                parameters.headerText = `Due Back in ${parameters.DueBackFewer} Days`;
            }
            if (parameters.DueBackOn) {
                parameters.Type = 'DUEBACK'
                parameters.ReportType = 'DUE_DATE';
                parameters.DueBackDate = moment(parameters.DueBackDate).locale(parameters.Locale).format('L');
                parameters.headerText = `Due Back on ${parameters.DueBackDate}`;
            }


            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response); 
                    //for (let i = 0; i < data.length; i++) {
                    //    if (data[i].RowType === 'OrderNumberheader') {
                    //        data[i].OrderDate = data[i + 1].OrderDate;
                    //        data[i].OrderDescription = data[i + 1].OrderDescription;
                    //        data[i].Agent = data[i + 1].Agent;
                    //        data[i].OrderedByName = data[i + 1].OrderedByName;
                    //        data[i].BillDateRange = data[i + 1].BillDateRange;
                    //        //data[i].OrderUnitValue = data[i + 1].OrderUnitValue; // These values were not served by the API => Undefined
                    //        //data[i].OrderReplacementCost = data[i + 1].OrderReplacementCost;
                    //        data[i].OrderFromDate = data[i + 1].OrderFromDate;
                    //        data[i].OrderToDate = data[i + 1].OrderToDate;
                    //        data[i].OrderPastDue = data[i + 1].OrderPastDue;
                    //    }
                    //}


                    data.Report = 'Late Return / Due Back Report';
                    data.Type = parameters.Type;
                    data.subtitle = parameters.headerText;
                    this.setReportMetadata(parameters, data, response);
                    if (parameters.ShowUnit) { data.ShowUnit = true };
                    if (parameters.ShowReplacement) { data.ShowReplacement = true };
                    if (parameters.ShowBarCode) { data.ShowBarCode = true };
                    if (parameters.ShowSerial) { data.ShowSerial = true };
                    this.renderFooterHtml(data);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    if (parameters.isCustomReport) {
                        document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    } else {
                        document.getElementById('pageBody').innerHTML = hbReport(data);
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

    renderFooterHtml(model: DataTable) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new LateReturnsReport();