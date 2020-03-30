import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as QrCodeGen from '../../../../lib/FwReportLibrary/src/scripts/QrCodeGen';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class PickListReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/picklistreport/runreport`, authorizationHeader, parameters)
                .then((response: any) => {
                    const data: any = response;
                    //data.rows = DataTable.toObjectList(response.Items);
                    data.PrintTime = moment().format('h:mm:ss A');
                    data.PrintDate = moment().format('MM/DD/YYYY');
                    data.PrintDateTime = `${moment().format('MM/DD/YYYY')} ${moment().format('h:mm:ss A')}`;
                    data.System = 'RENTALWORKS';
                    data.Company = parameters.companyName;
                    data.NewPagePerType = parameters.NewPagePerType;
                    //data.rows[1].IsFirstInventoryTypeHeader = true;
                    data.Items[1].IsFirstInventoryTypeHeader = true;

                    if (parameters.OrderType === 'T') {
                        data.Report = 'TRANSFER PICK LIST';
                        data.Type = 'Transfer';
                    } else {
                        ;
                        data.Report = 'PICK LIST';
                        data.Type = 'Order';
                    }

                    if (parameters.BarCodeStyle != '1D') parameters.BarCodeStyle = '2D';
                    data.BarCodeStyle = parameters.BarCodeStyle;

                    const qr = QrCodeGen.QrCode.encodeText(data.OrderNumber, QrCodeGen.Ecc.MEDIUM);
                    const svg = qr.toSvgString(4);
                    data.QrCode = svg;

                    console.log(data);

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

    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new PickListReport();