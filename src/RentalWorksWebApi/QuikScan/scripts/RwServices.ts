var RwServices: any = {
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
//    RwAppData.jsonPost(true, 'api/v1/mobile?path=' + path, request, doneCallback, timeoutSeconds);
//};
//----------------------------------------------------------------------------------------------
RwServices.account.getAuthToken                        = function(request, doneCallback) {RwAppData.jsonPost(false, 'api/v1/mobile?path=/account/getauthtoken',                        request, doneCallback);};
RwServices.account.authPassword                        = function(request, doneCallback) {RwAppData.jsonPost(false, 'api/v1/mobile?path=/account/authpassword',                        request, doneCallback);};
RwServices.FillContainer.AddAllQtyItemsToContainer     = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/addallqtyitemstocontainer',     request, doneCallback);};
RwServices.FillContainer.CloseContainer                = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/closecontainer',                request, doneCallback);};
RwServices.FillContainer.CreateContainer               = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/createcontainer',               request, doneCallback);};
RwServices.FillContainer.GetContainerItems             = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/getcontaineritems',             request, doneCallback);};
RwServices.FillContainer.GetContainerPendingItems      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/getcontainerpendingitems',      request, doneCallback);};
RwServices.FillContainer.HasCheckinFillContainerButton = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/hascheckinfillcontainerbutton', request, doneCallback);};
RwServices.FillContainer.InstantiateContainer          = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/instantiatecontainer',          request, doneCallback);};
RwServices.FillContainer.RemoveItemFromContainer       = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/removeitemfromcontainer',       request, doneCallback);};
RwServices.FillContainer.SelectContainer               = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/selectcontainer',               request, doneCallback);};
RwServices.FillContainer.SetContainerNo                = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/fillcontainer/setcontainerno',                request, doneCallback);};
RwServices.inventory.getBarcodeFromRfid                = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/getbarcodefromrfid',                request, doneCallback);};
RwServices.inventory.addInventoryWebImage              = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/addinventorywebimage',              request, doneCallback);};
RwServices.inventory.selectRepairOrder                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/selectrepairorder',                 request, doneCallback);};
RwServices.inventory.updateRepairOrder                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/updaterepairorder',                 request, doneCallback);};
RwServices.inventory.repairstatusrfid                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/repairstatusrfid',                  request, doneCallback);};
RwServices.inventory.qcstatusrfid                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/qcstatusrfid',                      request, doneCallback);};
RwServices.order.cancelContract                        = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/cancelcontract',                        request, doneCallback);};
RwServices.order.checkInItem                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/checkinitem',                           request, doneCallback);};
RwServices.order.completeQCItem                        = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/completeqcitem',                    request, doneCallback);};
RwServices.order.createInContract                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/createincontract',                      request, doneCallback);};
RwServices.order.createNewInContractAndSuspend         = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/createnewincontractandsuspend',         request, doneCallback);};
RwServices.order.createOutContract                     = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/createoutcontract',                     request, doneCallback);};
RwServices.order.getOrders                             = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getorders',                             request, doneCallback);};
RwServices.order.getPOSubReceiveReturnPendingList      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getposubreceivereturnpendinglist',      request, doneCallback);};
RwServices.order.getPOSubReceiveReturnSessionList      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getposubreceivereturnsessionlist',      request, doneCallback);};
RwServices.order.getSelectSerialNo                     = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getselectserialno',                     request, doneCallback);};
RwServices.order.getStagingPendingItems                = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getstagingpendingitems',                request, doneCallback);};
RwServices.order.getStagingStagedItems                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getstagingstageditems',                 request, doneCallback);};
RwServices.order.phyCountItem                          = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/phycountitem',                      request, doneCallback);};
RwServices.order.repairItem                            = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/repairitem',                        request, doneCallback);};
RwServices.order.selectOrder                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/selectorder',                           request, doneCallback);};
RwServices.order.selectSession                         = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/selectsession',                         request, doneCallback);};
RwServices.order.pdastageitem                          = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/pdastageitem',                          request, doneCallback);};
RwServices.order.stageAllQtyItems                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/stageallqtyitems',                      request, doneCallback);};
RwServices.order.subReceiveReturnItem                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/inventory/subreceivereturnitem',              request, doneCallback);};
RwServices.order.unstageItem                           = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/unstageitem',                           request, doneCallback);};
RwServices.order.getorderitemstosub                    = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getorderitemstosub',                    request, doneCallback);};
RwServices.order.getordercompletestosub                = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getordercompletestosub',                request, doneCallback);};
RwServices.order.substituteatstaging                   = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/substituteatstaging',                   request, doneCallback);};
RwServices.order.getRFIDStatus                         = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/getrfidstatus',                         request, doneCallback);};
RwServices.order.loadRFIDPending                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/loadrfidpending',                       request, doneCallback);};
RwServices.order.loadRFIDExceptions                    = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/loadrfidexceptions',                    request, doneCallback);};
RwServices.order.rfidClearSession                      = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/rfidclearsession',                      request, doneCallback);};
RwServices.order.processrfidexception                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/order/processrfidexception',                  request, doneCallback);};
RwServices.po.selectPO                                 = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/po/selectpo',                                 request, doneCallback);};
RwServices.po.webSubReceiveReturnItem                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/po/websubreceivereturnitem',                  request, doneCallback);};
RwServices.utility.timelogsearch                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/utility/timelogsearch',                       request, doneCallback);};
RwServices.utility.timelogsubmit                       = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/utility/timelogsubmit',                       request, doneCallback);};
RwServices.utility.timelogviewentries                  = function(request, doneCallback) {RwAppData.jsonPost(true,  'api/v1/mobile?path=/utility/timelogviewentries',                  request, doneCallback);};

//----------------------------------------------------------------------------------------------
// mv2016-04-20 got tired of registering services, now you can do this...
RwServices.callMethod = function (servicename, methodname, request, doneCallback, async, timeout) {
    if (typeof timeout !== 'number') {
        timeout = null;
    }
    RwAppData.jsonPost(true, 'api/v1/mobile?path=/services/' + servicename + '/' + methodname, request, doneCallback, timeout, async);
}
//----------------------------------------------------------------------------------------------
