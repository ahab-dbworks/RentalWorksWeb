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

export class RepairOrderReport extends WebpackReport {
    repair: Repair = null;
    renderReport(apiUrl: string, authorizationHeader: string, parameters: any): void {
        try {
            super.renderReport(apiUrl, authorizationHeader, parameters);
            // Report rendering and Logo
            Ajax.get<DataTable>(`${apiUrl}/api/v1/logosettings/1`, authorizationHeader)
                .then((response: DataTable) => {
                    const logoObject: any = response;
                    Ajax.post<Repair>(`${apiUrl}/api/v1/repairorderreport/runreport`, authorizationHeader, parameters)
                        .then((response: Repair) => {
                            const data: any = response;
                    this.setReportMetadata(parameters, data);
                            data.Report = 'REPAIR';
                            if (logoObject.LogoImage != '') {
                                data.Logosrc = logoObject.LogoImage;
                            }

                            console.log('DATA: ', data);

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
                .catch((ex) => {
                    console.log('exception: ', ex)
                });

        } catch (ex) {
            this.onRenderReportFailed(ex);
        }
    }

    renderFooterHtml(model: Repair): string {
        this.footerHtml = hbFooter(model);
        return this.footerHtml;
    }
}

(<any>window).report = new RepairOrderReport();

class Repair {
    _Custom = new Array<CustomField>();
    RepairId: string;
    RepairNumber: string;
    RepairDate: string;
    BarCode: string;
    SerialNumber: string;
    ICode: string;
    Description: string;
    Quantity: string;
    Damage: string;
    Correction: string;
    Billable: boolean;
    BillableMaintenance: string;
    Status: string;
    InputByUserId: string;
    InputBy: string;
    Deal: string;
    DealNumber: string;
    OrderNumber: string;
    OrderDescription: string;
    ContractNumber: string;
    ContractDate: string;
    ContractNumberAndDate: string;
    OfficeLocation: string;
    OfficeLocationCompany: string;
    OfficeLocationAddress1: string;
    OfficeLocationAddress2: string;
    OfficeLocationCityStateZipCode: string;
    OfficeLocationCountry: string;
    OfficeLocationCityStateZipCodeCountry: string;
    OfficeLocationPhone: string;
    OfficeLocationFax: string;
    OfficeLocationEmail: string;
    OfficeLocationWebAddress: string;
    Tax1ReferenceName: string;
    Tax1ReferenceNumber: string;
    Tax2ReferenceName: string;
    Tax2ReferenceNumber: string;
    TaxOptionId: string;
    TaxOption: string;
    Tax1Name: string;
    Tax2Name: string;
    TaxRentalRate1: string;
    TaxRentalRate2: string;
    TaxSalesRate1: string;
    TaxSalesRate2: string;
    TaxLaborRate1: string;
    TaxLaborRate2: string;
    IssuedTo: string;
    IssuedToAddress1: string;
    IssuedToAddress2: string;
    IssuedToCity: string;
    IssuedToState: string;
    IssuedToZipCode: string;
    IssuedToCountry: string;
    IssuedToPhone: string;
    IssuedToFax: string;
    Items: any;
    PartsItems: any;
    MiscItems: any;
    LaborItems: any;
    PrintTime: string;
    PrintDate: string;
    PrintDateTime: string;
}