//----------------------------------------------------------------------------------------------
RwOrderController.getTransferInMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
          captionPageTitle:   RwLanguages.translate('Transfer In Menu')
        , captionSingleOrder: RwLanguages.translate('Single Order')
        , captionSession:     RwLanguages.translate('Session')
    }, viewModel);
    combinedViewModel.htmlPageBody  = Mustache.render(jQuery('#tmpl-transferInMenu').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel, properties);
    screen.$view
        .on('click', '#transferInMenu-iconSingleOrder', function() {
            try {
                application.navigate('order/transferin/singleorder');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#transferInMenu-iconSession', function() {
            try {
                application.navigate('order/transferin/session');
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