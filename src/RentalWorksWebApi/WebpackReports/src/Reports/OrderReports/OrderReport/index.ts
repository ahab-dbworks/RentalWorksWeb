﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as QrCodeGen from '../../../../lib/FwReportLibrary/src/scripts/QrCodeGen';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';

const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class OrderReport extends WebpackReport {
    order: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/orderreport/runreport`, authorizationHeader, parameters)
                        .then((response: DataTable) => {
                            const data: any = response;

                    this.setReportMetadata(parameters, data, response);
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }
                            data.Report = 'ORDER';

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
                            // Order Print Notes
                            //let isNotes = false;
                            if (data.Notes !== null && data.Notes !== '') {
                                const notesEl = document.getElementById('notes');
                                const notes = data.Notes;
                                if (notes.length) {
                                    const container: Array<string> = [];
                                    for (let i = 0; i < notes.length; i++) {
                                        container.push(`<div><div style="font-weight:700;">${notes[i].Description}:</div><div class="note-cell">${notes[i].Notes}</div></div>`);
                                    }
                                    notesEl.innerHTML = container.join('');
                                    const notesRow = document.getElementById('notesRow');
                                    notesRow.style.cssText = "padding:5px 10px 0px 10px;font-size:1em;width:1110px;";
                                    //isNotes = true;
                                }
                            }
                            // Terms and Conditions
                            if (data.TermsAndConditions !== null && data.TermsAndConditions !== '') {
                                const termEl = document.getElementById('terms');
                                termEl.innerHTML = data.TermsAndConditions;
                                if (data.TermsAndConditionsNewPage) {
                                    const termsRow = document.getElementById('termsRow');
                                    //termsRow.style.cssText = `${isNotes ? '' : 'page-break-before:always;'}padding:10px 10px 0px 10px;font-size:1em;width:1110px;`;
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

(<any>window).report = new OrderReport();

/*class Order {
    _Custom = new Array<CustomField>();
    Report: string;
    Company: string;
    System: string;
    Logosrc: string;
    OrderId: string;
    OfficeLocationId: string;
    OfficeLocation: string;
    OfficeLocationAddress1: string;
    OfficeLocationAddress2: string;
    OfficeLocationCityStateZipCode: string;
    OfficeLocationCountry: string;
    OfficeLocationCityStateZipCodeCountry: string;
    OfficeLocationPhone: string;
    DealAndDealNumber: string;
    CustomerId: string;
    Customer: string;
    DealId: string;
    Deal: string;
    DealNumber: string;
    VendorAndVendorNumber: string;
    Vendor: string;
    VendorNumber: string;
    VersionNumber: string;
    OrderNumber: string;
    OrderDate: string;
    DepartmentId: string;
    Department: string;
    PoNumber: string;
    PoAmount: number;
    BillToName: string;
    BillToAttention: string;
    BillToAddress1: string;
    BillToAddress2: string;
    BillToCity: string;
    BillToState: string;
    BillToZipCode: string;
    BillToCountry: string;
    BillToCityStateZipCode: string;
    BillToCityStateZipCodeCountry: string;
    OrderType: string;
    Description: string;
    OrderNumberAndDescription: string;
    EstimatedStartDate: string;
    EstimatedStopDate: string;
    EstimatedStartTime: string;
    EstimatedStopTime: string;
    EstimatedStartDateTime: string;
    EstimatedStopDateTime: string;
    UsageDates: string;
    UsageDatesAndTimes: string;
    BillingDates: string;
    BillingCycle: string;
    Location: string;
    PaymentTerms: string;
    Agent: string;
    AgentEmail: string;
    AgentPhoneAndExtension: string;
    AgentPhone: string;
    AgentExtension: string;
    AgentFax: string;
    ProjectManager: string;
    ProjectManagerEmail: string;
    ProjectManagerPhoneAndExtension: string;
    ProjectManagerPhone: string;
    ProjectManagerExtension: string;
    ProjectManagerFax: string;
    TermsconditionsId: string;
    TermsAndConditionsFileName: string;
    TermsAndConditionsNewPage: boolean;
    ReferenceNumber: string;
    Approver: string;
    ApprovedDate: string;
    SecondApprover: string;
    SecondApprovedDate: string;
    RequireDate: string;
    RequiredTime: string;
    SubPoOrderNumber: string;
    SubPoOrderDescription: string;
    SubPoDeal: string;
    SubPoDealNumber: string;
    SubPoDealAndDealNumber: string;
    RequisitionNumber: string;
    RequisitionDate: string;
    SummaryInvoiceGroup: number;
    LoadInDate: string;
    LoadInTime: string;
    StrikeDate: string;
    StrikeTime: string;
    IsNoCharge: boolean;
    Consignment: boolean;
    ConsignorAgreementId: string;
    CoverLetterFileName: string;
    ProductionContactId: string;
    ConfirmedBy: string;
    ConfirmedSignature: string;
    ConfirmedDateTime: string;
    CustomerNumber: string;
    OrderUnit: string;
    OrderTypeId: string;
    OrderTypeDescription: string;
    IssuedToCompany: string;
    IssuedToAttention1: string;
    IssuedToAttention2: string;
    IssuedToAddress1: string;
    IssuedToAddress2: string;
    IssuedToCity: string;
    IssuedToState: string;
    IssuedToZipCode: string;
    IssuedToCountry: string;
    IssuedToCountryId: string;
    IssuedToPhone: string;
    IssuedToFax: string;
    IssuedToContact: string;
    IssuedToContactPhone: string;
    IssuedToContactEmail: string;
    OutDeliveryId: string;
    OutDeliveryType: string;
    OutDeliveryTypeDisplay: string;
    OutDeliveryAttention: string;
    OutDeliveryLocation: string;
    OutDeliveryAddress1: string;
    OutDeliveryAddress2: string;
    OutDeliveryCity: string;
    OutDeliveryState: string;
    OutDeliveryZipCode: string;
    OutDeliveryCountryId: string;
    OutDeliveryCountry: string;
    OutDeliveryContact: string;
    OutDeliveryContactPhone: string;
    OutDeliveryRequiredByDate: string;
    OutDeliveryRequiredbyTime: string;
    OutDeliveryRequiredByDateTime: string;
    OutDeliveryCarrierId: string;
    OutDeliveryCarrier: string;
    OutDeliveryShipViaId: string;
    OutDeliveryShipVia: string;
    OutDeliveryDeliveryNotes: string;
    InDeliveryId: string;
    InDeliveryType: string;
    InDeliveryTypeDisplay: string;
    InDeliveryAttention: string;
    InDeliveryLocation: string;
    InDeliveryAddress1: string;
    InDeliveryAddress2: string;
    InDeliveryCity: string;
    InDeliveryState: string;
    InDeliveryZipCode: string;
    InDeliveryCountryId: string;
    InDeliveryCountry: string;
    InDeliveryContact: string;
    InDeliveryContactPhone: string;
    InDeliveryRequiredByDate: string;
    InDeliveryRequiredbyTime: string;
    InDeliveryRequiredByDateTime: string;
    InDeliveryCarrierId: string;
    InDeliveryCarrier: string;
    InDeliveryShipViaId: string;
    InDeliveryShipVia: string;
    InDeliveryDeliveryNotes: string
    Items: any;
    RentalItems: any;
    SalesItems: any;
    MiscItems: any;
    LaborItems: any;
    PrintTime: string;
}
*/
