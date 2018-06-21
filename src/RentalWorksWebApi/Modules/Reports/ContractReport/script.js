var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Report = (function () {
    function Report() {
        this.contract = null;
        this.renderReportCompleted = false;
        this.renderReportFailed = false;
        this.headerHtml = '';
        this.footerHtml = '';
    }
    Report.prototype.renderReport = function (apiToken, baseUrl, parameters) {
        try {
            var me_1 = this;
            sessionStorage.setItem('apiToken', apiToken);
            sessionStorage.setItem('baseUrl', baseUrl);
            $('#apiToken').text(sessionStorage.getItem('apiToken'));
            this.getContract(apiToken, baseUrl, parameters.contractid)
                .then(function (contract) {
                me_1.getDeal(apiToken, baseUrl, contract.DealId)
                    .then(function (deal) {
                    contract.DealObj = deal;
                    me_1.contract = contract;
                    var templateOutContract = $('#outContractView').html();
                    var hbOutContract = Handlebars.compile(templateOutContract);
                    var renderedOutContractHtml = hbOutContract(contract);
                    $('#contractHeader').html(renderedOutContractHtml);
                    me_1.getRentalItems(apiToken, baseUrl, parameters.contractid)
                        .then(function (items) {
                        contract.RentalItems = items;
                        var rentalItemsTemplate = $('#rentalItemsTemplate').html();
                        var hbRentalItems = Handlebars.compile(rentalItemsTemplate);
                        var renderedRentalItems = hbRentalItems(contract);
                        $('#rentalItems').html(renderedRentalItems);
                        var receivedByTemplate = $('#receivedByTemplate').html();
                        var hbReceivedBy = Handlebars.compile(receivedByTemplate);
                        var renderedReceivedBy = hbReceivedBy(contract);
                        $('#receivedBy').html(renderedReceivedBy);
                        me_1.headerHtml = me_1.getHeaderHtml(contract);
                        $('#pageHeader').html(me_1.headerHtml);
                        me_1.footerHtml = me_1.getFooterHtml(contract);
                        $('#pageFooter').html(me_1.footerHtml);
                        me_1.renderReportCompleted = true;
                    })
                        .catch(function (message) {
                        me_1.renderReportCompleted = true;
                        me_1.renderReportFailed = true;
                    });
                })
                    .catch(function (xhr) {
                    me_1.renderReportCompleted = true;
                    me_1.renderReportFailed = true;
                });
            })
                .catch(function (xhr) {
                me_1.renderReportCompleted = true;
                me_1.renderReportFailed = true;
            });
        }
        catch (ex) {
            console.log(ex);
        }
    };
    Report.prototype.getContract = function (apiToken, baseUrl, contractId) {
        return $.ajax({
            url: baseUrl + "/api/v1/contract/" + contractId,
            method: 'GET',
            data: {},
            cache: false,
            contentType: 'application/json',
            headers: {
                'Authorization': "Bearer " + apiToken
            }
        });
    };
    Report.prototype.getDeal = function (apiToken, baseUrl, dealId) {
        return $.ajax({
            url: baseUrl + "/api/v1/deal/" + dealId,
            method: 'GET',
            data: {},
            cache: false,
            contentType: 'application/json',
            headers: {
                'Authorization': "Bearer " + apiToken
            }
        });
    };
    Report.prototype.getRentalItems = function (apiToken, baseUrl, dealId) {
        return new Promise(function (resolve, reject) {
            try {
                var items = Array();
                for (var i = 0; i < 200; i++) {
                    var item = new ContractItem();
                    item.ICode = '5302275';
                    item.Description = 'CHIMERA SPEED RINNG 7 1/4';
                    item.QtyOrdered = 2;
                    item.Out = 2;
                    item.TotalOut = 2;
                    items.push(item);
                }
                resolve(items);
            }
            catch (ex) {
                reject(ex.Message);
            }
        });
    };
    Report.prototype.getHeaderHtml = function (contract) {
        var template = jQuery('#headerTemplate').html();
        var hb = Handlebars.compile(template);
        var renderedHtml = hb(contract);
        return renderedHtml;
    };
    Report.prototype.getFooterHtml = function (contract) {
        var template = jQuery('#footerTemplate').html();
        var hb = Handlebars.compile(template);
        var renderedHtml = hb(contract);
        return renderedHtml;
    };
    return Report;
}());
var report = new Report();
var ContractLogic = (function () {
    function ContractLogic() {
    }
    return ContractLogic;
}());
var ContractItem = (function () {
    function ContractItem() {
    }
    return ContractItem;
}());
var RentalItem = (function (_super) {
    __extends(RentalItem, _super);
    function RentalItem() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return RentalItem;
}(ContractItem));
var DealLogic = (function () {
    function DealLogic() {
    }
    return DealLogic;
}());
//# sourceMappingURL=script.js.map