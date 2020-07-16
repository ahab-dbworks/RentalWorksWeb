import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ReceiveContractReport extends WebpackReport {
    contract: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<any>(`${apiUrl}/api/v1/receivecontractreport/runreport`, authorizationHeader, parameters)
                        .then((response: any) => {
                            const data: any = response;
                            data.PrintTime = moment().format('h:mm:ss A');
                            data.PrintDate = moment().format('MM/DD/YYYY');
                            data.PrintDateTime = `${moment().format('MM/DD/YYYY')} ${moment().format('h:mm:ss A')}`;
                            data.System = 'RENTALWORKS';
                            data.Report = 'RECEIVE CONTRACT';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }

                            data.IncludeBarCodes = parameters.IncludeBarCodes;
                            data.IncludeSerialNumbers = parameters.IncludeSerialNumbers;
                            data.IncludeRfids = parameters.IncludeRfids;
                            data.IncludeManufacturerPartNumbers = parameters.IncludeManufacturerPartNumbers;

                            this.renderFooterHtml(data);
                            if (this.action === 'Preview' || this.action === 'PrintHtml') {
                                document.getElementById('pageFooter').innerHTML = this.footerHtml;
                            }
                            if (parameters.isCustomReport) {
                                document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                            } else {
                                document.getElementById('pageBody').innerHTML = hbReport(data);
                            }
                            // Terms and Conditions
                            if (data.TermsAndConditionsHtml !== null && data.TermsAndConditionsHtml !== '') {
                                const termEl = document.getElementById('terms');
                                termEl.innerHTML = data.TermsAndConditionsHtml;
                                if (data.TermsAndConditionsNewPage) {
                                    const termsRow = document.getElementById('termsRow');
                                    termsRow.style.cssText = `page-break-before:always;padding:10px 10px 0px 10px;font-size:1em;width:1110px;`;
                                }
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

    renderFooterHtml(model: any): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new ReceiveContractReport();

