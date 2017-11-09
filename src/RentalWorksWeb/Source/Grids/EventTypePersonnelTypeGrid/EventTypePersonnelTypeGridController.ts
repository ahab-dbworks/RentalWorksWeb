class EventTypePersonnelTypeGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Event Type Personnel Type';
        this.apiurl = 'api/v1/eventtypepersonneltype';
    }
}

(<any>window).EventTypePersonnelTypeGridController = new EventTypePersonnelTypeGrid();
//----------------------------------------------------------------------------------------------