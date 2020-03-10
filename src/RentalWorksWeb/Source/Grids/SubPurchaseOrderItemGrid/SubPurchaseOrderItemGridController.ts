﻿class SubPurchaseOrderItemGrid {
    Module: string = 'SubPurchaseOrderItemGrid';
     apiurl: string = 'api/v1/subpurchaseorderitem';

     addLegend($control) {
         FwBrowse.addLegend($control, 'Vendor', '#ffd666'); // orange
     }
};
//----------------------------------------------------------------------------------------------
var SubPurchaseOrderItemGridController = new SubPurchaseOrderItemGrid();