import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
import { Session } from 'inspector';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class InvoiceReport extends WebpackReport {
    invoice: any;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<any>(`${apiUrl}/api/v1/invoicereport/runreport`, authorizationHeader, parameters)
                        .then((response: any) => {
                            const data: any = response;
                            this.setReportMetadata(parameters, data, response);
                            data.Report = 'INVOICE';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
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

                            if (data.Notes !== null && data.Notes !== '') {
                                const notesEl = document.getElementById('notes');
                                const notes = data.Notes;
                                if (notes.length) {
                                    const container: Array<string> = [];
                                    for (let i = 0; i < notes.length; i++) {
                                        container.push(`<div><div style="font-weight:700;">${notes[i].Description}:</div><div class="note-cell">${notes[i].Notes}</div></div>`);
                                    }
                                    notesEl.innerHTML = container.join('');
                                    const notesRow = document.getElementById('notesRow');
                                    notesRow.style.cssText = "padding:5px 10px 0px 10px;font-size:1em;width:1110px;";
                                }
                            }

                            this.onRenderReportCompleted();
                        })
                        .catch((ex) => {
                            this.onRenderReportFailed(ex);
                        });
                })
                .catch((ex) => {
                    console.log('exception: ', ex)
                });

        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: any): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new InvoiceReport();

