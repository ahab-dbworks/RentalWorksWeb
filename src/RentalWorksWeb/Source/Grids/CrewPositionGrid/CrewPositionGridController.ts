class CrewPositionGrid {
    Module: string = 'CrewPositionGrid';
    apiurl: string = 'api/v1/crewposition';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'LaborTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelabortype`);
                break;
            case 'RateId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterate`);
                break;
        }
    }
}

var CrewPositionGridController = new CrewPositionGrid();
//----------------------------------------------------------------------------------------------