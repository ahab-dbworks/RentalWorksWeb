var CompanyValidation = (function () {
    function CompanyValidation() {
        this.Module = 'CompanyValidation';
        this.apiurl = 'api/v1/company';
    }
    CompanyValidation.prototype.addLegend = function ($control) {
        FwBrowse.addLegend($control, "TEST", "#FF0000");
        FwBrowse.addLegend($control, "TEST2", "#CCCCCC");
        FwBrowse.addLegend($control, "TEST3", "#FFF000");
        FwBrowse.addLegend($control, "TEST4", "#FF00FF");
    };
    return CompanyValidation;
}());
var CompanyValidationController = new CompanyValidation();
//# sourceMappingURL=CompanyValidationController.js.map