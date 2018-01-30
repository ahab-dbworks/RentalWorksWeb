class WarehouseQuikLocateApproverGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseQuikLocateApprover';
        this.apiurl = 'api/v1/warehousequiklocateapprover';
    }
}

(<any>window).WarehouseQuikLocateApproverGridController = new WarehouseQuikLocateApproverGrid();
//----------------------------------------------------------------------------------------------