class RentalInventoryValidation extends BaseInventoryValidation {
    constructor() {
        super(...arguments);
        this.Module = 'RentalInventoryValidation';
        this.apiurl = 'api/v1/rentalinventory';
    }
}
var RentalInventoryValidationController = new RentalInventoryValidation();
//# sourceMappingURL=RentalInventoryValidationController.js.map