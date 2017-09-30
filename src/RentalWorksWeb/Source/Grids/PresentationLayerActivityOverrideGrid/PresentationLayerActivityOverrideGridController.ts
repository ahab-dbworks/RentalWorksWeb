class PresentationLayerActivityOverrideGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PresentationLayerActivityOverrideGrid';
        this.apiurl = 'api/v1/presentationlayeractivityoverride';
    }
}

(<any>window).PresentationLayerActivityOverrideGridController = new PresentationLayerActivityOverrideGrid();
//----------------------------------------------------------------------------------------------