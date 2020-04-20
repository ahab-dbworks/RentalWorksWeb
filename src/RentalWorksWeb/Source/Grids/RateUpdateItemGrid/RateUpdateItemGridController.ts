class RateUpdateItemGrid {
    Module: string = 'RateUpdateItemGrid';
    apiurl: string = 'api/v1/rateupdateitem';

    generateRow($control, $generatedtr) {
        const availFor = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailableFor');
        let peekForm;
        switch (availFor) {
            case 'R':
                peekForm = 'RentalInventory';
                break;
            case 'S':
                peekForm = 'SalesInventory';
                break;
            case 'P':
                peekForm = 'PartsInventory';
                break;
            case 'M':
                peekForm = '';
                break;
            case 'L':
                peekForm = '';
                break;
        }
        const $td = $generatedtr.find('[data-validationname="GeneralItemValidation"]');
        $td.attr('data-peekForm', peekForm);
    }
}

var RateUpdateItemGridController = new RateUpdateItemGrid();
//----------------------------------------------------------------------------------------------