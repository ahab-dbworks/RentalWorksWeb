class CrewPositionGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CrewPositionGrid';
        this.apiurl = 'api/v1/crewposition';
    }
}

var CrewPositionGridController = new CrewPositionGrid();
//----------------------------------------------------------------------------------------------