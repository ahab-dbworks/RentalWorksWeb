class CustomFormUserGrid {
    Module: string = 'CustomFormUser';
    apiurl: string = 'api/v1/customformuser';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var CustomFormUserGridController = new CustomFormUserGrid();
//----------------------------------------------------------------------------------------------