class RateUpdateItemGrid {
    Module: string = 'RateUpdateItemGrid';
    apiurl: string = 'api/v1/rateupdateitem';

    addLegend($control: any) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            if ($field.attr('data-caption') == 'New') {
                if ($field.attr('data-originalvalue') != '0') {
                    $field.css({
                        'background-color': '#9aeabe'
                    });
                }
            }
        });

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const availFor = FwBrowse.getValueByDataField($control, $generatedtr, 'AvailableFor');
            let inventoryPeekForm;
            let categoryPeekForm;
            switch (availFor) {
                case 'R':
                    inventoryPeekForm = 'RentalInventory';
                    categoryPeekForm = 'RentalCategory';
                    break;
                case 'S':
                    inventoryPeekForm = 'SalesInventory';
                    categoryPeekForm = 'SalesCategory';
                    break;
                case 'P':
                    inventoryPeekForm = 'PartsInventory';
                    categoryPeekForm = 'PartsCategory';
                    break;
                case 'M':
                    inventoryPeekForm = 'MiscRate';
                    categoryPeekForm = 'MiscCategory';
                    break;
                case 'L':
                    inventoryPeekForm = 'LaborRate';
                    categoryPeekForm = 'LaborCategory';
                    break;
            }
            const $inventoryTd = $generatedtr.find('[data-validationname="GeneralItemValidation"]');
            $inventoryTd.attr('data-peekForm', inventoryPeekForm);

            const $categoryTd = $generatedtr.find('[data-validationname="CategoryValidation"]');
            $categoryTd.attr('data-peekForm', categoryPeekForm);
        });
    }
}

var RateUpdateItemGridController = new RateUpdateItemGrid();
//----------------------------------------------------------------------------------------------