var ContractReportClass = (function () {
    function ContractReportClass() {
    }
    ContractReportClass.prototype.generatePdf = function (contractid) {
        var request = {
            contractid: contractid
        };
        RwServices.callMethod('ContractReport', 'GeneratePdf', request, function (response) {
            try {
                console.log(response);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    return ContractReportClass;
}());
var ContractReport = new ContractReportClass();
//# sourceMappingURL=ContractReport.js.map