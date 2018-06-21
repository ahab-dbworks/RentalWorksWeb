class Report {
    contract: ContractLogic = null;
    renderReportCompleted = false;
    renderReportFailed = false;
    headerHtml = '';
    footerHtml = '';

    //renderReport(apiToken: string, baseUrl: string, contractId: string): void {
    renderReport(apiToken, baseUrl, parameters): void {
        try {
            let me = this;
            sessionStorage.setItem('apiToken', apiToken);
            sessionStorage.setItem('baseUrl', baseUrl);
            $('#apiToken').text(sessionStorage.getItem('apiToken'));

            this.getContract(apiToken, baseUrl, parameters.contractid)
                .then(function (contract: ContractLogic) {
                    me.getDeal(apiToken, baseUrl, contract.DealId)
                        .then(function (deal: DealLogic) {
                            contract.DealObj = deal;
                            me.contract = contract;
                            let templateOutContract = $('#outContractView').html();
                            let hbOutContract = Handlebars.compile(templateOutContract);
                            let renderedOutContractHtml = hbOutContract(contract);
                            $('#contractHeader').html(renderedOutContractHtml);

                            me.getRentalItems(apiToken, baseUrl, parameters.contractid)
                                .then(function (items: ContractItem[]) {
                                    contract.RentalItems = items;
                                    let rentalItemsTemplate = $('#rentalItemsTemplate').html();
                                    let hbRentalItems = Handlebars.compile(rentalItemsTemplate);
                                    let renderedRentalItems = hbRentalItems(contract);
                                    $('#rentalItems').html(renderedRentalItems);

                                    let receivedByTemplate = $('#receivedByTemplate').html();
                                    let hbReceivedBy = Handlebars.compile(receivedByTemplate);
                                    let renderedReceivedBy = hbReceivedBy(contract);
                                    $('#receivedBy').html(renderedReceivedBy);

                                    me.headerHtml = me.getHeaderHtml(contract);
                                    $('#pageHeader').html(me.headerHtml);

                                    me.footerHtml = me.getFooterHtml(contract);
                                    $('#pageFooter').html(me.footerHtml);

                                    me.renderReportCompleted = true;
                                })
                                .catch(function (message: string) {
                                    me.renderReportCompleted = true;
                                    me.renderReportFailed = true;
                                });
                        })
                        .catch(function (xhr: XMLHttpRequest) {
                            me.renderReportCompleted = true;
                            me.renderReportFailed = true;
                        });
                })
                .catch(function (xhr: XMLHttpRequest) {
                    me.renderReportCompleted = true;
                    me.renderReportFailed = true;
                });
        } catch (ex) {
            console.log(ex)
        }
    }

    getContract(apiToken: string, baseUrl: string, contractId: string): JQueryXHR {
         return $.ajax({
            url: `${baseUrl}/api/v1/contract/${contractId}`,
            method: 'GET',
            data: {},
            cache: false,
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${apiToken}`
            }
        });
    }

    getDeal(apiToken: string, baseUrl: string, dealId: string): JQueryXHR {
        return $.ajax({
            url: `${baseUrl}/api/v1/deal/${dealId}`,
            method: 'GET',
            data: {},
            cache: false,
            contentType: 'application/json',
            headers: {
                'Authorization': `Bearer ${apiToken}`
            }
        });
    }

    getRentalItems(apiToken: string, baseUrl: string, dealId: string): Promise<ContractItem[]> {
        return new Promise<ContractItem[]>(function (resolve, reject) {
            try {
                let items = Array<ContractItem>();
                for (let i = 0; i < 200; i++) {
                    let item = new ContractItem();
                    item.ICode = '5302275';
                    item.Description = 'CHIMERA SPEED RINNG 7 1/4';
                    item.QtyOrdered = 2;
                    item.Out = 2;
                    item.TotalOut = 2;
                    items.push(item);
                }
                resolve(items);
            } catch (ex) {
                reject(ex.Message);
            }
        });
    }

    getHeaderHtml(contract: ContractLogic): string {
        let template = jQuery('#headerTemplate').html();
        let hb = Handlebars.compile(template);
        let renderedHtml = hb(contract);
        return renderedHtml;
    }

    getFooterHtml(contract: ContractLogic) : string {
        let template = jQuery('#footerTemplate').html();
        let hb = Handlebars.compile(template);
        let renderedHtml = hb(contract);
        return renderedHtml;
    }
}
var report = new Report();

class ContractLogic {
    _Custom: {
        FieldName: string;
        FieldValue: string;
        FieldType: string;
    }[];
    ContractId: string;
    ContractNumber: string;
    ContractType: string;
    ContractDate: string;
    ContractTime: string;
    LocationId: string;
    LocationCode: string;
    Location: string;
    WarehouseId: string;
    WarehouseCode: string;
    Warehouse: string;
    CustomerId: string;
    DealId: string;
    Deal: string;
    DepartmentId: string;
    Department: string;
    PurchaseOrderId: string;
    PurchaseOrderNumber: string;
    RequisitionNumber: string;
    VendorId: string;
    Vendor: string;
    Migrated: boolean;
    NeedReconcile: boolean;
    PendingExchange: boolean;
    ExchangeContractId: string;
    Rental: boolean;
    Sales: boolean;
    InputByUserId: string;
    InputByUser: string;
    DealInactive: boolean;
    Truck: boolean;
    BillingDate: string;
    HasAdjustedBillingDate: boolean;
    HasVoId: boolean;
    SessionId: string;
    OrderDescription: string;
    DateStamp: string;
    RecordTitle: string;
    DealObj: DealLogic;
    RentalItems: RentalItem[]
}

class ContractItem {
    ICode: string;
    Description: string;
    QtyOrdered: number;
    Out: number;
    TotalOut: number;
}

class RentalItem extends ContractItem {

}

class DealLogic {
    _Custom: {
        FieldName: string;
        FieldValue: string;
        FieldType: string;
    }[];
    DealId:	string
    Deal:	string
    DealNumber:	string
    CustomerId:	string
    Customer:	string
    LocationId:	string
    Location:	string
    DealTypeId:	string
    DealType:	string
    Address1:	string
    Address2:	string
    City:	string
    State:	string
    ZipCode:	string
    CountryId:	string
    Country:	string
    Phone:	string
    Phone800:	string
    Fax:	string
    PhoneOther:	string
    DepartmentId:	string
    Department:	string
    CsrId:	string
    Csr:	string
    DefaultAgentId:	string
    DefaultAgent:	string
    DefaultProjectManagerId:	string
    DefaultProjectManager:	string
    DealClassificationId:	string
    DealClassification:	string
    ProductionTypeId:	string
    ProductionType:	string
    DealStatusId:	string
    DealStatus:	string
    StatusAsOf:	string
    ExpectedWrapDate:	string
    BillingCycleId:	string
    BillingCycle:	string
    PaymentTermsId:	string
    PaymentTerms:	string
    PaymentTypeId:	string
    PaymentType:	string
    DefaultRate:	string
    UseCustomerDiscount:	boolean
    UseDiscountTemplate:	boolean
    DiscountTemplateId:	string
    DiscountTemplate:	string
    SalesRepresentativeId:	string
    SalesRepresentative:	string
    CommissionRate:	number
    CommissionIncludesVendorItems:	boolean
    PoRequired:	boolean
    PoType:	string
    BillToAddressType:	string
    BillToAttention1:	string
    BillToAttention2:	string
    BillToAddress1:	string
    BillToAddress2:	string
    BillToCity:	string
    BillToState:	string
    BillToCountryId:	string
    BillToCountry:	string
    BillToZipCode:	string
    AssessFinanceCharge:	boolean
    AllowBillingScheduleOverride:	boolean
    AllowRebateCreditInvoices:	boolean
    UseCustomerCredit:	boolean
    CreditStatusId:	string
    CreditStatus:	string
    CreditStatusThrough:	string
    CreditApplicationOnFile:	boolean
    UnlimitedCredit:	boolean
    CreditLimit:	number
    CreditBalance:	number
    CreditAvailable:	number
    CustomerCreditLimit:	number
    CustomerCreditBalance:	number
    CustomerCreditAvailable:	number
    CreditResponsiblePartyOnFile:	boolean
    CreditResponsibleParty:	string
    TradeReferencesVerified:	boolean
    TradeReferencesVerifiedBy:	string
    TradeReferencesVerifiedOn:	string
    CreditCardTypeId:	string
    CreditCardType:	string
    CreditCardLimit:	number
    CreditCardNumber:	string
    CreditCardCode:	string
    CreditCardName:	string
    CreditCardExpirationMonth:	number
    CreditCardExpirationYear:	number
    CreditCardAuthorizationFormOnFile:	boolean
    DepletingDepositThresholdAmount:	number
    DepletingDepositThresholdPercent:	number
    DepletingDepositTotal:	number
    DepletingDepositApplied:	number
    DepletingDepositRemaining:	number
    UseCustomerInsurance:	boolean
    InsuranceCertification:	boolean
    InsuranceCertificationValidThrough:	string
    InsuranceCoverageLiability:	number
    InsuranceCoverageLiabilityDeductible:	number
    InsuranceCoverageProperty:	number
    InsuranceCoveragePropertyDeductible:	number
    SecurityDepositAmount:	number
    InsuranceCompanyId:	string
    InsuranceCompany:	string
    InsuranceCompanyAgent:	string
    InsuranceCompanyAddress1:	string
    InsuranceCompanyAddress2:	string
    InsuranceCompanyCity:	string
    InsuranceCompanyState:	string
    InsuranceCompanyZipCode:	string
    InsuranceCompanyCountryId:	string
    InsuranceCompanyCountry:	string
    InsuranceCompanyPhone:	string
    InsuranceCompanyFax:	string
    VehicleInsuranceCertification:	boolean
    UseCustomerTax:	boolean
    Taxable:	boolean
    TaxStateOfIncorporationId:	string
    TaxStateOfIncorporation:	string
    TaxFederalNo:	string
    NonTaxableYear:	number
    NonTaxableCertificateNo:	string
    NonTaxableCertificateValidThrough:	string
    NonTaxableCertificateOnFile:	boolean
    DisableQuoteOrderActivity:	boolean
    DisableRental:	boolean
    DisableSales:	boolean
    DisableFacilities:	boolean
    DisableTransportation:	boolean
    DisableLabor:	boolean
    DisableMisc:	boolean
    DisableRentalSale:	boolean
    DisableSubRental:	boolean
    DisableSubSale:	boolean
    DisableSubLabor:	boolean
    DisableSubMisc:	boolean
    DefaultOutgoingDeliveryType:	string
    DefaultIncomingDeliveryType:	string
    ShippingAddressType:	string
    ShipAttention:	string
    ShipAddress1:	string
    ShipAddress2:	string
    ShipCity:	string
    ShipState:	string
    ShipCountryId:	string
    ShipCountry:	string
    ShipZipCode:	string
    RebateRental:	boolean
    RebateCustomerId:	string
    RebateCustomer:	string
    OwnedEquipmentRebateRentalPerecent:	number
    SubRentalEquipmentRebateRentalPerecent:	number
    RecordTitle:	string
}
