class RepairCostGrid {
    Module: string = 'RepairCostGrid';
    apiurl: string = 'api/v1/repaircost';

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="RateId"]').data('onchange', $tr => {
            let rateId = $generatedtr.find('div[data-browsedatafield="RateId"] input').val();
            var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

            if ($generatedtr.hasClass("newmode")) {

                FwAppData.apiMethod(true, 'GET', `api/v1/pricing/${rateId}/${warehouseId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="Rate"] input').val(response[0].Price);
                }, null, $form);
            }

            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Unit"] input').val($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="Billable"] input').prop('checked', true);;
        });
    };
}

var RepairCostGridController = new RepairCostGrid();
//----------------------------------------------------------------------------------------------