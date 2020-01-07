class WidgetUserGrid {
    Module: string = 'WidgetUser';
    apiurl: string = 'api/v1/widgetuser';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var WidgetUserGridController = new WidgetUserGrid();
//----------------------------------------------------------------------------------------------