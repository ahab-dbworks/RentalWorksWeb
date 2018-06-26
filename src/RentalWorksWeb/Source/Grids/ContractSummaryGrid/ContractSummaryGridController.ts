class ContractSummaryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ContractSummaryGrid';
        this.apiurl = 'api/v1/contractitemsummary';
    }
}

var ContractSummaryGridController = new ContractSummaryGrid();
//----------------------------------------------------------------------------------------------