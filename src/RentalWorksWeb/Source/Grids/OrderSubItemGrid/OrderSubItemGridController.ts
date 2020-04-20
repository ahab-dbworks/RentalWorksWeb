class OrderSubItemGrid {
    Module: string = 'OrderSubItemGrid';
    apiurl: string = 'api/v1/ordersubitem';


    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const icodetd = $tr.find('[data-validationname="GeneralItemValidation"]');
            const recType = $tr.find('[data-browsedatafield="RecType"]').attr('data-originalvalue');
            let peekForm;
            switch (recType) {
                case 'R':
                    peekForm = 'RentalInventory';
                    break;
                case 'S':
                    peekForm = 'SalesInventory';
                    break;
                case 'M':
                    peekForm = 'MiscRate';
                    break;
                case 'L':
                    peekForm = 'LaborRate';
                    break;
            }
            icodetd.attr('data-peekForm', peekForm);
        });
    }

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

}
//----------------------------------------------------------------------------------------------
var OrderSubItemGridController = new OrderSubItemGrid();