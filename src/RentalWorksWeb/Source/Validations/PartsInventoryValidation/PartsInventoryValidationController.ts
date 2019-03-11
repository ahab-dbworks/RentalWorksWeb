class PartsInventoryValidation extends BaseInventoryValidation {
    Module: string = 'PartsInventoryValidation';
    apiurl: string = 'api/v1/partsinventory';
}

var PartsInventoryValidationController = new PartsInventoryValidation();