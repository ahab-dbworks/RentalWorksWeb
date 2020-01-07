class WidgetGroupGrid {
    Module: string = 'WidgetGroup';
    apiurl: string = 'api/v1/widgetgroup'; 

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'GroupId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validategroup`);
                break;
        }
    }


}

var WidgetGroupGridController = new WidgetGroupGrid();
//----------------------------------------------------------------------------------------------