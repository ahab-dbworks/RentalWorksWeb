//----------------------------------------------------------------------------------------------
RwOrderController.getCheckInMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
          captionPageTitle:   RwLanguages.translate('Check-In Menu')
        , captionCheckInMenu: RwLanguages.translate('Check-In Menu')
        //, captionSingleOrder: RwLanguages.translate('Single Order')
        //, captionMultiOrder:  RwLanguages.translate('Multi-Order')
        //, captionSession:     RwLanguages.translate('Session')
        //, captionDeal:        RwLanguages.translate('Deal')
        , captionSingleOrder: RwLanguages.translate('Order No')
        , captionMultiOrder:  RwLanguages.translate('Bar Code No')
        , captionSession:     RwLanguages.translate('Session No')
        , captionDeal:        RwLanguages.translate('Deal No')
        , captionQuikCheckIn: RwLanguages.translate('Quik Check-In')
    }, viewModel);
    combinedViewModel.htmlPageBody  = Mustache.render(jQuery('#tmpl-checkInMenu').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    screen.$view
        .on('click', '#checkInMenuView-iconSingleOrder', function() {
            try {
                application.navigate('order/checkin/singleorder');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkInMenuView-iconMultiOrder', function() {
            try {
                application.navigate('order/checkin/multiorder');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkInMenuView-iconSession', function() {
            try {
                application.navigate('order/checkin/session');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#checkInMenuView-iconDeal', function() {
            try {
                application.navigate('order/checkin/deal');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        if (!Modernizr.touch) {
            jQuery('#scanBarcodeView-txtBarcodeData').select();
        }
    };

    return screen;
};