class ContractDetailGrid {
    Module: string = 'ContractDetailGrid';
    apiurl: string = 'api/v1/contractitemdetail';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        // Bold Row
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            if ($tr.find('[data-browsedatafield="IsVoid"]').attr('data-originalvalue') === 'true') {
                $tr.css('text-decoration', 'line-through');
                $tr.find('td.column div.field').css('background-color', '#00ffff');
            }
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
var ContractDetailGridController = new ContractDetailGrid();
//----------------------------------------------------------------------------------------------