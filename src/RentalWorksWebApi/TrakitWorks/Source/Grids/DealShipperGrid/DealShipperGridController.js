class DealShipperGrid {
    constructor() {
        this.Module = 'DealShipperGrid';
        this.apiurl = 'api/v1/dealshipper';
    }
    beforeValidate(datafield, request, $validationbrowse, $gridbrowse, $tr) {
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
//# sourceMappingURL=DealShipperGridController.js.map