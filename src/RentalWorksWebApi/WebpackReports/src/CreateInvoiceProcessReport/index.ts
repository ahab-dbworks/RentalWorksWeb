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

export class CreateInvoiceProcessReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            let request = new BrowseRequest();
            request.uniqueids = {};
       
            HandlebarsHelpers.registerHelpers(); 
            let createInvoiceProcess: any = {};
            console.log('parameters: ', parameters);
            request.uniqueids.BatchId = parameters.BatchId;
            request.uniqueids.ExceptionsOnly = parameters.ShowExceptions;
            request.orderby = 'OfficeLocation, Department, Deal, OrderNumber';
            console.log('request: ', request);

            
            let CreateInvoiceProcessPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/createinvoiceprocessreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    createInvoiceProcess = DataTable.toObjectList(response);
                    console.log('createInvoiceProcess: ', createInvoiceProcess); 

                    createInvoiceProcess.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    createInvoiceProcess.Report = 'Create Invoice Process Report';
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

    renderFooterHtml(model: DataTable) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CreateInvoiceProcessReport();