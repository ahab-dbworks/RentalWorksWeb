import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class BillingStatementReport extends WebpackReport {

    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<DataTable>(`${apiUrl}/api/v1/BillingStatementReport/runreport`, authorizationHeader, parameters)
                        .then((response: DataTable) => {
                            const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data, response);
                            data.FromDate = moment(parameters.FromDate).locale(parameters.Locale).format('L');
                            data.ToDate = moment(parameters.ToDate).locale(parameters.Locale).format('L');
                            data.Report = 'Billing Statement Report';
                            
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
                            this.renderFooterHtml(data);
                            if (this.action === 'Preview' || this.action === 'PrintHtml') {
                                document.getElementById('pageFooter').innerHTML = this.footerHtml;
                            }
                            if (parameters.isCustomReport) {
                                document.getElementById('pageBody').innerHTML = parameters.CustomReport(data);
                            } else {
                                document.getElementById('pageBody').innerHTML = hbReport(data);
                            }
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