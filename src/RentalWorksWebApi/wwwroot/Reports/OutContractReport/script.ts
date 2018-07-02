class Report {
    contract: OutContract = null;
    renderReportCompleted = false;
    renderReportFailed = false;
    headerHtml = '';
    footerHtml = '';

    renderReport(authorizationHeader, parameters): void {
        let me = this;
        try {
            Handlebars.registerHelper('gt', function(leftSide, rightSide, options) {
                if (leftSide > rightSide) {
                    var result = options.fn(this);
                    return result;
                }
            });
            let contract: OutContract = new OutContract();

            // get the Contract
            let contractPromise = this.apiGet<OutContract>(`/api/v1/outcontractreport/${parameters.contractid}`, authorizationHeader)
                .then((value: OutContract) => {
                    contract = value;
                    contract.PrintTime = (<any>window).moment().format('YYYY-MM-DD h:mm:ss A');
                    contract.ContractTime = (<any>window).moment(contract.ContractTime,'h:mm a').format('h:mm a');
                })
                .catch((ex) => {
                    console.log(ex);
                })
            ;

            Promise.all([contractPromise]).then(() => {
                // render the Contract
                let templateOutContract = $('#outContractView').html();
                let hbOutContract = Handlebars.compile(templateOutContract);
                let renderedOutContractHtml = hbOutContract(contract);
                $('#contract').html(renderedOutContractHtml);

                // render the page header for when you're viewing this on a screen
                me.headerHtml = me.getHeaderHtml(contract);
                $('#pageHeader').html(me.headerHtml);

                me.footerHtml = me.getFooterHtml(contract);
                $('#pageFooter').html(me.footerHtml);

                me.renderReportCompleted = true;
            });

        } catch (err) {
            me.logError('An error occured while rendering the report.', err);
            me.renderReportCompleted = true;
            me.renderReportFailed = true;
        }
    }

    logError(message, err: any) {
        console.log(message, err.message);
    }

    apiGet<T>(url: string, authorizationHeader: string): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.onload = function () {
                if (req.status == 200) {
                    resolve(JSON.parse(req.response));
                }
                else {
                    reject(Error(req.statusText));
                    console.log(req.responseText);
                }
            };
            req.onerror = function () {
                reject(Error("Network Error"));
            };
            req.send();
        });
    }

    apiPost<T>(url: string, authorizationHeader: string, data: any): Promise<T> {
        return new Promise<T>(function (resolve, reject) {
            var req = new XMLHttpRequest();
            req.open('POST', url);
            req.setRequestHeader('Authorization', authorizationHeader);
            req.setRequestHeader('Content-Type', 'application/json');
            req.onload = function () {
                if (req.status == 200) {
                    resolve(JSON.parse(req.response));
                }
                else {
                    reject(Error(req.statusText));
                    console.log(req.responseText);
                }
            };
            req.onerror = function () {
                reject(Error("Network Error"));
            };
            req.send(JSON.stringify(data));
        });
    }

    getHeaderHtml(model): string {
        let template = jQuery('#headerTemplate').html();
        let hb = Handlebars.compile(template);
        let renderedHtml = hb(model);
        return renderedHtml;
    }

    getFooterHtml(model) : string {
        let template = jQuery('#footerTemplate').html();
        let hb = Handlebars.compile(template);
        let renderedHtml = hb(model);
        return renderedHtml;
    }
}
var report = new Report();

class DataTable {
    ColumnIndex: any;
    Columns: Array<DataTableColumn>;
    Rows: Array<Array<any>>;
    PageNo:	number
    PageSize:	number;
    TotalPages:	number;
    TotalRows:	number;
    ColumnNameByIndex: any;

    static toObjectList<T>(dt: DataTable): Array<T> {
        let objects = [];
        for (let rowno = 0; rowno < dt.Rows.length; rowno++) {
            let row = dt.Rows[rowno];
            let object = {};
            for (let colno = 0; colno < dt.Columns.length; colno++) {
                let column = dt.Columns[colno];
                object[dt.Columns[colno].DataField] = row[colno];
            }
            objects.push(object);
        }
        return objects;
    }
}

class DataTableColumn {
    "Name": string;
    "DataField": string;
    "DataType": string;
    "IsUniqueId": boolean;
    "IsVisible": boolean;
}

class CustomField {
    FieldName: string;
    FieldValue: string;
    FieldType: string;
}

class OutContract {
    _Custom = new Array<CustomField>();
    ContractId = '';
    ContractNumber = '';
    ContractType = '';
    ContractDate = '';
    ContractTime = '';
    LocationId = '';
    LocationCode = '';
    Location = '';
    WarehouseId = '';
    WarehouseCode = '';
    Warehouse = '';
    CustomerId = '';
    DealId = '';
    Deal = '';
    DepartmentId = '';
    Department = '';
    PurchaseOrderId = '';
    PurchaseOrderNumber = '';
    RequisitionNumber = '';
    VendorId = '';
    Vendor = '';
    Migrated = false;
    NeedReconcile = false;
    PendingExchange = false;
    ExchangeContractId = '';
    Rental = false;
    Sales = false;
    InputByUserId = '';
    InputByUser = '';
    DealInactive = false;
    Truck = false;
    BillingDate = '';
    HasAdjustedBillingDate = false;
    HasVoId = false;
    SessionId = '';
    OrderDescription = '';
    DateStamp = '';
    RecordTitle = '';
    RentalItems = new Array<OutContractItem>();
    SalesItems = new Array<OutContractItem>();
    PrintTime = '';
}

class OutContractItemRequest {
    "miscfields" = {};
    "module" = '';
    "options" = {};
    "orderby" = '';
    "pageno" = 0;
    "pagesize" = 0;
    "searchfieldoperators" = [];
    "searchfields" = [];
    "searchfieldvalues" = [];
    "uniqueids" = { "ContractId": '', "RecType": '' };
}

class OutContractItem {
    "ICode": string;
    "ICodeColor": string;
    "Description": string;
    "DescriptionColor": string;
    "QuantityOrdered": string;
    "QuantityOut": string;
    "TotalOut": string;
    "ItemClass": string;
    "Notes": string;
    "Barcode": string;
}

