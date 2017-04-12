//----------------------------------------------------------------------------------------------
RwAccountController.getSupportScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
            captionPageTitle:       RwLanguages.translate('Support')
        }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-account-support').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    return screen;
}