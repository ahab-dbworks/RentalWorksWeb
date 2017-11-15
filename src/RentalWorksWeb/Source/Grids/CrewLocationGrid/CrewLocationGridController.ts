class CrewLocationGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CrewLocationGrid';
        this.apiurl = 'api/v1/crewlocation';
    }
}

(<any>window).CrewLocationGridController = new CrewLocationGrid();
//----------------------------------------------------------------------------------------------