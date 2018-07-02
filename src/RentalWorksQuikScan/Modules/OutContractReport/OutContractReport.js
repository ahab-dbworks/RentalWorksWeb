var OutContractReportClass = (function () {
    function OutContractReportClass() {
    }
    OutContractReportClass.prototype.emailPdf = function (contractid, from, to, subject, body) {
        var request = {
            contractid: contractid,
            from: from,
            to: to,
            subject: subject,
            body: body
        };
        RwServices.callMethod('OutContractReport', 'EmailPdf', request, function (response) {
            try {
                console.log(response);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    return OutContractReportClass;
}());
var OutContractReport = new OutContractReportClass();
//# sourceMappingURL=OutContractReport.js.map