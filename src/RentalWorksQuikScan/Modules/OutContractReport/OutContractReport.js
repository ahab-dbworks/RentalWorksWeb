class OutContractReportClass {
    constructor() {
    }
    emailPdf(contractid, to, subject, body) {
        let request = {
            contractid: contractid,
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
        }, true, 60);
    }
}
var OutContractReport = new OutContractReportClass();
//# sourceMappingURL=OutContractReport.js.map