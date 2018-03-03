class MarketSegmentJobGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'MarketSegmentJobGrid';
        this.apiurl = 'api/v1/marketsegmentjob';
    }
}

var MarketSegmentJobGridController = new MarketSegmentJobGrid();
//----------------------------------------------------------------------------------------------