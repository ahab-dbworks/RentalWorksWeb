import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class LostContractReport extends WebpackReport {
    contract: LostContract = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<LostContract>(`${apiUrl}/api/v1/lostcontractreport/runreport`, authorizationHeader, parameters)
                        .then((response: LostContract) => {
                            const data: any = response;
                            data.Items = DataTable.toObjectList(response.Items);
                            data.PrintTime = moment().format('h:mm:ss A');
                            data.PrintDate = moment().format('MM/DD/YYYY');
                            data.PrintDateTime = `${moment().format('MM/DD/YYYY')} ${moment().format('h:mm:ss A')}`;
                            data.System = 'RENTALWORKS';
                            data.Report = 'LOST CONTRACT';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }
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

    renderFooterHtml(model: LostContract): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new LostContractReport();

class LostContract {
    _Custom = new Array<CustomField>();
    RowType: string;
    ContractId: string;
    ContractNumber: string;
    ContractDate: string;
    ContractTime: string;
    ContractDateAndTime: string;
    ContractType: string;
    HasPendingExchange: true;
    InputByUserId: string;
    BillingDate: string;
    Warehouse: string;
    WarehouseAddress1: string;
    WarehouseAddress2: string;
    WarehouseCityStateZipCode: string;
    WarehouseCityStateZipCodeCountry: string;
    WarehousePhone: string;
    WarehouseFax: string;
    DealId: string;
    Deal: string;
    DealNumber: string;
    DealNumberAndDeal: string;
    OrderId: string;
    OrderNumber: string;
    OrderDate: string;
    OrderPoNumber: string;
    OrderType: string;
    OrderDescription: string;
    OrderNumberAndDescription: string;
    ContainerBarCode: string;
    UsageDates: string;
    BillingDates: true;
    BillingCycle: string;
    OrderLocation: string;
    PaymentTerms: string;
    Agent: string;
    AgentPhoneAndExtension: string;
    AgentFax: string;
    Department: string;
    Vendor: string;
    VendorAddress1: string;
    VendorAddress2: string;
    VendorCity: string;
    VendorState: string;
    VendorZipCode: string;
    VendorPhone: string;
    VendorFax: string;
    VendorContact: string;
    PurchaseOrderId: string;
    PurchaseOrderNumber: string;
    DeliveryContact: string;
    DeliveryLocation: string;
    DeliveryAddress1: string;
    DeliveryAddress2: string;
    DeliveryCityStateZipCode: string;
    DeliveryCountry: string;
    DeliveryContactPhone: string;
    TermsAndConditionsId: string;
    TermsAndConditionsFileName: string;
    TermsAndConditionsNewPage: true;
    ResponsiblePersonId: string;
    ResponsiblePerson: string;
    Items: any;
    PrintTime: string;
}

