var RwServices = {
  account:       {},
  inventory:     {},
  order:         {},
  FillContainer: {},
  po:            {},
  quote:         {},
  reports:       {},
  utility:       {}
};
//----------------------------------------------------------------------------------------------
//FwServices.getData = function(path, request, doneCallback, timeoutSeconds) {
//    RwAppData.jsonPost(true, 'services.ashx?path=' + path, request, doneCallback, timeoutSeconds);
//};
//----------------------------------------------------------------------------------------------
RwServices.account.getAuthToken                        = function(request, doneCallback) {RwAppData.jsonPost(false, 'services.ashx?path=/account/getauthtoken',                        request, doneCallback);};
RwServices.account.authPassword                        = function(request, doneCallback) {RwAppData.jsonPost(false, 'services.ashx?path=/account/authpassword',                        request, doneCallback);};
RwServices.FillContainer.AddAllQtyItemsToContainer     = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/addallqtyitemstocontainer',     request, doneCallback);};
RwServices.FillContainer.CloseContainer                = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/closecontainer',                request, doneCallback);};
RwServices.FillContainer.CreateContainer               = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/createcontainer',               request, doneCallback);};
RwServices.FillContainer.GetContainerItems             = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/getcontaineritems',             request, doneCallback);};
RwServices.FillContainer.GetContainerPendingItems      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/getcontainerpendingitems',      request, doneCallback);};
RwServices.FillContainer.HasCheckinFillContainerButton = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/hascheckinfillcontainerbutton', request, doneCallback);};
RwServices.FillContainer.InstantiateContainer          = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/instantiatecontainer',          request, doneCallback);};
RwServices.FillContainer.RemoveItemFromContainer       = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/removeitemfromcontainer',       request, doneCallback);};
RwServices.FillContainer.SelectContainer               = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/selectcontainer',               request, doneCallback);};
RwServices.FillContainer.SetContainerNo                = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/fillcontainer/setcontainerno',                request, doneCallback);};
RwServices.inventory.getBarcodeFromRfid                = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/getbarcodefromrfid',                request, doneCallback);};
RwServices.inventory.addInventoryWebImage              = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/addinventorywebimage',              request, doneCallback);};
RwServices.inventory.selectRepairOrder                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/selectrepairorder',                 request, doneCallback);};
RwServices.inventory.updateRepairOrder                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/updaterepairorder',                 request, doneCallback);};
RwServices.inventory.repairstatusrfid                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/repairstatusrfid',                  request, doneCallback);};
RwServices.inventory.qcstatusrfid                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/qcstatusrfid',                      request, doneCallback);};
RwServices.order.cancelContract                        = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/cancelcontract',                        request, doneCallback);};
RwServices.order.checkInItem                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/checkinitem',                           request, doneCallback);};
RwServices.order.completeQCItem                        = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/completeqcitem',                    request, doneCallback);};
RwServices.order.createInContract                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/createincontract',                      request, doneCallback);};
RwServices.order.createNewInContractAndSuspend         = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/createnewincontractandsuspend',         request, doneCallback);};
RwServices.order.createOutContract                     = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/createoutcontract',                     request, doneCallback);};
RwServices.order.getOrders                             = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getorders',                             request, doneCallback);};
RwServices.order.getPOSubReceiveReturnPendingList      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getposubreceivereturnpendinglist',      request, doneCallback);};
RwServices.order.getPOSubReceiveReturnSessionList      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getposubreceivereturnsessionlist',      request, doneCallback);};
RwServices.order.getSelectSerialNo                     = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getselectserialno',                     request, doneCallback);};
RwServices.order.getStagingPendingItems                = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getstagingpendingitems',                request, doneCallback);};
RwServices.order.getStagingStagedItems                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getstagingstageditems',                 request, doneCallback);};
RwServices.order.phyCountItem                          = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/phycountitem',                      request, doneCallback);};
RwServices.order.repairItem                            = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/repairitem',                        request, doneCallback);};
RwServices.order.selectOrder                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/selectorder',                           request, doneCallback);};
RwServices.order.selectSession                         = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/selectsession',                         request, doneCallback);};
RwServices.order.pdastageitem                          = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/pdastageitem',                          request, doneCallback);};
RwServices.order.stageAllQtyItems                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/stageallqtyitems',                      request, doneCallback);};
RwServices.order.subReceiveReturnItem                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/inventory/subreceivereturnitem',              request, doneCallback);};
RwServices.order.unstageItem                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/unstageitem',                           request, doneCallback);};
RwServices.order.getorderitemstosub                    = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getorderitemstosub',                    request, doneCallback);};
RwServices.order.getordercompletestosub                = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getordercompletestosub',                request, doneCallback);};
RwServices.order.substituteatstaging                   = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/substituteatstaging',                   request, doneCallback);};
RwServices.order.getRFIDStatus                         = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/getrfidstatus',                         request, doneCallback);};
RwServices.order.loadRFIDPending                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/loadrfidpending',                       request, doneCallback);};
RwServices.order.loadRFIDExceptions                    = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/loadrfidexceptions',                    request, doneCallback);};
RwServices.order.rfidClearSession                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/rfidclearsession',                      request, doneCallback);};
RwServices.order.processrfidexception                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/order/processrfidexception',                  request, doneCallback);};
RwServices.po.selectPO                                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/po/selectpo',                                 request, doneCallback);};
RwServices.po.webSubReceiveReturnItem                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/po/websubreceivereturnitem',                  request, doneCallback);};
RwServices.quote.loadQuoteItems                        = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/loadquoteitems',                        request, doneCallback);};
RwServices.quote.addItem                               = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/additem',                               request, doneCallback);};
RwServices.quote.updateItemQty                         = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/updateitemqty',                         request, doneCallback);};
RwServices.quote.deleteItem                            = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/deleteitem',                            request, doneCallback);};
RwServices.quote.submitQuote                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/submitquote',                           request, doneCallback);};
RwServices.quote.cancelQuote                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/quote/cancelquote',                           request, doneCallback);};
RwServices.utility.timelogsearch                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/utility/timelogsearch',                       request, doneCallback);};
RwServices.utility.timelogsubmit                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/utility/timelogsubmit',                       request, doneCallback);};
RwServices.utility.timelogviewentries                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'services.ashx?path=/utility/timelogviewentries',                  request, doneCallback);};

//----------------------------------------------------------------------------------------------
// mv2016-04-20 got tired of registering services, now you can do this...
RwServices.callMethod = function (servicename, methodname, request, doneCallback, async, timeout) {
    if (typeof timeout !== 'number') {
        timeout = null;
    }
    RwAppData.jsonPost(true, 'services.ashx?path=/services/' + servicename + '/' + methodname, request, doneCallback, timeout, async);
}
//----------------------------------------------------------------------------------------------
