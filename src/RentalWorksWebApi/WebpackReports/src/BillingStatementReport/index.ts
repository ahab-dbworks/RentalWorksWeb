import { WebpackReport } from '../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../lib/FwReportLibrary/src/scripts/Ajax';
import { HandlebarsHelpers } from '../../lib/FwReportLibrary/src/scripts/HandlebarsHelpers';
import * as moment from 'moment';
import '../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class BillingStatementReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);

            HandlebarsHelpers.registerHelpers();
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/BillingStatementReport/runreport`, authorizationHeader, parameters)
                        .then((response: DataTable) => {
                            const data: any = DataTable.toObjectList(response);
                            data.PrintTime = `Printed on ${moment().format('MM/DD/YYYY')} at ${moment().format('h:mm:ss A')}`;
                            data.FromDate = parameters.FromDate;
                            data.ToDate = parameters.ToDate;
                            data.Report = 'Billing Statement Report';
                            data.System = 'RENTALWORKS';
                            data.Today = moment().format('MM/DD/YYYY')
                            data.Company = parameters.companyName;
                            
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }
                            if (data[2]) {
                                const firstDetail = data[2];
                                data.OfficeLocationCompany = firstDetail.OfficeLocationCompany;
                                data.OfficeLocationAddress1 = firstDetail.OfficeLocationAddress1;
                                data.OfficeLocationAddress2 = firstDetail.OfficeLocationAddress2;
                                data.OfficeLocationCity = firstDetail.OfficeLocationCity;
                                data.OfficeLocationState = firstDetail.OfficeLocationState;
                                data.OfficeLocationZipCode = firstDetail.OfficeLocationZipCode;
                                data.OfficeLocationCountry = firstDetail.OfficeLocationCountry;
                                data[0].isFirstDealheader = true;
                            }
                            console.log('rpt', data)
                            this.renderFooterHtml(data);
                            if (this.action === 'Preview' || this.action === 'PrintHtml') {
                                document.getElementById('pageFooter').innerHTML = this.footerHtml;
                            }
                            document.getElementById('pageBody').innerHTML = hbReport(data);
                            this.onRenderReportCompleted();
                        })
                        .catch((ex) => {
                            this.onRenderReportFailed(ex);
                        });
                })
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new BillingStatementReport();