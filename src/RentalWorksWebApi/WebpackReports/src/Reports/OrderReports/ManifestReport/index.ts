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
                    Ajax.post<ManifestReportResponse>(`${apiUrl}/api/v1/manifestreport/runreport`, authorizationHeader, parameters)
                        .then((response: ManifestReportResponse) => {
                            const data: any = response;
                            data.Items = DataTable.toObjectList(response.ItemsTable, parameters);
                            data.OrderNumber = parameters.orderno;
                            data.Report = "Value Sheet";
                            data.WhichReport = parameters.manifestReportItems;
                    this.setReportMetadata(parameters, data);
                            
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


                            this.renderFooterHtml(data);
                            if (this.action === 'Preview' || this.action === 'PrintHtml') {
                                document.getElementById('pageFooter').innerHTML = this.footerHtml;
                            }

                            if (parameters.isCustomReport) {
                                document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                            } else {
                                document.getElementById('pageBody').innerHTML = hbReport(data);
                            }
                                // want to add
                            //if (data.TermsAndConditions !== null && data.TermsAndConditions !== '') {
                            //    const termEl = document.getElementById('terms');
                            //    termEl.innerHTML = data.TermsAndConditions;
                            //    if (data.TermsAndConditionsNewPage) {
                            //        const termsRow = document.getElementById('termsRow');
                            //        termsRow.style.cssText = "page-break-before:always;padding:20px 10px 0px 10px;font-size:1em;";
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
interface ManifestReportResponse {
    _Custom: any[];
    Agent: string;
    AgentEmail: string;
    Warehouse: string;
    Department: string;
    OfficeLocation: string;
    OfficeLocationPhone: string;
    OfficeLocationAddress1: string;
    OfficeLocationAddress2: string;
    OfficeLocationCityStateZipCodeCountry: string;
    IssuedToCompany: string;
    IssuedToAttention1: string;
    IssuedToAttention2: string;
    IssuedToAddress1: string;
    IssuedToAddress2: string;
    IssuedToCity: string;
    IssuedToState: string;
    IssuedToZipCode: string;
    IssuedToCountry: string;
    IssuedToPhone: string;
    OutDeliveryLocation: string;
    OutDeliveryAddress1: string;
    OutDeliveryAddress2: string;
    OutDeliveryCity: string;
    OutDeliveryState: string;
    OutDeliveryZipCode: string;
    OutDeliveryCountryId: string;
    OutDeliveryCountry: string;
    OutDeliveryContactPhone: string;
    OutDeliveryCityStateZipCodeCountry: string;
    UsageDates: string;
    BillingDates: string;
    Description: string;
    Deal: string;
    ItemsTable: DataTable;
}

(<any>window).report = new ManifestReport();
