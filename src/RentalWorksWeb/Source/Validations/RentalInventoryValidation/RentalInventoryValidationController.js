var RentalInventoryValidation = (function () {
    function RentalInventoryValidation() {
        this.Module = 'RentalInventoryValidation';
        this.apiurl = 'api/v1/rentalinventory';
    }
    RentalInventoryValidation.prototype.addLegend = function ($control) {
        FwBrowse.addLegend($control, 'Item', '#FFFFFF');
        FwBrowse.addLegend($control, 'Accessory', '#fffa00');
        FwBrowse.addLegend($control, 'Complete', '#0080ff');
        FwBrowse.addLegend($control, 'Kit', '#00c400');
        FwBrowse.addLegend($control, 'Misc', '#ff0080');
        FwBrowse.addLegend($control, 'Container', '#FF8040');
    };
    return RentalInventoryValidation;
}());
var RentalInventoryValidationController = new RentalInventoryValidation();
//# sourceMappingURL=RentalInventoryValidationController.js.map