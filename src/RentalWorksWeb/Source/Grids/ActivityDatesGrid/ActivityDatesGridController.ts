class ActivityDatesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ActivityDatesGrid';
        this.apiurl = 'api/v1/activitydates';
    }
}

(<any>window).ActivityDatesGridController = new ActivityDatesGrid();
//----------------------------------------------------------------------------------------------