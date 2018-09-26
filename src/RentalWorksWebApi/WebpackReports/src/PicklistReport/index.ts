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
                    picklist.Items = DataTable.toObjectList(response.Items);
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
    Report: string;
    Company: string;
    System: string;
    PicklistId: string;
    OrderId: string;
    Customer: string;
    CustomerNumber: string;
    Deal: string;
    DealNumber: string;
    OrderNumber: string;
    OrderDescription: string;
    Location: string;
    WarehouseId: string;
    Warehouse: string;
    TransferToWarehouseId: string;
    TransferToWarehouse: string;
    PoNumber: string;
    DeliverType: string;
    RequiredDate: string;
    RequiredTime: string;
    RequiredDateTime: string;
    TargetShipDate: string;
    PickNumber: string;
    PhoneExtension: string;
    Agent: string;
    AgentPhoneExtension: string;
    RequestSentTo: string;
    PrepDate: string;
    PrepTime: string;
    EstimatedStartDate: string;
    EstimatedStartTime: string;
    EstimatedStopDate: string;
    EstimatedStopTime: string;
    OrderedBy: string;
    OrderedByPhoneExtension: string;
    Items: any;
    PrintTime: string;
}
