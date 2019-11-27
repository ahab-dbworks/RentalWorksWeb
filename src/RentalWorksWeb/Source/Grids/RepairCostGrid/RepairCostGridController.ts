class RepairCostGrid {
    Module: string = 'RepairCostGrid';
    apiurl: string = 'api/v1/repaircost';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="RateId"]').data('onchange', $tr => {
            const rateId = $generatedtr.find('div[data-browsedatafield="RateId"] input').val();
            const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

            if ($generatedtr.hasClass("newmode")) {
                FwAppData.apiMethod(true, 'GET', `api/v1/pricing/${rateId}/${warehouseId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="Rate"] input').val(response[0].Price);
                }, null, $form);
            }

            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Unit"] input').val($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="Billable"] input').prop('checked', true);
        });

        $generatedtr.find('.field_to_calc_extended').on('change', 'input.value', $tr => {
            calculateExtended();
        });
        $generatedtr.find('.field[data-browsedatafield="Extended"]').keypress(event => {
            if (event.which == 13) {
                calculateDiscount(event);
            }
        });

        function calculateExtended() {
            let quantityValue, url;
            quantityValue = $generatedtr.find('[data-browsedatafield="Quantity"] input.value').val();
            let rateValue = $generatedtr.find('.field[data-browsedatafield="Rate"] input').val();
            rateValue = +rateValue.substring(1).replace(',', '');
            let discountValue = $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val();
            discountValue = +discountValue.substring(1).replace(',', '');
            url = `api/v1/repaircost/calculateextended?Quantity=${quantityValue}&Rate=${rateValue}&DiscountAmount=${discountValue}`;

            FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, function onSuccess(response) {
                $generatedtr.find('.field[data-browsedatafield="Extended"] input').val(response.Extended);
            }, null, null);
        };

        function calculateDiscount(event) {
            let quantityValue, discountValue;
            quantityValue = $generatedtr.find('[data-browsedatafield="Quantity"] input.value').val();
            let rateValue = $generatedtr.find('.field[data-browsedatafield="Rate"] input').val();
            rateValue = +rateValue.substring(1).replace(',', '');
            let extendedValue = $generatedtr.find('.field[data-browsedatafield="Extended"] input').val();
            extendedValue = +extendedValue.substring(1).replace(',', '');
            discountValue = (quantityValue * rateValue) - extendedValue;
            $generatedtr.find('.field[data-browsedatafield="DiscountAmount"] input').val(discountValue);
        };
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'RateId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterate`);
                break;
        }
    }
}

var RepairCostGridController = new RepairCostGrid();
//----------------------------------------------------------------------------------------------