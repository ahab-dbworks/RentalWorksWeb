class OutContractReportClass {
    constructor() {
        
    }

    public emailPdf(contractid: string, to: string, subject: string, body: string) {
        let request = {
            contractid: contractid,
            to: to,
            subject: subject,
            body: body
        };
        RwServices.callMethod('OutContractReport', 'EmailPdf', request, function (response) {
            try {
                console.log(response);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, true, 60);
    }
}

var OutContractReport = new OutContractReportClass();
