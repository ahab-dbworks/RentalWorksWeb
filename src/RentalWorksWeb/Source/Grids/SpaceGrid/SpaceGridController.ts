class SpaceGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SpaceGrid';
        this.apiurl = 'api/v1/space';
    }
}

(<any>window).SpaceGridController = new SpaceGrid();
//----------------------------------------------------------------------------------------------