﻿import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs");
var hbFooter = require("./hbFooter.hbs");

export class OrderReportRequest {
    OrderId: string;
}

export class OrderReport extends WebpackReport {
    order: Order = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            let request = new OrderReportRequest();
            let order = new Order();
            let isOrder = true;
            if (parameters.OrderId) {
                request.OrderId = parameters.OrderId;
            } else {
                request.OrderId = parameters.QuoteId;
                isOrder = false;
            }

            //load report logo here
            //let LogoPromise = Ajax.get<ControlObject>(`${apiUrl}/api/v1/control/1`, authorizationHeader, request)
            // controlObject.ReportLogoImage    .ReportLogoImageHeight      .ReportLogoImageWidth

            let Promise = Ajax.post<Order>(`${apiUrl}/api/v1/orderreport/runreport`, authorizationHeader, request)
                .then((response: Order) => {
                    order = response;
                    order.Items = DataTable.toObjectList(response.Items);
                    order.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    //order.Logo = `<img class="clientlogo" src="${dataUrl}" style="margin-left: 30px; width:1.225in;" />`
                    if (isOrder) {
                        order.Report = 'ORDER';
                    } else {
                        order.Report = 'QUOTE';
                        document.title = 'Quote Report'
                    }
                    order.System = 'RENTALWORKS';
                    order.Company = '4WALL ENTERTAINMENT';
                    console.log('order: ', order)
                    this.renderFooterHtml(order);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(order);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: Order): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new OrderReport();

class Order {
    _Custom = new Array<CustomField>();
    Report: string;
    Company: string;
    System: string;
    Logo: string;
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
    PrintTime: string;
}