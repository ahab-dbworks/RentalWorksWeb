class RepairPartGrid {
    Module: string = 'RepairPartGrid';
    apiurl: string = 'api/v1/repairpart';
  
    generateRow($control, $generatedtr, $form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse')).warehouse;
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;

        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouse);
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.value').val(warehouseId);
        });
    };
}
 
var RepairPartGridController = new RepairPartGrid();
//----------------------------------------------------------------------------------------------