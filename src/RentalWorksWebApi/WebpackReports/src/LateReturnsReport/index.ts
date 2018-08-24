import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable'; // added Browse Request obj
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class LateReturnDueBackReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let lateReturnDueBack: any = {};
            console.log('parameters: ', parameters);

            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/latereturnsreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    lateReturnDueBack = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('lateReturnDueBack: ', lateReturnDueBack); // will help in building the handlebars

                    lateReturnDueBack.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    lateReturnDueBack.ContractTime = moment(lateReturnDueBack.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderHeaderHtml(lateReturnDueBack);
                    this.renderFooterHtml(lateReturnDueBack);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(lateReturnDueBack);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderHeaderHtml(model: LateReturnDueBack): string {
        this.headerHtml = hbHeader(model);
        return this.headerHtml;
    }

    renderFooterHtml(model: LateReturnDueBack) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new LateReturnDueBackReport();

class LateReturnDueBack {
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
    RentalItems = new Array<lateReturnDueBackItem>();
    SalesItems = new Array<lateReturnDueBackItem>();
    PrintTime = '';
}

class lateReturnDueBackItemRequest {
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

class lateReturnDueBackItem {
    "Agent": string;
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

