class SalesInventoryValidation extends BaseInventoryValidation {
    constructor() {
        super(...arguments);
        this.Module = 'SalesInventoryValidation';
        this.apiurl = 'api/v1/salesinventory';
    }
}
var SalesInventoryValidationController = new SalesInventoryValidation();
//# sourceMappingURL=SalesInventoryValidationController.js.map