var Report = (function () {
    function Report() {
        this.contract = null;
        this.renderReportCompleted = false;
        this.renderReportFailed = false;
        this.headerHtml = '';
        this.footerHtml = '';
    }
    Report.prototype.renderReport = function (authorizationHeader, parameters) {
        var me = this;
        try {
            Handlebars.registerHelper('gt', function (leftSide, rightSide, options) {
                if (leftSide > rightSide) {
                    var result = options.fn(this);
                    return result;
                }
            });
            var contract_1 = new OutContract();
            var contractPromise = RwAjax.apiGet("/api/v1/outcontractreport/" + parameters.contractid, authorizationHeader)
                .then(function (value) {
                contract_1 = value;
                contract_1.PrintTime = moment().format('YYYY-MM-DD h:mm:ss A');
                contract_1.ContractTime = window.moment(contract_1.ContractTime, 'h:mm a').format('h:mm a');
            })
                .catch(function (ex) {
                console.log(ex);
            });
            Promise.all([contractPromise]).then(function () {
                var templateOutContract = $('#outContractView').html();
                var hbOutContract = Handlebars.compile(templateOutContract);
                var renderedOutContractHtml = hbOutContract(contract_1);
                $('#contract').html(renderedOutContractHtml);
                me.headerHtml = me.getHeaderHtml(contract_1);
                $('#pageHeader').html(me.headerHtml);
                me.footerHtml = me.getFooterHtml(contract_1);
                $('#pageFooter').html(me.footerHtml);
                me.renderReportCompleted = true;
            });
        }
        catch (err) {
            RwAjax.logError('An error occured while rendering the report.', err);
            me.renderReportCompleted = true;
            me.renderReportFailed = true;
        }
    };
    Report.prototype.getHeaderHtml = function (model) {
        var template = jQuery('#headerTemplate').html();
        var hb = Handlebars.compile(template);
        var renderedHtml = hb(model);
        return renderedHtml;
    };
    Report.prototype.getFooterHtml = function (model) {
        var template = jQuery('#footerTemplate').html();
        var hb = Handlebars.compile(template);
        var renderedHtml = hb(model);
        return renderedHtml;
    };
    return Report;
}());
var report = new Report();
var DataTable = (function () {
    function DataTable() {
    }
    DataTable.toObjectList = function (dt) {
        var objects = [];
        for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
            var row = dt.Rows[rowno];
            var object = {};
            for (var colno = 0; colno < dt.Columns.length; colno++) {
                var column = dt.Columns[colno];
                object[dt.Columns[colno].DataField] = row[colno];
            }
            objects.push(object);
        }
        return objects;
    };
    return DataTable;
}());
var DataTableColumn = (function () {
    function DataTableColumn() {
    }
    return DataTableColumn;
}());
var CustomField = (function () {
    function CustomField() {
    }
    return CustomField;
}());
var OutContract = (function () {
    function OutContract() {
        this._Custom = new Array();
        this.ContractId = '';
        this.ContractNumber = '';
        this.ContractType = '';
        this.ContractDate = '';
        this.ContractTime = '';
        this.LocationId = '';
        this.LocationCode = '';
        this.Location = '';
        this.WarehouseId = '';
        this.WarehouseCode = '';
        this.Warehouse = '';
        this.CustomerId = '';
        this.DealId = '';
        this.Deal = '';
        this.DepartmentId = '';
        this.Department = '';
        this.PurchaseOrderId = '';
        this.PurchaseOrderNumber = '';
        this.RequisitionNumber = '';
        this.VendorId = '';
        this.Vendor = '';
        this.Migrated = false;
        this.NeedReconcile = false;
        this.PendingExchange = false;
        this.ExchangeContractId = '';
        this.Rental = false;
        this.Sales = false;
        this.InputByUserId = '';
        this.InputByUser = '';
        this.DealInactive = false;
        this.Truck = false;
        this.BillingDate = '';
        this.HasAdjustedBillingDate = false;
        this.HasVoId = false;
        this.SessionId = '';
        this.OrderDescription = '';
        this.DateStamp = '';
        this.RecordTitle = '';
        this.RentalItems = new Array();
        this.SalesItems = new Array();
        this.PrintTime = '';
    }
    return OutContract;
}());
var OutContractItemRequest = (function () {
    function OutContractItemRequest() {
        this["miscfields"] = {};
        this["module"] = '';
        this["options"] = {};
        this["orderby"] = '';
        this["pageno"] = 0;
        this["pagesize"] = 0;
        this["searchfieldoperators"] = [];
        this["searchfields"] = [];
        this["searchfieldvalues"] = [];
        this["uniqueids"] = { "ContractId": '', "RecType": '' };
    }
    return OutContractItemRequest;
}());
var OutContractItem = (function () {
    function OutContractItem() {
    }
    return OutContractItem;
}());
//# sourceMappingURL=script.js.map