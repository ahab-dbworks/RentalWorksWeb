var RwSelectDeal = {};
//----------------------------------------------------------------------------------------------
RwSelectDeal.getSelectDealScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
        captionPageTitle:    RwOrderController.getPageTitle(properties)
      , captionPageSubTitle: ''
      , htmlScanBarcode:     RwPartialController.getScanBarcodeHtml({
          captionBarcodeICode:RwLanguages.translate('Deal No.')
        })
      , captionDealNo:       RwLanguages.translate('Deal No.')
      , captionDealDesc:     RwLanguages.translate('Deal Desc.')
      , captionDepartment:   RwLanguages.translate('Department')
      , captionMsg:          RwLanguages.translate('Messages')
      , captionConfirm:      RwLanguages.translate('Continue...')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-selectDeal').html(), combinedViewModel, {});
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);

    screen.$view.find('#selectDealView-response').hide();
    
    screen.$view
        .on('change', '#scanBarcodeView-txtBarcodeData', function() {
            var $this, request;
            try {
                $this = jQuery(this);
                request = {
                    dealNo:       $this.val().toUpperCase()
                    , moduleType:   RwConstants.moduleTypes.Deal
                    , activityType: RwConstants.activityTypes.CheckIn
                };
                RwServices.order.selectDeal(request, function(response) {
                    try {
                        properties.webSelectDeal = response.webSelectDeal;
                        application.playStatus(response.webSelectDeal.status === 0);
                        jQuery('#selectDealView-response').show();
                        jQuery('#selectDealView-deal-txtDealNo').html(response.webSelectDeal.dealNo);
                        jQuery('#selectDealView-deal-txtDealDesc').html(response.webSelectDeal.dealdesc);
                        jQuery('#selectDealView-department-txtDepartment').html(response.webSelectDeal.department);
                        jQuery('#selectDealView-error').html(response.webSelectDeal.msg);
                        jQuery('#selectDealView-error').toggle(response.webSelectDeal.status !== 0);
                        jQuery('#selectDealView-selectDeal').toggle(response.webSelectDeal.status === 0);
                    }
                    catch(ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#selectDealView-btnSelectDeal', function() {
            var checkInItemScreen, checkInItemScreen_viewModel, checkInItemScreen_properties;
            try {
                if ((properties.moduleType == RwConstants.moduleTypes.Order) && (properties.activityType == RwConstants.activityTypes.CheckIn)) {
                    checkInItemScreen = {};
                    checkInItemScreen_viewModel = {};
                    checkInItemScreen_properties = jQuery.extend({}, properties, {
                        webSelectDeal: properties.webSelectDeal
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
    ;

    screen.load = function() {
        application.setScanTarget('#scanBarcodeView-txtBarcodeData');
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
    };

    return screen;
};