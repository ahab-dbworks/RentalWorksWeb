import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as QrCodeGen from '../../../../lib/FwReportLibrary/src/scripts/QrCodeGen';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';

const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ManifestReport extends WebpackReport {
    order: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/manifestreport/runreport`, authorizationHeader, parameters)
                        .then((response: any) => {
                            const data: any = response;
                            data.Items = DataTable.toObjectList(response.ItemsTable);
                            data.Company = parameters.companyName;
                            data.OrderNumber = parameters.orderno;
                            data.Report = "Value Sheet";
                            data.WhichReport = parameters.manifestReportItems;
                            data.Date = moment().format('MM/DD/YYYY');
                            data.PrintTime = ` Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                            data.System = 'RENTALWORKS';
                            data.TermsAndConditions == '';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }

                            for (let i = 0; i < data.Items.length; i++) {
                                data.Items[i].WhichReport = data.WhichReport;
                                if (data.Items[i].ValuePerItem !== null) {
                                    data.Items[i].ValuePerItem = data.Items[i].ValuePerItem.toLocaleString('en-US', {
                                        style: 'currency',
                                        currency: 'USD'
                                    });
                                }
                                if (data.Items[i].ValueExtended !== null) {
                                    data.Items[i].ValueExtended = data.Items[i].ValueExtended.toLocaleString('en-US', {
                                        style: 'currency',
                                        currency: 'USD'
                                    });
                                }
                            }


                            const qr = QrCodeGen.QrCode.encodeText(data.OrderNumber, QrCodeGen.Ecc.MEDIUM);
                            const svg = qr.toSvgString(4);
                            data.OrderNumberQrCode = svg;

                            console.log(data, 'DATA');

                            this.renderFooterHtml(data);
                            if (this.action === 'Preview' || this.action === 'PrintHtml') {
                                document.getElementById('pageFooter').innerHTML = this.footerHtml;
                            }

                            if (parameters.isCustomReport) {
                                document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                            } else {
                                document.getElementById('pageBody').innerHTML = hbReport(data);
                            }
                            //if (data.TermsAndConditions !== null || data.TermsAndConditions !== '') {
                            //    const termEl = document.getElementById('terms');
                            //    termEl.innerHTML = data.TermsAndConditions;
                            //    if (data.TermsAndConditionsNewPage) {
                            //        const termsRow = document.getElementById('termsRow');
                            //        termsRow.style.cssText = "page-break-before:always;";
                            //    }
                            //}
                            this.onRenderReportCompleted();
                        }).catch((ex) => {
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

(<any>window).report = new ManifestReport();
