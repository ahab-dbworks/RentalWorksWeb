class CustomerResaleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CustomerResaleGrid';
        this.apiurl = 'api/v1/companytaxresale';
    }
}

(<any>window).CustomerResaleGridController = new CustomerResaleGrid();
//----------------------------------------------------------------------------------------------