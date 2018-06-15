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
        });
        $generatedtr.find('.field_to_calc_extended').on('change', 'input.value', function ($tr) {
            calculateExtended();
        });
        $generatedtr.find('.field[data-browsedatafield="Extended"]').keypress(function (event) {
            if (event.which == 13) {
                calculateDiscount(event);
            }
        });
        function calculateExtended() {
            var quantityValue, url;
            quantityValue = $generatedtr.find('[data-browsedatafield="Quantity"] input.value').val();
            var rateValue = $generatedtr.find('.field[data-browsedatafield="Rate"] input').val();
            rateValue = +rateValue.substring(1).replace(',', '');
            var discountValue = $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val();
            discountValue = +discountValue.substring(1).replace(',', '');
            url = "api/v1/repaircost/calculateextended?Quantity=" + quantityValue + "&Rate=" + rateValue + "&DiscountAmount=" + discountValue;
            FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, function onSuccess(response) {
                $generatedtr.find('.field[data-browsedatafield="Extended"] input').val(response.Extended);
            }, null, null);
        }
        ;
        function calculateDiscount(event) {
            var quantityValue, discountValue;
            quantityValue = $generatedtr.find('[data-browsedatafield="Quantity"] input.value').val();
            var rateValue = $generatedtr.find('.field[data-browsedatafield="Rate"] input').val();
            rateValue = +rateValue.substring(1).replace(',', '');
            var extendedValue = $generatedtr.find('.field[data-browsedatafield="Extended"] input').val();
            extendedValue = +extendedValue.substring(1).replace(',', '');
            discountValue = (quantityValue * rateValue) - extendedValue;
            $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val(discountValue);
        }
        ;
    };
    ;
    return RepairCostGrid;
}());
var RepairCostGridController = new RepairCostGrid();
//# sourceMappingURL=RepairCostGridController.js.map