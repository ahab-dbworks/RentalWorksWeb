class CheckOutPendingItemGrid {
    Module: string = 'CheckOutPendingItemGrid';
    apiurl: string = 'api/v1/checkoutpendingitem';

    //justin 10/29/2019. Concept copied from QuikActivityGrid.  Thanks Jason!
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const recType = FwBrowse.getValueByDataField($control, $tr, 'RecType'); // should only be "R" or "S"
            let inventoryControllerValidation: string = "";
            switch (recType) {
                case "R": inventoryControllerValidation = "RentalInventoryValidation";
                    break;
                case "S": inventoryControllerValidation = "SalesInventoryValidation";
                    break;
                default: inventoryControllerValidation = "";
                    break;
            }
            $tr.find('[data-browsedisplayfield="ICode"]').attr('data-validationname', inventoryControllerValidation);
        });
    }
}

var CheckOutPendingItemGridController = new CheckOutPendingItemGrid();
//----------------------------------------------------------------------------------------------