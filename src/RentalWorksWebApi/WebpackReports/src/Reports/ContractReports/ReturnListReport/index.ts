import { WebpackReport } from '../../../../lib/FwReportLibrary/src/scripts/WebpackReport';
import { CustomField } from '../../../../lib/FwReportLibrary/src/scripts/CustomField';
import { DataTable } from '../../../../lib/FwReportLibrary/src/scripts/Browse';
import { Ajax } from '../../../../lib/FwReportLibrary/src/scripts/Ajax';
import * as moment from 'moment';
import '../../../../lib/FwReportLibrary/src/theme/webpackReports.scss';
import * as QrCodeGen from '../../../../lib/FwReportLibrary/src/scripts/QrCodeGen';
import './index.scss';
const hbReport = require("./hbReport.hbs");
const hbFooter = require("./hbFooter.hbs");

export class ReturnListReport extends WebpackReport {
    contract: any = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            Ajax.get<SuspendedSession>(`${apiUrl}/api/v1/suspendedsession/${parameters.ContractId}`, authorizationHeader)
                .then((response: SuspendedSession) => {
                    let sessionNumber = response.SessionNumber;
                    let DealName = response.Deal;
                    Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                        .then((response: DataTable) => {
                            const logoObject: any = response;
                            Ajax.post<DataTable>(`${apiUrl}/api/v1/returnlistreport/runreport`, authorizationHeader, parameters)
                                .then((response: any) => {
                                    const data: any = response;
                                    data.Items = DataTable.toObjectList(response.ItemsTable);
                    this.setReportMetadata(parameters, data, response);
                                    data.Report = 'RETURN LIST';
                                    data.Session = sessionNumber;
                                    data.DealName = DealName;
                                    data.Department = parameters.department;
                                    data.PrintAisleShelf = parameters.PrintAisleShelf;
                                    for (let i = 0; i < data.Items.length; i++) {
                                        data.Items[i].PrintAisleShelf = parameters.PrintAisleShelf;
                                    }
                                    for (let i = 0; i < data.Items.length; i++) {
                                        data.Items[i].PrintIn = data.PrintIn;
                                        data.Items[i].PrintOut = data.PrintOut;
                                    }
                                    data.Warehouse = parameters.warehouse;

                                    if (parameters.BarCodeStyle === '1D') {
                                        parameters.BarCodeStyle = '1D';
                                    } else if (parameters.BarCodeStyle === '2D') {
                                        parameters.BarCodeStyle = '2D';
                                    } else {
                                        parameters.BarCodeStyle = '1D';
                                    }
                                    data.BarCodeStyle = parameters.BarCodeStyle;

                                    const qr = QrCodeGen.QrCode.encodeText(data.Session, QrCodeGen.Ecc.MEDIUM);
                                    const svg = qr.toSvgString(4);
                                    data.QrCode = svg;

                                    //if (logoObject.LogoImage != '') {
                                    //    data.Logosrc = logoObject.LogoImage;
                                    //}

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
                        }).catch((ex) => {
                            console.log('exception: ', ex)
                        });
                }).catch((ex) => {
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

interface SuspendedSession {
    _Custom: any[];
    ContractId: string;
    SessionNumber: number;
    Deal: string;
    DealNumber: string;
    DealOrVendor: string;
    OrderNumber: string;
    OrderDescription: string;
    OrderId: string;
    UserName: string;
    UserNameFirstMiddleLast: string;
    Status: string;
    StatusDate: string;
    UsersId: string;
    ContractDate: string;
    ContractTime: string;
    DealId: string;
    DepartmentId: string;
    OfficeLocationId: string;
    WarehouseId: string;
    Warehouse: string;
    ExchangeContractId: string;
    ContractType: string;
    IsForcedSuspend: boolean;
    ContainerItemId: string;
    RecordTitle: string;
}

(<any>window).report = new ReturnListReport();
