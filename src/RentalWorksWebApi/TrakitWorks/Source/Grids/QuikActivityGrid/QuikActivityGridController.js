class QuikActivityGrid {
    constructor() {
        this.Module = 'QuikActivityGrid';
        this.apiurl = 'api/v1/quikactivity';
    }
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            const orderTypeController = FwBrowse.getValueByDataField($control, $tr, 'OrderTypeController');
            $tr.find('[data-browsedisplayfield="OrderNumber"]').attr('data-validationname', `${orderTypeController}Validation`);
        });
    }
}
var QuikActivityGridController = new QuikActivityGrid();
//# sourceMappingURL=QuikActivityGridController.js.map