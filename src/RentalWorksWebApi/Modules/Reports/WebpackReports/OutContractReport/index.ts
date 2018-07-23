import { IReport } from '../../FwReportLibrary/src/IReport';
import { CustomField } from '../../FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn } from '../../FwReportLibrary/src/DataTable';
import { Ajax } from '../../FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class OutContractReport implements IReport {
    contract: OutContract = null;
    renderReportCompleted = false;
    renderReportFailed = false;
    headerHtml = '';
    footerHtml = '';
    
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            HandlebarsHelpers.registerHelpers();
            let contract = new OutContract();

            // get the Contract
            let contractPromise = Ajax.get<OutContract>(`${apiUrl}/api/v1/outcontractreport/${parameters.contractid}`, authorizationHeader)
                .then((value: OutContract) => {
                    contract = value;
                    contract.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    contract.ContractTime = moment(contract.ContractTime, 'h:mm a').format('h:mm a');

                    document.getElementById('contract').innerHTML = hbReport(contract);
                    document.getElementById('pageHeader').innerHTML = this.getHeaderHtml(contract);
                    document.getElementById('pageFooter').innerHTML = this.getFooterHtml(contract);
                    this.renderReportCompleted = true;
                })
                .catch((ex) => {
                    console.log(ex);
                });
        } catch (err) {
            Ajax.logError('An error occured while rendering the report.', err);
            this.renderReportCompleted = true;
            this.renderReportFailed = true;
        }
    }

    getHeaderHtml(model: OutContract): string {
        return hbHeader(model);
    }

    getFooterHtml(model: OutContract) : string {
        return hbFooter(model);
    }
}

(<any>window).report = new OutContractReport();

class OutContract {
    _Custom = new Array<CustomField>();
    ContractId = '';
    ContractNumber = '';
    ContractType = '';
    ContractDate = '';
    ContractTime = '';
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

