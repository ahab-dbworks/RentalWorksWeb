var RwSelectTransferOrder = {};
//----------------------------------------------------------------------------------------------
RwSelectTransferOrder.getSelectTransferOrderScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwOrderController.getPageTitle(properties)
      , captionPageSubTitle: ''
      , htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({
          captionBarcodeICode: RwLanguages.translate('Order Number')
        })
      , captionOrderDesc:     RwLanguages.translate('Order')
      , captionFromWarehouse: RwLanguages.translate('From Warehouse')
      , captionToWarehouse:   RwLanguages.translate('To Warehouse')
      , captionMsg:           RwLanguages.translate('Messages')
      , captionConfirm:       RwLanguages.translate('Continue...')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectTransferOrder').html(), combinedViewModel, {});
    screen = {};
    screen.viewModel = viewModel;
    screen.combinedViewModel = combinedViewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    
    screen.$view.find('#selectTransferOrder-response').hide();
    
    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                request = {
                    orderNo:      $this.val().toUpperCase()
                  , moduleType:   properties.moduleType
                };
                if (typeof properties.activityType !== 'undefined') {
                    request.activityType = properties.activityType;
                }
                RwServices.callMethod('SelectOrder', 'WebSelectOrder', request, function(response) {
                    application.playStatus(response.webSelectOrder.status === 0);
                    if ((response) && (typeof response.request !== 'undefined') && (typeof response.webSelectOrder !== 'undefined')) {
                        properties.webSelectOrder = response.webSelectOrder;
                        if (typeof response.contract === 'object') {
                            properties.contract = response.contract;
                        }
                        if (typeof response.suspendedInContracts === 'object') {
                            properties.suspendedInContracts = response.suspendedInContracts;
                        }
                        jQuery('#selectTransferOrder-txtOrderNo').html(response.request.orderNo);
                        jQuery('#selectTransferOrder-txtOrderDesc').html(response.webSelectOrder.orderDesc);
                        jQuery('#selectTransferOrder-txtFromWarehouse').html(response.webSelectOrder.fromWarehouse);
                        jQuery('#selectTransferOrder-txtToWarehouse').html(response.webSelectOrder.warehouse);
                        jQuery('#selectTransferOrder-error').html(response.webSelectOrder.msg);
                        jQuery('#selectTransferOrder-response').show();
                        jQuery('#selectTransferOrder-order').toggle(response.webSelectOrder.status === 0);
                        jQuery('#selectTransferOrder-error').toggle(response.webSelectOrder.status !== 0);
                    } else {
                        throw 'RwServices.order.selectOrder: Response was not in an expected format';
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#selectTransferOrder-btnSelectOrder', function() {
            var stageItemScreen, stageItemScreen_viewModel, stageItemScreen_properties,
                checkInItemScreen, checkInItemScreen_viewModel, checkInItemScreen_properties;
            try {
                switch(properties.activityType) {
                    case RwConstants.activityTypes.Staging:
                        stageItemScreen_viewModel = {};
                        stageItemScreen_properties = jQuery.extend({}, properties, {
                            webSelectOrder: properties.webSelectOrder
                        });
                        stageItemScreen = RwOrderController.getStagingScreen(stageItemScreen_viewModel, stageItemScreen_properties);
                        application.pushScreen(stageItemScreen);
                        break;
                    case RwConstants.activityTypes.CheckIn:
                        if (typeof properties.suspendedInContracts === 'object') {
                            suspendedContractsPopup = RwOrderController.getOrderSuspendedSessionPopup(properties.suspendedInContracts);
                            suspendedContractsPopup.$popup.on('click', '.checkin-suspendedsession', function() {
                                var requestSelectSession, $suspendedsession, sessionno;
                                try {
                                    $suspendedsession = jQuery(this);
                                    requestSelectSession = {};
                                    requestSelectSession.sessionNo  = $suspendedsession.find('.checkin-suspendedsession-sessionno-value').text();
                                    requestSelectSession.moduleType = properties.moduleType
                                    if ((properties.checkInMode === RwConstants.checkInModes.Session) && (isNaN(requestSelectSession.sessionNo))) {
                                        throw 'Please enter a numeric Session No.';
                                    }
                                    delete properties.webSelectOrder;
                                    properties.checkInMode = 'Session';
                                    RwServices.order.selectSession(requestSelectSession, function(responseSelectSession) {
                                        try {
                                            properties.selectedsession = responseSelectSession.webSelectSession;
                                            if (((properties.moduleType == RwConstants.moduleTypes.Order) || (properties.moduleType == RwConstants.moduleTypes.Transfer)) && 
                                                (properties.activityType == RwConstants.activityTypes.CheckIn)) {
                                                checkInItemScreen_viewModel = {};
                                                checkInItemScreen_properties = jQuery.extend({}, properties, {
                                                    selectedsession: properties.selectedsession
                                                });
                                                checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                application.pushScreen(checkInItemScreen);
                                            } else {
                                                throw 'Not implemented!';
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                            suspendedContractsPopup.$btnNewSession.on('click', function() {
                                var requestCreateSession;
                                try {
                                    requestCreateSession = {};
                                    requestCreateSession.orderid      = properties.webSelectOrder.orderId;
                                    requestCreateSession.dealid       = properties.webSelectOrder.dealId;
                                    requestCreateSession.departmentid = properties.webSelectOrder.departmentId;
                                    RwServices.order.createNewInContractAndSuspend(requestCreateSession, function(responseCreateSession) {
                                        try {
                                            if (typeof responseCreateSession.contract === 'object') {
                                                properties.contract = responseCreateSession.contract;
                                                checkInItemScreen_viewModel = {};
                                                checkInItemScreen_properties =  jQuery.extend({}, properties, {
                                                    webSelectOrder: properties.webSelectOrder
                                                    , sessionNo:      properties.contract.sessionNo
                                                    , contractId:     properties.contract.contractId 
                                                    , dealId:         properties.webSelectOrder.dealId 
                                                    , departmentId:   properties.webSelectOrder.departmentId
                                                });
                                                checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                                                application.pushScreen(checkInItemScreen);
                                            }
                                        } catch(ex) {
                                            FwFunc.showError(ex);
                                        }
                                    });
                                } catch(ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            checkInItemScreen_viewModel = {};
                            checkInItemScreen_properties =  jQuery.extend({}, properties, {
                                webSelectOrder: properties.webSelectOrder
                                , sessionNo:      properties.contract.sessionNo
                                , contractId:     properties.contract.contractId 
                                , dealId:         properties.webSelectOrder.dealId 
                                , departmentId:   properties.webSelectOrder.departmentId
                            });
                            checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                            application.pushScreen(checkInItemScreen);
                        }
                        break;
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;
    
    screen.load = function() {
        application.setScanTarget('#scanBarcodeView-txtBarcodeData');
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
    };

    return screen;
};