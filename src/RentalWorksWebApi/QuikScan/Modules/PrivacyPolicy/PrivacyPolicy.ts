//----------------------------------------------------------------------------------------------
RwAccountController.getPrivacyPolicyScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
            captionPageTitle:       RwLanguages.translate('Privacy Policy')
        }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-account-privacyPolicy').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    return screen;
}