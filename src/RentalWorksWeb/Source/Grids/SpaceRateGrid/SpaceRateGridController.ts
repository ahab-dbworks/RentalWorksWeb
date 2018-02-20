class SpaceRateGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SpaceRateGrid';
        this.apiurl = 'api/v1/spacerate';
    }
}

var SpaceRateGridController = new SpaceRateGrid();
//----------------------------------------------------------------------------------------------