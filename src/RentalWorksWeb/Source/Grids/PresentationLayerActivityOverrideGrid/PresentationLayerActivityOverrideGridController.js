var PresentationLayerActivityOverrideGrid = (function () {
    function PresentationLayerActivityOverrideGrid() {
        this.Module = 'PresentationLayerActivityOverrideGrid';
        this.apiurl = 'api/v1/presentationlayeractivityoverride';
    }
    PresentationLayerActivityOverrideGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="MasterId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return PresentationLayerActivityOverrideGrid;
}());
var PresentationLayerActivityOverrideGridController = new PresentationLayerActivityOverrideGrid();
//# sourceMappingURL=PresentationLayerActivityOverrideGridController.js.map