class RentalInventoryWarehouseGrid {
    Module: string = 'InventoryWarehouseGrid';  //justin 04/10/2019 potential issue with shared Module value. See Issue #401
    apiurl: string = 'api/v1/inventorywarehouse';

    init($control: JQuery) {
        const enableConsignment = JSON.parse(sessionStorage.getItem('controldefaults')).enableconsignment;
        if (!enableConsignment) {
            $control.find(`[data-field="QtyConsigned"]`).parent().hide();
        }
    }

}

var RentalInventoryWarehouseGridController = new RentalInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------