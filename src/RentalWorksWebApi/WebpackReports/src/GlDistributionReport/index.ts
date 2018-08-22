import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class GLDistributionReport extends WebpackReport {
    glDistribution: GLDistribution = null;

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            // experimental
            this.renderProgress = 50;
            this.renderStatus = 'Running';

            HandlebarsHelpers.registerHelpers();
            let glDistribution = new GLDistribution();

            // get the Report
            let glDistributionPromise = Ajax.get<GLDistribution>(`${apiUrl}/api/v1/gldistributionreport/`, authorizationHeader)
                .then((response: GLDistribution) => {
                    glDistribution = response;
                    this.renderHeaderHtml(glDistribution);
                    this.renderFooterHtml(glDistribution);
                    glDistribution.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    glDistribution.FromDate = parameters.fromdate;
                    glDistribution.ToDate = parameters.todate;
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(glDistribution);
                    
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderHeaderHtml(model: GLDistribution): string {
        this.headerHtml = hbHeader(model);
        return this.headerHtml;
    }

    renderFooterHtml(model: GLDistribution) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new GLDistributionReport();

class GLDistribution {
    _Custom = new Array<CustomField>();
    GLItems = new Array<glDistributionItem>();
    PrintTime = '';
    FromDate = '';
    ToDate = '';
}

class glDistributionItemRequest {
    "miscfields" = {};
    "module" = '';
    "options" = {};
    "orderby" = 'Location,GroupHeadingOrder,AccountNumber,AccountDescription';
    "pageno" = 1;
    "pagesize" = 10;
    "searchfieldoperators": Array<any> = [];
    "searchfields": Array<any> = [];
    "searchfieldvalues": Array<any> = [];
    "uniqueids" = { "LocationId": '', "FromDate": '', "ToDate":'' };
}

class glDistributionItem {
    "LocationId": string;
    "Location": string;
    "GroupHeading": string;
    "GroupHeadingOrder": string;
    "AccountNumber": string;
    "AccountDescription": string;
    "Debit": string;
    "Credit": string;
}
