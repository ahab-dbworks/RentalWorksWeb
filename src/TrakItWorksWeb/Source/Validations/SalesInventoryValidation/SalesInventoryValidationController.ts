class SalesInventoryValidation extends BaseInventoryValidation {
    Module: string = 'SalesInventoryValidation';
    apiurl: string = 'api/v1/salesinventory';
}

var SalesInventoryValidationController = new SalesInventoryValidation();