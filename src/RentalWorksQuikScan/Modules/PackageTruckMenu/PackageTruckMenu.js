//----------------------------------------------------------------------------------------------
RwOrderController.getPackageTruckMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
          captionPageTitle:   RwLanguages.translate('Package Truck Menu')
        , captionSingleOrder: RwLanguages.translate('Single Order')
        , captionSession:     RwLanguages.translate('Session')
        , captionCheckIn:     RwLanguages.translate('Check-In')
        , captionCheckOut:    RwLanguages.translate('Check-Out')
        , captionFromTruck:   RwLanguages.translate('From Truck')
        , captionToTruck:     RwLanguages.translate('To Truck')
    }, viewModel);
    combinedViewModel.htmlPageBody  = Mustache.render(jQuery('#tmpl-packageTruckMenu').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    screen.$view
        .on('click', '#homeView-miPackageTruckStaging', function() {
            try {
                program.navigate('order/packagetruck/staging');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#homeView-miPackageTruckCheckIn', function() {
            try {
                program.navigate('order/packagetruck/checkin');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    screen.load = function() {
        
    };

    return screen;
};