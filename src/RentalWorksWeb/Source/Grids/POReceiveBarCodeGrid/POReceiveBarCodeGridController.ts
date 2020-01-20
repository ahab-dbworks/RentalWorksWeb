class POReceiveBarCodeGrid {
    Module: string = 'POReceiveBarCodeGrid';
    apiurl: string = 'api/v1/purchaseorderreceivebarcode';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            //set validation dynamically
            const availFor = FwBrowse.getValueByDataField($control, $tr, 'AvailFor');
            const $td = $tr.find('[data-browsedatafield="InventoryId"]');
            let peekForm;
            switch (availFor) {
                case 'R':
                    peekForm = 'RentalInventory';
                    break;
                case 'S':
                    peekForm = 'SalesInventory';
                    break;
                case 'P':
                    peekForm = 'PartsInventory';
                    break;
            }
            $td.attr('data-peekForm', peekForm);
        });

    }

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InspectionVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinspectionvendor`);
                break;
        }
    }
}

var POReceiveBarCodeGridController = new POReceiveBarCodeGrid();
//----------------------------------------------------------------------------------------------