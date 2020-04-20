class RateUpdateItemGrid {
    Module: string = 'RateUpdateItemGrid';
    apiurl: string = 'api/v1/rateupdateitem';

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
            let type;
            switch (availFor) {
                case 'R':
                    type = 'Rental';
                    break;
                case 'S':
                    type = 'Sales';
                    break;
                case 'P':
                    type = 'Parts';
                    break;
                case 'M':
                    type = 'Misc';
                    break;
                case 'L':
                    type = 'Labor';
                    break;
            }
            const $inventoryTd = $generatedtr.find('[data-validationname="GeneralItemValidation"]');
            if (availFor == 'R' || availFor == 'S' || availFor == 'P') {
                $inventoryTd.attr('data-peekForm', type + 'Inventory');
            }

            const $categoryTd = $generatedtr.find('[data-validationname="CategoryValidation"]');
            $categoryTd.attr('data-peekForm', type + 'Category');
        });
    }
}

var RateUpdateItemGridController = new RateUpdateItemGrid();
//----------------------------------------------------------------------------------------------