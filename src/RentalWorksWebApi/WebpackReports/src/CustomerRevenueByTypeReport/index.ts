import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse'; // added Browse Request obj
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class CustomerRevenueByTypeReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new BrowseRequest();
       
            HandlebarsHelpers.registerHelpers();
            let customerReveneByType: any = {};
            console.log('parameters: ', parameters);

            if (parameters.FromDate !== '') request.uniqueids.FromDate = parameters.FromDate;
            if (parameters.ToDate !== '') request.uniqueids.ToDate = parameters.ToDate
            if (parameters.DateType !== '') request.uniqueids.DateType = parameters.DateType;
            if (parameters.LocationId !== '') request.uniqueids.LocationId = parameters.LocationId;
            if (parameters.DepartmentId !== '') request.uniqueids.DepartmentId = parameters.DepartmentId;
            if (parameters.CustomerId !== '') request.uniqueids.CustomerId = parameters.CustomerId;
            if (parameters.DealId !== '') request.uniqueids.DealId = parameters.DealId;
            if (parameters.OrderTypeId !== '') request.uniqueids.OrderTypeId = parameters.OrderTypeId;

            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/customerrevenuebytypereport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    customerReveneByType = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('customerReveneByType: ', customerReveneByType); // will help in building the handlebars

                    customerReveneByType.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    customerReveneByType.ContractTime = moment(customerReveneByType.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(customerReveneByType);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(customerReveneByType);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: customerReveneByType) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CustomerRevenueByTypeReport();

class customerReveneByType {
    _Custom = new Array<CustomField>();
    InvoiceId = '';
    InvoiceNumber = '';
    OfficeLocationId = '';
    OfficeLocation = '';
    DepartmentId = '';
    Department = '';
    OrderId = '';
    OrderNumber = '';
    OrderTypeId = '';
    CustomerId = '';
    Customer = '';
    DealId = '';
    Deal = '';
    BillingStart = '';
    BillingEnd = '';
    AgentId = '';
    Agent = '';
    Rental = '';
    Sales = '';
    Facilities = '';
    Labor = '';
    Miscelleaneous = '';
    AssetSale = '';
    Parts = '';
    Tax = '';
    Total = '';
    DealType = '';
    NonBillable = false;
}

class customerReveneByTypeItemRequest {
    "miscfields" = {};
    "module" = '';
    "options" = {};
    "orderby" = '';
    "pageno" = 0;
    "pagesize" = 0;
    "searchfieldoperators": Array<any> = [];
    "searchfields": Array<any> = [];
    "searchfieldvalues": Array<any> = [];
    "uniqueids" = {
        'FromDate': '',
        'ToDate': '',
        'DateType': '',
        'LocationDate': '',
        'DepartmentId': '',
        'CustomerId': '',
        'DealId': '',
        'OrderTypeId': ''
    };
}

class customerReveneByTypeItem {
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

