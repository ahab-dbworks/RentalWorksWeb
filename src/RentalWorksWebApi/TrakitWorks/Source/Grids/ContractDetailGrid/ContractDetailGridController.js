class ContractDetailGrid {
    constructor() {
        this.Module = 'ContractDetailGrid';
        this.apiurl = 'api/v1/contractitemdetail';
    }
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
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
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
var ContractDetailGridController = new ContractDetailGrid();
//# sourceMappingURL=ContractDetailGridController.js.map