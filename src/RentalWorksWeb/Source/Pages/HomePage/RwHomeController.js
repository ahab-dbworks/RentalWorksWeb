//----------------------------------------------------------------------------------------------
var RwHomeController = {};
//----------------------------------------------------------------------------------------------
RwHomeController.getHomeScreen = function(viewModel, properties) {
    var combinedViewModel, screen, applicationOptions;
    applicationOptions = program.getApplicationOptions();
    combinedViewModel = jQuery.extend({

    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-pages-Home').html(), combinedViewModel);
    screen            = {};
    screen.$view      = RwMasterController.getMasterView(combinedViewModel);
    screen.viewModel  = viewModel;
    screen.properties = properties;

    return screen;
};
//----------------------------------------------------------------------------------------------
