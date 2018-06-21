class ContractReportClass {
    constructor() {
        
    }

    public generatePdf(contractid: string) {
        let request = {
            contractid: contractid
        };
        RwServices.callMethod('ContractReport', 'GeneratePdf', request, function (response) {
            try {
                console.log(response);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
}

var ContractReport = new ContractReportClass();