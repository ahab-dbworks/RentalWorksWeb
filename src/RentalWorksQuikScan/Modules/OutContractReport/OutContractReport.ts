class OutContractReportClass {
    constructor() {
        
    }

    public emailPdf(contractid: string, from: string, to: string, subject: string, body: string) {
        let request = {
            contractid: contractid,
            from: from,
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
        });
    }
}

var OutContractReport = new OutContractReportClass();
