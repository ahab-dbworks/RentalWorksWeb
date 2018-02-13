class PresentationLayerActivityOverrideGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PresentationLayerActivityOverrideGrid';
        this.apiurl = 'api/v1/presentationlayeractivityoverride';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="MasterId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

(<any>window).PresentationLayerActivityOverrideGridController = new PresentationLayerActivityOverrideGrid();
//----------------------------------------------------------------------------------------------