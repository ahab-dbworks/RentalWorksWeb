class CompanyResaleGrid {
    Module: string = 'CompanyResaleGrid';
    apiurl: string = 'api/v1/companytaxresale';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'StateId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatestate`);
                break;
        }
    }
}

var CompanyResaleGridController = new CompanyResaleGrid();
//----------------------------------------------------------------------------------------------