import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../theme/webpackReports.scss'
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class AgentBillingReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let agentBilling: any = {};
            let today = new Date();
            console.log('parameters: ', parameters);
            
            let agentBillingPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/agentbillingreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    agentBilling = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('agentBilling: ', agentBilling); 

                    agentBilling.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    agentBilling.ContractTime = moment(agentBilling.ContractTime, 'h:mm a').format('h:mm a');
                    agentBilling.FromDate = parameters.FromDate;
                    agentBilling.ToDate = parameters.ToDate;
                    agentBilling.Report = 'AGENT BILLING REPORT';
                    agentBilling.System = 'RENTALWORKS';
                    agentBilling.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(agentBilling);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(agentBilling);

                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: AgentBilling) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new AgentBillingReport();

class AgentBilling {
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
    RentalItems = new Array<agentBillingItem>();
    SalesItems = new Array<agentBillingItem>();
    PrintTime = '';
}

class agentBillingItemRequest {
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

class agentBillingItem {
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

