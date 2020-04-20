class InventoryVendorGrid {
    Module: string = 'InventoryVendorGrid';
    apiurl: string = 'api/v1/inventoryvendor';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'VendorId':
                if ($form.attr('data-controller') === 'RentalInventoryController') {
                    request.uniqueids = {
                        SubRent: true,
                    };
                } else if ($form.attr('data-controller') === 'SalesInventoryController') {
                    request.uniqueids = {
                        SubSalet: true,
                    };
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendor`);
                break;
        }
    }
}

var InventoryVendorGridController = new InventoryVendorGrid();
//----------------------------------------------------------------------------------------------