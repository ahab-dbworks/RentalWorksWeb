var RepairCostGrid = (function () {
    function RepairCostGrid() {
        this.Module = 'RepairCostGrid';
        this.apiurl = 'api/v1/repaircost';
    }
    RepairCostGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="RateId"]').data('onchange', function ($tr) {
            var rateId = $generatedtr.find('div[data-browsedatafield="RateId"] input').val();
            var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            if ($generatedtr.hasClass("newmode")) {
                FwAppData.apiMethod(true, 'GET', "api/v1/pricing/" + rateId + "/" + warehouseId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="Rate"] input').val(response[0].Price);
                }, null, $form);
            }
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Unit"] input').val($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="Billable"] input').prop('checked', true);
            console.log($generatedtr.find('.field[data-browsedatafield="Quantity"] input').val());
        });
        $control.find('.field_to_calc_extended').on('change', 'input.value', function ($tr) {
            calculateExtended($tr);
        });
        function calculateExtended($tr) {
            var quantityValue, rateValue, discountValue, url;
            quantityValue = $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val();
            rateValue = $generatedtr.find('.field[data-browsedatafield="Rate"] input').val();
            discountValue = $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val();
            url = "api/v1/repaircost/calculateextended?Quantity=" + quantityValue + "&Rate=" + rateValue + "&DiscountAmount=" + discountValue;
            FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, function onSuccess(response) {
                $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.Extended);
            }, null, null);
            console.log(quantityValue, rateValue, discountValue);
        }
    };
    ;
    return RepairCostGrid;
}());
var RepairCostGridController = new RepairCostGrid();
//# sourceMappingURL=RepairCostGridController.js.map