var SalesInventoryValidation = (function () {
    function SalesInventoryValidation() {
        this.Module = 'SalesInventoryValidation';
        this.apiurl = 'api/v1/salesinventory';
    }
    SalesInventoryValidation.prototype.addLegend = function ($control) {
        FwBrowse.addLegend($control, 'Item', '#FFFFFF');
        FwBrowse.addLegend($control, 'Accessory', '#fffa00');
        FwBrowse.addLegend($control, 'Complete', '#0080ff');
        FwBrowse.addLegend($control, 'Kit', '#00c400');
        FwBrowse.addLegend($control, 'Misc', '#ff0080');
        FwBrowse.addLegend($control, 'Container', '#FF8040');
    };
    return SalesInventoryValidation;
}());
var SalesInventoryValidationController = new SalesInventoryValidation();
//# sourceMappingURL=SalesInventoryValidationController.js.map