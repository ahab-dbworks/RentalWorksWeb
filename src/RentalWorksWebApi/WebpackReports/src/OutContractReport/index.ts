import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbHeader = require("./hbHeader.hbs"); 
const hbReport = require("./hbReport.hbs"); 
const hbFooter = require("./hbFooter.hbs"); 

export class OutContractReport extends WebpackReport {
    contract: OutContract = null;

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();

            // get the Contract
            Ajax.get<OutContract>(`${apiUrl}/api/v1/outcontractreport/${parameters.ContractId}`, authorizationHeader)
                .then((response: OutContract) => {
                    const contract: any = response;
                    contract.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                    this.renderHeaderHtml(contract);
                    this.renderFooterHtml(contract);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(contract);
                    
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderHeaderHtml(model: OutContract): string {
        this.headerHtml = hbHeader(model);
        return this.headerHtml;
    }

    renderFooterHtml(model: OutContract) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new OutContractReport();

class OutContract {
    _Custom = new Array<CustomField>();
    ContractId = '';
    ContractNumber = '';
    ContractType = '';
    ContractDate = '';
    LocationId = '';
    LocationCode = '';
    Location = '';
    WarehouseId = '';
    WarehouseCode = '';
    Warehouse = '';
    CustomerId = '';
    DealId = '';
    Deal = '';
    DepartmentId = '';
    Department = '';
    PurchaseOrderId = '';
    PurchaseOrderNumber = '';
    RequisitionNumber = '';
    VendorId = '';
    Vendor = '';
    Migrated = false;
    NeedReconcile = false;
    PendingExchange = false;
    ExchangeContractId = '';
    Rental = false;
    Sales = false;
    InputByUserId = '';
    InputByUser = '';
    DealInactive = false;
    Truck = false;
    BillingDate = '';
    HasAdjustedBillingDate = false;
    HasVoId = false;
    SessionId = '';
    OrderDescription = '';
    DateStamp = '';
    RecordTitle = '';
    RentalItems = new Array<OutContractItem>();
    SalesItems = new Array<OutContractItem>();
    PrintTime = '';
}

class OutContractItemRequest {
    "miscfields" = {};
    "module" = '';
    "options" = {};
    "orderby" = '';
    "pageno" = 0;
    "pagesize" = 0;
    "searchfieldoperators": Array<any> = [];
    "searchfields": Array<any> = [];
    "searchfieldvalues": Array<any> = [];
    "uniqueids" = { "ContractId": '', "RecType": '' };
}

class OutContractItem {
    "ICode": string;
    "ICodeColor": string;
    "Description": string;
    "DescriptionColor": string;
    "QuantityOrdered": string;
    "QuantityOut": string;
    "TotalOut": string;
    "ItemClass": string;
    "Notes": string;
    "Barcode": string;
}

