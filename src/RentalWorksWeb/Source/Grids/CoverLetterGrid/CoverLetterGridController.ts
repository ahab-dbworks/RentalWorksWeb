class CoverLetterGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CoverLetter';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

(<any>window).CoverLetterGridController = new CoverLetterGrid();
//----------------------------------------------------------------------------------------------