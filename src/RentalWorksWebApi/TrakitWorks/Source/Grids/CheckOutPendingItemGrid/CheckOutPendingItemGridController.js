class CheckOutPendingItemGrid {
    constructor() {
        this.Module = 'CheckOutPendingItemGrid';
        this.apiurl = 'api/v1/checkoutpendingitem';
    }
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            const recType = FwBrowse.getValueByDataField($control, $tr, 'RecType');
            let inventoryControllerValidation = "";
            switch (recType) {
                case "R":
                    inventoryControllerValidation = "RentalInventoryValidation";
                    break;
                case "S":
                    inventoryControllerValidation = "SalesInventoryValidation";
                    break;
                default:
                    inventoryControllerValidation = "";
                    break;
            }
            $tr.find('[data-browsedisplayfield="ICode"]').attr('data-validationname', inventoryControllerValidation);
        });
    }
}
var CheckOutPendingItemGridController = new CheckOutPendingItemGrid();
//# sourceMappingURL=CheckOutPendingItemGridController.js.map