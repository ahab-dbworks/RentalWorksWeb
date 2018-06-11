class RepairPartGrid {
    Module: string = 'RepairPartGrid';
    apiurl: string = 'api/v1/repairpart';
  
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse')).warehouse;
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;

        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();

            if ($generatedtr.hasClass("newmode")) {
                FwAppData.apiMethod(true, 'GET', `api/v1/pricing/${inventoryId}/${warehouseId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    console.log(response)
                    $generatedtr.find('.field[data-browsedatafield="Price"] input').val(response[0].Price);
                }, null, $form);
            }

            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedisplayfield="Warehouse"] input.text').val(warehouse);
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.value').val(warehouseId);
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="Billable"] input').prop('checked', true);;
        });

        $generatedtr.find('.field_to_calc_extended').on('change', 'input.value', $tr => {
            calculateExtended();
        });

        function calculateExtended() {
            let quantityValue, url;
            quantityValue = $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val();
            let priceValue = $generatedtr.find('.field[data-browsedatafield="Price"] input').val();
            priceValue = +priceValue.substring(1).replace(',', '');
            let discountValue = $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val();
            discountValue = +discountValue.substring(1).replace(',', '');
            url = `api/v1/repairpart/calculateextended?Quantity=${quantityValue}&Rate=${priceValue}&DiscountAmount=${discountValue}`;

            FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, function onSuccess(response) {
                $generatedtr.find('.field[data-browsedatafield="Extended"]').text(`$${response.Extended}`);
            }, null, null);
        }
    };
}
 
var RepairPartGridController = new RepairPartGrid();
//----------------------------------------------------------------------------------------------