class RentalInventoryValidation extends BaseInventoryValidation {
    Module: string = 'RentalInventoryValidation';
    apiurl: string = 'api/v1/rentalinventory';
}

var RentalInventoryValidationController = new RentalInventoryValidation();