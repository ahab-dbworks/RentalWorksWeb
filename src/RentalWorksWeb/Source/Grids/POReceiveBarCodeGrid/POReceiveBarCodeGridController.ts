class POReceiveBarCodeGrid {
    Module: string = 'POReceiveBarCodeGrid';
    apiurl: string = 'api/v1/purchaseorderreceivebarcode';

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