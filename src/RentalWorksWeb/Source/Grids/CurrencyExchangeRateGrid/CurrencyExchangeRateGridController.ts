class CurrencyExchangeRateGrid {
    Module: string = 'CurrencyExchangeRateGrid';
    apiurl: string = 'api/v1/currencyexchangerate';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        FwBrowse.setFieldValue($control, $tr, 'AsOfDate', { value: FwFunc.getDate() });
    }

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="ToCurrencyId"]').data('onchange', $tr => {
            let currencyName = FwBrowse.getValueByDataField($control, $tr, 'Currency');
            FwBrowse.setFieldValue($control, $generatedtr, 'ToCurrency', { value: currencyName, text: currencyName })
        });
    }
}

var CurrencyExchangeRateGridController = new CurrencyExchangeRateGrid();
//----------------------------------------------------------------------------------------------