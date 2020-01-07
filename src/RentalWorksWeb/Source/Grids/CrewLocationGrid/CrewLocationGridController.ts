class CrewLocationGrid {
    Module: string = 'CrewLocationGrid';
    apiurl: string = 'api/v1/crewlocation';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'LocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelocation`);
                break;
        }
    }
}

var CrewLocationGridController = new CrewLocationGrid();
//----------------------------------------------------------------------------------------------