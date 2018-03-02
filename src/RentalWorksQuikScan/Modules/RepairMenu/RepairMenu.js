                                                                //----------------------------------------------------------------------------------------------
RwInventoryController.getRepairMenuScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({
          captionPageTitle:   RwLanguages.translate('Repair Menu')
        , captionComplete:    RwLanguages.translate('Complete')
        , captionRelease:     RwLanguages.translate('Release')
        , captionRepairOrder: RwLanguages.translate('Repair Order')
    }, viewModel);
    combinedViewModel.htmlPageBody  = Mustache.render(jQuery('#tmpl-repairMenu').html(), combinedViewModel);
    screen = {};
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    //screen.$view.find('#repairMenuView-iconPhoto').toggle(typeof navigator.camera !== 'undefined');

    screen.$view
        .on('click', '#repairMenuView-iconComplete', function() {
            try {
                program.navigate('inventory/repair/complete');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#repairMenuView-iconRelease', function() {
            try {
                program.navigate('inventory/repair/release');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#repairMenuView-iconRepairOrder', function() {
            try {
                program.navigate('inventory/repair/repairorder');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    return screen;
};