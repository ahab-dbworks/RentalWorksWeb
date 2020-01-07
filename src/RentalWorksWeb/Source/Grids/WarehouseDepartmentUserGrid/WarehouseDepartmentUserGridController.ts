class WarehouseDepartmentUserGrid {
    Module = 'WarehouseDepartmentUser';
    apiurl = 'api/v1/warehousedepartment';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'RequestToId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterequestto`);
                break;
        }
    }

}

var WarehouseDepartmentUserGridController = new WarehouseDepartmentUserGrid();
//----------------------------------------------------------------------------------------------