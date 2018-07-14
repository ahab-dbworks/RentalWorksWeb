class PresentationLayerActivityOverrideGrid {
    constructor() {
        this.Module = 'PresentationLayerActivityOverrideGrid';
        this.apiurl = 'api/v1/presentationlayeractivityoverride';
    }
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="MasterId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    }
    ;
}
var PresentationLayerActivityOverrideGridController = new PresentationLayerActivityOverrideGrid();
//# sourceMappingURL=PresentationLayerActivityOverrideGridController.js.map