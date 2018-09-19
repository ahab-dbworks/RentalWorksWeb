//import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
//import { CustomField } from '../../lib/FwReportLibrary/src/scripts/CustomField';
//import { DataTable, DataTableColumn, BrowseRequest } from '../../lib/FwReportLibrary/src/scripts/Browse';
//import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
//import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
//import * as moment from 'moment';
//import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
//import './index.scss';
//var hbReport = require("./hbReport.hbs");
//var hbFooter = require("./hbFooter.hbs");

//export class DealOutstandingReportRequest {
//    FromDate: Date;
//    ToDate: Date;
//    DateToUse: boolean;
//    OnlyShowOrdersWith: string;
//    ShowDepartmentFrom: string;
//    AllEverOut: string;
//    IncludeValueCost: string;
//    ShowBarcodes: boolean;
//    ShowVendors: boolean;
//    ShowResponsiblePerson: boolean;
//    ShowImages: boolean;
//    ShowContainersOnly: boolean;
//    IncludePendingExchanges: boolean;
//    OfficeLocationId: string;
//    DepartmentId: string;
//    CustomerId: string;
//    DealId: string;
//    InventoryTypeId: string;
//    CategoryId: string;
//}

//export class DealOutstandingReport extends WebpackReport {

//    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
//        console.log('parameters: ',parameters)
//        try {
//            super.renderReport(apiUrl, authorizationHeader, parameters);

//            HandlebarsHelpers.registerHelpers();
//            let request = new DealOutstandingReportRequest();

//            request.ToDate = parameters.ToDate;
//            request.FromDate = parameters.FromDate;
//            request.OnlyShowOrdersWith = parameters.OnlyShowOrdersWith;
//            request.ShowDepartmentFrom = parameters.ShowDepartmentFrom;
//            request.AllEverOut = parameters.AllEverOut;
//            request.IncludeValueCost = parameters.IncludeValueCost;
//            request.ShowBarcodes = parameters.ShowBarcodes;
//            request.ShowVendors = parameters.ShowVendors;
//            request.ShowImages = parameters.ShowImages;
//            request.ShowContainersOnly = parameters.ShowContainersOnly;
//            request.IncludePendingExchanges = parameters.IncludePendingExchanges;
//            request.OfficeLocationId = parameters.OfficeLocationId;
//            request.DepartmentId = parameters.DepartmentId;
//            request.CustomerId = parameters.CustomerId;
//            request.DealId = parameters.DealId;
//            request.InventoryTypeId = parameters.InventoryTypeId;
//            request.CategoryId = parameters.CategoryId;

//            let dealOutstanding: any = {};

//            let Promise = Ajax.post<DataTable>(`${apiUrl}/api/v1/dealoutstandingreport/runreport`, authorizationHeader, request)
//                .then((response: DataTable) => {
//                    dealOutstanding = DataTable.toObjectList(response);
//                    dealOutstanding.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
//                    dealOutstanding.FromDate = parameters.FromDate;
//                    dealOutstanding.ToDate = parameters.ToDate;
//                    dealOutstanding.Report = 'Deal Outstanding Report';
//                    dealOutstanding.System = 'RENTALWORKS';
//                    dealOutstanding.Company = '4WALL ENTERTAINMENT';
//                    this.renderFooterHtml(dealOutstanding);
//                    if (this.action === 'Preview' || this.action === 'PrintHtml') {
//                        document.getElementById('pageFooter').innerHTML = this.footerHtml;
//                    }
//                    document.getElementById('pageBody').innerHTML = hbReport(dealOutstanding);
//                    console.log('dealOutstanding: ', dealOutstanding )
//                    this.onRenderReportCompleted();
//                })
//                .catch((ex) => {
//                    this.onRenderReportFailed(ex);
//                });
//        } catch (ex) {
//            this.onRenderReportFailed(ex);
//        }
//    }

//    renderFooterHtml(model: DataTable): string {
//        this.footerHtml = hbFooter(model);
//        return this.footerHtml;
//    }
//}

//(<any>window).report = new DealOutstandingReport();