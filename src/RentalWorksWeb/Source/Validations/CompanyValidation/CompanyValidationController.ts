class CompanyValidation {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyValidation';
        this.apiurl = 'api/v1/company';

    }

    addLegend($control) {
        FwBrowse.addLegend($control, "TEST", "#FF0000");
        FwBrowse.addLegend($control, "TEST2", "#CCCCCC");
        FwBrowse.addLegend($control, "TEST3", "#FFF000");
        FwBrowse.addLegend($control, "TEST4", "#FF00FF");
   
    }
}

var CompanyValidationController = new CompanyValidation();