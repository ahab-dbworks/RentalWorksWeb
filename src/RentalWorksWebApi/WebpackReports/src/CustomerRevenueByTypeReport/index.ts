import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse'; // added Browse Request obj
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 


export class CustomerRevenueByTypeReportRequest {
    FromDate: Date;
    ToDate: Date;
    DateType: string;
    OfficeLocationId: string;
    DepartmentId: string;
    CustomerId: string;
    DealTypeId: string;
    DealId: string;
    OrderTypeId: string;
}


export class CustomerRevenueByTypeReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';
            let request = new CustomerRevenueByTypeReportRequest();
            request.FromDate = parameters.FromDate;
            request.ToDate = parameters.ToDate
            request.DateType = parameters.DateType;
            request.OfficeLocationId = parameters.OfficeLocationId;
            request.DepartmentId = parameters.DepartmentId;
            request.CustomerId = parameters.CustomerId;
            request.DealTypeId = parameters.DealTypeId;
            request.DealId = parameters.DealId;
            request.OrderTypeId = parameters.OrderTypeId;

            HandlebarsHelpers.registerHelpers();
            let customerReveneByType: any = {};
            console.log('parameters: ', parameters);

            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/customerrevenuebytypereport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    customerReveneByType = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('customerReveneByType: ', customerReveneByType); // will help in building the handlebars

                    customerReveneByType.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    customerReveneByType.ContractTime = moment(customerReveneByType.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(customerReveneByType);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
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

