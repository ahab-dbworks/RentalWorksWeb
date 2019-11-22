class DealShipperGrid {
    Module: string = 'DealShipperGrid';
    apiurl: string = 'api/v1/dealshipper';

    //----------------------------------------------------------------------------------------------
    beforeValidateCarrier = function ($browse, $grid, request, datafield, $tr) {
        request.uniqueIds = {
            Freight: true,
        };
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateShipVia = function ($browse, $grid, request, datafield, $tr) {
        let carrierId = $tr.find('.field[data-browsedatafield="CarrierId"] input').val();
        request.uniqueIds = {
            VendorId: carrierId,
        };
    };
    //----------------------------------------------------------------------------------------------
}
var DealShipperGridController = new DealShipperGrid();
//----------------------------------------------------------------------------------------------