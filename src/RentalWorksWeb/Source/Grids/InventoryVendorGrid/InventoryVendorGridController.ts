class InventoryVendorGrid {
    Module: string = 'InventoryVendorGrid';
    apiurl: string = 'api/v1/inventoryvendor';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'VendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendor`);
                break;
        }
    }
}

var InventoryVendorGridController = new InventoryVendorGrid();
//----------------------------------------------------------------------------------------------