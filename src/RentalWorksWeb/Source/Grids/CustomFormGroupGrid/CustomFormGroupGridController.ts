class CustomFormGroupGrid {
    Module: string = 'CustomFormGroup';
    apiurl: string = 'api/v1/customformgroup';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'GroupId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validategroupname`);
                break;
        }
    }
}

var CustomFormGroupGridController = new CustomFormGroupGrid();
//----------------------------------------------------------------------------------------------