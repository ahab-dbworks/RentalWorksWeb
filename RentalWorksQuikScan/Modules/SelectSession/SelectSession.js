var RwSelectSession = {};
//----------------------------------------------------------------------------------------------
RwSelectSession.getSelectSessionScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwOrderController.getPageTitle(properties)
      , captionPageSubTitle: ''
      , htmlScanBarcode:      RwPartialController.getScanBarcodeHtml({
            captionBarcodeICode:RwLanguages.translate('Session No')
        })
      , captionOrderDesc:     RwLanguages.translate('Order')
      , captionDeal:          RwLanguages.translate('Deal')
      , captionFromWarehouse: RwLanguages.translate('From Warehouse')
      , captionToWarehouse:   RwLanguages.translate('To Warehouse')
      , captionMsg:           RwLanguages.translate('Messages')
      , captionConfirm:       RwLanguages.translate('Continue...')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectSession').html(), combinedViewModel, {});
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    
    screen.$view.find('#selectSessionView-response').hide();
    
    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                request = {
                    sessionNo: $this.val().toUpperCase()
                    , moduleType: properties.moduleType
                };
                if ((properties.checkInMode === RwConstants.checkInModes.Session) && (isNaN(request.sessionNo))) {
                    throw 'Please enter a numeric Session No.';
                }
                RwServices.order.selectSession(request, function(response) {
                    try {
                        properties.webSelectSession = response.webSelectSession
                        application.playStatus(response.webSelectSession.status === 0);
                        jQuery('#selectSessionView-error').html(response.webSelectSession.msg);
                        jQuery('#selectSessionView-order-txtOrderNo').html(response.webSelectSession.orderNo);
                        jQuery('#selectSessionView-order-txtOrderDesc').html(response.webSelectSession.orderDesc);
                        jQuery('#selectSessionView-deal-txtDealNo').html(response.webSelectSession.dealNo);
                        jQuery('#selectSessionView-deal-txtDeal').html(response.webSelectSession.deal);
                        jQuery('#selectSessionView-fromWarehouse-txtFromWarehouse').html(response.webSelectSession.fromWarehouse);
                        jQuery('#selectSessionView-toWarehouse-txtToWarehouse').html(response.webSelectSession.warehouse);
                        jQuery('#selectSessionView-response').show();
                        jQuery('#selectSessionView-error').toggle(response.webSelectSession.status !== 0);
                        jQuery('#selectSessionView-selectSession').toggle(response.webSelectSession.status === 0);
                        jQuery('#selectSessionView-deal').toggle(properties.moduleType === RwConstants.moduleTypes.Order);
                        jQuery('#selectSessionView-fromWarehouse').toggle(properties.moduleType === RwConstants.moduleTypes.Transfer);
                        jQuery('#selectSessionView-toWarehouse').toggle(properties.moduleType === RwConstants.moduleTypes.Transfer);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#selectSessionView-btnSelectSession', function() {
            var checkInItemScreen, checkInItemScreen_viewModel, checkInItemScreen_properties;
            try {
                if (((properties.moduleType == RwConstants.moduleTypes.Order) || (properties.moduleType == RwConstants.moduleTypes.Transfer)) && 
                    (properties.activityType == RwConstants.activityTypes.CheckIn)) {
                    checkInItemScreen_viewModel = {};
                    checkInItemScreen_properties = jQuery.extend({}, properties, {
                        webSelectSession: properties.webSelectSession
                    });
                    checkInItemScreen = RwOrderController.getCheckInScreen(checkInItemScreen_viewModel, checkInItemScreen_properties);
                    application.pushScreen(checkInItemScreen);
                } else {
                    throw 'Not implemented!';
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