class OrderTypeCoverLetterGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderTypeCoverLetter';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

(<any>window).OrderTypeCoverLetterGridController = new OrderTypeCoverLetterGrid();
//----------------------------------------------------------------------------------------------