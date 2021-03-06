import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import * as QrCodeGen from '../../../../lib/FwReportLibrary/src/scripts/QrCodeGen';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class PurchaseOrderReturnList extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/purchaseorderreturnlist/runreport`, authorizationHeader, parameters)
                .then((response: any) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data, response);
                    data.Report = 'VENDOR RETURN LIST';
                    data.Department = parameters.department;

                    //data.PrintAisleShelf = parameters.PrintAisleShelf;
                    //for (let i = 0; i < data.Items.length; i++) {
                    //    data.Items[i].PrintAisleShelf = parameters.PrintAisleShelf;
                    //}
                    //for (let i = 0; i < data.Items.length; i++) {
                    //    data.Items[i].PrintIn = data.PrintIn;
                    //    data.Items[i].PrintOut = data.PrintOut;
                    //}

                    if (parameters.BarCodeStyle === '1D') {
                        parameters.BarCodeStyle = '1D';
                    } else if (parameters.BarCodeStyle === '2D') {
                        parameters.BarCodeStyle = '2D';
                    } else {
                        parameters.BarCodeStyle = '1D';
                    }
                    data.BarCodeStyle = parameters.BarCodeStyle;

                    const qr = QrCodeGen.QrCode.encodeText(data.Session, QrCodeGen.Ecc.MEDIUM);
                    const svg = qr.toSvgString(4);
                    data.QrCode = svg;

                    console.log(parameters, 'parameters');

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

    renderFooterHtml(model: any): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new PurchaseOrderReturnList();
