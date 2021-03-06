﻿import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class RentalInventoryValueReport extends WebpackReport {
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.post<DataTable>(`${apiUrl}/api/v1/rentalinventoryvaluereport/runreport`, authorizationHeader, parameters)
                .then((response: DataTable) => {
                    const data: any = DataTable.toObjectList(response);
                    this.setReportMetadata(parameters, data, response);
                    data.FromDate = moment(parameters.FromDate).locale(parameters.Locale).format('L');
                    data.ToDate = moment(parameters.ToDate).locale(parameters.Locale).format('L');
                    data.Report = 'Rental Inventory Value Report';
                    // Consigned or Owned Items header text
                    if (parameters.IncludeConsigned === true && parameters.IncludeOwned === false) {
                        data.ConsignedOwnedHeader = 'Includes Consigned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === false) {
                        data.ConsignedOwnedHeader = 'Includes Owned Items Only';
                    } else if (parameters.IncludeOwned === true && parameters.IncludeConsigned === true) {
                        data.ConsignedOwnedHeader = 'Includes Owned and Consigned Items';
                    } else {
                        data.ConsignedOwnedHeader = '';
                    }
                    // Determine Summary or Detail View
                    if (parameters.Summary === 'true') {
                        data.ViewSetting = 'SummaryView';
                    } else {
                        data.ViewSetting = 'DetailView';
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
        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: DataTable): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new RentalInventoryValueReport();