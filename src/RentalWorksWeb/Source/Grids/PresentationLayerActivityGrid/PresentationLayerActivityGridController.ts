class PresentationLayerActivityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PresentationLayerActivityGrid';
        this.apiurl = 'api/v1/presentationlayeractivity';
    }
}

(<any>window).PresentationLayerActivityGridController = new PresentationLayerActivityGrid();
//----------------------------------------------------------------------------------------------