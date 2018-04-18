var CompanyValidation = (function () {
    function CompanyValidation() {
        this.Module = 'CompanyValidation';
        this.apiurl = 'api/v1/company';
    }
    CompanyValidation.prototype.addLegend = function ($control) {
        FwBrowse.addLegend($control, 'Lead', '#ff8040');
        FwBrowse.addLegend($control, 'Prospect', '#ff0080');
        FwBrowse.addLegend($control, 'Customer', '#ffff80');
        FwBrowse.addLegend($control, 'Deal', '#03de3a');
        FwBrowse.addLegend($control, 'Vendor', '#20b7ff');
    };
    return CompanyValidation;
}());
var CompanyValidationController = new CompanyValidation();
//# sourceMappingURL=CompanyValidationController.js.map