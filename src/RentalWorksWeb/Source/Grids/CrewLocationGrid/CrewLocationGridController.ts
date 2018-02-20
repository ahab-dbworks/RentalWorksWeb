class CrewLocationGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CrewLocationGrid';
        this.apiurl = 'api/v1/crewlocation';
    }
}

var CrewLocationGridController = new CrewLocationGrid();
//----------------------------------------------------------------------------------------------