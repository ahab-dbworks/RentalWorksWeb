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
            let customerRevenueByType: any = {};
            console.log('parameters: ', parameters);

            // get the Contract
            let contractPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/customerrevenuebytypereport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {
                    customerRevenueByType = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('customerRevenueByType: ', customerRevenueByType); // will help in building the handlebars

                    customerRevenueByType.FromDate = parameters.FromDate;
                    customerRevenueByType.ToDate = parameters.ToDate;
                    customerRevenueByType.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    customerRevenueByType.ContractTime = moment(customerRevenueByType.ContractTime, 'h:mm a').format('h:mm a');
                    this.renderFooterHtml(customerRevenueByType);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(customerRevenueByType);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: customerRevenueByType): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CustomerRevenueByTypeReport();

class customerRevenueByType {
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

class customerRevenueByTypeItemRequest {
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

class customerRevenueByTypeItem {
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

