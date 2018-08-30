import { WebpackReport } from '../../lib/FwReportLibrary/src/WebpackReport';
import { CustomField } from '../../lib/FwReportLibrary/src/CustomField';
import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/DataTable';
import { Ajax } from '../../lib/FwReportLibrary/src/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/HandlebarsHelpers';
import * as moment from 'moment';
import './index.scss';
import '../../theme/webpackReports.scss'
var hbHeader = require("./hbHeader.hbs"); 
var hbReport = require("./hbReport.hbs"); 
var hbFooter = require("./hbFooter.hbs"); 

export class CrewSignInReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            let request = new BrowseRequest();
            request.uniqueids = {};

            let crewSignIn: any = {};

            request.orderby = 'Location, Department, RentFromDate';
            request.uniqueids.ToDate = parameters.ToDate;
            request.uniqueids.FromDate = parameters.FromDate;
            if (parameters.OfficeLocationId != '') {
                request.uniqueids.LocationId = parameters.OfficeLocationId
            }
            if (parameters.DepartmentId != '') {
                request.uniqueids.DepartmentId = parameters.DepartmentId
            }
            if (parameters.DealId != '') {
                request.uniqueids.DealId = parameters.DealId
            }
            if (parameters.OrderId != '') {
                request.uniqueids.OrderId = parameters.OrderId
            }
            console.log('parameters: ', parameters);
            console.log('request: ', request)

            
//            let today = new Date();

            let crewSignInPromise = Ajax.post<DataTable>(`${apiUrl}/api/v1/crewsigninreport/browse`, authorizationHeader, request)
                .then((response: DataTable) => {
                    crewSignIn.Rows = DataTable.toObjectList(response); // converts res to javascript obj
                    console.log('crewsignin: ', crewSignIn); 

                    crewSignIn.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                    crewSignIn.FromDate = parameters.FromDate;
                    crewSignIn.ToDate = parameters.ToDate;
                    crewSignIn.Report = 'Crew Sign-In Report';
                    crewSignIn.System = 'RENTALWORKS';
                    crewSignIn.Company = '4WALL ENTERTAINMENT';
                    this.renderHeaderHtml(crewSignIn);
                    this.renderFooterHtml(crewSignIn);
                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
                        document.getElementById('pageHeader').innerHTML = this.headerHtml;
                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
                    }
                    document.getElementById('pageBody').innerHTML = hbReport(crewSignIn);

                    this.onRenderReportCompleted();
                })
                .catch((ex) => {
                    this.onRenderReportFailed(ex);
                });
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderHeaderHtml(model: CrewSignIn): string {
        this.headerHtml = hbHeader(model);
        return this.headerHtml;
    }

    renderFooterHtml(model: CrewSignIn) : string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new CrewSignInReport();

class CrewSignIn{
    _Custom = new Array<CustomField>();
    OrderId = '';
    LocationId = '';
    MasterId = '';
    DepartmentId = '';
    OrderLocation = '';
    Location = '';
    Department = '';
    RentFromDate = '';
    RentToDate = '';
    RentFromTime = '';
    RentToTime = '';
    OrderNo = '';
    OrderDescription = '';
    DealId = '';
    DealNo = '';
    Deal = '';
    Position = '';
    EmployeeId = '';
    Person = '';
    CrewContactId = '';
}

