class InventoryCompleteGrid {
    Module: string = 'InventoryPackageInventory';
    apiurl: string = 'api/v1/inventorypackageinventory';
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="DefaultQuantity"] input').val('1');
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const totalRowCount = $tr.closest('.fwbrowse').data('totalRowCount');
        if (!totalRowCount) {
            switch (datafield) {
                case 'InventoryId':
                    request.uniqueids = {
                        Classification: 'I,A'
                    };
                    break;
                default:
                    break;
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, ex => {
                FwFunc.showError(ex);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
}

var InventoryCompleteGridController = new InventoryCompleteGrid();

