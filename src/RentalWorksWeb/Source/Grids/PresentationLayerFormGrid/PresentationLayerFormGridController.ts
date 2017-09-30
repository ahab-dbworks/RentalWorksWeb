class PresentationLayerFormGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PresentationLayerFormGrid';
        this.apiurl = 'api/v1/presentationlayerform';
    }
}

(<any>window).PresentationLayerFormGridController = new PresentationLayerFormGrid();
//----------------------------------------------------------------------------------------------