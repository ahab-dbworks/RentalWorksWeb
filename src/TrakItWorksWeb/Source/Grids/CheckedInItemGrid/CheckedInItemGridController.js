class CheckedInItemGrid {
    constructor() {
        this.Module = 'CheckedInItemGrid';
        this.apiurl = 'api/v1/checkedinitem';
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'Swapped Item', '#dc407e');
    }
}
var CheckedInItemGridController = new CheckedInItemGrid();
//# sourceMappingURL=CheckedInItemGridController.js.map