import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class CreateInvoiceProcessReportRequest {
    BatchId: string;
    ExceptionsOnly: boolean;
}

export class CreateInvoiceProcessReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            let request = new CreateInvoiceProcessReportRequest();
            request.BatchId = parameters.BatchId;
            request.ExceptionsOnly = parameters.ShowExceptions;
       
            HandlebarsHelpers.registerHelpers(); 
            let createInvoiceProcess: any = {};
            
            let CreateInvoiceProcessPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/createinvoiceprocessreport/runreport`, authorizationHeader, request)
                .then((response: DataTable) => {

                    createInvoiceProcess = DataTable.toObjectList(response);
                    createInvoiceProcess.BatchNumber = createInvoiceProcess[3].BatchNumber;
                    createInvoiceProcess.Today = moment().format('LL');
                    createInvoiceProcess.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    createInvoiceProcess.Report = 'Create Invoice Process Report';
                    createInvoiceProcess.System = 'RENTALWORKS';
                    createInvoiceProcess.Company = '4WALL ENTERTAINMENT';
                    console.log('createInvoiceProcess: ', createInvoiceProcess);
                    
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