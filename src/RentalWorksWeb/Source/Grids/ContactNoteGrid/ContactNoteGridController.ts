class ContactNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ContactNoteGrid';
        this.apiurl = 'api/v1/contactnote';
    }
}

(<any>window).ContactNoteGridController = new ContactNoteGrid();
//----------------------------------------------------------------------------------------------