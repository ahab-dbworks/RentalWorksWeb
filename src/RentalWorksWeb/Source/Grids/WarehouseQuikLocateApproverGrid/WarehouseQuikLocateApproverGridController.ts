class WarehouseQuikLocateApproverGrid {
    Module: string = 'WarehouseQuikLocateApprover';
    apiurl: string = 'api/v1/warehousequiklocateapprover';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UsersId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var WarehouseQuikLocateApproverGridController = new WarehouseQuikLocateApproverGrid();
//----------------------------------------------------------------------------------------------