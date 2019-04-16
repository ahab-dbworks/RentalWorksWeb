import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class InvoiceReport extends WebpackReport {
    invoice: Invoice = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            HandlebarsHelpers.registerHelpers();
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/control/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const controlObject: any = response;
                    Ajax.post<Invoice>(`${apiUrl}/api/v1/invoicereport/runreport`, authorizationHeader, parameters)
                    .then((response: Invoice) => {
                        const report: any = response;
                        report.Items = DataTable.toObjectList(response.Items);
                        report.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                        report.System = 'RENTALWORKS';
                        report.Company = '4WALL ENTERTAINMENT';
                        report.Report = 'INVOICE';
                        if (controlObject.ReportLogoImage != '') {
                            report.Logosrc = controlObject.ReportLogoImage;
                        } 
                        this.renderFooterHtml(report);
                        if (this.action === 'Preview' || this.action === 'PrintHtml') {
                            document.getElementById('pageFooter').innerHTML = this.footerHtml;
                        }
                        document.getElementById('pageBody').innerHTML = hbReport(report);
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

    renderFooterHtml(model: Invoice): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new InvoiceReport();

class Invoice {
    _Custom = new Array<CustomField>();
    Report: string;
    Company: string;
    System: string;
    Logosrc: string;
    InvoiceId: string;
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
    InvoiceNumber: string;
    InvoiceDate: string;
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
    InvoiceType: string;
    Description: string;
    InvoiceNumberAndDescription: string;
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
    SubPoInvoiceNumber: string;
    SubPoInvoiceDescription: string;
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
    InvoiceUnit: string;
    InvoiceTypeId: string;
    InvoiceTypeDescription: string;
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