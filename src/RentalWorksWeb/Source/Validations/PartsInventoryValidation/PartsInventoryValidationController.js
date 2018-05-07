var PartsInventoryValidation = (function () {
    function PartsInventoryValidation() {
        this.Module = 'PartsInventoryValidation';
        this.apiurl = 'api/v1/partsinventory';
    }
    PartsInventoryValidation.prototype.addLegend = function ($control) {
        FwBrowse.addLegend($control, 'Item', '#FFFFFF');
        FwBrowse.addLegend($control, 'Accessory', '#fffa00');
        FwBrowse.addLegend($control, 'Complete', '#0080ff');
        FwBrowse.addLegend($control, 'Kit', '#00c400');
        FwBrowse.addLegend($control, 'Misc', '#ff0080');
        FwBrowse.addLegend($control, 'Container', '#FF8040');
    };
    return PartsInventoryValidation;
}());
var PartsInventoryValidationController = new PartsInventoryValidation();
//# sourceMappingURL=PartsInventoryValidationController.js.map