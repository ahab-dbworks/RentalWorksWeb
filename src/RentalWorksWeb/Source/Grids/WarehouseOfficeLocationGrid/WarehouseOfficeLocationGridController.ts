class WarehouseOfficeLocationGrid {
    Module: string = 'WarehouseOfficeLocation';
    apiurl: string = 'api/v1/warehouselocation';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
        }
    }
}

var WarehouseOfficeLocationGridController = new WarehouseOfficeLocationGrid();
//----------------------------------------------------------------------------------------------