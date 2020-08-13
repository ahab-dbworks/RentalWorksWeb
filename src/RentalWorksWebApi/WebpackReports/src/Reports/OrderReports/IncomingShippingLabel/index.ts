﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
//import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
//const hbFooter = require("./hbFooter.hbs");

export class IncomingShippingLabel extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/incomingshippinglabel/runreport`, authorizationHeader, parameters)
                        .then((response: DataTable) => {
                            const data: any = DataTable.toObjectList(response);
                            //data.PrintTime = moment().format('h:mm:ss A');
                            //data.PrintDate = moment().format('MM/DD/YYYY');
                            //data.PrintDateTime = `${moment().format('MM/DD/YYYY')} ${moment().format('h:mm:ss A')}`;
                            //data.AsOfDate = parameters.AsOfDate;
                            //data.Report = 'Incoming Shipping Label';
                            //data.System = 'RENTALWORKS';
                            //data.Company = parameters.companyName;
                            //this.renderFooterHtml(data);
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }
                            console.log('data: ', data);
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
                })
                .catch((ex) => {
                    console.log('exception: ', ex)
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }
    //renderFooterHtml(model: DataTable): string {
    //    this.footerHtml = hbFooter(model);
    //    return this.footerHtml;
    //}
}

(<any>window).report = new IncomingShippingLabel();