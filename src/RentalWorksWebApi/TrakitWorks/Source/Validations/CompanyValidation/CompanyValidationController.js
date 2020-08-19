class CompanyValidation {
    constructor() {
        this.Module = 'CompanyValidation';
        this.apiurl = 'api/v1/company';
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'Lead', '#ff8040');
        FwBrowse.addLegend($control, 'Prospect', '#ff0080');
        FwBrowse.addLegend($control, 'Customer', '#ffff80');
        FwBrowse.addLegend($control, 'Deal', '#03de3a');
        FwBrowse.addLegend($control, 'Vendor', '#20b7ff');
    }
}
var CompanyValidationController = new CompanyValidation();
//# sourceMappingURL=CompanyValidationController.js.map