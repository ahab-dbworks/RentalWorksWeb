import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../theme/webpackReports.scss'
var hbReport = require("./hbReport2.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class CreateInvoiceProcessReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let createInvoiceProcess: any = {};
            let today = new Date();
            console.log('parameters: ', parameters);
            
            let CreateInvoiceProcessPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/createinvoiceprocessreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    createInvoiceProcess = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('createInvoiceProcess: ', createInvoiceProcess); 

                    createInvoiceProcess.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    createInvoiceProcess.ContractTime = moment(createInvoiceProcess.ContractTime, 'h:mm a').format('h:mm a');
                    createInvoiceProcess.FromDate = parameters.FromDate;
                    createInvoiceProcess.ToDate = parameters.ToDate;
                    createInvoiceProcess.Report = 'AGENT BILLING REPORT';
                    createInvoiceProcess.System = 'RENTALWORKS';
                    createInvoiceProcess.Company = '4WALL ENTERTAINMENT';
                    this.renderFooterHtml(createInvoiceProcess);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(createInvoiceProcess);

                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: CreateInvoiceProcess) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CreateInvoiceProcessReport();

class CreateInvoiceProcess {
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
    PrintTime = '';
}