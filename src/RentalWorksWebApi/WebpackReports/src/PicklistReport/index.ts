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

export class PickListReportRequest {
    PickListId: string;
}

export class PickListReport extends WebpackReport {
    picklist: Picklist = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new PickListReportRequest();
            let picklist = new Picklist();

            request.PickListId = parameters.PickListId;

            let Promise = Ajax.post<Picklist>(`${apiUrl}/api/v1/picklistreport/runreport`, authorizationHeader, request)
                .then((response: Picklist) => {
                    picklist = response;
                    //pickList = DataTable.toObjectList(response);
                    picklist.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    picklist.Report = 'Pick List Report';
                    picklist.System = 'RENTALWORKS';
                    picklist.Company = '4WALL ENTERTAINMENT';
                    console.log('pickList: ', picklist )
                    this.renderFooterHtml(picklist);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(picklist);
                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: Picklist): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new PickListReport();

class Picklist {
    _Custom = new Array<CustomField>();
    Report = '';
    Company = '';
    System = '';
    PicklistId = '';
    PickNumber = '';
    OrderId = '';
    Customer = '';
    CustomerNumber = '';
    Deal = '';
    OrderNumber = '';
    OrderDescription = '';
    Location = '';
    WarehouseId = '';
    Warehouse = '';
    TransferToWarehouseId = '';
    TransferToWarehouse = '';
    PoNumber = '';
    DeliverType = '';
    RequiredDate = '';
    RequiredTime = '';
    RequiredDateTime = '';
    TargetShipDate = '';
    PhoneExtension = '';
    Agent = '';
    AgentPhoneExtension = '';
    RequestSentTo = '';
    PrepDate = '';
    PrepTime = '';
    EstimatedStartDate = '';
    EstimatedStartTime = '';
    EstimatedStopDate = '';
    EstimatedStopTime = '';
    OrderedBy = '';
    OrderedByPhoneExtension = '';
    Items: DataTable;
    PrintTime = '';
}