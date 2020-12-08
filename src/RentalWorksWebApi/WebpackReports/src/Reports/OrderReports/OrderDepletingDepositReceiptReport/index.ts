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

export class OrderDepletingDepositReceiptReport extends WebpackReport {
    //order: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<any>(`${apiUrl}/api/v1/orderdepletingdepositreceiptreport/runreport`, authorizationHeader, parameters)
                        .then((response: any) => {
                            const data: any = response;
                            data.Report = "Order Depleting Deposit Receipt";
                            this.setReportMetadata(parameters, data, response);

                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }

                            //const qr = QrCodeGen.QrCode.encodeText(data.OrderNumber, QrCodeGen.Ecc.MEDIUM);
                            //const svg = qr.toSvgString(4);
                            //data.OrderNumberQrCode = svg;

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

(<any>window).report = new OrderDepletingDepositReceiptReport();
