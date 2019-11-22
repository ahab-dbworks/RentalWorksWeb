class DealShipperGrid {
    Module: string = 'DealShipperGrid';
    apiurl: string = 'api/v1/dealshipper';

    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'CarrierId':
                request.uniqueids = {
                    Freight: true,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecarrier`);
                break;
            case 'ShipViaId':
                let carrierId = $tr.find('.field[data-browsedatafield="CarrierId"] input').val();
                request.uniqueids = {
                    VendorId: carrierId,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateshipvia`);
        }
    }
}
var DealShipperGridController = new DealShipperGrid();
//----------------------------------------------------------------------------------------------