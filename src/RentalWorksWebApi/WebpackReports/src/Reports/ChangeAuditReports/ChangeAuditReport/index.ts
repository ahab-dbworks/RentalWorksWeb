import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ChangeAuditReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/changeauditreport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data);
                    data.FromDate = parameters.FromDate;
                    data.ToDate = parameters.ToDate;
                    data.Report = 'Change Audit Report';

                    //adds module and user headers
                    let moduleName;
                    let userName;
                    for (let i = 0; i < data.length; i++) {
                        if (data[i].ModuleName == moduleName) {
                            data[i].RenderModuleHeader = false;

                            if (data[i].UserName == userName) {
                                data[i].RenderUserHeader = false;
                            } else {
                                userName = data[i].UserName;
                                data[i].RenderUserHeader = true;
                            }
                        } else {
                            moduleName = data[i].ModuleName;
                            userName = data[i].UserName;
                            data[i].RenderModuleHeader = true;
                            data[i].RenderUserHeader = true;
                        }

                        //parse changes json
                        data[i].Json = JSON.parse(data[i].Json);
                        for (let j = 0; j < data[i].Json.length; j++) {
                            data[i].Json[j].FieldName = data[i].Json[j].FieldName.replace(/([A-Z]+)/g, " $1").trim();
                        }
                    }

                    console.log('rpt: ', data)
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
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new ChangeAuditReport();