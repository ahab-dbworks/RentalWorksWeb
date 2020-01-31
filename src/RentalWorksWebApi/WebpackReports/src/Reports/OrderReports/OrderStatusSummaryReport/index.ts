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

export class OrderStatusSummaryReport extends WebpackReport {
    order: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/orderstatussummaryreport/runreport`, authorizationHeader, parameters)
                        .then((response: DataTable) => {
                            const data: any = DataTable.toObjectList(response);
                            data.Items = data;

                            //let data: any = {
                            //    OfficeLocationCompany: "Fuse Technical Group",
                            //    OfficeLocationAddress1: "3700 Cohasset St",
                            //    OfficeLocationAddress2: "Some Suite",
                            //    OfficeLocationCityStateZipCodeCountry: "Burbank, CA 91505",
                            //    OfficeLocationPhone: "1-800-IM-GONNA-SHOW-YA",
                            //    Report: "Order Status",
                            //    OrderNumber: "L301318 AHAB TEST2",
                            //    Deal: "L200021 A KNIGHT AT THE OPERA",

                            //    Items: [
                            //        {
                            //            RowType: "RecTypeDisplayheader",
                            //            RecTypeDisplay: "RENTAL",
                            //            Bold: false
                            //        },
                            //        {
                            //            RowType: "detail",
                            //            RecTypeDisplay: "RENTAL",
                            //            ICode: "100893",
                            //            Description: "MEYER UPA-1P",
                            //            Bold: false,
                            //            QuantityOrdered: "1",
                            //            StageQuantity: 1,
                            //            OutQuantity: 0,
                            //            InQuantity: 1,
                            //            StillOutQuantity: 0
                            //        },
                            //        {
                            //            RowType: "RecTypeDisplayheader",
                            //            RecTypeDisplay: "SALES",
                            //            Bold: false
                            //        },

                            //    ]
                            //};
                            data.PrintTime = ` Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                            data.System = 'RENTALWORKS';
                            data.TermsAndConditions == '';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
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
               


                    //Ajax.post<DataTable>(`${apiUrl}/api/v1/orderstatusreport/runreport`, authorizationHeader, parameters)
                    //    .then((response: DataTable) => {
                    //        const data: any = DataTable.toObjectList<any>(response);
                    //        //data.Items = DataTable.toObjectList(response.Items);
                    //        //data.RentalItems = DataTable.toObjectList(response.RentalItems);
                    //        //data.SalesItems = DataTable.toObjectList(response.SalesItems);
                    //        //data.MiscItems = DataTable.toObjectList(response.MiscItems);
                    //        //data.LaborItems = DataTable.toObjectList(response.LaborItems);
                    //        data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    //        data.System = 'RENTALWORKS';
                    //        if (logoObject.LogoImage != '') {
                    //            data.Logosrc = logoObject.LogoImage;
                    //        }
                    //        data.Report = 'ORDER STATUS';

                    //        const qr = QrCodeGen.QrCode.encodeText(data.OrderNumber, QrCodeGen.Ecc.MEDIUM);
                    //        const svg = qr.toSvgString(4);
                    //        data.OrderNumberQrCode = svg;

                    //        console.log(data, 'DATA');

                    //        this.renderFooterHtml(data);
                    //        if (this.action === 'Preview' || this.action === 'PrintHtml') {
                    //            document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    //        }

                    //        if (parameters.isCustomReport) {
                    //            document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                    //        } else {
                    //            document.getElementById('pageBody').innerHTML = hbReport(data);
                    //        }
                    //        if (data.TermsAndConditions !== null || data.TermsAndConditions !== '') {
                    //            const termEl = document.getElementById('terms');
                    //            termEl.innerHTML = data.TermsAndConditions;
                    //            if (data.TermsAndConditionsNewPage) {
                    //                const termsRow = document.getElementById('termsRow');
                    //                termsRow.style.cssText = "page-break-before:always;";
                    //            }
                    //        }
                    //        this.onRenderReportCompleted();
                    //    })
                    //    .catch((ex) => {
                    //        this.onRenderReportFailed(ex);
                    //    });
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

(<any>window).report = new OrderStatusSummaryReport();
